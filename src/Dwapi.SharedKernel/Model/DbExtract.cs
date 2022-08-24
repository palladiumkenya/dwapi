using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SharedKernel.Model
{
    public class DbExtract : Entity<Guid>
    {
        [MaxLength(100)] public string Name { get; set; }
        [MaxLength(100)] public string Display { get; set; }
        [MaxLength(8000)] public string ExtractSql { get; set; }
        public decimal Rank { get; set; }
        public bool IsPriority { get; set; }
        [NotMapped] public string Emr { get; set; }

        [NotMapped] public string MainName => GetMainName();
        [NotMapped] public string TableName => GetName();
        [NotMapped] public string TempTableName => GetTempName();
        
        

        private string GetName()
        {
            if (Name.IsSameAs("PatientBaselineExtract"))
                return "PatientBaselinesExtract";

            if (Name.IsSameAs("PatientLabExtract"))
                return "PatientLaboratoryExtract";

            if (Name.IsSameAs("HTSClientExtract"))
                return "HtsClientsExtract";

            if (Name.IsSameAs("HTSClientLinkageExtract"))
                return "HtsClientLinkageExtract";

            if (Name.IsSameAs("HTSClientPartnerExtract"))
                return "HtsClientPartnerExtract";

            if (Name.IsSameAs("HtsClient"))
                return "HtsClientsExtract";
            
            return Name;
        }

        private string GetMainName()
        {

            if (Name.StartsWith("Hts"))
                return "TempHtsClientsExtracts";


            return "TempPatientExtracts";
        }

        private string GetTempName()
        {
            if (Name.IsSameAs("HtsClient"))
                return "TempHtsClientsExtract";
            if (Name.IsSameAs("HtsClientTests"))
                return "TempHtsClientTestsExtract";
            if (Name.IsSameAs("HtsTestKits"))
                return "TempHtsTestKitsExtract";
            if (Name.IsSameAs("HtsClientTracing"))
                return "TempHtsClientTracingExtract";
            if (Name.IsSameAs("HtsPartnerTracing"))
                return "TempHtsPartnerTracingExtract";
            if (Name.IsSameAs("HtsPartnerNotificationServices"))
                return "TempHtsPartnerNotificationServicesExtract";
            if (Name.IsSameAs("HtsClientLinkage"))
                return "TempHtsClientsLinkageExtract";
            
            return $"Temp{TableName}";
        }


        public string GetCountSQL()
        {
            return $@"select count(*) from ({ExtractSql.ToLower()})xt".ToLower();
        }
        
        public string GetDiffSQL(DateTime? maxCreated, DateTime? maxModified, int siteCode)
        {
            return $@"select * from ({ExtractSql.ToLower()})xt where xt.Date_Created > {maxCreated} or xt.Date_Modified > {maxModified} and xt.SiteCode={siteCode}".ToLower();
        }

        public void SetupDiffSql(DbProtocol dbProtocol)
        {
            if (AppConstants.DiffSupport)
                return;

            if (dbProtocol.Id != new Guid("a6221aa4-0e85-11e8-ba89-0ed5f89f718b"))
                return;

            if (Name == "PatientStatusExtract")
            {
                ExtractSql = @"
                                select
                                '' AS SatelliteName,
                                0 AS FacilityId,
                                d.unique_patient_no as PatientID,
                                d.patient_id as PatientPK,(select value_reference from location_attribute
                                where location_id in (select property_value from global_property where property='kenyaemr.defaultLocation') and attribute_type_id=1) as siteCode, (select name from location where location_id in (select property_value
                                from global_property
                                where property='kenyaemr.defaultLocation')) as FacilityName,
                                '' as ExitDescription,
                                /*disc.visit_date as ExitDate,
                                case
                                when disc.discontinuation_reason is not null then cn.name
                                else '' end as ExitReason,*/
                                 'KenyaEMR' as Emr,
                                 'Kenya HMIS II' as Project,
                                CAST(now() as Date) AS DateExtracted,
                                max(disc.visit_date) AS ExitDate,
                                mid(max(concat(disc.visit_date,case when disc.discontinuation_reason is not null then cn.name
                                else '' end  )),20) as ExitReason
                                ,null as date_created,null as date_last_modified
                                from kenyaemr_etl.etl_patient_program_discontinuation disc
                                join kenyaemr_etl.etl_patient_demographics d on d.patient_id=disc.patient_id
                                left outer join concept_name cn on cn.concept_id=disc.discontinuation_reason  and cn.concept_name_type='FULLY_SPECIFIED'
                                and cn.locale='en'
                                where d.unique_patient_no is not null
                                group by PatientID
                                order by disc.visit_date ASC;
                            ";
            }

            if (Name == "PatientArtExtract")
            {
                ExtractSql = @"
 select '' AS SatelliteName, 0 AS FacilityId, d.DOB,
       d.Gender, '' AS Provider,
       d.unique_patient_no as PatientID,
       d.patient_id as PatientPK,
       timestampdiff(year,d.DOB, hiv.visit_date) as AgeEnrollment,
       timestampdiff(year,d.DOB, reg.art_start_date) as AgeARTStart,
       timestampdiff(year,d.DOB, reg.latest_vis_date) as AgeLastVisit,
       (select value_reference from location_attribute
        where location_id in (select property_value
                              from global_property
                              where property='kenyaemr.defaultLocation') and attribute_type_id=1) as siteCode,
       (select name from location
        where location_id in (select property_value
                              from global_property
                              where property='kenyaemr.defaultLocation')) as FacilityName,
       CAST(coalesce(date_first_enrolled_in_care,min(hiv.visit_date)) as Date) as RegistrationDate,
       case  max(hiv.entry_point)
         when 160542 then 'OPD'
         when 160563 then 'Other'
         when 160539 then 'VCT'
         when 160538 then 'PMTCT'
         when 160541 then 'TB'
         when 160536 then 'IPD - Adult'
         else cn.name
           end as PatientSource,
       reg.art_start_date as StartARTDate,
       hiv.date_started_art_at_transferring_facility as PreviousARTStartDate,
       '' as PreviousARTRegimen,
       reg.art_start_date as StartARTAtThisFacility,
       reg.regimen as StartRegimen,
       reg.regimen_line as StartRegimenLine,
      -- reg.last_art_date as LastARTDate,
	   case
		when reg.latest_vis_date is not null then reg.latest_vis_date else  reg.last_art_date end as LastARTDate,

       reg.last_regimen as LastRegimen,
       reg.last_regimen_line as LastRegimenLine,
       reg.latest_tca as ExpectedReturn,
       reg.latest_vis_date as LastVisit ,
       timestampdiff(month,reg.art_start_date, reg.latest_vis_date) as duration,
       disc.visit_date as ExitDate,
       case
         when disc.discontinuation_reason is not null then dis_rsn.name
         else '' end as ExitReason,
       'KenyaEMR' as Emr,
       'Kenya HMIS II' as Project,
       CAST(now() as Date) AS DateExtracted
 ,null as date_created,null as date_last_modified
from kenyaemr_etl.etl_hiv_enrollment hiv
       join kenyaemr_etl.etl_patient_demographics d on d.patient_id=hiv.patient_id
       left outer join  kenyaemr_etl.etl_patient_program_discontinuation disc on disc.patient_id=hiv.patient_id
       left outer join (select e.patient_id,
                               if(enr.date_started_art_at_transferring_facility is not null,enr.date_started_art_at_transferring_facility,
                                  e.date_started)as art_start_date, e.date_started, e.gender,e.dob,d.visit_date as dis_date, if(d.visit_date is not null, 1, 0) as TOut,
                               e.regimen, e.regimen_line, e.alternative_regimen, max(fup.next_appointment_date) as latest_tca,
                               last_art_date,last_regimen,last_regimen_line,
                               if(enr.transfer_in_date is not null, 1, 0) as TIn, max(fup.visit_date) as  latest_vis_date
                        from (select e.patient_id,p.dob,p.Gender,min(e.date_started) as date_started,
                                     max(e.date_started) as last_art_date,
                                     mid(min(concat(e.date_started,e.regimen_name)),11) as regimen,
                                     mid(min(concat(e.date_started,e.regimen_line)),11) as regimen_line,
                                     mid(max(concat(e.date_started,e.regimen_name)),11) as last_regimen,
                                     mid(max(concat(e.date_started,e.regimen_line)),11) as last_regimen_line,
                                     max(if(discontinued,1,0))as alternative_regimen
                              from kenyaemr_etl.etl_drug_event e
                                     join kenyaemr_etl.etl_patient_demographics p on p.patient_id=e.patient_id
                                     left join  kenyaemr_etl.etl_pharmacy_extract ph on ph.patient_id = e.patient_id and is_arv=1
                              group by e.patient_id) e
                               left outer join kenyaemr_etl.etl_patient_program_discontinuation d on d.patient_id=e.patient_id
                               left outer join kenyaemr_etl.etl_hiv_enrollment enr on enr.patient_id=e.patient_id
                               left outer join kenyaemr_etl.etl_patient_hiv_followup fup on fup.patient_id=e.patient_id
                        group by e.patient_id)reg on reg.patient_id=hiv.patient_id
       left outer join concept_name dis_rsn on dis_rsn.concept_id=disc.discontinuation_reason  and dis_rsn.concept_name_type='FULLY_SPECIFIED'
                                                 and dis_rsn.locale='en'
       left outer join concept_name cn on cn.concept_id=hiv.entry_point  and cn.concept_name_type='FULLY_SPECIFIED'
                                            and cn.locale='en'
where d.unique_patient_no is not null
group by d.patient_id
having min(hiv.visit_date) is not null and reg.art_start_date is not null;
";
            }

            if (Name == "PatientExtract")
            {
                ExtractSql = @"
select
CASE WHEN prg.program='TB' THEN prg.status  ELSE null end  AS StatusAtTBClinic,
CASE WHEN prg.program='PMTCT' THEN prg.status  ELSE null end  AS  StatusAtPMTCT,
CASE WHEN prg.program='HIV' THEN prg.status  ELSE null end  AS   StatusATCCC,

'' AS SatelliteName,
0 AS FacilityId,d.unique_patient_no as PatientID,
                                 d.patient_id as PatientPK,
                                 (select value_reference from location_attribute
                                  where location_id in (select property_value
                                                        from global_property
                                                        where property='kenyaemr.defaultLocation') and attribute_type_id=1) as siteCode,
                                 (select name from location
                                  where location_id in (select property_value
                                                        from global_property
                                                        where property='kenyaemr.defaultLocation')) as FacilityName,
                                 case d.gender when 'M' then 'Male' when 'F' then 'Female' end  as Gender,
                                 d.dob as DOB,
                                 CAST(min(hiv.visit_date) as Date) as RegistrationDate,
                                 CAST(coalesce(date_first_enrolled_in_care,min(hiv.visit_date)) as Date) as RegistrationAtCCC,
                                 CAST(min(mch.visit_date)as Date) as RegistrationATPMTCT,
                                 CAST(min(tb.visit_date)as Date) as RegistrationAtTBClinic,
                                 case  max(hiv.entry_point)
                                   when 160542 then 'OPD'
                                   when 160563 then 'Other'
                                   when 160539 then 'VCT'
                                   when 160538 then 'PMTCT'
                                   when 160541 then 'TB'
                                   when 160536 then 'IPD - Adult'
                                   /*else cn.name*/
                                     end as PatientSource,

                                (select state_province from location
                                                           where location_id in (select property_value
                                                                                 from global_property
                                                                                 where property='kenyaemr.defaultLocation')) as Region,
                                 (select county_district from location
                                  where location_id in (select property_value
                                                        from global_property
                                                        where property='kenyaemr.defaultLocation'))as District,
                                 (select address6 from location
                                  where location_id in (select property_value
                                                        from global_property
                                                        where property='kenyaemr.defaultLocation')) as Village,
                                 UPPER(ts.name) as ContactRelation,
                                 CAST(GREATEST(coalesce(max(hiv.visit_date),date('1000-01-01')),coalesce(max(de.visit_date),date('1000-01-01')), coalesce(max(enr.visit_date),date('1000-01-01'))) as Date) as LastVisit,
                                 UPPER(d.marital_status) as MaritalStatus,
                                 UPPER(d.education_level) as EducationLevel,

                                 CAST(min(hiv.date_confirmed_hiv_positive) as Date) as DateConfirmedHIVPositive,
                                 max(hiv.arv_status) as PreviousARTExposure,
                                 NULL as PreviousARTStartDate,


                                 'KenyaEMR' as Emr,
                                 'Kenya HMIS II' as Project,


                                CASE hiv.patient_type
									WHEN 160563 THEN 'Transfer in'
									WHEN 164144	THEN 'New client'
									WHEN 159833	THEN 'Re-enroll'
                                ELSE hiv.patient_type
                                END AS PatientType,



                                 (select CASE
									WHEN f.key_population_type  IS NOT NULL AND f.key_population_type  !=1175
										THEN 'Key population'
									WHEN f.key_population_type  =1175 THEN 'General Population'
									ELSE pt.name  END  from  kenyaemr_etl.etl_patient_hiv_followup f
									left join concept_name pt on f.population_type = pt.concept_id AND pt.concept_name_type='FULLY_SPECIFIED'
								WHERE f.encounter_id=min(enr.encounter_id)) AS PopulationType,

                                 (select
									case f.key_population_type
									WHEN 105 THEN 'PWID'
									WHEN 160578 THEN 'MSM'
									WHEN 160579  THEN 'FSW'
									WHEN 1175 THEN 'N/A'
									ELSE null END

                                from  kenyaemr_etl.etl_patient_hiv_followup f
								WHERE f.encounter_id=min(enr.encounter_id)) AS KeyPopulationType,
                                case orp.value_coded when 1 THEN 'Yes' when 2 THEN 'No' ELSE null END as  'Orphan',
                                case sch.value_coded when 1 THEN 'Yes' when 2 THEN 'No' ELSE null END as 'InSchool',
								patAd.county_district AS  PatientResidentCounty,
                                patAd.state_province AS PatientResidentSubCounty,
                                patAd.address6 AS PatientResidentLocation,
                                patAd.address5 AS PatientResidentSubLocation,
                                patAd.address4 AS PatientResidentWard,
                                patAd.city_village AS PatientResidentVillage,
                                cast(min(hiv.transfer_in_date) as Date) as TransferInDate
,null as date_created,null as date_last_modified
from kenyaemr_etl.etl_hiv_enrollment hiv
inner join  kenyaemr_etl.etl_patient_demographics d on hiv.patient_id=d.patient_id
left outer join kenyaemr_etl.etl_mch_enrollment mch on mch.patient_id=d.patient_id
left outer join kenyaemr_etl.etl_patient_hiv_followup enr on enr.patient_id=d.patient_id
left outer join kenyaemr_etl.etl_tb_enrollment tb on tb.patient_id=d.patient_id
left outer join kenyaemr_etl.etl_drug_event de on de.patient_id = d.patient_id
left join concept_name ts on ts.concept_id=hiv.relationship_of_treatment_supporter and ts.concept_name_type = 'FULLY_SPECIFIED' and ts.locale='en'
left join person_address patAd ON patAd.person_id=d.patient_id and patAd.voided = 0
left join (select distinct person_id, value_coded
	from obs
    where concept_id=5629 and voided=0) sch on  sch.person_id = d.patient_id
left join (select distinct person_id, value_coded
	from obs
    where concept_id=11704 and voided=0) orp on  sch.person_id = d.patient_id
left join
(
	select Patient_Id,   program ,
	if(mid(max(concat(date_enrolled,date_completed)), 20) is null, 'Active', 'Inactive') as status
	from kenyaemr_etl.etl_patient_program
	group by Patient_Id,program
) as prg on prg.patient_id = d.patient_id
where unique_patient_no is not null
group by d.patient_id
order by d.patient_id;
";
            }

            if (Name == "PatientBaselineExtract")
            {
                ExtractSql = @"
select distinct '' AS SatelliteName, 0 AS FacilityId, d.unique_patient_no as PatientID,
d.patient_id as PatientPK,
(select value_reference from location_attribute
where location_id in (select property_value
from global_property
where property='kenyaemr.defaultLocation') and attribute_type_id=1) as facilityId,
(select value_reference from location_attribute
where location_id in (select property_value
from global_property
where property='kenyaemr.defaultLocation') and attribute_type_id=1) as siteCode,
 mid(max(if(l.visit_date<=p_dates.enrollment_date,concat(l.visit_date,test_result),null)),11) as eCd4,
 CAST(left(max(if(l.visit_date<=p_dates.enrollment_date,concat(l.visit_date,test_result),null)),10) AS DATE) as eCd4Date,
 if(fup.visit_date<=p_dates.enrollment_date,
 case who_stage
 when 1220 then 'WHO I'
 when 1221 then 'WHO II'
 when 1222 then 'WHO III'
 when 1223 then 'WHO IV'
 when 1204 then 'WHO I'
 when 1205 then 'WHO II'
 when 1206 then 'WHO III'
 when 1207 then 'WHO IV'
 end,null) as ewho,
 CAST(if(fup.visit_date<=p_dates.enrollment_date and who_stage is not null and fup.visit_date>'1900-01-01',
fup.visit_date,null) AS DATE) as ewhodate,
'' as bCD4,
NULL as bCD4Date,
'' as bWHO,
NULL as bWHODate,
 mid(max(concat(fup.visit_date,case who_stage
 when 1220 then 'WHO I'
 when 1221 then 'WHO II'
 when 1222 then 'WHO III'
 when 1223 then 'WHO IV'
 when 1204 then 'WHO I'
 when 1205 then 'WHO II'
 when 1206 then 'WHO III'
 when 1207 then 'WHO IV'
 end)),11) as lastwho,
 CAST(left(max(concat(fup.visit_date,case who_stage
 when 1220 then 'WHO I'
 when 1221 then 'WHO II'
 when 1222 then 'WHO III'
 when 1223 then 'WHO IV'
 when 1204 then 'WHO I'
 when 1205 then 'WHO II'
 when 1206 then 'WHO III'
 when 1207 then 'WHO IV'
 end)),10) AS DATE) as lastwhodate,
  mid(max(concat(l.visit_date,l.test_result)),11) as lastcd4,
 CAST(left(max(concat(l.visit_date,l.test_result)),10) AS DATE) as lastcd4date,
 mid(max(if(l.visit_date>p_dates.six_month_date and l.visit_date<p_dates.twelve_month_date ,concat(l.visit_date,test_result),null)),11) as m6Cd4,
 CAST(left(max(if(l.visit_date>=p_dates.six_month_date and l.visit_date<p_dates.twelve_month_date ,concat(l.visit_date,test_result),null)),10) AS DATE) as m6Cd4Date,
 mid(max(if(l.visit_date>=p_dates.twelve_month_date,concat(l.visit_date,test_result),null)),11) as m6Cd4,
 CAST(left(max(if(l.visit_date>p_dates.twelve_month_date,concat(l.visit_date,test_result),null)),10) AS DATE) as m6Cd4Date,
 '' as eWAB, NULL as eWABDate,'' as bWAB, NULL as bWABDAte,
 'KenyaEMR' as Emr,
 'Kenya HMIS II' as Project,
 '' AS LastWaB,NULL AS LastWABDate,
 '' as m12CD4,
NULL as m12CD4Date,
'' AS m6CD4,
NULL m6CD4Date
    ,null as date_created,null as date_last_modified
from kenyaemr_etl.etl_patient_hiv_followup fup
join kenyaemr_etl.etl_patient_demographics d on d.patient_id=fup.patient_id
join (select e.patient_id,date_add(date_add(min(e.visit_date),interval 3 month), interval 1 day) as enrollment_date,
date_add(date_add(min(e.visit_date), interval 6 month),interval 1 day) as six_month_date,
date_add(date_add(min(e.visit_date), interval 12 month),interval 1 day) as twelve_month_date
from kenyaemr_etl.etl_hiv_enrollment e
group by e.patient_id) p_dates on p_dates.patient_id=fup.patient_id
left outer join kenyaemr_etl.etl_laboratory_extract l on l.patient_id=fup.patient_id and l.lab_test in (5497,730) where d.unique_patient_no is not null
group by fup.patient_id
order by m6cd4date desc
";
            }

            if (Name == "PatientLabExtract")
            {
                ExtractSql = @"
select distinct '' AS SatelliteName, 0 AS FacilityId, d.unique_patient_no as patientID, d.patient_id as patientPK, l.encounter_id as visitID,
CAST(l.visit_date AS DATE) as orderedByDate,CAST(l.visit_date AS DATE) as reportedByDate, null as reason, (select value_reference from location_attribute
where location_id in (select property_value
from global_property
where property='kenyaemr.defaultLocation') and attribute_type_id=1) as facilityID,
(select value_reference from location_attribute
where location_id in (select property_value
from global_property
where property='kenyaemr.defaultLocation') and attribute_type_id=1) as siteCode,
(select name from location
where location_id in (select property_value
from global_property
where property='kenyaemr.defaultLocation')) as facilityName,
cn.name as testName,
case
when c.datatype_id=2 then cn2.name
else
 l.test_result
end as TestResult,
NULL as enrollmentTest,
 'KenyaEMR' as Emr,
 'Kenya HMIS II' as Project
  ,null as date_created,null as date_last_modified
from kenyaemr_etl.etl_laboratory_extract l
join kenyaemr_etl.etl_patient_demographics d on d.patient_id=l.patient_id
join concept_name cn on cn.concept_id=l.lab_test and cn.concept_name_type='FULLY_SPECIFIED'and cn.locale='en'
join concept c on c.concept_id = l.lab_test
left outer join concept_name cn2 on cn2.concept_id=l.test_result and cn2.concept_name_type='FULLY_SPECIFIED'
and cn2.locale='en' where d.unique_patient_no is not null

";
            }

            if (Name == "PatientPharmacyExtract")
            {
                ExtractSql = @"
select distinct
'' AS Provider,'' AS SatelliteName, 0 AS FacilityId, d.unique_patient_no as PatientID,
d.patient_id as PatientPK,
(select name from location
where location_id in (select property_value
from global_property
where property='kenyaemr.defaultLocation')) as FacilityName,
(select value_reference from location_attribute
where location_id in (select property_value
from global_property
where property='kenyaemr.defaultLocation') and attribute_type_id=1) as siteCode,
ph.visit_id as VisitID,
-- if(cn2.name is not null, cn2.name,cn.name) as Drug,
case
when is_arv=1 then ph.drugreg
else if(cn2.name is not null, cn2.name,cn.name) END as Drug,
ph.visit_date as DispenseDate,
ph.duration AS duration,
ph.duration AS PeriodTaken,
fup.next_appointment_date as ExpectedReturn,
'KenyaEMR' as Emr,
'Kenya HMIS II' as Project,
CASE WHEN is_arv=1 THEN 'ARV'
WHEN is_ctx=1 OR is_dapsone= 1 THEN 'Prophylaxis' END AS TreatmentType,
ph.RegimenLine,
CASE WHEN is_ctx=1 THEN 'CTX'
WHEN is_dapsone =1 THEN 'DAPSON' END AS ProphylaxisType,
CAST(now() as Date) AS DateExtracted
,null as date_created,null as date_last_modified
from (SELECT * FROM (
select patient_id, visit_id,visit_date,encounter_id,drug,is_arv, is_ctx,is_dapsone,drug_name as drugreg,frequency,
'' as DispenseDate,duration, duration PeriodTaken,
''ExpectedReturn, CASE WHEN is_ctx=1 OR is_dapsone= 1 THEN 'Prophylaxis' END AS TreatmentType,'' as RegimenLine,
CASE WHEN is_ctx=1 THEN 'CTX'
WHEN is_dapsone =1 THEN 'DAPSON' END AS ProphylaxisType from kenyaemr_etl.etl_pharmacy_extract ph
left outer join concept_name cn2 on cn2.concept_id=ph.drug and cn2.concept_name_type='SHORT'
and cn2.locale='en' where is_ctx=1 OR is_dapsone= 1
GROUP BY patient_id, visit_id,visit_date,encounter_id
UNION

SELECT patient_id,''visit_id,visit_date,encounter_id,regimen drug,1 as is_arv, 0 as is_ctx, 0 as is_dapsone, regimen_name as drugreg, '' frequency, date_started as DispenseDate,''duration,''PeriodTaken,
''ExpectedReturn, 'ARV' AS TreatmentType,regimen_line as RegimenLine,NULL as ProphylaxisType FROM kenyaemr_etl.etl_drug_event
GROUP BY patient_id, visit_id,visit_date,encounter_id
)A )ph
join kenyaemr_etl.etl_patient_demographics d on d.patient_id=ph.patient_id
left outer join concept_name cn on cn.concept_id=ph.drug and cn.concept_name_type='FULLY_SPECIFIED'
and cn.locale='en'
left outer join concept_name cn2 on cn2.concept_id=ph.drug and cn2.concept_name_type='SHORT'
and cn.locale='en'
left outer join kenyaemr_etl.etl_patient_hiv_followup fup on fup.encounter_id=ph.encounter_id
and fup.patient_id=ph.patient_id
where unique_patient_no is not null and (is_arv=1 OR is_ctx=1 OR is_dapsone =1 ) and drugreg is not null
order by ph.patient_id,ph.visit_date;
";
            }

            if (Name == "PatientVisitExtract")
            {
                ExtractSql = @"
select distinct
'' AS SatelliteName, 0 AS FacilityId, d.unique_patient_no as PatientID,
d.patient_id as PatientPK,
(select name from location
where location_id in (select property_value
from global_property
where property='kenyaemr.defaultLocation')) as FacilityName,
(select value_reference from location_attribute
where location_id in (select property_value
from global_property
where property='kenyaemr.defaultLocation') and attribute_type_id=1) as siteCode,
fup.visit_id as VisitID,
case when fup.visit_date < '1990-01-01' then null else CAST(fup.visit_date AS DATE) end  AS VisitDate,
'Out Patient' as Service,
fup.visit_scheduled as VisitType,
case fup.who_stage
when 1220 then 'WHO I'
when 1221 then 'WHO II'
when 1222 then 'WHO III'
when 1223 then 'WHO IV'
when 1204 then 'WHO I'
when 1205 then 'WHO II'
when 1206 then 'WHO III'
when 1207 then 'WHO IV'
  else ''
end as WHOStage,
'' as WABStage,
case fup.pregnancy_status
when 1065 then 'Yes'
when 1066 then 'No'
end as Pregnant,
CAST(fup.last_menstrual_period AS DATE) as LMP,
CAST(fup.expected_delivery_date AS DATE) as EDD,
fup.height as Height,
fup.weight as Weight,
concat(fup.systolic_pressure,'/',fup.diastolic_pressure) as BP,
'ART|CTX' as AdherenceCategory,
concat(
IF(fup.arv_adherence=159405, 'Good', IF(fup.arv_adherence=159406, 'Fair', IF(fup.arv_adherence=159407, 'Poor', ''))), IF(fup.arv_adherence in (159405,159406,159407), '|','') ,
IF(fup.ctx_adherence=159405, 'Good', IF(fup.ctx_adherence=159406, 'Fair', IF(fup.ctx_adherence=159407, 'Poor', '')))
) AS Adherence,
'' as OI,
NULL as OIDate,
case fup.family_planning_status
when 695 then 'Currently using FP'
when 160652 then 'Not using FP'
when 1360 then 'Wants FP'
else ''
end as FamilyPlanningMethod,
concat(
case fup.condom_provided
when 1065 then 'Condoms,'
else ''
end,
case fup.pwp_disclosure
when 1065 then 'Disclosure|'
else ''
end,
case fup.pwp_partner_tested
when 1065 then 'Partner Testing|'
else ''
end,
case fup.screened_for_sti
when 1065 then 'Screened for STI'
else ''
end )as PWP,
if(fup.last_menstrual_period is not null, timestampdiff(week,fup.last_menstrual_period,fup.visit_date),'') as GestationAge,
case when fup.next_appointment_date < '1990-01-01' then null else CAST(fup.next_appointment_date AS DATE) end  AS NextAppointmentDate,
'KenyaEMR' as Emr,
'Kenya HMIS II' as Project,

CAST(fup.substitution_first_line_regimen_date AS DATE)  AS SubstitutionFirstlineRegimenDate,
fup.substitution_first_line_regimen_reason AS SubstitutionFirstlineRegimenReason,
CAST(fup.substitution_second_line_regimen_date AS DATE) AS SubstitutionSecondlineRegimenDate,
fup.substitution_second_line_regimen_reason AS SubstitutionSecondlineRegimenReason,
CAST(fup.second_line_regimen_change_date AS DATE) AS SecondlineRegimenChangeDate,
fup.second_line_regimen_change_reason AS SecondlineRegimenChangeReason,


 CASE fup.stability
 WHEN 1 THEN 'Stable'
 WHEN 2 THEN 'Not Stable' END as StabilityAssessment,

dc.name as DifferentiatedCare,
CASE
               WHEN fup.key_population_type  IS NOT NULL AND fup.key_population_type  !=1175
               THEN 'Key population'
    ELSE pt.name  END as PopulationType,

case fup.key_population_type
WHEN 105 THEN 'PWID'
WHEN 160578 THEN 'MSM'
WHEN 160579  THEN 'FSW'
WHEN 1175 THEN 'N/A'
ELSE fup.key_population_type  END as KeyPopulationType
,null as date_created,null as date_last_modified
from kenyaemr_etl.etl_patient_demographics d
join kenyaemr_etl.etl_patient_hiv_followup fup on fup.patient_id=d.patient_id
left join concept_name dc on dc.concept_id =  fup.differentiated_care and dc.concept_name_type='FULLY_SPECIFIED'
left join concept_name pt on fup.population_type = pt.concept_id AND pt.concept_name_type='FULLY_SPECIFIED'
where d.unique_patient_no is not null and fup.visit_date > '1990-01-01' 
";
            }

            if (Name == "PatientAdverseEventExtract")
            {

                ExtractSql = @"
SELECT '' AS PatientID,
       '' AS PatientPK,
       '' AS SiteCode,
	   '' AS FacilityID,
       'Kenya EMR' AS EMR,
       'Kenya HMIS II' AS Project,
       NOW() AS VisitDate,
       '' AS AdverseEventRegimen,
       '' AS AdverseEvent,
       '' AS AdverseEventCause,
       '' AS Severity,
       '' AS AdverseEventActionTaken,
       '' AS AdverseEventClinicalOutcome,
	   NOW() AS AdverseEventStartDate,
       NOW() AS AdverseEventEndDate,
       '' AS AdverseEventIsPregnant,null as date_created,null as date_last_modified
";
            }
        }

        public override string ToString()
        {
            return Display;
        }
    }
}
