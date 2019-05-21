using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HtsModelViews : Migration
    {
          protected override void Up(MigrationBuilder migrationBuilder)
        {
            //HTSClient Extract Errors

            migrationBuilder.Sql($@"
                        create view vTempHTSClientExtractError as
                        SELECT  *
                        FROM    {nameof(ExtractsContext.TempHtsClientExtracts)}
                        WHERE   (CheckError = 1)");

            migrationBuilder.Sql(@"
                        CREATE VIEW vTempHTSClientExtractErrorSummary
                        AS
                        SELECT
                                ValidationError.Id,Validator.Extract,Validator.Field,Validator.Type,Validator.Summary,ValidationError.DateGenerated,ValidationError.RecordId,
                              vTempHTSClientExtractError.FacilityName,
                              vTempHTSClientExtractError.SiteCode,
                              vTempHTSClientExtractError.PatientPk,
                              vTempHTSClientExtractError.HtsNumber,
                              vTempHTSClientExtractError.Emr,
                              vTempHTSClientExtractError.Project,
                              vTempHTSClientExtractError.CheckError,
                              vTempHTSClientExtractError.DateExtracted,
                              vTempHTSClientExtractError.EncounterId,
                              vTempHTSClientExtractError.VisitDate,
                              vTempHTSClientExtractError.Dob,
                              vTempHTSClientExtractError.Gender,
                              vTempHTSClientExtractError.MaritalStatus,
                              vTempHTSClientExtractError.KeyPop,
                              vTempHTSClientExtractError.TestedBefore,
                              vTempHTSClientExtractError.MonthsLastTested,
                              vTempHTSClientExtractError.ClientTestedAs,
                              vTempHTSClientExtractError.StrategyHTS,
                              vTempHTSClientExtractError.TestKitName1,
                              vTempHTSClientExtractError.TestKitLotNumber1,
                              vTempHTSClientExtractError.TestKitExpiryDate1,
                              vTempHTSClientExtractError.TestResultsHTS1,
                              vTempHTSClientExtractError.TestKitName2,
                              vTempHTSClientExtractError.TestKitLotNumber2,
                              vTempHTSClientExtractError.TestKitExpiryDate2,
                              vTempHTSClientExtractError.TestResultsHTS2,
                              vTempHTSClientExtractError.FinalResultHTS,
                              vTempHTSClientExtractError.FinalResultsGiven,
                              vTempHTSClientExtractError.TBScreeningHTS,
                              vTempHTSClientExtractError.ClientSelfTested,
                              vTempHTSClientExtractError.CoupleDiscordant,
                              vTempHTSClientExtractError.TestType,
                              vTempHTSClientExtractError.KeyPopulationType,
                              vTempHTSClientExtractError.PopulationType,
                              vTempHTSClientExtractError.PatientDisabled,
                              vTempHTSClientExtractError.DisabilityType
                        FROM
                                vTempHTSClientExtractError INNER JOIN
                                ValidationError ON vTempHTSClientExtractError.Id = ValidationError.RecordId INNER JOIN
                                Validator ON ValidationError.ValidatorId = Validator.Id");


            //HTSClientLinkage Extract Errors

            migrationBuilder.Sql($@"
                        create view vTempHTSClientLinkageExtractError as
                        SELECT  *
                        FROM    {nameof(ExtractsContext.TempHtsClientLinkageExtracts)}
                        WHERE   (CheckError = 1)");

            migrationBuilder.Sql(@"
                        CREATE VIEW vTempHTSClientLinkageExtractErrorSummary
                        AS
                        SELECT
                                ValidationError.Id,Validator.Extract,Validator.Field,Validator.Type,Validator.Summary,ValidationError.DateGenerated,ValidationError.RecordId,
                                 vTempHTSClientLinkageExtractError.FacilityName,
                                  vTempHTSClientLinkageExtractError.SiteCode,
                                  vTempHTSClientLinkageExtractError.PatientPk,
                                  vTempHTSClientLinkageExtractError.HtsNumber,
                                  vTempHTSClientLinkageExtractError.Emr,
                                  vTempHTSClientLinkageExtractError.Project,
                                  vTempHTSClientLinkageExtractError.CheckError,
                                  vTempHTSClientLinkageExtractError.DateExtracted,
                                  vTempHTSClientLinkageExtractError.PhoneTracingDate,
                                  vTempHTSClientLinkageExtractError.PhysicalTracingDate,
                                  vTempHTSClientLinkageExtractError.TracingOutcome,
                                  vTempHTSClientLinkageExtractError.CccNumber,
                                  vTempHTSClientLinkageExtractError.ReferralDate,
                                  vTempHTSClientLinkageExtractError.DateEnrolled,
                                  vTempHTSClientLinkageExtractError.EnrolledFacilityName
                        FROM
                                vTempHTSClientLinkageExtractError INNER JOIN
                                ValidationError ON vTempHTSClientLinkageExtractError.Id = ValidationError.RecordId INNER JOIN
                                Validator ON ValidationError.ValidatorId = Validator.Id");

            //HTSClientPartner Extract Errors

            migrationBuilder.Sql($@"
                        create view vTempHTSClientPartnerExtractError as
                        SELECT  *
                        FROM    {nameof(ExtractsContext.TempHtsClientPartnerExtracts)}
                        WHERE   (CheckError = 1)");

            migrationBuilder.Sql(@"
                        CREATE VIEW vTempHTSClientPartnerExtractErrorSummary
                        AS
                        SELECT
                                ValidationError.Id,Validator.Extract,Validator.Field,Validator.Type,Validator.Summary,ValidationError.DateGenerated,ValidationError.RecordId,
                                  vTempHTSClientPartnerExtractError.FacilityName,
                                  vTempHTSClientPartnerExtractError.SiteCode,
                                  vTempHTSClientPartnerExtractError.PatientPk,
                                  vTempHTSClientPartnerExtractError.HtsNumber,
                                  vTempHTSClientPartnerExtractError.Emr,
                                  vTempHTSClientPartnerExtractError.Project,
                                  vTempHTSClientPartnerExtractError.CheckError,
                                  vTempHTSClientPartnerExtractError.DateExtracted,
                                  vTempHTSClientPartnerExtractError.PartnerPatientPk,
                                  vTempHTSClientPartnerExtractError.PartnerPersonId,
                                  vTempHTSClientPartnerExtractError.RelationshipToIndexClient,
                                  vTempHTSClientPartnerExtractError.ScreenedForIpv,
                                  vTempHTSClientPartnerExtractError.IpvScreeningOutcome,
                                  vTempHTSClientPartnerExtractError.CurrentlyLivingWithIndexClient,
                                  vTempHTSClientPartnerExtractError.KnowledgeOfHivStatus,
                                  vTempHTSClientPartnerExtractError.PnsApproach,
                                  vTempHTSClientPartnerExtractError.Trace1Outcome,
                                  vTempHTSClientPartnerExtractError.Trace1Type,
                                  vTempHTSClientPartnerExtractError.Trace1Date,
                                  vTempHTSClientPartnerExtractError.Trace2Outcome,
                                  vTempHTSClientPartnerExtractError.Trace2Type,
                                  vTempHTSClientPartnerExtractError.Trace2Date,
                                  vTempHTSClientPartnerExtractError.Trace3Outcome,
                                  vTempHTSClientPartnerExtractError.Trace3Type,
                                  vTempHTSClientPartnerExtractError.Trace3Date,
                                  vTempHTSClientPartnerExtractError.PnsConsent,
                                  vTempHTSClientPartnerExtractError.Linked,
                                  vTempHTSClientPartnerExtractError.LinkDateLinkedToCare,
                                  vTempHTSClientPartnerExtractError.CccNumber,
                                  vTempHTSClientPartnerExtractError.Age,
                                  vTempHTSClientPartnerExtractError.Sex
                        FROM
                                vTempHTSClientPartnerExtractError INNER JOIN
                                ValidationError ON vTempHTSClientPartnerExtractError.Id = ValidationError.RecordId INNER JOIN
                                Validator ON ValidationError.ValidatorId = Validator.Id");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"DROP VIEW vTempHTSClientExtractErrorSummary");
            migrationBuilder.Sql(@"DROP VIEW vTempHTSClientExtractError");
            migrationBuilder.Sql(@"DROP VIEW vTempHTSClientLinkageExtractErrorSummary");
            migrationBuilder.Sql(@"DROP VIEW vTempHTSClientLinkageExtractError");
            migrationBuilder.Sql(@"DROP VIEW vTempHTSClientPartnerExtractErrorSummary");
            migrationBuilder.Sql(@"DROP VIEW vTempHTSClientPartnerExtractError");
        }
    }
}
