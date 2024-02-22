using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class AddedDefaulterTracingVariablesViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	        migrationBuilder.Sql(@"ALTER view vTempDefaulterTracingExtractError as SELECT * FROM TempDefaulterTracingExtracts WHERE (CheckError = 1)");
	        migrationBuilder.Sql(@"ALTER view vTempAllergiesChronicIllnessExtractError as SELECT * FROM TempAllergiesChronicIllnessExtracts WHERE (CheckError = 1)");


            migrationBuilder.Sql(@"

						ALTER VIEW vTempAllergiesChronicIllnessExtractErrorSummary
						AS
						SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId, 
					 
					vTempAllergiesChronicIllnessExtractError.PatientPK,
					vTempAllergiesChronicIllnessExtractError.PatientID,
					vTempAllergiesChronicIllnessExtractError.FacilityId,
					vTempAllergiesChronicIllnessExtractError.SiteCode,
					vTempAllergiesChronicIllnessExtractError.DateExtracted,
					vTempAllergiesChronicIllnessExtractError.Emr,
					vTempAllergiesChronicIllnessExtractError.Project,
					vTempAllergiesChronicIllnessExtractError.CheckError,
					vTempAllergiesChronicIllnessExtractError.ErrorType,
					vTempAllergiesChronicIllnessExtractError.FacilityName,
					vTempAllergiesChronicIllnessExtractError.VisitID,
					vTempAllergiesChronicIllnessExtractError.VisitDate,
					vTempAllergiesChronicIllnessExtractError.ChronicIllness,
					vTempAllergiesChronicIllnessExtractError.ChronicOnsetDate,
					vTempAllergiesChronicIllnessExtractError.knownAllergies,
					vTempAllergiesChronicIllnessExtractError.AllergyCausativeAgent,
					vTempAllergiesChronicIllnessExtractError.AllergicReaction,
					vTempAllergiesChronicIllnessExtractError.AllergySeverity,
					vTempAllergiesChronicIllnessExtractError.AllergyOnsetDate,
					vTempAllergiesChronicIllnessExtractError.Skin,
					vTempAllergiesChronicIllnessExtractError.Eyes,
					vTempAllergiesChronicIllnessExtractError.ENT,
					vTempAllergiesChronicIllnessExtractError.Chest,
					vTempAllergiesChronicIllnessExtractError.CVS,
					vTempAllergiesChronicIllnessExtractError.Abdomen,
					vTempAllergiesChronicIllnessExtractError.CNS,
					vTempAllergiesChronicIllnessExtractError.Genitourinary,
					vTempAllergiesChronicIllnessExtractError.Date_Created,
					vTempAllergiesChronicIllnessExtractError.Date_Last_Modified,
					vTempAllergiesChronicIllnessExtractError.RecordUUID,
					vTempAllergiesChronicIllnessExtractError.Voided,
					vTempAllergiesChronicIllnessExtractError.Controlled


						FROM            vTempAllergiesChronicIllnessExtractError INNER JOIN
							 ValidationError ON vTempAllergiesChronicIllnessExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
            
            
            migrationBuilder.Sql(@"
				ALTER VIEW vTempDefaulterTracingExtractErrorSummary
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
							vTempDefaulterTracingExtractError.Date_Last_Modified,
							vTempDefaulterTracingExtractError.RecordUUID,
							vTempDefaulterTracingExtractError.Voided,
							vTempDefaulterTracingExtractError.DatePromisedToCome,
							vTempDefaulterTracingExtractError.ReasonForMissedAppointment,
							vTempDefaulterTracingExtractError.DateOfMissedAppointment
			                   
				FROM            vTempDefaulterTracingExtractError INNER JOIN
										 ValidationError ON vTempDefaulterTracingExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
			                ");



             
             
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
