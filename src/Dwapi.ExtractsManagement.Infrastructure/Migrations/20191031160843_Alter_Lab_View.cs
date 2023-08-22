using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class Alter_Lab_View : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	        if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
	        {
		        migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 0;");
		        migrationBuilder.Sql(
			        @"alter table TempPatientLaboratoryExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        migrationBuilder.Sql(@"alter table Validator convert to character set utf8 collate utf8_unicode_ci;");
		        migrationBuilder.Sql(
			        @"alter table ValidationError convert to character set utf8 collate utf8_unicode_ci;");
		        migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 1;");
	        }

	        migrationBuilder.Sql(@"
	            ALTER view vTempPatientLaboratoryExtractError as
	            SELECT        *
	            FROM            TempPatientLaboratoryExtracts
	            WHERE        (CheckError = 1)
                            ");

            migrationBuilder.Sql(@"
	            ALTER view vTempPatientLaboratoryExtractErrorSummary
	            AS
	            SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientLaboratoryExtractError.PatientPK,vTempPatientLaboratoryExtractError.FacilityId,
							             vTempPatientLaboratoryExtractError.PatientID, vTempPatientLaboratoryExtractError.SiteCode, vTempPatientLaboratoryExtractError.FacilityName, ValidationError.RecordId,

							             vTempPatientLaboratoryExtractError.OrderedByDate, 
							             vTempPatientLaboratoryExtractError.ReportedByDate, 
							             vTempPatientLaboratoryExtractError.TestName, 
							             vTempPatientLaboratoryExtractError.EnrollmentTest, 
							             vTempPatientLaboratoryExtractError.TestResult, 
                                         vTempPatientLaboratoryExtractError.Reason, 
							             vTempPatientLaboratoryExtractError.VisitId

	            FROM            vTempPatientLaboratoryExtractError INNER JOIN
							             ValidationError ON vTempPatientLaboratoryExtractError.Id = ValidationError.RecordId INNER JOIN
							             Validator ON ValidationError.ValidatorId = Validator.Id
                            ");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
