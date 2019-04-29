using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HtsClientReviewViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //HTSClient Extract Errors

            migrationBuilder.Sql($@"
                        alter view vTempHTSClientExtractError as
                        SELECT  *
                        FROM    {nameof(ExtractsContext.TempHtsClientExtracts)}
                        WHERE   (CheckError = 1)");

            migrationBuilder.Sql(@"
                        alter VIEW vTempHTSClientExtractErrorSummary
                        AS
                        SELECT
                                ValidationError.Id,Validator.Extract,Validator.Field,Validator.Type,Validator.Summary,ValidationError.DateGenerated,ValidationError.RecordId,
                                vTempHTSClientExtractError.PatientPk,
                                vTempHTSClientExtractError.HtsNumber,
                                vTempHTSClientExtractError.SiteCode,
                                vTempHTSClientExtractError.FacilityName
                        FROM
                                vTempHTSClientExtractError INNER JOIN
                                ValidationError ON vTempHTSClientExtractError.Id = ValidationError.RecordId INNER JOIN
                                Validator ON ValidationError.ValidatorId = Validator.Id");


            //HTSClientLinkage Extract Errors

            migrationBuilder.Sql($@"
                        alter view vTempHTSClientLinkageExtractError as
                        SELECT  *
                        FROM    {nameof(ExtractsContext.TempHtsClientLinkageExtracts)}
                        WHERE   (CheckError = 1)");

            migrationBuilder.Sql(@"
                        alter VIEW vTempHTSClientLinkageExtractErrorSummary
                        AS
                        SELECT
                                ValidationError.Id,Validator.Extract,Validator.Field,Validator.Type,Validator.Summary,ValidationError.DateGenerated,ValidationError.RecordId,
                                vTempHTSClientLinkageExtractError.PatientPk,
                                vTempHTSClientLinkageExtractError.HtsNumber,
                                vTempHTSClientLinkageExtractError.SiteCode,
                                vTempHTSClientLinkageExtractError.FacilityName
                        FROM
                                vTempHTSClientLinkageExtractError INNER JOIN
                                ValidationError ON vTempHTSClientLinkageExtractError.Id = ValidationError.RecordId INNER JOIN
                                Validator ON ValidationError.ValidatorId = Validator.Id");

            //HTSClientPartner Extract Errors

            migrationBuilder.Sql($@"
                        alter view vTempHTSClientPartnerExtractError as
                        SELECT  *
                        FROM    {nameof(ExtractsContext.TempHtsClientPartnerExtracts)}
                        WHERE   (CheckError = 1)");

            migrationBuilder.Sql(@"
                        alter VIEW vTempHTSClientPartnerExtractErrorSummary
                        AS
                        SELECT
                                ValidationError.Id,Validator.Extract,Validator.Field,Validator.Type,Validator.Summary,ValidationError.DateGenerated,ValidationError.RecordId,
                                vTempHTSClientPartnerExtractError.PatientPk,
                                vTempHTSClientPartnerExtractError.HtsNumber,
                                vTempHTSClientPartnerExtractError.SiteCode,
                                vTempHTSClientPartnerExtractError.FacilityName
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
