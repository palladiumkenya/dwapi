using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class ExtractErrorSummaries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Patient ART Errors
            migrationBuilder.Sql(@"create view vTempPatientArtExtractError as
SELECT        *
FROM            TempPatientArtExtracts
WHERE        (CheckError = 1)
                ");
            migrationBuilder.Sql(@"CREATE VIEW vTempPatientArtExtractErrorSummary
AS
SELECT        dbo.ValidationError.Id, dbo.Validator.Extract, dbo.Validator.Field, dbo.Validator.Type, dbo.Validator.Summary, dbo.ValidationError.DateGenerated, dbo.vTempPatientArtExtractError.PatientPK,dbo.vTempPatientArtExtractError.FacilityId,
                         dbo.vTempPatientArtExtractError.PatientID, dbo.vTempPatientArtExtractError.SiteCode, dbo.vTempPatientArtExtractError.FacilityName, dbo.ValidationError.RecordId,
						 
						 dbo.vTempPatientArtExtractError.DOB, 
						 dbo.vTempPatientArtExtractError.Gender, 
                         dbo.vTempPatientArtExtractError.PatientSource, 
						 dbo.vTempPatientArtExtractError.RegistrationDate, 
						 dbo.vTempPatientArtExtractError.AgeLastVisit, 
						 dbo.vTempPatientArtExtractError.PreviousARTStartDate, 
                         dbo.vTempPatientArtExtractError.PreviousARTRegimen, 
						 dbo.vTempPatientArtExtractError.StartARTAtThisFacility, 
						 dbo.vTempPatientArtExtractError.StartARTDate, 
						 dbo.vTempPatientArtExtractError.StartRegimen, 
                         dbo.vTempPatientArtExtractError.StartRegimenLine, 
						 dbo.vTempPatientArtExtractError.LastARTDate, 
						 dbo.vTempPatientArtExtractError.LastRegimen, 
						 dbo.vTempPatientArtExtractError.LastRegimenLine, 
						 dbo.vTempPatientArtExtractError.LastVisit, 
                         dbo.vTempPatientArtExtractError.ExitReason, 
						 dbo.vTempPatientArtExtractError.ExitDate
FROM            dbo.vTempPatientArtExtractError INNER JOIN
                         dbo.ValidationError ON dbo.vTempPatientArtExtractError.Id = dbo.ValidationError.RecordId INNER JOIN
                         dbo.Validator ON dbo.ValidationError.ValidatorId = dbo.Validator.Id

                ");

            //Patient Baselines errors
            migrationBuilder.Sql(@"create view vTempPatientBaselinesExtractError as
SELECT        *
FROM            TempPatientBaselinesExtracts
WHERE        (CheckError = 1)
                ");
            migrationBuilder.Sql(@"CREATE VIEW vTempPatientBaselinesExtractErrorSummary
AS
SELECT        dbo.ValidationError.Id, dbo.Validator.Extract, dbo.Validator.Field, dbo.Validator.Type, dbo.Validator.Summary, dbo.ValidationError.DateGenerated, dbo.vTempPatientBaselinesExtractError.PatientPK,dbo.vTempPatientBaselinesExtractError.FacilityId,
                         dbo.vTempPatientBaselinesExtractError.PatientID, dbo.vTempPatientBaselinesExtractError.SiteCode, dbo.ValidationError.RecordId,
						 
						 dbo.vTempPatientBaselinesExtractError.bCD4, 
						 dbo.vTempPatientBaselinesExtractError.bCD4Date, 
						 dbo.vTempPatientBaselinesExtractError.bWAB, 
						 dbo.vTempPatientBaselinesExtractError.bWABDate, 
                         dbo.vTempPatientBaselinesExtractError.bWHO, 
						 dbo.vTempPatientBaselinesExtractError.bWHODate, 
						 dbo.vTempPatientBaselinesExtractError.eWAB, 
						 dbo.vTempPatientBaselinesExtractError.eWABDate, 
                         dbo.vTempPatientBaselinesExtractError.eCD4,
						 dbo.vTempPatientBaselinesExtractError.eCD4Date, 
						 dbo.vTempPatientBaselinesExtractError.eWHO, 
						 dbo.vTempPatientBaselinesExtractError.eWHODate, 
                         dbo.vTempPatientBaselinesExtractError.lastWHO, 
						 dbo.vTempPatientBaselinesExtractError.lastWHODate, 
						 dbo.vTempPatientBaselinesExtractError.lastCD4, 
						 dbo.vTempPatientBaselinesExtractError.lastCD4Date, 
                         dbo.vTempPatientBaselinesExtractError.lastWAB, 
						 dbo.vTempPatientBaselinesExtractError.lastWABDate, 
						 dbo.vTempPatientBaselinesExtractError.m12CD4, 
						 dbo.vTempPatientBaselinesExtractError.m12CD4Date, 
                         dbo.vTempPatientBaselinesExtractError.m6CD4, 
						 dbo.vTempPatientBaselinesExtractError.m6CD4Date

FROM            dbo.vTempPatientBaselinesExtractError INNER JOIN
                         dbo.ValidationError ON dbo.vTempPatientBaselinesExtractError.Id = dbo.ValidationError.RecordId INNER JOIN
                         dbo.Validator ON dbo.ValidationError.ValidatorId = dbo.Validator.Id

                ");

            //Patient Labs Errors
            migrationBuilder.Sql(@"create view vTempPatientLaboratoryExtractError as
SELECT        *
FROM            TempPatientLaboratoryExtracts
WHERE        (CheckError = 1)
                ");
            migrationBuilder.Sql(@"vTempPatientLaboratoryExtractErrorSummary
AS
SELECT        dbo.ValidationError.Id, dbo.Validator.Extract, dbo.Validator.Field, dbo.Validator.Type, dbo.Validator.Summary, dbo.ValidationError.DateGenerated, dbo.vTempPatientLaboratoryExtractError.PatientPK,dbo.vTempPatientLaboratoryExtractError.FacilityId,
                         dbo.vTempPatientLaboratoryExtractError.PatientID, dbo.vTempPatientLaboratoryExtractError.SiteCode, dbo.vTempPatientLaboratoryExtractError.FacilityName, dbo.ValidationError.RecordId,

						 dbo.vTempPatientLaboratoryExtractError.OrderedByDate, 
						 dbo.vTempPatientLaboratoryExtractError.ReportedByDate, 
						 dbo.vTempPatientLaboratoryExtractError.TestName, 
                         dbo.vTempPatientLaboratoryExtractError.EnrollmentTest, 
						 dbo.vTempPatientLaboratoryExtractError.TestResult, 
						 dbo.vTempPatientLaboratoryExtractError.VisitId

FROM            dbo.vTempPatientLaboratoryExtractError INNER JOIN
                         dbo.ValidationError ON dbo.vTempPatientLaboratoryExtractError.Id = dbo.ValidationError.RecordId INNER JOIN
                         dbo.Validator ON dbo.ValidationError.ValidatorId = dbo.Validator.Id
                ");

            // Patient Pharmacy Errors
            migrationBuilder.Sql(@"create view vTempPatientPharmacyExtractError as
SELECT        *
FROM            TempPatientPharmacyExtracts
WHERE        (CheckError = 1)
                ");
            migrationBuilder.Sql(@"CREATE VIEW vTempPatientPharmacyExtractErrorSummary
AS
SELECT        dbo.ValidationError.Id, dbo.Validator.Extract, dbo.Validator.Field, dbo.Validator.Type, dbo.Validator.Summary, dbo.ValidationError.DateGenerated, dbo.vTempPatientPharmacyExtractError.PatientPK,dbo.vTempPatientPharmacyExtractError.FacilityId,
                         dbo.vTempPatientPharmacyExtractError.PatientID, dbo.vTempPatientPharmacyExtractError.SiteCode,  dbo.ValidationError.RecordId,

					     dbo.vTempPatientPharmacyExtractError.Drug, 
						 dbo.vTempPatientPharmacyExtractError.DispenseDate,
						 dbo.vTempPatientPharmacyExtractError.Duration, 
						 dbo.vTempPatientPharmacyExtractError.ExpectedReturn, 
                         dbo.vTempPatientPharmacyExtractError.TreatmentType, 
						 dbo.vTempPatientPharmacyExtractError.RegimenLine, 
						 dbo.vTempPatientPharmacyExtractError.PeriodTaken, 
                         dbo.vTempPatientPharmacyExtractError.ProphylaxisType, 
						 dbo.vTempPatientPharmacyExtractError.Provider, 
						 dbo.vTempPatientPharmacyExtractError.VisitID

FROM            dbo.vTempPatientPharmacyExtractError INNER JOIN
                         dbo.ValidationError ON dbo.vTempPatientPharmacyExtractError.Id = dbo.ValidationError.RecordId INNER JOIN
                         dbo.Validator ON dbo.ValidationError.ValidatorId = dbo.Validator.Id
                ");

            //Patient Status Errors
            migrationBuilder.Sql(@"vTempPatientStatusExtractError as
SELECT        *
FROM            TempPatientStatusExtracts
WHERE        (CheckError = 1)
                ");
            migrationBuilder.Sql(@"CREATE VIEW vTempPatientStatusExtractErrorSummary
AS
SELECT        dbo.ValidationError.Id, dbo.Validator.Extract, dbo.Validator.Field, dbo.Validator.Type, dbo.Validator.Summary, dbo.ValidationError.DateGenerated, dbo.vTempPatientStatusExtractError.PatientPK,dbo.vTempPatientStatusExtractError.FacilityId,
                         dbo.vTempPatientStatusExtractError.PatientID, dbo.vTempPatientStatusExtractError.SiteCode, dbo.vTempPatientStatusExtractError.FacilityName, dbo.ValidationError.RecordId,

						 dbo.vTempPatientStatusExtractError.ExitDescription, 
						 dbo.vTempPatientStatusExtractError.ExitDate, 
						 dbo.vTempPatientStatusExtractError.ExitReason

FROM            dbo.vTempPatientStatusExtractError INNER JOIN
                         dbo.ValidationError ON dbo.vTempPatientStatusExtractError.Id = dbo.ValidationError.RecordId INNER JOIN
                         dbo.Validator ON dbo.ValidationError.ValidatorId = dbo.Validator.Id
                ");

            //Patient visit errors
            migrationBuilder.Sql(@"create view vTempPatientVisitExtractError as
SELECT        *
FROM            TempPatientVisitExtracts
WHERE        (CheckError = 1)
                ");
            migrationBuilder.Sql(@"CREATE VIEW vTempPatientVisitExtractErrorSummary
AS
SELECT        dbo.ValidationError.Id, dbo.Validator.Extract, dbo.Validator.Field, dbo.Validator.Type, dbo.Validator.Summary, dbo.ValidationError.DateGenerated, dbo.vTempPatientVisitExtractError.PatientPK,dbo.vTempPatientVisitExtractError.FacilityId,
                         dbo.vTempPatientVisitExtractError.PatientID, dbo.vTempPatientVisitExtractError.SiteCode, dbo.vTempPatientVisitExtractError.FacilityName, dbo.ValidationError.RecordId,

						 dbo.vTempPatientVisitExtractError.VisitDate, 
						 dbo.vTempPatientVisitExtractError.Service, 
						 dbo.vTempPatientVisitExtractError.VisitType, 
						 dbo.vTempPatientVisitExtractError.WHOStage, 
                         dbo.vTempPatientVisitExtractError.WABStage, 
						 dbo.vTempPatientVisitExtractError.Pregnant, 
						 dbo.vTempPatientVisitExtractError.LMP, 
						 dbo.vTempPatientVisitExtractError.EDD, 
                         dbo.vTempPatientVisitExtractError.Height, 
						 dbo.vTempPatientVisitExtractError.Weight, 
						 dbo.vTempPatientVisitExtractError.BP, 
						 dbo.vTempPatientVisitExtractError.OI, 
						 dbo.vTempPatientVisitExtractError.OIDate, 
                         dbo.vTempPatientVisitExtractError.Adherence, 
						 dbo.vTempPatientVisitExtractError.AdherenceCategory, 
						 dbo.vTempPatientVisitExtractError.SubstitutionFirstlineRegimenDate, 
                         dbo.vTempPatientVisitExtractError.SubstitutionFirstlineRegimenReason, 
						 dbo.vTempPatientVisitExtractError.SubstitutionSecondlineRegimenDate, 
                         dbo.vTempPatientVisitExtractError.SubstitutionSecondlineRegimenReason, 
						 dbo.vTempPatientVisitExtractError.SecondlineRegimenChangeDate, 
                         dbo.vTempPatientVisitExtractError.SecondlineRegimenChangeReason, 
						 dbo.vTempPatientVisitExtractError.FamilyPlanningMethod, 
						 dbo.vTempPatientVisitExtractError.PwP, 
                         dbo.vTempPatientVisitExtractError.GestationAge, 
						 dbo.vTempPatientVisitExtractError.NextAppointmentDate, 
						 dbo.vTempPatientVisitExtractError.VisitId

FROM            dbo.vTempPatientVisitExtractError INNER JOIN
                         dbo.ValidationError ON dbo.vTempPatientVisitExtractError.Id = dbo.ValidationError.RecordId INNER JOIN
                         dbo.Validator ON dbo.ValidationError.ValidatorId = dbo.Validator.Id
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
