using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class CTViewzRev : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	        migrationBuilder.Sql(@"ALTER view vTempPatientLaboratoryExtractError as SELECT * FROM TempPatientLaboratoryExtracts WHERE (CheckError = 1)");
	        migrationBuilder.Sql(@"ALTER view vTempPatientVisitExtractError as SELECT * FROM TempPatientVisitExtracts WHERE (CheckError = 1)");

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
							 vTempPatientLaboratoryExtractError.VisitId,
							vTempPatientLaboratoryExtractError.Reason,
							vTempPatientLaboratoryExtractError.Date_Created,
							vTempPatientLaboratoryExtractError.Date_Last_Modified

	FROM            vTempPatientLaboratoryExtractError INNER JOIN
							 ValidationError ON vTempPatientLaboratoryExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

            migrationBuilder.Sql(@"

	ALTER VIEW vTempPatientVisitExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientVisitExtractError.PatientPK,vTempPatientVisitExtractError.FacilityId,
							 vTempPatientVisitExtractError.PatientID, vTempPatientVisitExtractError.SiteCode, vTempPatientVisitExtractError.FacilityName, ValidationError.RecordId,

							 vTempPatientVisitExtractError.VisitDate, 
							 vTempPatientVisitExtractError.Service, 
							 vTempPatientVisitExtractError.VisitType, 
							 vTempPatientVisitExtractError.WHOStage, 
							 vTempPatientVisitExtractError.WABStage, 
							 vTempPatientVisitExtractError.Pregnant, 
							 vTempPatientVisitExtractError.LMP, 
							 vTempPatientVisitExtractError.EDD, 
							 vTempPatientVisitExtractError.Height, 
							 vTempPatientVisitExtractError.Weight, 
							 vTempPatientVisitExtractError.BP, 
							 vTempPatientVisitExtractError.OI, 
							 vTempPatientVisitExtractError.OIDate, 
							 vTempPatientVisitExtractError.Adherence, 
							 vTempPatientVisitExtractError.AdherenceCategory, 
							 vTempPatientVisitExtractError.SubstitutionFirstlineRegimenDate, 
							 vTempPatientVisitExtractError.SubstitutionFirstlineRegimenReason, 
							 vTempPatientVisitExtractError.SubstitutionSecondlineRegimenDate, 
							 vTempPatientVisitExtractError.SubstitutionSecondlineRegimenReason, 
							 vTempPatientVisitExtractError.SecondlineRegimenChangeDate, 
							 vTempPatientVisitExtractError.SecondlineRegimenChangeReason, 
							 vTempPatientVisitExtractError.FamilyPlanningMethod, 
							 vTempPatientVisitExtractError.PwP, 
							 vTempPatientVisitExtractError.GestationAge, 
							 vTempPatientVisitExtractError.NextAppointmentDate, 
							 vTempPatientVisitExtractError.VisitId,
							vTempPatientVisitExtractError.DifferentiatedCare,
							vTempPatientVisitExtractError.KeyPopulationType,
							vTempPatientVisitExtractError.PopulationType,
							vTempPatientVisitExtractError.StabilityAssessment,
							vTempPatientVisitExtractError.Date_Created,
							vTempPatientVisitExtractError.Date_Last_Modified

	FROM            vTempPatientVisitExtractError INNER JOIN
							 ValidationError ON vTempPatientVisitExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
