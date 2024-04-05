using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class UpdatedCancerScreeningViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
 migrationBuilder.Sql(@"alter view vTempCancerScreeningExtractError as SELECT * FROM TempCancerScreeningExtracts WHERE (CheckError = 1)");

            migrationBuilder.Sql(@"
				alter VIEW vTempCancerScreeningExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,

							vTempCancerScreeningExtractError.PatientPK,
							vTempCancerScreeningExtractError.SiteCode,
							vTempCancerScreeningExtractError.PatientID,
							vTempCancerScreeningExtractError.FacilityID,
							vTempCancerScreeningExtractError.Emr,
							vTempCancerScreeningExtractError.Project,
							vTempCancerScreeningExtractError.FacilityName,
							vTempCancerScreeningExtractError.VisitType,
							vTempCancerScreeningExtractError.VisitID,
							vTempCancerScreeningExtractError.VisitDate,
							vTempCancerScreeningExtractError.SmokesCigarette,
							vTempCancerScreeningExtractError.NumberYearsSmoked,
							vTempCancerScreeningExtractError.NumberCigarettesPerDay,
							vTempCancerScreeningExtractError.OtherFormTobacco,
							vTempCancerScreeningExtractError.TakesAlcohol,
							vTempCancerScreeningExtractError.HIVStatus,
							vTempCancerScreeningExtractError.FamilyHistoryOfCa,
							vTempCancerScreeningExtractError.PreviousCaTreatment,
							vTempCancerScreeningExtractError.SymptomsCa,
							vTempCancerScreeningExtractError.CancerType,
							vTempCancerScreeningExtractError.FecalOccultBloodTest,
							vTempCancerScreeningExtractError.TreatmentOccultBlood,
							vTempCancerScreeningExtractError.Colonoscopy,
							vTempCancerScreeningExtractError.TreatmentColonoscopy,
							vTempCancerScreeningExtractError.EUA,
							vTempCancerScreeningExtractError.TreatmentRetinoblastoma    ,
							vTempCancerScreeningExtractError.RetinoblastomaGene ,
							vTempCancerScreeningExtractError.TreatmentEUA,
							vTempCancerScreeningExtractError.DRE,
							vTempCancerScreeningExtractError.TreatmentDRE,
							vTempCancerScreeningExtractError.PSA,
							vTempCancerScreeningExtractError.TreatmentPSA,
							vTempCancerScreeningExtractError.VisualExamination,
							vTempCancerScreeningExtractError.TreatmentVE,
							vTempCancerScreeningExtractError.Cytology,
							vTempCancerScreeningExtractError.TreatmentCytology,
							vTempCancerScreeningExtractError.Imaging,
							vTempCancerScreeningExtractError.TreatmentImaging,
							vTempCancerScreeningExtractError.Biopsy,
							vTempCancerScreeningExtractError.TreatmentBiopsy,
							vTempCancerScreeningExtractError.PostTreatmentComplicationCause,
							vTempCancerScreeningExtractError.OtherPostTreatmentComplication,
							vTempCancerScreeningExtractError.ReferralReason,
							vTempCancerScreeningExtractError.NextAppointmentDate,
							vTempCancerScreeningExtractError.ScreeningType,
							vTempCancerScreeningExtractError.HPVScreeningResult,
							vTempCancerScreeningExtractError.TreatmentHPV,
							vTempCancerScreeningExtractError.VIAVILIScreeningResult,
							vTempCancerScreeningExtractError.PAPSmearScreeningResult,
							vTempCancerScreeningExtractError.TreatmentPapSmear,
							vTempCancerScreeningExtractError.ReferalOrdered,
							vTempCancerScreeningExtractError.Colposcopy,
							vTempCancerScreeningExtractError.TreatmentColposcopy,
							vTempCancerScreeningExtractError.CBE,
							vTempCancerScreeningExtractError.TreatmentCBE,
							vTempCancerScreeningExtractError.Ultrasound,
							vTempCancerScreeningExtractError.TreatmentUltraSound,
							vTempCancerScreeningExtractError.IfTissueDiagnosis,
							vTempCancerScreeningExtractError.DateTissueDiagnosis,
							vTempCancerScreeningExtractError.ReasonNotDone,
							vTempCancerScreeningExtractError.Referred,
							vTempCancerScreeningExtractError.ReasonForReferral,
							vTempCancerScreeningExtractError.RecordUUID,
							vTempCancerScreeningExtractError.Voided,
							vTempCancerScreeningExtractError.Date_Created,
							vTempCancerScreeningExtractError.Date_Last_Modified			

				FROM            vTempCancerScreeningExtractError INNER JOIN
										 ValidationError ON vTempCancerScreeningExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
            
            

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
