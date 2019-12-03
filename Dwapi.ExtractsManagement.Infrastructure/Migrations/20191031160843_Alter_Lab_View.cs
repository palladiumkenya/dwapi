using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class Alter_Lab_View : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        { 
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
