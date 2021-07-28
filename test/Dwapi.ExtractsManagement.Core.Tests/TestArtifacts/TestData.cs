using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.SettingsManagement.Core.Model;
using FizzWare.NBuilder;
using Newtonsoft.Json;

namespace Dwapi.ExtractsManagement.Core.Tests.TestArtifacts
{
    public class TestData
    {
        public static List<EmrSystem> GenerateEmrSystems(string connectionString)
        {
            var emr = JsonConvert.DeserializeObject<EmrSystem>(EmrJson());
            emr.Id=Guid.NewGuid();

            var db = JsonConvert.DeserializeObject<DatabaseProtocol>(DbJson());
            db.Id=Guid.NewGuid();
            db.EmrSystemId = emr.Id;
            db.DatabaseName = connectionString;
            emr.DatabaseProtocols.Add(db);

            emr.Extracts= GetExtracts(emr.Id, db.Id);

            return new List<EmrSystem> {emr};
        }

        public static List<MasterPatientIndex> GenerateMpis(int count = 2)
        {
          return Builder<MasterPatientIndex>.CreateListOfSize(count).All().With(x => x.Status = "").Build().ToList();
        }
        public static List<T> GenerateData<T>(int count = 2)
        {
          return Builder<T>.CreateListOfSize(count).All().Build().ToList();
        }

        public static List<ValidationError> GenerateErrors(int count=2)
        {
          return Builder<ValidationError>.CreateListOfSize(count)
            .All()
            .With(x=>x.ValidatorId=new Guid("6c5c70be-2a95-11e7-93ae-92361f002671"))
            .Build()
            .ToList();
        }

        private static string EmrJson()
        {
            string Json =@"
                 {
                    ^Id^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^,
                    ^Name^: ^KenyaCare^,
                    ^Version^: ^1^,
                    ^IsMiddleware^: false,
                    ^IsDefault^: true,
                    ^EmrSetup^: 0
                  }
            ";
            return Json.Replace("^", "\"");
        }

        private static string DbJson()
        {
            string Json = @"
                    {
                        ^Id^: ^A6221AA4-0E85-11E8-BA89-0ED5F89F718B^,
                        ^DatabaseType^: 3,
                        ^DatabaseName^: ^openmrs^,
                        ^EmrSystemId^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^
                      }
            ";

            return Json.Replace("^", "\"");
        }

        private static List<Extract> GetExtracts(Guid emr,Guid dbId)
        {
          var list = JsonConvert.DeserializeObject<List<Extract>>(ExJson());
          list.ForEach(x =>
          {
            x.Id=Guid.NewGuid();
            x.EmrSystemId = emr;
            x.DatabaseProtocolId = dbId;
          });
            return list;
        }

