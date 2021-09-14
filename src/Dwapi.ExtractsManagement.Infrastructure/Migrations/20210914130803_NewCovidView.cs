using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
	public partial class NewCovidView : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(
				@"create view vTempCovidExtractError as SELECT * FROM TempCovidExtracts WHERE (CheckError = 1)");
			migrationBuilder.Sql(
				@"create view vTempDefaulterTracingExtractError as SELECT * FROM TempDefaulterTracingExtracts WHERE (CheckError = 1)");

			migrationBuilder.Sql(@"
				CREATE VIEW vTempDefaulterTracingExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,
							
							vTempDefaulterTracingExtractError.PatientPK,
							vTempDefaulterTracingExtractError.FacilityId,
							vTempDefaulterTracingExtractError.SiteCode,
							vTempDefaulterTracingExtractError.PatientID,
							vTempDefaulterTracingExtractError.Emr,
							vTempDefaulterTracingExtractError.Project,
							vTempDefaulterTracingExtractError.FacilityName,
							vTempDefaulterTracingExtractError.VisitID,
							vTempDefaulterTracingExtractError.VisitDate,
							vTempDefaulterTracingExtractError.EncounterId,
							vTempDefaulterTracingExtractError.TracingType,
							vTempDefaulterTracingExtractError.TracingOutcome,
							vTempDefaulterTracingExtractError.AttemptNumber,
							vTempDefaulterTracingExtractError.IsFinalTrace,
							vTempDefaulterTracingExtractError.TrueStatus,
							vTempDefaulterTracingExtractError.CauseOfDeath,
							vTempDefaulterTracingExtractError.Comments,
							vTempDefaulterTracingExtractError.BookingDate,
							
							vTempDefaulterTracingExtractError.Date_Created,
							vTempDefaulterTracingExtractError.Date_Last_Modified
			                   
				FROM            vTempDefaulterTracingExtractError INNER JOIN
										 ValidationError ON vTempDefaulterTracingExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
			                ");

						migrationBuilder.Sql(@"
				CREATE VIEW vTempCovidExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,


			vTempCovidExtractError.PatientPK,
			vTempCovidExtractError.FacilityId,
			vTempCovidExtractError.PatientID, 
			vTempCovidExtractError.SiteCode,
			vTempCovidExtractError.Emr,
			vTempCovidExtractError.Project,

			vTempCovidExtractError.FacilityName,
			vTempCovidExtractError.VisitID,
			vTempCovidExtractError.Covid19AssessmentDate,
			vTempCovidExtractError.ReceivedCOVID19Vaccine,
			vTempCovidExtractError.DateGivenFirstDose,
			vTempCovidExtractError.FirstDoseVaccineAdministered,
			vTempCovidExtractError.DateGivenSecondDose,
			vTempCovidExtractError.SecondDoseVaccineAdministered,
			vTempCovidExtractError.VaccinationStatus,
			vTempCovidExtractError.VaccineVerification,
			vTempCovidExtractError.BoosterGiven,
			vTempCovidExtractError.BoosterDose,
			vTempCovidExtractError.BoosterDoseDate,
			vTempCovidExtractError.EverCOVID19Positive,
			vTempCovidExtractError.COVID19TestDate,
			vTempCovidExtractError.PatientStatus,
			vTempCovidExtractError.AdmissionStatus,
			vTempCovidExtractError.AdmissionUnit,
			vTempCovidExtractError.MissedAppointmentDueToCOVID19,
			vTempCovidExtractError.COVID19PositiveSinceLasVisit,
			vTempCovidExtractError.COVID19TestDateSinceLastVisit,
			vTempCovidExtractError.PatientStatusSinceLastVisit,
			vTempCovidExtractError.AdmissionStatusSinceLastVisit,
			vTempCovidExtractError.AdmissionStartDate,
			vTempCovidExtractError.AdmissionEndDate,
			vTempCovidExtractError.AdmissionUnitSinceLastVisit,
			vTempCovidExtractError.SupplementalOxygenReceived,
			vTempCovidExtractError.PatientVentilated,
			vTempCovidExtractError.TracingFinalOutcome,
			vTempCovidExtractError.CauseOfDeath,

			vTempCovidExtractError.Date_Created,
			vTempCovidExtractError.Date_Last_Modified


			                        
				FROM            vTempCovidExtractError INNER JOIN
										 ValidationError ON vTempCovidExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{

		}
	}
}
