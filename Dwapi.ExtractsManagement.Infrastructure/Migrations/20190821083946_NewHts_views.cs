using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class NewHts_views : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Hts Clients Extract Errors

            migrationBuilder.Sql(@"
                        create view vTempHtsClientsExtractError as
                        SELECT  *
                        FROM    TempHtsClientsExtracts
                        WHERE   (CheckError = 1)");

            migrationBuilder.Sql(@"
                        CREATE VIEW vTempHtsClientsExtractErrorSummary
                        AS
                        SELECT ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, 
                        ValidationError.RecordId, 
                        vTempHtsClientsExtractError.FacilityName,
                        vTempHtsClientsExtractError.HtsNumber,
                        vTempHtsClientsExtractError.PatientPK,
                        vTempHtsClientsExtractError.SiteCode,
                        vTempHtsClientsExtractError.Dob,
                        vTempHtsClientsExtractError.Gender,
                        vTempHtsClientsExtractError.MaritalStatus,
                        vTempHtsClientsExtractError.PopulationType,
                        vTempHtsClientsExtractError.KeyPopulationType,
                        vTempHtsClientsExtractError.PatientDisabled,
                        vTempHtsClientsExtractError.County,
                        vTempHtsClientsExtractError.SubCounty,
                        vTempHtsClientsExtractError.Ward
                        FROM vTempHtsClientsExtractError 
                        INNER JOIN ValidationError ON vTempHtsClientsExtractError.Id = ValidationError.RecordId 
                        INNER JOIN Validator ON ValidationError.ValidatorId = Validator.Id");

            //Hts Client Tests Extract Errors

            migrationBuilder.Sql(@"
                        create view vTempHtsClientTestsExtractError as
                        SELECT  *
                        FROM    TempHtsClientTestsExtracts
                        WHERE   (CheckError = 1)");

            migrationBuilder.Sql(@"
                        CREATE VIEW vTempHtsClientTestsExtractErrorSummary
                        AS
                        SELECT ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, 
                        ValidationError.RecordId, 
                        vTempHtsClientTestsExtractError.FacilityName,
                        vTempHtsClientTestsExtractError.HtsNumber,
                        vTempHtsClientTestsExtractError.PatientPK,
                        vTempHtsClientTestsExtractError.SiteCode,
                        vTempHtsClientTestsExtractError.EncounterId,
                        vTempHtsClientTestsExtractError.TestDate,
                        vTempHtsClientTestsExtractError.EverTestedForHiv,
                        vTempHtsClientTestsExtractError.MonthsSinceLastTest,
                        vTempHtsClientTestsExtractError.ClientTestedAs,
                        vTempHtsClientTestsExtractError.EntryPoint,
                        vTempHtsClientTestsExtractError.TestStrategy,
                        vTempHtsClientTestsExtractError.TestResult1,
                        vTempHtsClientTestsExtractError.TestResult2,
                        vTempHtsClientTestsExtractError.FinalTestResult,
                        vTempHtsClientTestsExtractError.PatientGivenResult,
                        vTempHtsClientTestsExtractError.TbScreening,
                        vTempHtsClientTestsExtractError.ClientSelfTested,
                        vTempHtsClientTestsExtractError.CoupleDiscordant,
                        vTempHtsClientTestsExtractError.TestType,
                        vTempHtsClientTestsExtractError.Consent
                        FROM vTempHtsClientTestsExtractError 
                        INNER JOIN ValidationError ON vTempHtsClientTestsExtractError.Id = ValidationError.RecordId 
                        INNER JOIN Validator ON ValidationError.ValidatorId = Validator.Id");

            //Hts Client Linkage Extract Errors

            migrationBuilder.Sql(@"
                        create view vTempHtsClientsLinkageExtractError as
                        SELECT  *
                        FROM    TempHtsClientsLinkageExtracts
                        WHERE   (CheckError = 1)");

            migrationBuilder.Sql(@"
                        CREATE VIEW vTempHtsClientsLinkageExtractErrorSummary
                        AS
                        SELECT ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, 
                        ValidationError.RecordId, 
                        vTempHtsClientsLinkageExtractError.FacilityName,
                        vTempHtsClientsLinkageExtractError.HtsNumber,
                        vTempHtsClientsLinkageExtractError.PatientPK,
                        vTempHtsClientsLinkageExtractError.SiteCode,
                        vTempHtsClientsLinkageExtractError.DatePrefferedToBeEnrolled,
                        vTempHtsClientsLinkageExtractError.FacilityReferredTo,
                        vTempHtsClientsLinkageExtractError.HandedOverTo,
                        vTempHtsClientsLinkageExtractError.HandedOverToCadre,
                        vTempHtsClientsLinkageExtractError.EnrolledFacilityName,
                        vTempHtsClientsLinkageExtractError.ReferralDate,
                        vTempHtsClientsLinkageExtractError.DateEnrolled,
                        vTempHtsClientsLinkageExtractError.ReportedCCCNumber,
                        vTempHtsClientsLinkageExtractError.ReportedStartARTDate 
                        FROM vTempHtsClientsLinkageExtractError 
                        INNER JOIN ValidationError ON vTempHtsClientsLinkageExtractError.Id = ValidationError.RecordId 
                        INNER JOIN Validator ON ValidationError.ValidatorId = Validator.Id");

            //Hts Client Tracing Extract Errors

            migrationBuilder.Sql(@"
                        create view vTempHtsClientTracingExtractError as
                        SELECT  *
                        FROM    TempHtsClientTracingExtracts
                        WHERE   (CheckError = 1)");

            migrationBuilder.Sql(@"
                        CREATE VIEW vTempHtsClientTracingExtractErrorSummary
                        AS
                        SELECT ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, 
                        ValidationError.RecordId, 
                        vTempHtsClientTracingExtractError.FacilityName,
                        vTempHtsClientTracingExtractError.HtsNumber,
                        vTempHtsClientTracingExtractError.PatientPK,
                        vTempHtsClientTracingExtractError.SiteCode,
                        vTempHtsClientTracingExtractError.TracingType,
                        vTempHtsClientTracingExtractError.TracingDate,
                        vTempHtsClientTracingExtractError.TracingOutcome
                        FROM vTempHtsClientTracingExtractError 
                        INNER JOIN ValidationError ON vTempHtsClientTracingExtractError.Id = ValidationError.RecordId 
                        INNER JOIN Validator ON ValidationError.ValidatorId = Validator.Id");

            //Hts Partner Tracing Extract Errors

            migrationBuilder.Sql(@"
                        create view vTempHtsPartnerTracingExtractError as
                        SELECT  *
                        FROM    TempHtsPartnerTracingExtracts
                        WHERE   (CheckError = 1)");

            migrationBuilder.Sql(@"
                        CREATE VIEW vTempHtsPartnerTracingExtractErrorSummary
                        AS
                        SELECT ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, 
                        ValidationError.RecordId, 
                        vTempHtsPartnerTracingExtractError.FacilityName,
                        vTempHtsPartnerTracingExtractError.HtsNumber,
                        vTempHtsPartnerTracingExtractError.PatientPK,
                        vTempHtsPartnerTracingExtractError.SiteCode,
                        vTempHtsPartnerTracingExtractError.TraceType,
                        vTempHtsPartnerTracingExtractError.PartnerPersonId,
                        vTempHtsPartnerTracingExtractError.TraceDate,
                        vTempHtsPartnerTracingExtractError.TraceOutcome,
                        vTempHtsPartnerTracingExtractError.BookingDate

                        FROM vTempHtsPartnerTracingExtractError 
                        INNER JOIN ValidationError ON vTempHtsPartnerTracingExtractError.Id = ValidationError.RecordId 
                        INNER JOIN Validator ON ValidationError.ValidatorId = Validator.Id");

            //Hts Test Kits Extract Errors

            migrationBuilder.Sql(@"
                        create view vTempHtsTestKitsExtractError as
                        SELECT  *
                        FROM    TempHtsTestKitsExtracts
                        WHERE   (CheckError = 1)");

            migrationBuilder.Sql(@"
                        CREATE VIEW vTempHtsTestKitsExtractErrorSummary
                        AS
                        SELECT ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, 
                        ValidationError.RecordId, 
                        vTempHtsTestKitsExtractError.FacilityName,
                        vTempHtsTestKitsExtractError.HtsNumber,
                        vTempHtsTestKitsExtractError.PatientPK,
                        vTempHtsTestKitsExtractError.SiteCode,
                        vTempHtsTestKitsExtractError.EncounterId,
                        vTempHtsTestKitsExtractError.TestKitName1,
                        vTempHtsTestKitsExtractError.TestKitLotNumber1,
                        vTempHtsTestKitsExtractError.TestKitExpiry1,
                        vTempHtsTestKitsExtractError.TestResult1,
                        vTempHtsTestKitsExtractError.TestKitName2,
                        vTempHtsTestKitsExtractError.TestKitLotNumber2,
                        vTempHtsTestKitsExtractError.TestKitExpiry2,
                        vTempHtsTestKitsExtractError.TestResult2
                        FROM vTempHtsTestKitsExtractError 
                        INNER JOIN ValidationError ON vTempHtsTestKitsExtractError.Id = ValidationError.RecordId 
                        INNER JOIN Validator ON ValidationError.ValidatorId = Validator.Id");

            //Hts Partner Notification Services Extract Errors

            migrationBuilder.Sql(@"
                        create view vTempHtsPartnerNotificationServicesExtractError as
                        SELECT  *
                        FROM    TempHtsPartnerNotificationServicesExtracts
                        WHERE   (CheckError = 1)");

            migrationBuilder.Sql(@"
                        CREATE VIEW vTempHtsPartnerNotificationServicesExtractErrorSummary
                        AS
                        SELECT ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, 
                        ValidationError.RecordId, 
                        vTempHtsPartnerNotificationServicesExtractError.FacilityName,
                        vTempHtsPartnerNotificationServicesExtractError.HtsNumber,
                        vTempHtsPartnerNotificationServicesExtractError.PatientPK,
                        vTempHtsPartnerNotificationServicesExtractError.SiteCode,
                        vTempHtsPartnerNotificationServicesExtractError.PartnerPatientPk,
                        vTempHtsPartnerNotificationServicesExtractError.PartnerPersonID,
                        vTempHtsPartnerNotificationServicesExtractError.Age,
                        vTempHtsPartnerNotificationServicesExtractError.Sex,
                        vTempHtsPartnerNotificationServicesExtractError.RelationsipToIndexClient,
                        vTempHtsPartnerNotificationServicesExtractError.ScreenedForIpv,
                        vTempHtsPartnerNotificationServicesExtractError.IpvScreeningOutcome,
                        vTempHtsPartnerNotificationServicesExtractError.CurrentlyLivingWithIndexClient,
                        vTempHtsPartnerNotificationServicesExtractError.KnowledgeOfHivStatus,
                        vTempHtsPartnerNotificationServicesExtractError.PnsApproach,
                        vTempHtsPartnerNotificationServicesExtractError.PnsConsent,
                        vTempHtsPartnerNotificationServicesExtractError.LinkedToCare,
                        vTempHtsPartnerNotificationServicesExtractError.LinkDateLinkedToCare,
                        vTempHtsPartnerNotificationServicesExtractError.CccNumber,
                        vTempHtsPartnerNotificationServicesExtractError.FacilityLinkedTo,
                        vTempHtsPartnerNotificationServicesExtractError.Dob,
                        vTempHtsPartnerNotificationServicesExtractError.DateElicited,
                        vTempHtsPartnerNotificationServicesExtractError.MaritalStatus
                        FROM vTempHtsPartnerNotificationServicesExtractError 
                        INNER JOIN ValidationError ON vTempHtsPartnerNotificationServicesExtractError.Id = ValidationError.RecordId 
                        INNER JOIN Validator ON ValidationError.ValidatorId = Validator.Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP View vTempHtsClientsExtractError");
            migrationBuilder.Sql("DROP View vTempHtsClientsExtractErrorSummary");
            migrationBuilder.Sql("DROP View vTempHtsClientTestsExtractError");
            migrationBuilder.Sql("DROP View vTempHtsClientTestsExtractErrorSummary");
            migrationBuilder.Sql("DROP View vTempHtsClientLinkageExtractError");
            migrationBuilder.Sql("DROP View vTempHtsClientLinkageExtractErrorSummary");
            migrationBuilder.Sql("DROP View vTempHtsClientTracingExtractError");
            migrationBuilder.Sql("DROP View vTempHtsClientTracingExtractErrorSummary");
            migrationBuilder.Sql("DROP View vTempHtsPartnerTracingExtractError");
            migrationBuilder.Sql("DROP View vTempHtsPartnerTracingExtractErrorSummary");
            migrationBuilder.Sql("DROP View vTempHtsTestKitsExtractError");
            migrationBuilder.Sql("DROP View vTempHtsTestKitsExtractErrorSummary");
            migrationBuilder.Sql("DROP View vTempHtsPartnerNotificationServicesExtractError");
            migrationBuilder.Sql("DROP View vTempHtsPartnerNotificationServicesExtractErrorSummary");
        }
    }
}
