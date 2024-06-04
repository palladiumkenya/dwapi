using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class IPTandVisitsMissingVariablesViews : Migration
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
							vTempPatientVisitExtractError.RefillDate,
							vTempPatientVisitExtractError.PaedsDisclosure,
							vTempPatientVisitExtractError.ZScoreAbsolute,
							vTempPatientVisitExtractError.ZScore,
							vTempPatientVisitExtractError.DifferentiatedCare,
							vTempPatientVisitExtractError.KeyPopulationType,
							vTempPatientVisitExtractError.PopulationType,
							vTempPatientVisitExtractError.StabilityAssessment,
							vTempPatientVisitExtractError.RecordUUID,
							vTempPatientVisitExtractError.Voided,
							vTempPatientVisitExtractError.WHOStagingOI,
							vTempPatientVisitExtractError.WantsToGetPregnant,
							vTempPatientVisitExtractError.AppointmentReminderWillingness


				FROM            vTempPatientVisitExtractError INNER JOIN
										 ValidationError ON vTempPatientVisitExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
				");
					 
					 
					 
					 
				 migrationBuilder.Sql(@"ALTER view vTempIptExtractError as SELECT * FROM TempIptExtracts WHERE (CheckError = 1)");
				 migrationBuilder.Sql(@"
						ALTER VIEW vTempIptExtractErrorSummary
						AS
						SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,

					vTempIptExtractError.PatientPK,
					vTempIptExtractError.PatientID,
					vTempIptExtractError.FacilityId,
					vTempIptExtractError.SiteCode,
					vTempIptExtractError.DateExtracted,
					vTempIptExtractError.Emr,
					vTempIptExtractError.Project,
					vTempIptExtractError.CheckError,
					vTempIptExtractError.ErrorType,
					vTempIptExtractError.FacilityName,
					vTempIptExtractError.VisitID,
					vTempIptExtractError.VisitDate,
					vTempIptExtractError.OnTBDrugs,
					vTempIptExtractError.OnIPT,
					vTempIptExtractError.EverOnIPT,
					vTempIptExtractError.Cough,
					vTempIptExtractError.Fever,
					vTempIptExtractError.NoticeableWeightLoss,
					vTempIptExtractError.NightSweats,
					vTempIptExtractError.Lethargy,
					vTempIptExtractError.ICFActionTaken,
					vTempIptExtractError.TestResult,
					vTempIptExtractError.TBClinicalDiagnosis,
					vTempIptExtractError.ContactsInvited,
					vTempIptExtractError.EvaluatedForIPT,
					vTempIptExtractError.StartAntiTBs,
					vTempIptExtractError.TBRxStartDate,
					vTempIptExtractError.TBScreening,
					vTempIptExtractError.IPTClientWorkUp,
					vTempIptExtractError.StartIPT,
					vTempIptExtractError.IndicationForIPT,
					vTempIptExtractError.Date_Created,
					vTempIptExtractError.Date_Last_Modified,
					vTempIptExtractError.TPTInitiationDate	,
					vTempIptExtractError.IPTDiscontinuation	,
					vTempIptExtractError.DateOfDiscontinuation,	
					vTempIptExtractError.RecordUUID,
					vTempIptExtractError.Voided,
					vTempIptExtractError.Hepatoxicity,
					vTempIptExtractError.PeripheralNeuropathy,
					vTempIptExtractError.Rash,
					vTempIptExtractError.Adherence

						FROM            vTempIptExtractError INNER JOIN
												 ValidationError ON vTempIptExtractError.Id = ValidationError.RecordId INNER JOIN
												 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
