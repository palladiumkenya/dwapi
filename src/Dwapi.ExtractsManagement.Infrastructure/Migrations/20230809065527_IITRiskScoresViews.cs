using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class IITRiskScoresViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
	        {
		        migrationBuilder.Sql(@"alter table IITRiskScoresExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        migrationBuilder.Sql(@"alter table TempIITRiskScoresExtracts convert to character set utf8 collate utf8_unicode_ci;");
	        }

            migrationBuilder.Sql(@"create view vTempIITRiskScoresExtractError as SELECT * FROM TempIITRiskScoresExtracts WHERE (CheckError = 1)");

            		migrationBuilder.Sql(@"
				CREATE VIEW vTempIITRiskScoresExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,

							vTempIITRiskScoresExtractError.PatientPK,
							vTempIITRiskScoresExtractError.SiteCode,
							vTempIITRiskScoresExtractError.PatientID,
							vTempIITRiskScoresExtractError.FacilityID,
							vTempIITRiskScoresExtractError.Emr,
							vTempIITRiskScoresExtractError.Project,
							vTempIITRiskScoresExtractError.FacilityName,
							vTempIITRiskScoresExtractError.SourceSysUUID,
							vTempIITRiskScoresExtractError.RiskScore,
							vTempIITRiskScoresExtractError.RiskFactors,
							vTempIITRiskScoresExtractError.RiskDescription,
							vTempIITRiskScoresExtractError.RiskEvaluationDate,							
							vTempIITRiskScoresExtractError.Date_Created,
							vTempIITRiskScoresExtractError.Date_Last_Modified					


				FROM            vTempIITRiskScoresExtractError INNER JOIN
										 ValidationError ON vTempIITRiskScoresExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
