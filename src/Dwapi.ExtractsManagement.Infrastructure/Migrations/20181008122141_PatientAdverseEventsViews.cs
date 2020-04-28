using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class PatientAdverseEventsViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Patient Adverse Events Errors

            migrationBuilder.Sql(@"
	create view vTempPatientAdverseEventExtractError as
	SELECT        *
	FROM            TempPatientAdverseEventExtracts
	WHERE        (CheckError = 1)
                ");
            migrationBuilder.Sql(@"
	CREATE VIEW vTempPatientAdverseEventExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientAdverseEventExtractError.PatientPK,vTempPatientAdverseEventExtractError.FacilityId,
							 vTempPatientAdverseEventExtractError.PatientID, vTempPatientAdverseEventExtractError.SiteCode, ValidationError.RecordId,

							 vTempPatientAdverseEventExtractError.AdverseEvent, 
							 vTempPatientAdverseEventExtractError.AdverseEventStartDate, 
							 vTempPatientAdverseEventExtractError.AdverseEventEndDate,
                             vTempPatientAdverseEventExtractError.Severity,
                             vTempPatientAdverseEventExtractError.VisitDate,
'' AS FacilityName

	FROM            vTempPatientAdverseEventExtractError INNER JOIN
							 ValidationError ON vTempPatientAdverseEventExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP View vTempPatientAdverseEventExtractError");
            migrationBuilder.Sql("DROP View vTempPatientAdverseEventExtractErrorSummary");
        }
    }
}
