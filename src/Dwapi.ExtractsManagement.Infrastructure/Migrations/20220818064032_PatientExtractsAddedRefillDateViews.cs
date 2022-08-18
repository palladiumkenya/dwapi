using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class PatientExtractsAddedRefillDateViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	        migrationBuilder.Sql(@"ALTER view vTempPatientVisitExtractError as SELECT * FROM TempPatientVisitExtracts WHERE (CheckError = 1)");

           migrationBuilder.Sql(@"
				ALTER VIEW vTempPatientVisitExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, ValidationError.RecordId,
							vTempPatientVisitExtractError.PatientPK,
							vTempPatientVisitExtractError.FacilityId,
							 vTempPatientVisitExtractError.PatientID,
							 vTempPatientVisitExtractError.SiteCode,
							 vTempPatientVisitExtractError.FacilityName,
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
							vTempPatientVisitExtractError.Date_Last_Modified,
							vTempPatientVisitExtractError.RefillDate


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
