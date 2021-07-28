using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class NewCTQViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER view vTempPatientVisitExtractError as SELECT * FROM TempPatientVisitExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempPatientArtExtractError as SELECT * FROM TempPatientArtExtracts WHERE (CheckError = 1)");


            migrationBuilder.Sql(@"
	ALTER VIEW vTempPatientArtExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientArtExtractError.PatientPK,vTempPatientArtExtractError.FacilityId,
							 vTempPatientArtExtractError.PatientID, vTempPatientArtExtractError.SiteCode, vTempPatientArtExtractError.FacilityName, ValidationError.RecordId,
						 
							 vTempPatientArtExtractError.DOB, 
							 vTempPatientArtExtractError.Gender, 
							 vTempPatientArtExtractError.PatientSource, 
							 vTempPatientArtExtractError.RegistrationDate, 
							 vTempPatientArtExtractError.AgeLastVisit, 
							 vTempPatientArtExtractError.PreviousARTStartDate, 
							 vTempPatientArtExtractError.PreviousARTRegimen, 
							 vTempPatientArtExtractError.StartARTAtThisFacility, 
							 vTempPatientArtExtractError.StartARTDate, 
							 vTempPatientArtExtractError.StartRegimen, 
							 vTempPatientArtExtractError.StartRegimenLine, 
							 vTempPatientArtExtractError.LastARTDate, 
							 vTempPatientArtExtractError.LastRegimen, 
							 vTempPatientArtExtractError.LastRegimenLine, 
							 vTempPatientArtExtractError.LastVisit, 
							 vTempPatientArtExtractError.ExitReason, 
							 vTempPatientArtExtractError.ExitDate,
							 vTempPatientArtExtractError.PreviousARTUse,
							vTempPatientArtExtractError.PreviousARTPurpose,
							vTempPatientArtExtractError.DateLastUsed,
							vTempPatientArtExtractError.Date_Created,
							vTempPatientArtExtractError.Date_Last_Modified
	FROM            vTempPatientArtExtractError INNER JOIN
							 ValidationError ON vTempPatientArtExtractError.Id = ValidationError.RecordId INNER JOIN
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

							vTempPatientVisitExtractError.GeneralExamination,
							vTempPatientVisitExtractError.SystemExamination,
							vTempPatientVisitExtractError.Skin,
							vTempPatientVisitExtractError.Eyes,
							vTempPatientVisitExtractError.ENT,
							vTempPatientVisitExtractError.Chest,
							vTempPatientVisitExtractError.CVS,
							vTempPatientVisitExtractError.Abdomen,
							vTempPatientVisitExtractError.CNS,
							vTempPatientVisitExtractError.Genitourinary,

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
