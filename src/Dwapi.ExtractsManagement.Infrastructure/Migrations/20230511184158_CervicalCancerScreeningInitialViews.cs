using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class CervicalCancerScreeningInitialViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	        if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
	        {
		        migrationBuilder.Sql(@"alter table CervicalCancerScreeningExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        migrationBuilder.Sql(@"alter table TempCervicalCancerScreeningExtracts convert to character set utf8 collate utf8_unicode_ci;");
	        }

            migrationBuilder.Sql(@"create view vTempCervicalCancerScreeningExtractError as SELECT * FROM TempCervicalCancerScreeningExtracts WHERE (CheckError = 1)");

            		migrationBuilder.Sql(@"
				CREATE VIEW vTempCervicalCancerScreeningExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,

							vTempCervicalCancerScreeningExtractError.PatientPK,
							vTempCervicalCancerScreeningExtractError.SiteCode,
							vTempCervicalCancerScreeningExtractError.PatientID,
							vTempCervicalCancerScreeningExtractError.FacilityID,
							vTempCervicalCancerScreeningExtractError.Emr,
							vTempCervicalCancerScreeningExtractError.Project,
							vTempCervicalCancerScreeningExtractError.FacilityName,
							vTempCervicalCancerScreeningExtractError.VisitType,
							vTempCervicalCancerScreeningExtractError.VisitID,
							vTempCervicalCancerScreeningExtractError.VisitDate,
							vTempCervicalCancerScreeningExtractError.ScreeningMethod,
							vTempCervicalCancerScreeningExtractError.TreatmentToday,
							vTempCervicalCancerScreeningExtractError.ReferredOut,
							vTempCervicalCancerScreeningExtractError.NextAppointmentDate,
							vTempCervicalCancerScreeningExtractError.ScreeningType,
							vTempCervicalCancerScreeningExtractError.ScreeningResult,
					        vTempCervicalCancerScreeningExtractError.PostTreatmentComplicationCause,
					        vTempCervicalCancerScreeningExtractError.OtherPostTreatmentComplication,
					        vTempCervicalCancerScreeningExtractError.ReferralReason,
							vTempCervicalCancerScreeningExtractError.Date_Created,
							vTempCervicalCancerScreeningExtractError.Date_Last_Modified					


				FROM            vTempCervicalCancerScreeningExtractError INNER JOIN
										 ValidationError ON vTempCervicalCancerScreeningExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
        }
        

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