        private static string ExJson()
        {
          var Json= @"
            [
    {
      ^Id^: ^8578AD00-C34B-11E9-9CB5-2A2AE2DBCCE4^,
      ^IsPriority^: true,
      ^Rank^: 1.00,
      ^Name^: ^HtsClient^,
      ^Display^: ^Hts Clients^,
      ^ExtractSql^: ^SELECT *,'IQCare' as Emr,'KenyaHMIS' as Project FROM TempHtsClientsExtracts^,
      ^Destination^: ^DWHStage^,
      ^EmrSystemId^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^,
      ^DocketId^: ^HTS^,
      ^DatabaseProtocolId^: ^A6221AA4-0E85-11E8-BA89-0ED5F89F718B^
    },
    {
      ^Id^: ^8578B700-C34B-11E9-9CB5-2A2AE2DBCCE4^,
      ^IsPriority^: true,
      ^Rank^: 4.00,
      ^Name^: ^HtsTestKits^,
      ^Display^: ^Hts Test Kits^,
      ^ExtractSql^: ^SELECT *,'IQCare' as Emr,'KenyaHMIS' as Project FROM TempHtsTestKitsExtracts^,
      ^Destination^: ^DWHStage^,
      ^EmrSystemId^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^,
      ^DocketId^: ^HTS^,
      ^DatabaseProtocolId^: ^A6221AA4-0E85-11E8-BA89-0ED5F89F718B^
    },
    {
      ^Id^: ^8578B21E-C34B-11E9-9CB5-2A2AE2DBCCE4^,
      ^IsPriority^: true,
      ^Rank^: 3.00,
      ^Name^: ^HtsClientLinkage^,
      ^Display^: ^Hts Client Linkage^,
      ^ExtractSql^: ^SELECT *,'IQCare' as Emr,'KenyaHMIS' as Project FROM TempHtsClientsLinkageExtracts^,
      ^Destination^: ^DWHStage^,
      ^EmrSystemId^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^,
      ^DocketId^: ^HTS^,
      ^DatabaseProtocolId^: ^A6221AA4-0E85-11E8-BA89-0ED5F89F718B^
    },
    {
      ^Id^: ^8578C042-C34B-11E9-9CB5-2A2AE2DBCCE4^,
      ^IsPriority^: true,
      ^Rank^: 7.00,
      ^Name^: ^HtsPartnerNotificationServices^,
      ^Display^: ^Hts Partner Notification Services^,
      ^ExtractSql^: ^SELECT *,'IQCare' as Emr,'KenyaHMIS' as Project FROM TempHtsPartnerNotificationServicesExtracts^,
      ^Destination^: ^DWHStage^,
      ^EmrSystemId^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^,
      ^DocketId^: ^HTS^,
      ^DatabaseProtocolId^: ^A6221AA4-0E85-11E8-BA89-0ED5F89F718B^
    },
    {
      ^Id^: ^8578B962-C34B-11E9-9CB5-2A2AE2DBCCE4^,
      ^IsPriority^: true,
      ^Rank^: 5.00,
      ^Name^: ^HtsClientTracing^,
      ^Display^: ^Hts Client Tracing^,
      ^ExtractSql^: ^SELECT *,'IQCare' as Emr,'KenyaHMIS' as Project FROM TempHtsClientTracingExtracts^,
      ^Destination^: ^DWHStage^,
      ^EmrSystemId^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^,
      ^DocketId^: ^HTS^,
      ^DatabaseProtocolId^: ^A6221AA4-0E85-11E8-BA89-0ED5F89F718B^
    },
    {
      ^Id^: ^8578BBBA-C34B-11E9-9CB5-2A2AE2DBCCE4^,
      ^IsPriority^: true,
      ^Rank^: 6.00,
      ^Name^: ^HtsPartnerTracing^,
      ^Display^: ^Hts Partner Tracing^,
      ^ExtractSql^: ^SELECT *,'IQCare' as Emr,'KenyaHMIS' as Project FROM TempHtsPartnerTracingExtracts^,
      ^Destination^: ^DWHStage^,
      ^EmrSystemId^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^,
      ^DocketId^: ^HTS^,
      ^DatabaseProtocolId^: ^A6221AA4-0E85-11E8-BA89-0ED5F89F718B^
    },
    {
      ^Id^: ^8578AFBC-C34B-11E9-9CB5-2A2AE2DBCCE4^,
      ^IsPriority^: true,
      ^Rank^: 2.00,
      ^Name^: ^HtsClientTests^,
      ^Display^: ^Hts Client Tests^,
      ^ExtractSql^: ^SELECT *,'IQCare' as Emr,'KenyaHMIS' as Project FROM TempHtsClientTestsExtracts^,
      ^Destination^: ^DWHStage^,
      ^EmrSystemId^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^,
      ^DocketId^: ^HTS^,
      ^DatabaseProtocolId^: ^A6221AA4-0E85-11E8-BA89-0ED5F89F718B^
    },
    {
      ^Id^: ^A6226EB4-0E85-11E8-BA89-6FB398C5F449^,
      ^IsPriority^: false,
      ^Rank^: 8.00,
      ^Name^: ^PatientAdverseEventExtract^,
      ^Display^: ^Patient Adverse Events^,
      ^ExtractSql^: ^SELECT *,'IQCare' as Emr,'KenyaHMIS' as Project FROM TempPatientAdverseEventExtracts^,
      ^Destination^: ^dwhstage^,
      ^EmrSystemId^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^,
      ^DocketId^: ^NDWH^,
      ^DatabaseProtocolId^: ^A6221AA4-0E85-11E8-BA89-0ED5F89F718B^
    },
    {
      ^Id^: ^38742EB0-5856-E811-8E16-9CB6D0DA773C^,
      ^IsPriority^: false,
      ^Rank^: 4.00,
      ^Name^: ^PatientStatusExtract^,
      ^Display^: ^Patient Status^,
      ^ExtractSql^: ^SELECT *,'IQCare' as Emr,'KenyaHMIS' as Project FROM TempPatientStatusExtracts^,
      ^Destination^: ^dwhstage^,
      ^EmrSystemId^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^,
      ^DocketId^: ^NDWH^,
      ^DatabaseProtocolId^: ^A6221AA4-0E85-11E8-BA89-0ED5F89F718B^
    },
    {
      ^Id^: ^39742EB0-5856-E811-8E16-9CB6D0DA773C^,
      ^IsPriority^: false,
      ^Rank^: 2.00,
      ^Name^: ^PatientArtExtract^,
      ^Display^: ^ART Patients^,
      ^ExtractSql^: ^SELECT *,'IQCare' as Emr,'KenyaHMIS' as Project FROM TempPatientArtExtracts^,
      ^Destination^: ^dwhstage^,
      ^EmrSystemId^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^,
      ^DocketId^: ^NDWH^,
      ^DatabaseProtocolId^: ^A6221AA4-0E85-11E8-BA89-0ED5F89F718B^
    },
    {
      ^Id^: ^3A742EB0-5856-E811-8E16-9CB6D0DA773C^,
      ^IsPriority^: true,
      ^Rank^: 1.00,
      ^Name^: ^PatientExtract^,
      ^Display^: ^All Patients^,
      ^ExtractSql^: ^SELECT *,'IQCare' as Emr,'KenyaHMIS' as Project FROM TempPatientExtracts^,
      ^Destination^: ^dwhstage^,
      ^EmrSystemId^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^,
      ^DocketId^: ^NDWH^,
      ^DatabaseProtocolId^: ^A6221AA4-0E85-11E8-BA89-0ED5F89F718B^
    },
    {
      ^Id^: ^3C742EB0-5856-E811-8E16-9CB6D0DA773C^,
      ^IsPriority^: false,
      ^Rank^: 3.00,
      ^Name^: ^PatientBaselineExtract^,
      ^Display^: ^Patient Baselines^,
      ^ExtractSql^: ^SELECT *,'IQCare' as Emr,'KenyaHMIS' as Project FROM TempPatientBaselinesExtracts^,
      ^Destination^: ^dwhstage^,
      ^EmrSystemId^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^,
      ^DocketId^: ^NDWH^,
      ^DatabaseProtocolId^: ^A6221AA4-0E85-11E8-BA89-0ED5F89F718B^
    },
    {
      ^Id^: ^3D742EB0-5856-E811-8E16-9CB6D0DA773C^,
      ^IsPriority^: false,
      ^Rank^: 5.00,
      ^Name^: ^PatientLabExtract^,
      ^Display^: ^Patient Labs^,
      ^ExtractSql^: ^SELECT *,'IQCare' as Emr,'KenyaHMIS' as Project FROM TempPatientLaboratoryExtracts^,
      ^Destination^: ^dwhstage^,
      ^EmrSystemId^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^,
      ^DocketId^: ^NDWH^,
      ^DatabaseProtocolId^: ^A6221AA4-0E85-11E8-BA89-0ED5F89F718B^
    },
    {
      ^Id^: ^3F742EB0-5856-E811-8E16-9CB6D0DA773C^,
      ^IsPriority^: false,
      ^Rank^: 6.00,
      ^Name^: ^PatientPharmacyExtract^,
      ^Display^: ^Patient Pharmacy^,
      ^ExtractSql^: ^SELECT *,'IQCare' as Emr,'KenyaHMIS' as Project FROM TempPatientPharmacyExtracts^,
      ^Destination^: ^dwhstage^,
      ^EmrSystemId^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^,
      ^DocketId^: ^NDWH^,
      ^DatabaseProtocolId^: ^A6221AA4-0E85-11E8-BA89-0ED5F89F718B^
    },
    {
      ^Id^: ^40742EB0-5856-E811-8E16-9CB6D0DA773C^,
      ^IsPriority^: false,
      ^Rank^: 7.00,
      ^Name^: ^PatientVisitExtract^,
      ^Display^: ^Patient Visit^,
      ^ExtractSql^: ^SELECT *,'IQCare' as Emr,'KenyaHMIS' as Project FROM TempPatientVisitExtracts^,
      ^Destination^: ^dwhstage^,
      ^EmrSystemId^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^,
      ^DocketId^: ^NDWH^,
      ^DatabaseProtocolId^: ^A6221AA4-0E85-11E8-BA89-0ED5F89F718B^
    },
    {
      ^Id^: ^48742EB0-6656-E811-8E16-9CB6D0DA773C^,
      ^IsPriority^: true,
      ^Rank^: 1.00,
      ^Name^: ^MasterPatientIndex^,
      ^Display^: ^Master Patient Index^,
      ^ExtractSql^: ^SELECT *,'IQCare' as Emr,'KenyaHMIS' as Project FROM TempMasterPatientIndices^,
      ^Destination^: ^MPI^,
      ^EmrSystemId^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^,
      ^DocketId^: ^CBS^,
      ^DatabaseProtocolId^: ^A6221AA4-0E85-11E8-BA89-0ED5F89F718B^
    },
    {
      ^Id^: ^48742EB0-6656-E811-8E16-9CB6D0DA773C^,
      ^IsPriority^: true,
      ^Rank^: 1.00,
      ^Name^: ^MetricMigrationExtract^,
      ^Display^: ^Migration^,
      ^ExtractSql^: ^SELECT ID as MetricId,Dataset,Metric,MetricValue,SiteCode,Emr,FacilityName,'KenyaHMIS' as Project,CreateDate FROM DWAPI_Migration_Metrics^,
      ^Destination^: ^MgsStage^,
      ^EmrSystemId^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^,
      ^DocketId^: ^MGS^,
      ^DatabaseProtocolId^: ^A6221AA4-0E85-11E8-BA89-0ED5F89F718B^
    },

   {
      ^Id^: ^48743EB0-6656-E811-8E16-9CB6D0DA773C^,
      ^IsPriority^: true,
      ^Rank^: 1.00,
      ^Name^: ^IndicatorExtract^,
      ^Display^: ^IndicatorExtract^,
      ^ExtractSql^: ^SELECT indicator,indicatorvalue,indicatordate FROM IndicatorExtracts^,
      ^Destination^: ^IndicatorExtract^,
      ^EmrSystemId^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^,
      ^DocketId^: ^MTS^,
      ^DatabaseProtocolId^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^
    },

{
    ^Id^: ^2bbeec20-7754-11eb-9439-0242ac130002^,
    ^Display^: ^Allergies ChronicIllness^,
    ^DocketId^: ^NDWH^,
    ^EmrSystemId^: ^a6221856-0e85-11e8-ba89-0ed5f89f718b^,
    ^ExtractSql^: ^select *,visitdate VisitDate from tempAllergiesChronicIllness^,
    ^Destination^: ^dwhstage^,
    ^IsPriority^: 0,
    ^Name^: ^AllergiesChronicIllnessExtract^,
    ^Rank^: 11,
    ^DatabaseProtocolId^: ^a6221aa4-0e85-11e8-ba89-0ed5f89f718b^
  },
  {
    ^Id^: ^2bbeec21-7754-11eb-9439-0242ac130002^,
    ^Display^: ^IPT^,
    ^DocketId^: ^NDWH^,
    ^EmrSystemId^: ^a6221856-0e85-11e8-ba89-0ed5f89f718b^,
    ^ExtractSql^: ^select *,visit_date VisitDate from  tempIpt^,
    ^Destination^: ^dwhstage^,
    ^IsPriority^: 0,
    ^Name^: ^IptExtract^,
    ^Rank^: 12,
    ^DatabaseProtocolId^: ^a6221aa4-0e85-11e8-ba89-0ed5f89f718b^
  },
  {
    ^Id^: ^2bbeec22-7754-11eb-9439-0242ac130002^,
    ^Display^: ^Depression Screening^,
    ^DocketId^: ^NDWH^,
    ^EmrSystemId^: ^a6221856-0e85-11e8-ba89-0ed5f89f718b^,
    ^ExtractSql^: ^select * from tempDepressionScreening^,
    ^Destination^: ^dwhstage^,
    ^IsPriority^: 0,
    ^Name^: ^DepressionScreeningExtract^,
    ^Rank^: 13,
    ^DatabaseProtocolId^: ^a6221aa4-0e85-11e8-ba89-0ed5f89f718b^
  },
  {
    ^Id^: ^2bbeec23-7754-11eb-9439-0242ac130002^,
    ^Display^: ^Contact Listing^,
    ^DocketId^: ^NDWH^,
    ^EmrSystemId^: ^a6221856-0e85-11e8-ba89-0ed5f89f718b^,
    ^ExtractSql^: ^select * from tempContactListing^,
    ^Destination^: ^dwhstage^,
    ^IsPriority^: 0,
    ^Name^: ^ContactListingExtract^,
    ^Rank^: 14,
    ^DatabaseProtocolId^: ^a6221aa4-0e85-11e8-ba89-0ed5f89f718b^
  },
  {
    ^Id^: ^2bbeec24-7754-11eb-9439-0242ac130002^,
    ^Display^: ^GBV Screening^,
    ^DocketId^: ^NDWH^,
    ^EmrSystemId^: ^a6221856-0e85-11e8-ba89-0ed5f89f718b^,
    ^ExtractSql^: ^select * from tempGbvScreening^,
    ^Destination^: ^dwhstage^,
    ^IsPriority^: 0,
    ^Name^: ^GbvScreeningExtract^,
    ^Rank^: 15,
    ^DatabaseProtocolId^: ^a6221aa4-0e85-11e8-ba89-0ed5f89f718b^
  },
  {
    ^Id^: ^2bbeec25-7754-11eb-9439-0242ac130002^,
    ^Display^: ^Enhanced Adherence Counselling^,
    ^DocketId^: ^NDWH^,
    ^EmrSystemId^: ^a6221856-0e85-11e8-ba89-0ed5f89f718b^,
    ^ExtractSql^: ^select * from tempEnhancedAdherenceCounselling^,
    ^Destination^: ^dwhstage^,
    ^IsPriority^: 0,
    ^Name^: ^EnhancedAdherenceCounsellingExtract^,
    ^Rank^: 16,
    ^DatabaseProtocolId^: ^a6221aa4-0e85-11e8-ba89-0ed5f89f718b^
  },
  {
    ^Id^: ^2bbeec26-7754-11eb-9439-0242ac130002^,
    ^Display^: ^Drug and Alcohol Screening^,
    ^DocketId^: ^NDWH^,
    ^EmrSystemId^: ^a6221856-0e85-11e8-ba89-0ed5f89f718b^,
    ^ExtractSql^: ^select * from tempDrugAlcoholScreening^,
    ^Destination^: ^dwhstage^,
    ^IsPriority^: 0,
    ^Name^: ^DrugAlcoholScreeningExtract^,
    ^Rank^: 17,
    ^DatabaseProtocolId^: ^a6221aa4-0e85-11e8-ba89-0ed5f89f718b^
  },
  {
    ^Id^: ^2bbeec27-7754-11eb-9439-0242ac130002^,
    ^Display^: ^OVC^,
    ^DocketId^: ^NDWH^,
    ^EmrSystemId^: ^a6221856-0e85-11e8-ba89-0ed5f89f718b^,
    ^ExtractSql^: ^select *,OVCEnrolmentDate OVCEnrollmentDate from tempOvc^,
    ^Destination^: ^dwhstage^,
    ^IsPriority^: 0,
    ^Name^: ^OvcExtract^,
    ^Rank^: 18,
    ^DatabaseProtocolId^: ^a6221aa4-0e85-11e8-ba89-0ed5f89f718b^
  },
  {
    ^Id^: ^2bbeec28-7754-11eb-9439-0242ac130002^,
    ^Display^: ^OTZ^,
    ^DocketId^: ^NDWH^,
    ^EmrSystemId^: ^a6221856-0e85-11e8-ba89-0ed5f89f718b^,
    ^ExtractSql^: ^select *,OTZEnrolmentDate OTZEnrollmentDate from tempOtz^,
    ^Destination^: ^dwhstage^,
    ^IsPriority^: 0,
    ^Name^: ^OtzExtract^,
    ^Rank^: 19,
    ^DatabaseProtocolId^: ^a6221aa4-0e85-11e8-ba89-0ed5f89f718b^
  },


  {
      ^Id^: ^82650e9a-9db2-11eb-a8b3-0242ac130003^,
      ^Display^: ^PatientMnch^,
      ^DocketId^: ^MNCH^,
      ^EmrSystemId^: ^a6221856-0e85-11e8-ba89-0ed5f89f718b^,
      ^ExtractSql^: ^select * from PatientMnchExtracts^,
      ^Destination^: ^TempPatientMnchExtract^,
      ^IsPriority^: 1,
      ^Name^: ^PatientMnchExtract^,
      ^Rank^: 19,
      ^DatabaseProtocolId^: ^a6221aa4-0e85-11e8-ba89-0ed5f89f718b^
    },
    {
      ^Id^: ^82651214-9db2-11eb-a8b3-0242ac130003^,
      ^Display^: ^MnchEnrolment^,
      ^DocketId^: ^MNCH^,
      ^EmrSystemId^: ^a6221856-0e85-11e8-ba89-0ed5f89f718b^,
      ^ExtractSql^: ^select * from MnchEnrolmentExtracts^,
      ^Destination^: ^TempMnchEnrolmentExtract^,
      ^IsPriority^: 0,
      ^Name^: ^MnchEnrolmentExtract^,
      ^Rank^: 20,
      ^DatabaseProtocolId^: ^a6221aa4-0e85-11e8-ba89-0ed5f89f718b^
    },
    {
      ^Id^: ^82651386-9db2-11eb-a8b3-0242ac130003^,
      ^Display^: ^MnchArt^,
      ^DocketId^: ^MNCH^,
      ^EmrSystemId^: ^a6221856-0e85-11e8-ba89-0ed5f89f718b^,
      ^ExtractSql^: ^select * from MnchArtExtracts^,
      ^Destination^: ^TempMnchArtExtract^,
      ^IsPriority^: 0,
      ^Name^: ^MnchArtExtract^,
      ^Rank^: 21,
      ^DatabaseProtocolId^: ^a6221aa4-0e85-11e8-ba89-0ed5f89f718b^
    },
    {
      ^Id^: ^82651494-9db2-11eb-a8b3-0242ac130003^,
      ^Display^: ^AncVisit^,
      ^DocketId^: ^MNCH^,
      ^EmrSystemId^: ^a6221856-0e85-11e8-ba89-0ed5f89f718b^,
      ^ExtractSql^: ^select * from AncVisitExtracts^,
      ^Destination^: ^TempAncVisitExtract^,
      ^IsPriority^: 0,
      ^Name^: ^AncVisitExtract^,
      ^Rank^: 22,
      ^DatabaseProtocolId^: ^a6221aa4-0e85-11e8-ba89-0ed5f89f718b^
    },
    {
      ^Id^: ^826515fc-9db2-11eb-a8b3-0242ac130003^,
      ^Display^: ^MatVisit^,
      ^DocketId^: ^MNCH^,
      ^EmrSystemId^: ^a6221856-0e85-11e8-ba89-0ed5f89f718b^,
      ^ExtractSql^: ^select * from MatVisitExtracts^,
      ^Destination^: ^TempMatVisitExtract^,
      ^IsPriority^: 0,
      ^Name^: ^MatVisitExtract^,
      ^Rank^: 23,
      ^DatabaseProtocolId^: ^a6221aa4-0e85-11e8-ba89-0ed5f89f718b^
    },
    {
      ^Id^: ^82651750-9db2-11eb-a8b3-0242ac130003^,
      ^Display^: ^PncVisit^,
      ^DocketId^: ^MNCH^,
      ^EmrSystemId^: ^a6221856-0e85-11e8-ba89-0ed5f89f718b^,
      ^ExtractSql^: ^select * from PncVisitExtracts^,
      ^Destination^: ^TempPncVisitExtract^,
      ^IsPriority^: 0,
      ^Name^: ^PncVisitExtract^,
      ^Rank^: 24,
      ^DatabaseProtocolId^: ^a6221aa4-0e85-11e8-ba89-0ed5f89f718b^
    },
    {
      ^Id^: ^82651886-9db2-11eb-a8b3-0242ac130003^,
      ^Display^: ^MotherBabyPair^,
      ^DocketId^: ^MNCH^,
      ^EmrSystemId^: ^a6221856-0e85-11e8-ba89-0ed5f89f718b^,
      ^ExtractSql^: ^select * from MotherBabyPairExtracts^,
      ^Destination^: ^TempMotherBabyPairExtract^,
      ^IsPriority^: 0,
      ^Name^: ^MotherBabyPairExtract^,
      ^Rank^: 25,
      ^DatabaseProtocolId^: ^a6221aa4-0e85-11e8-ba89-0ed5f89f718b^
    },
    {
      ^Id^: ^826519c6-9db2-11eb-a8b3-0242ac130003^,
      ^Display^: ^CwcEnrolment^,
      ^DocketId^: ^MNCH^,
      ^EmrSystemId^: ^a6221856-0e85-11e8-ba89-0ed5f89f718b^,
      ^ExtractSql^: ^select * from CwcEnrolmentExtracts^,
      ^Destination^: ^TempCwcEnrolmentExtract^,
      ^IsPriority^: 0,
      ^Name^: ^CwcEnrolmentExtract^,
      ^Rank^: 26,
      ^DatabaseProtocolId^: ^a6221aa4-0e85-11e8-ba89-0ed5f89f718b^
    },
    {
      ^Id^: ^82651ade-9db2-11eb-a8b3-0242ac130003^,
      ^Display^: ^CwcVisit^,
      ^DocketId^: ^MNCH^,
      ^EmrSystemId^: ^a6221856-0e85-11e8-ba89-0ed5f89f718b^,
      ^ExtractSql^: ^select * from CwcVisitExtracts^,
      ^Destination^: ^TempCwcVisitExtract^,
      ^IsPriority^: 0,
      ^Name^: ^CwcVisitExtract^,
      ^Rank^: 27,
      ^DatabaseProtocolId^: ^a6221aa4-0e85-11e8-ba89-0ed5f89f718b^
    },
    {
      ^Id^: ^82651c00-9db2-11eb-a8b3-0242ac130003^,
      ^Display^: ^Hei^,
      ^DocketId^: ^MNCH^,
      ^EmrSystemId^: ^a6221856-0e85-11e8-ba89-0ed5f89f718b^,
      ^ExtractSql^: ^select * from HeiExtracts^,
      ^Destination^: ^TempHeiExtract^,
      ^IsPriority^: 0,
      ^Name^: ^HeiExtract^,
      ^Rank^: 28,
      ^DatabaseProtocolId^: ^a6221aa4-0e85-11e8-ba89-0ed5f89f718b^
    },
    {
      ^Id^: ^82651cfa-9db2-11eb-a8b3-0242ac130003^,
      ^Display^: ^MnchLab^,
      ^DocketId^: ^MNCH^,
      ^EmrSystemId^: ^a6221856-0e85-11e8-ba89-0ed5f89f718b^,
      ^ExtractSql^: ^select * from MnchLabExtracts^,
      ^Destination^: ^TempMnchLabExtract^,
      ^IsPriority^: 0,
      ^Name^: ^MnchLabExtract^,
      ^Rank^: 29,
      ^DatabaseProtocolId^: ^a6221aa4-0e85-11e8-ba89-0ed5f89f718b^
    }
  ]
            ";

            return Json.Replace("^", "\"");
        }
    }
}
