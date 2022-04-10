using System.Data;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Prep;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Prep;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Profiles.Prep
{
    public class PrepExtractProfile : Profile
    {
        public PrepExtractProfile()
        {
            CreateMap<IDataRecord, TempPatientPrepExtract>()
                .ForMember(x => x.PatientPK,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientPrepExtract.PatientPK))))
                .ForMember(x => x.SiteCode,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientPrepExtract.SiteCode))))
                .ForMember(x => x.FacilityId,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientPrepExtract.FacilityId))))
                .ForMember(x => x.Project,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.Project))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.Emr))))
                .ForMember(x => x.FacilityName,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.FacilityName))))
                .ForMember(x => x.PrepNumber,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.PrepNumber))))
                .ForMember(x => x.HtsNumber,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.HtsNumber))))
                .ForMember(x => x.PrepEnrollmentDate,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.PrepEnrollmentDate))))
                .ForMember(x => x.Sex, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.Sex))))
                .ForMember(x => x.DateofBirth,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.DateofBirth))))
                .ForMember(x => x.CountyofBirth,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.CountyofBirth))))
                .ForMember(x => x.County,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.County))))
                .ForMember(x => x.SubCounty,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.SubCounty))))
                .ForMember(x => x.Location,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.Location))))
                .ForMember(x => x.LandMark,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.LandMark))))
                .ForMember(x => x.Ward, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.Ward))))
                .ForMember(x => x.ClientType,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.ClientType))))
                .ForMember(x => x.ReferralPoint,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.ReferralPoint))))
                .ForMember(x => x.MaritalStatus,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.MaritalStatus))))
                .ForMember(x => x.Inschool,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.Inschool))))
                .ForMember(x => x.PopulationType,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.PopulationType))))
                .ForMember(x => x.KeyPopulationType,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.KeyPopulationType))))
                .ForMember(x => x.Refferedfrom,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.Refferedfrom))))
                .ForMember(x => x.TransferIn,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.TransferIn))))
                .ForMember(x => x.TransferInDate,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.TransferInDate))))
                .ForMember(x => x.TransferFromFacility,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.TransferFromFacility))))
                .ForMember(x => x.DatefirstinitiatedinPrepCare,
                    o => o.MapFrom(s =>
                        s.GetStringOrDefault(nameof(TempPatientPrepExtract.DatefirstinitiatedinPrepCare))))
                .ForMember(x => x.DateStartedPrEPattransferringfacility,
                    o => o.MapFrom(s =>
                        s.GetStringOrDefault(nameof(TempPatientPrepExtract.DateStartedPrEPattransferringfacility))))
                .ForMember(x => x.ClientPreviouslyonPrep,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.ClientPreviouslyonPrep))))
                .ForMember(x => x.PrevPrepReg,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.PrevPrepReg))))
                .ForMember(x => x.DateLastUsedPrev,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPrepExtract.DateLastUsedPrev))));



            CreateMap<IDataRecord, TempPrepBehaviourRiskExtract>()
                .ForMember(x => x.PatientPK,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.PatientPK))))
                .ForMember(x => x.SiteCode,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.SiteCode))))
                .ForMember(x => x.PrepNumber,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.PrepNumber))))
                .ForMember(x => x.HtsNumber,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.HtsNumber))))
                .ForMember(x => x.Emr,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.Emr))))
                .ForMember(x => x.Project,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.Project))))
                .ForMember(x => x.VisitDate,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.VisitDate))))
                .ForMember(x => x.VisitID,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.VisitID))))
                .ForMember(x => x.SexPartnerHIVStatus,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.SexPartnerHIVStatus))))
                .ForMember(x => x.IsHIVPositivePartnerCurrentonART,
                    o => o.MapFrom(s =>
                        s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.IsHIVPositivePartnerCurrentonART))))
                .ForMember(x => x.IsPartnerHighrisk,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.IsPartnerHighrisk))))
                .ForMember(x => x.PartnerARTRisk,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.PartnerARTRisk))))
                .ForMember(x => x.ClientAssessments,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.ClientAssessments))))
                .ForMember(x => x.ClientRisk,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.ClientRisk))))
                .ForMember(x => x.ClientWillingToTakePrep,
                    o => o.MapFrom(s =>
                        s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.ClientWillingToTakePrep))))
                .ForMember(x => x.PrEPDeclineReason,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.PrEPDeclineReason))))
                .ForMember(x => x.RiskReductionEducationOffered,
                    o => o.MapFrom(s =>
                        s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.RiskReductionEducationOffered))))
                .ForMember(x => x.ReferralToOtherPrevServices,
                    o => o.MapFrom(s =>
                        s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.ReferralToOtherPrevServices))))
                .ForMember(x => x.FirstEstablishPartnerStatus,
                    o => o.MapFrom(s =>
                        s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.FirstEstablishPartnerStatus))))
                .ForMember(x => x.PartnerEnrolledtoCCC,
                    o => o.MapFrom(s =>
                        s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.PartnerEnrolledtoCCC))))
                .ForMember(x => x.HIVPartnerCCCnumber,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.HIVPartnerCCCnumber))))
                .ForMember(x => x.HIVPartnerARTStartDate,
                    o => o.MapFrom(s =>
                        s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.HIVPartnerARTStartDate))))
                .ForMember(x => x.MonthsknownHIVSerodiscordant,
                    o => o.MapFrom(s =>
                        s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.MonthsknownHIVSerodiscordant))))
                .ForMember(x => x.SexWithoutCondom,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.SexWithoutCondom))))
                .ForMember(x => x.NumberofchildrenWithPartner,
                    o => o.MapFrom(s =>
                        s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.NumberofchildrenWithPartner))))
                .ForMember(x => x.FacilityName,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepBehaviourRiskExtract.FacilityName))));


            CreateMap<IDataRecord, TempPrepVisitExtract>()
                .ForMember(x => x.PatientPK,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.PatientPK))))
                .ForMember(x => x.SiteCode,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.SiteCode))))
                .ForMember(x => x.PrepNumber,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.PrepNumber))))
                .ForMember(x => x.HtsNumber,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.HtsNumber))))
                .ForMember(x => x.EncounterId,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.EncounterId))))
                .ForMember(x => x.VisitID,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.VisitID))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.Emr))))
                .ForMember(x => x.Project,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.Project))))
                .ForMember(x => x.VisitDate,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.VisitDate))))
                .ForMember(x => x.BloodPressure,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.BloodPressure))))
                .ForMember(x => x.Temperature,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.Temperature))))
                .ForMember(x => x.Weight,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.Weight))))
                .ForMember(x => x.Height,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.Height))))
                .ForMember(x => x.BMI, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.BMI))))
                .ForMember(x => x.STIScreening,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.STIScreening))))
                .ForMember(x => x.STISymptoms,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.STISymptoms))))
                .ForMember(x => x.STITreated,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.STITreated))))
                .ForMember(x => x.Circumcised,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.Circumcised))))
                .ForMember(x => x.VMMCReferral,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.VMMCReferral))))
                .ForMember(x => x.LMP, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.LMP))))
                .ForMember(x => x.MenopausalStatus,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.MenopausalStatus))))
                .ForMember(x => x.PregnantAtThisVisit,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.PregnantAtThisVisit))))
                .ForMember(x => x.EDD, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.EDD))))
                .ForMember(x => x.PlanningToGetPregnant,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.PlanningToGetPregnant))))
                .ForMember(x => x.PregnancyPlanned,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.PregnancyPlanned))))
                .ForMember(x => x.PregnancyEnded,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.PregnancyEnded))))
                .ForMember(x => x.PregnancyEndDate,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.PregnancyEndDate))))
                .ForMember(x => x.PregnancyOutcome,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.PregnancyOutcome))))
                .ForMember(x => x.BirthDefects,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.BirthDefects))))
                .ForMember(x => x.Breastfeeding,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.Breastfeeding))))
                .ForMember(x => x.FamilyPlanningStatus,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.FamilyPlanningStatus))))
                .ForMember(x => x.FPMethods,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.FPMethods))))
                .ForMember(x => x.AdherenceDone,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.AdherenceDone))))
                .ForMember(x => x.AdherenceOutcome,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.AdherenceOutcome))))
                .ForMember(x => x.AdherenceReasons,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.AdherenceReasons))))
                .ForMember(x => x.SymptomsAcuteHIV,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.SymptomsAcuteHIV))))
                .ForMember(x => x.ContraindicationsPrep,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.ContraindicationsPrep))))
                .ForMember(x => x.PrepTreatmentPlan,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.PrepTreatmentPlan))))
                .ForMember(x => x.PrepPrescribed,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.PrepPrescribed))))
                .ForMember(x => x.RegimenPrescribed,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.RegimenPrescribed))))
                .ForMember(x => x.MonthsPrescribed,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.MonthsPrescribed))))
                .ForMember(x => x.CondomsIssued,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.CondomsIssued))))
                .ForMember(x => x.Tobegivennextappointment,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.Tobegivennextappointment))))
                .ForMember(x => x.Reasonfornotgivingnextappointment,
                    o => o.MapFrom(s =>
                        s.GetStringOrDefault(nameof(TempPrepVisitExtract.Reasonfornotgivingnextappointment))))
                .ForMember(x => x.HepatitisBPositiveResult,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.HepatitisBPositiveResult))))
                .ForMember(x => x.HepatitisCPositiveResult,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.HepatitisCPositiveResult))))
                .ForMember(x => x.VaccinationForHepBStarted,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.VaccinationForHepBStarted))))
                .ForMember(x => x.TreatedForHepB,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.TreatedForHepB))))
                .ForMember(x => x.VaccinationForHepCStarted,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.VaccinationForHepCStarted))))
                .ForMember(x => x.TreatedForHepC,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.TreatedForHepC))))
                .ForMember(x => x.NextAppointment,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.NextAppointment))))
                .ForMember(x => x.ClinicalNotes,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.ClinicalNotes))))
                .ForMember(x => x.FacilityName,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepVisitExtract.FacilityName))));




            CreateMap<IDataRecord, TempPrepLabExtract>()
                .ForMember(x => x.PatientPK,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepLabExtract.PatientPK))))
                .ForMember(x => x.SiteCode,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepLabExtract.SiteCode))))
                .ForMember(x => x.PrepNumber,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepLabExtract.PrepNumber))))
                .ForMember(x => x.HtsNumber,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepLabExtract.HtsNumber))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepLabExtract.Emr))))
                .ForMember(x => x.Project,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepLabExtract.Project))))
                .ForMember(x => x.VisitID,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepLabExtract.VisitID))))
                .ForMember(x => x.TestName,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepLabExtract.TestName))))
                .ForMember(x => x.TestResult,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepLabExtract.TestResult))))
                .ForMember(x => x.SampleDate,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepLabExtract.SampleDate))))
                .ForMember(x => x.TestResultDate,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepLabExtract.TestResultDate))))
                .ForMember(x => x.Reason, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepLabExtract.Reason))))
                .ForMember(x => x.FacilityName,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepLabExtract.FacilityName))));


                 CreateMap<IDataRecord, TempPrepPharmacyExtract>()
                     .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepPharmacyExtract.PatientPK))))
                     .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepPharmacyExtract.SiteCode))))
                     .ForMember(x => x.PrepNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepPharmacyExtract.PrepNumber))))
                     .ForMember(x => x.HtsNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepPharmacyExtract.HtsNumber))))
                     .ForMember(x => x.FacilityID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepPharmacyExtract.FacilityID))))
                     .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepPharmacyExtract.Emr))))
                     .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepPharmacyExtract.Project))))
                     .ForMember(x => x.VisitID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepPharmacyExtract.VisitID))))
                     .ForMember(x => x.RegimenPrescribed, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepPharmacyExtract.RegimenPrescribed))))
                     .ForMember(x => x.DispenseDate, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepPharmacyExtract.DispenseDate))))
                     .ForMember(x => x.Duration, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepPharmacyExtract.Duration))))
                     .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepPharmacyExtract.FacilityName))));


                 CreateMap<IDataRecord, TempPrepAdverseEventExtract>()
                     .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepAdverseEventExtract.PatientPK))))
.ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepAdverseEventExtract.SiteCode))))
.ForMember(x => x.PrepNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepAdverseEventExtract.PrepNumber))))
.ForMember(x => x.FacilityID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepAdverseEventExtract.FacilityID))))
.ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepAdverseEventExtract.Emr))))
.ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepAdverseEventExtract.Project))))
.ForMember(x => x.AdverseEvent, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepAdverseEventExtract.AdverseEvent))))
.ForMember(x => x.AdverseEventStartDate, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepAdverseEventExtract.AdverseEventStartDate))))
.ForMember(x => x.AdverseEventEndDate, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepAdverseEventExtract.AdverseEventEndDate))))
.ForMember(x => x.Severity, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepAdverseEventExtract.Severity))))
.ForMember(x => x.VisitDate, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepAdverseEventExtract.VisitDate))))
.ForMember(x => x.AdverseEventActionTaken, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepAdverseEventExtract.AdverseEventActionTaken))))
.ForMember(x => x.AdverseEventClinicalOutcome, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepAdverseEventExtract.AdverseEventClinicalOutcome))))
.ForMember(x => x.AdverseEventIsPregnant, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepAdverseEventExtract.AdverseEventIsPregnant))))
.ForMember(x => x.AdverseEventCause, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepAdverseEventExtract.AdverseEventCause))))
.ForMember(x => x.AdverseEventRegimen, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepAdverseEventExtract.AdverseEventRegimen))))
.ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepAdverseEventExtract.FacilityName))));


                 CreateMap<IDataRecord, TempPrepCareTerminationExtract>()
                     .ForMember(x => x.PatientPK,
                         o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepCareTerminationExtract.PatientPK))))
                     .ForMember(x => x.SiteCode,
                         o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepCareTerminationExtract.SiteCode))))
                     .ForMember(x => x.PrepNumber,
                         o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepCareTerminationExtract.PrepNumber))))
                     .ForMember(x => x.Emr,
                         o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepCareTerminationExtract.Emr))))
                     .ForMember(x => x.Project,
                         o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepCareTerminationExtract.Project))))
                     .ForMember(x => x.HtsNumber,
                         o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepCareTerminationExtract.HtsNumber))))
                     .ForMember(x => x.ExitDate,
                         o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepCareTerminationExtract.ExitDate))))
                     .ForMember(x => x.ExitReason,
                         o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPrepCareTerminationExtract.ExitReason))))
                     .ForMember(x => x.DateOfLastPrepDose,
                         o => o.MapFrom(s =>
                             s.GetStringOrDefault(nameof(TempPrepCareTerminationExtract.DateOfLastPrepDose))))
                     .ForMember(x => x.FacilityName,
                         o => o.MapFrom(s =>
                             s.GetStringOrDefault(nameof(TempPrepCareTerminationExtract.FacilityName))));


                 CreateMap<TempPatientPrepExtract, PatientPrepExtract>();
                 CreateMap<TempPrepAdverseEventExtract, PrepAdverseEventExtract>();
                 CreateMap<TempPrepBehaviourRiskExtract,PrepBehaviourRiskExtract>();
                 CreateMap<TempPrepCareTerminationExtract, PrepCareTerminationExtract>();
                 CreateMap<TempPrepLabExtract, PrepLabExtract>();
                 CreateMap<TempPrepPharmacyExtract, PrepPharmacyExtract>();
                 CreateMap<TempPrepVisitExtract, PrepVisitExtract>();

        }
    }
}
