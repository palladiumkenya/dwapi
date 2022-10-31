using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HtsRiskScoresInitialViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"alter table HtsRiskScoresExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempHtsRiskScoresExtracts convert to character set utf8 collate utf8_unicode_ci;");
            }
            
            migrationBuilder.Sql(@"create view vTempHtsRiskScoresExtractsError as SELECT * FROM TempHtsRiskScoresExtracts WHERE (CheckError = 1)");

            migrationBuilder.Sql(@"
				CREATE VIEW vTempHtsRiskScoresExtractsErrorSummary
				AS
         		SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, ValidationError.RecordId,
 
					vTempHtsRiskScoresExtractsError.PatientPK,
					vTempHtsRiskScoresExtractsError.SiteCode,
					vTempHtsRiskScoresExtractsError.FacilityName,
					vTempHtsRiskScoresExtractsError.Emr,
					vTempHtsRiskScoresExtractsError.Project,
					vTempHtsRiskScoresExtractsError.HtsNumber,
					
					vTempHtsRiskScoresExtractsError.SourceSysUUID,
					vTempHtsRiskScoresExtractsError.RiskScore,
					vTempHtsRiskScoresExtractsError.RiskFactors,
					vTempHtsRiskScoresExtractsError.Description,
					vTempHtsRiskScoresExtractsError.EvaluationDate,
					vTempHtsRiskScoresExtractsError.DateCreated,
					vTempHtsRiskScoresExtractsError.DateLastModified

				FROM            vTempHtsRiskScoresExtractsError INNER JOIN
										 ValidationError ON vTempHtsRiskScoresExtractsError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
            ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
