using System;
using System.Collections.Generic;
using Dwapi.SettingsManagement.Core.Model;
using Newtonsoft.Json;

namespace Dwapi.ExtractsManagement.Infrastructure.Tests.TestArtifacts
{
    public class TestData
    {
        public static EmrSystem GenerateEmrSystem(string conection)
        {
            var emr = JsonConvert.DeserializeObject<EmrSystem>(EmrJson());
            emr.Id=Guid.NewGuid();

            var db = JsonConvert.DeserializeObject<DatabaseProtocol>(DbJson());
            db.Id=Guid.NewGuid();
            db.EmrSystemId = emr.Id;
            db.DatabaseName = conection;
            emr.DatabaseProtocols.Add(db);

            emr.Extracts= GetExtracts(emr.Id, db.Id);

            return emr;
        }

        private static string EmrJson()
        {
            string Json =@"
                 {
                    ^Id^: ^A6221856-0E85-11E8-BA89-0ED5F89F718B^,
                    ^Name^: ^KenyaEMR^,
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

            return Json;
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
            return @"
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
    }
  ]
            ";
        }
    }
}
