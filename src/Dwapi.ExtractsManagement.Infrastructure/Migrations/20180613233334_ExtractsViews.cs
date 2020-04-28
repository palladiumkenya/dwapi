using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class ExtractsViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Patient Extract Errors

            migrationBuilder.Sql(@"
                        create view vTempPatientExtractError as
                        SELECT  *
                        FROM    TempPatientExtracts
                        WHERE   (CheckError = 1)");

            migrationBuilder.Sql(@"
                        CREATE VIEW vTempPatientExtractErrorSummary
                        AS
                        SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientExtractError.PatientPK,vTempPatientExtractError.FacilityId,
                                                 vTempPatientExtractError.PatientID, vTempPatientExtractError.SiteCode, vTempPatientExtractError.FacilityName, ValidationError.RecordId
                        FROM            vTempPatientExtractError INNER JOIN
                                                 ValidationError ON vTempPatientExtractError.Id = ValidationError.RecordId INNER JOIN
                                                 Validator ON ValidationError.ValidatorId = Validator.Id");

            //Patient ART Errors

            migrationBuilder.Sql(@"
	create view vTempPatientArtExtractError as
	SELECT        *
	FROM            TempPatientArtExtracts
	WHERE        (CheckError = 1)
                ");
            migrationBuilder.Sql(@"
	CREATE VIEW vTempPatientArtExtractErrorSummary
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
							 vTempPatientArtExtractError.ExitDate
	FROM            vTempPatientArtExtractError INNER JOIN
							 ValidationError ON vTempPatientArtExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

            //Patient Baselines errors

            migrationBuilder.Sql(@"
	create view vTempPatientBaselinesExtractError as
	SELECT        *
	FROM            TempPatientBaselinesExtracts
	WHERE        (CheckError = 1)
                ");
            migrationBuilder.Sql(@"
	CREATE VIEW vTempPatientBaselinesExtractErrorSummary
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
							 vTempPatientBaselinesExtractError.m6CD4Date

	FROM            vTempPatientBaselinesExtractError INNER JOIN
							 ValidationError ON vTempPatientBaselinesExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

            //Patient Labs Errors

            migrationBuilder.Sql(@"
	create view vTempPatientLaboratoryExtractError as
	SELECT        *
	FROM            TempPatientLaboratoryExtracts
	WHERE        (CheckError = 1)
                ");
            migrationBuilder.Sql(@"
	CREATE view vTempPatientLaboratoryExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientLaboratoryExtractError.PatientPK,vTempPatientLaboratoryExtractError.FacilityId,
							 vTempPatientLaboratoryExtractError.PatientID, vTempPatientLaboratoryExtractError.SiteCode, vTempPatientLaboratoryExtractError.FacilityName, ValidationError.RecordId,

							 vTempPatientLaboratoryExtractError.OrderedByDate, 
							 vTempPatientLaboratoryExtractError.ReportedByDate, 
							 vTempPatientLaboratoryExtractError.TestName, 
							 vTempPatientLaboratoryExtractError.EnrollmentTest, 
							 vTempPatientLaboratoryExtractError.TestResult, 
							 vTempPatientLaboratoryExtractError.VisitId

	FROM            vTempPatientLaboratoryExtractError INNER JOIN
							 ValidationError ON vTempPatientLaboratoryExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

            // Patient Pharmacy Errors

            migrationBuilder.Sql(@"
	create view vTempPatientPharmacyExtractError as
	SELECT        *
	FROM            TempPatientPharmacyExtracts
	WHERE        (CheckError = 1)
                ");
            migrationBuilder.Sql(@"
	CREATE VIEW vTempPatientPharmacyExtractErrorSummary
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
							 vTempPatientPharmacyExtractError.VisitID

	FROM            vTempPatientPharmacyExtractError INNER JOIN
							 ValidationError ON vTempPatientPharmacyExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

            //Patient Status Errors

            migrationBuilder.Sql(@"
	create view vTempPatientStatusExtractError as
	SELECT        *
	FROM            TempPatientStatusExtracts
	WHERE        (CheckError = 1)
                ");
            migrationBuilder.Sql(@"
	CREATE VIEW vTempPatientStatusExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientStatusExtractError.PatientPK,vTempPatientStatusExtractError.FacilityId,
							 vTempPatientStatusExtractError.PatientID, vTempPatientStatusExtractError.SiteCode, vTempPatientStatusExtractError.FacilityName, ValidationError.RecordId,

							 vTempPatientStatusExtractError.ExitDescription, 
							 vTempPatientStatusExtractError.ExitDate, 
							 vTempPatientStatusExtractError.ExitReason

	FROM            vTempPatientStatusExtractError INNER JOIN
							 ValidationError ON vTempPatientStatusExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

            //Patient visit errors

            migrationBuilder.Sql(@"
	create view vTempPatientVisitExtractError as
	SELECT        *
	FROM            TempPatientVisitExtracts
	WHERE        (CheckError = 1)
                ");
            migrationBuilder.Sql(@"

	CREATE VIEW vTempPatientVisitExtractErrorSummary
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
							 vTempPatientVisitExtractError.VisitId

	FROM            vTempPatientVisitExtractError INNER JOIN
							 ValidationError ON vTempPatientVisitExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW vTempPatientExtractErrorSummary");
            migrationBuilder.Sql(@"DROP VIEW vTempPatientExtractError");
            migrationBuilder.Sql("DROP View vTempPatientArtExtractError");
            migrationBuilder.Sql("DROP View vTempPatientArtExtractErrorSummary");
            migrationBuilder.Sql("DROP View vTempPatientBaselinesExtractError");
            migrationBuilder.Sql("DROP View vTempPatientBaselinesExtractErrorSummary");
            migrationBuilder.Sql("DROP View vTempPatientLaboratoryExtractError");
            migrationBuilder.Sql("DROP View vTempPatientLaboratoryExtractErrorSummary");
            migrationBuilder.Sql("DROP View vTempPatientPharmacyExtractError");
            migrationBuilder.Sql("DROP View vTempPatientPharmacyExtractErrorSummary");
            migrationBuilder.Sql("DROP View vTempPatientStatusExtractError");
            migrationBuilder.Sql("DROP View vTempPatientStatusExtractErrorSummary");
            migrationBuilder.Sql("DROP View vTempPatientVisitExtractError");
            migrationBuilder.Sql("DROP View vTempPatientVisitExtractErrorSummary");
        }
    }
}
