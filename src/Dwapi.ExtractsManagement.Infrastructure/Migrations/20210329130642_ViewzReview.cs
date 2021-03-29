using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class ViewzReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER view vTempPatientExtractError as SELECT * FROM TempPatientExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempPatientArtExtractError as SELECT * FROM TempPatientArtExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempPatientBaselinesExtractError as SELECT * FROM TempPatientBaselinesExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempPatientLaboratoryExtractError as SELECT * FROM TempPatientLaboratoryExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempPatientPharmacyExtractError as SELECT * FROM TempPatientPharmacyExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempPatientStatusExtractError as SELECT * FROM TempPatientStatusExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempPatientVisitExtractError as SELECT * FROM TempPatientVisitExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempPatientAdverseEventExtractError as SELECT * FROM TempPatientAdverseEventExtracts WHERE (CheckError = 1)");




            migrationBuilder.Sql(@"
                        ALTER VIEW vTempPatientExtractErrorSummary
                        AS
                        SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientExtractError.PatientPK,vTempPatientExtractError.FacilityId,
                                                 vTempPatientExtractError.PatientID, vTempPatientExtractError.SiteCode, vTempPatientExtractError.FacilityName, ValidationError.RecordId,
vTempPatientExtractError.Date_Created,
vTempPatientExtractError.Date_Last_Modified


                        FROM            vTempPatientExtractError INNER JOIN
                                                 ValidationError ON vTempPatientExtractError.Id = ValidationError.RecordId INNER JOIN
                                                 Validator ON ValidationError.ValidatorId = Validator.Id");


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
							vTempPatientArtExtractError.Date_Created,
							vTempPatientArtExtractError.Date_Last_Modified
	FROM            vTempPatientArtExtractError INNER JOIN
							 ValidationError ON vTempPatientArtExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");


            migrationBuilder.Sql(@"
	ALTER VIEW vTempPatientBaselinesExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientBaselinesExtractError.PatientPK,vTempPatientBaselinesExtractError.FacilityId,
							 vTempPatientBaselinesExtractError.PatientID, vTempPatientBaselinesExtractError.SiteCode, ValidationError.RecordId,
						 
							 vTempPatientBaselinesExtractError.bCD4, 
							 vTempPatientBaselinesExtractError.bCD4Date, 
							 vTempPatientBaselinesExtractError.bWAB, 
							 vTempPatientBaselinesExtractError.bWABDate, 
							 vTempPatientBaselinesExtractError.bWHO, 
							 vTempPatientBaselinesExtractError.bWHODate, 
							 vTempPatientBaselinesExtractError.eWAB, 
							 vTempPatientBaselinesExtractError.eWABDate, 
							 vTempPatientBaselinesExtractError.eCD4,
							 vTempPatientBaselinesExtractError.eCD4Date, 
							 vTempPatientBaselinesExtractError.eWHO, 
							 vTempPatientBaselinesExtractError.eWHODate, 
							 vTempPatientBaselinesExtractError.lastWHO, 
							 vTempPatientBaselinesExtractError.lastWHODate, 
							 vTempPatientBaselinesExtractError.lastCD4, 
							 vTempPatientBaselinesExtractError.lastCD4Date, 
							 vTempPatientBaselinesExtractError.lastWAB, 
							 vTempPatientBaselinesExtractError.lastWABDate, 
							 vTempPatientBaselinesExtractError.m12CD4, 
							 vTempPatientBaselinesExtractError.m12CD4Date, 
							 vTempPatientBaselinesExtractError.m6CD4, 
							 vTempPatientBaselinesExtractError.m6CD4Date,
							vTempPatientBaselinesExtractError.Date_Created,
							vTempPatientBaselinesExtractError.Date_Last_Modified

	FROM            vTempPatientBaselinesExtractError INNER JOIN
							 ValidationError ON vTempPatientBaselinesExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
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
							 vTempPatientLaboratoryExtractError.VisitId,
							vTempPatientLaboratoryExtractError.Date_Created,
							vTempPatientLaboratoryExtractError.Date_Last_Modified

	FROM            vTempPatientLaboratoryExtractError INNER JOIN
							 ValidationError ON vTempPatientLaboratoryExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

            migrationBuilder.Sql(@"
	ALTER VIEW vTempPatientPharmacyExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientPharmacyExtractError.PatientPK,vTempPatientPharmacyExtractError.FacilityId,
							 vTempPatientPharmacyExtractError.PatientID, vTempPatientPharmacyExtractError.SiteCode,  ValidationError.RecordId,

							 vTempPatientPharmacyExtractError.Drug, 
							 vTempPatientPharmacyExtractError.DispenseDate,
							 vTempPatientPharmacyExtractError.Duration, 
							 vTempPatientPharmacyExtractError.ExpectedReturn, 
							 vTempPatientPharmacyExtractError.TreatmentType, 
							 vTempPatientPharmacyExtractError.RegimenLine, 
							 vTempPatientPharmacyExtractError.PeriodTaken, 
							 vTempPatientPharmacyExtractError.ProphylaxisType, 
							 vTempPatientPharmacyExtractError.Provider, 
							 vTempPatientPharmacyExtractError.VisitID,
							vTempPatientPharmacyExtractError.Date_Created,
							vTempPatientPharmacyExtractError.Date_Last_Modified

	FROM            vTempPatientPharmacyExtractError INNER JOIN
							 ValidationError ON vTempPatientPharmacyExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

            migrationBuilder.Sql(@"
	ALTER VIEW vTempPatientStatusExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientStatusExtractError.PatientPK,vTempPatientStatusExtractError.FacilityId,
							 vTempPatientStatusExtractError.PatientID, vTempPatientStatusExtractError.SiteCode, vTempPatientStatusExtractError.FacilityName, ValidationError.RecordId,

							 vTempPatientStatusExtractError.ExitDescription, 
							 vTempPatientStatusExtractError.ExitDate, 
							 vTempPatientStatusExtractError.ExitReason,
							vTempPatientStatusExtractError.Date_Created,
							vTempPatientStatusExtractError.Date_Last_Modified

	FROM            vTempPatientStatusExtractError INNER JOIN
							 ValidationError ON vTempPatientStatusExtractError.Id = ValidationError.RecordId INNER JOIN
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
							vTempPatientVisitExtractError.Date_Created,
							vTempPatientVisitExtractError.Date_Last_Modified

	FROM            vTempPatientVisitExtractError INNER JOIN
							 ValidationError ON vTempPatientVisitExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

            migrationBuilder.Sql(@"
	ALTER VIEW vTempPatientAdverseEventExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientAdverseEventExtractError.PatientPK,vTempPatientAdverseEventExtractError.FacilityId,
							 vTempPatientAdverseEventExtractError.PatientID, vTempPatientAdverseEventExtractError.SiteCode, ValidationError.RecordId,

							 vTempPatientAdverseEventExtractError.AdverseEvent, 
							 vTempPatientAdverseEventExtractError.AdverseEventStartDate, 
							 vTempPatientAdverseEventExtractError.AdverseEventEndDate,
                             vTempPatientAdverseEventExtractError.Severity,
                             vTempPatientAdverseEventExtractError.VisitDate,
'' AS FacilityName,
vTempPatientAdverseEventExtractError.Date_Created,
vTempPatientAdverseEventExtractError.Date_Last_Modified

	FROM            vTempPatientAdverseEventExtractError INNER JOIN
							 ValidationError ON vTempPatientAdverseEventExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
