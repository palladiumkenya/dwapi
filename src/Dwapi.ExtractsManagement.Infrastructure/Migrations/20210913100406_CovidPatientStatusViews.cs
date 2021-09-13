using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class CovidPatientStatusViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 0;");
                migrationBuilder.Sql(
                    @"alter table PatientStatusExtracts convert to character set utf8 collate utf8_unicode_ci;");

                migrationBuilder.Sql(
                    @"alter table TempPatientStatusExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 1;");
            }

            migrationBuilder.Sql(@"
	ALTER view vTempPatientStatusExtractError as
	SELECT        *
	FROM            TempPatientStatusExtracts
	WHERE        (CheckError = 1)
                ");
            migrationBuilder.Sql(@"
	ALTER VIEW vTempPatientStatusExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientStatusExtractError.PatientPK,vTempPatientStatusExtractError.FacilityId,
							 vTempPatientStatusExtractError.PatientID, vTempPatientStatusExtractError.SiteCode, vTempPatientStatusExtractError.FacilityName, ValidationError.RecordId,

							 vTempPatientStatusExtractError.ExitDescription, 
							 vTempPatientStatusExtractError.ExitDate, 
							 vTempPatientStatusExtractError.ExitReason,
							 vTempPatientStatusExtractError.ReasonForDeath,
							 vTempPatientStatusExtractError.SpecificDeathReason,
							 vTempPatientStatusExtractError.DeathDate

	FROM            vTempPatientStatusExtractError INNER JOIN
							 ValidationError ON vTempPatientStatusExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
