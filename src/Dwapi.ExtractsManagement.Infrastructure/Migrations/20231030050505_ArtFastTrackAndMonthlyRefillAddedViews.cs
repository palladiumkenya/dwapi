using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class ArtFastTrackAndMonthlyRefillAddedViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"alter table CancerScreeningExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempCancerScreeningExtracts convert to character set utf8 collate utf8_unicode_ci;");
                
                migrationBuilder.Sql(@"alter table ArtFastTrackExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempArtFastTrackExtracts convert to character set utf8 collate utf8_unicode_ci;");
                
                migrationBuilder.Sql(@"alter table PrepMonthlyRefillExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempPrepMonthlyRefillExtracts convert to character set utf8 collate utf8_unicode_ci;");

            }

            migrationBuilder.Sql(@"create view vTempCancerScreeningExtractError as SELECT * FROM TempCancerScreeningExtracts WHERE (CheckError = 1)");

            migrationBuilder.Sql(@"
				CREATE VIEW vTempCancerScreeningExtractErrorSummary
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
							vTempCancerScreeningExtractError.ScreeningMethod,
							vTempCancerScreeningExtractError.TreatmentToday,
							vTempCancerScreeningExtractError.ReferredOut,
							vTempCancerScreeningExtractError.NextAppointmentDate,
							vTempCancerScreeningExtractError.ScreeningType,
							vTempCancerScreeningExtractError.HPVScreeningResult,
							vTempCancerScreeningExtractError.TreatmentHPV,
							vTempCancerScreeningExtractError.VIAScreeningResult,
							vTempCancerScreeningExtractError.VIAVILIScreeningResult,
							vTempCancerScreeningExtractError.VIATreatmentOptions,
							vTempCancerScreeningExtractError.PAPSmearScreeningResult,
							vTempCancerScreeningExtractError.TreatmentPapSmear,
							vTempCancerScreeningExtractError.ReferalOrdered,
							vTempCancerScreeningExtractError.Colposcopy,
							vTempCancerScreeningExtractError.TreatmentColposcopy,
							vTempCancerScreeningExtractError.BiopsyCINIIandAbove,
							vTempCancerScreeningExtractError.BiopsyCINIIandBelow,
							vTempCancerScreeningExtractError.BiopsyNotAvailable,
							vTempCancerScreeningExtractError.CBE,
							vTempCancerScreeningExtractError.TreatmentCBE,
							vTempCancerScreeningExtractError.Ultrasound,
							vTempCancerScreeningExtractError.TreatmentUltraSound,
							vTempCancerScreeningExtractError.IfTissueDiagnosis,
							vTempCancerScreeningExtractError.DateTissueDiagnosis,
							vTempCancerScreeningExtractError.ReasonNotDone,
							vTempCancerScreeningExtractError.FollowUpDate,
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
            
            
            migrationBuilder.Sql(@"create view vTempArtFastTrackExtractError as SELECT * FROM TempArtFastTrackExtracts WHERE (CheckError = 1)");

            migrationBuilder.Sql(@"
				CREATE VIEW vTempArtFastTrackExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,

							vTempArtFastTrackExtractError.PatientPK,
							vTempArtFastTrackExtractError.SiteCode,
							vTempArtFastTrackExtractError.PatientID,
							vTempArtFastTrackExtractError.FacilityID,
							vTempArtFastTrackExtractError.Emr,
							vTempArtFastTrackExtractError.Project,
							vTempArtFastTrackExtractError.FacilityName,
							vTempArtFastTrackExtractError.ARTRefillModel ,
							vTempArtFastTrackExtractError.VisitDate ,
							vTempArtFastTrackExtractError.CTXDispensed ,
							vTempArtFastTrackExtractError.DapsoneDispensed ,
							vTempArtFastTrackExtractError.CondomsDistributed ,
							vTempArtFastTrackExtractError.OralContraceptivesDispensed ,
							vTempArtFastTrackExtractError.MissedDoses ,
							vTempArtFastTrackExtractError.Fatigue ,
							vTempArtFastTrackExtractError.Cough ,
							vTempArtFastTrackExtractError.Fever ,
							vTempArtFastTrackExtractError.Rash ,
							vTempArtFastTrackExtractError.NauseaOrVomiting,
							vTempArtFastTrackExtractError.GenitalSoreOrDischarge ,
							vTempArtFastTrackExtractError.Diarrhea ,
							vTempArtFastTrackExtractError.OtherSymptoms ,
							vTempArtFastTrackExtractError.PregnancyStatus ,
							vTempArtFastTrackExtractError.FPStatus ,
							vTempArtFastTrackExtractError.FPMethod ,
							vTempArtFastTrackExtractError.ReasonNotOnFP ,
							vTempArtFastTrackExtractError.ReferredToClinic ,
							vTempArtFastTrackExtractError.ReturnVisitDate,
							vTempArtFastTrackExtractError.RecordUUID,
							vTempArtFastTrackExtractError.Voided,
							vTempArtFastTrackExtractError.Date_Created ,
							vTempArtFastTrackExtractError.Date_Last_Modified 						

				FROM            vTempArtFastTrackExtractError INNER JOIN
										 ValidationError ON vTempArtFastTrackExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
            
            
            migrationBuilder.Sql(@"create view vTempPrepMonthlyRefillExtractError as SELECT * FROM TempPrepMonthlyRefillExtracts WHERE (CheckError = 1)");

            migrationBuilder.Sql(@"
				CREATE VIEW vTempPrepMonthlyRefillExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,

							vTempPrepMonthlyRefillExtractError.PatientPK,
							vTempPrepMonthlyRefillExtractError.SiteCode,
							vTempPrepMonthlyRefillExtractError.PatientID,
							vTempPrepMonthlyRefillExtractError.FacilityID,
							vTempPrepMonthlyRefillExtractError.Emr,
							vTempPrepMonthlyRefillExtractError.Project,
							vTempPrepMonthlyRefillExtractError.FacilityName,
							vTempPrepMonthlyRefillExtractError.PrepNumber,
							vTempPrepMonthlyRefillExtractError.VisitDate,
							vTempPrepMonthlyRefillExtractError.BehaviorRiskAssessment,
							vTempPrepMonthlyRefillExtractError.SexPartnerHIVStatus,
							vTempPrepMonthlyRefillExtractError.SymptomsAcuteHIV,
							vTempPrepMonthlyRefillExtractError.AdherenceCounsellingDone,
							vTempPrepMonthlyRefillExtractError.ContraIndicationForPrEP,
							vTempPrepMonthlyRefillExtractError.PrescribedPrepToday,
							vTempPrepMonthlyRefillExtractError.RegimenPrescribed,
							vTempPrepMonthlyRefillExtractError.NumberOfMonths,
							vTempPrepMonthlyRefillExtractError.CondomsIssued,
							vTempPrepMonthlyRefillExtractError.NumberOfCondomsIssued,
							vTempPrepMonthlyRefillExtractError.ClientGivenNextAppointment,
							vTempPrepMonthlyRefillExtractError.AppointmentDate,
							vTempPrepMonthlyRefillExtractError.ReasonForFailureToGiveAppointment,
							vTempPrepMonthlyRefillExtractError.DateOfLastPrepDose        ,
							vTempPrepMonthlyRefillExtractError.RecordUUID,
							vTempPrepMonthlyRefillExtractError.Voided,
							vTempPrepMonthlyRefillExtractError.Date_Created,
							vTempPrepMonthlyRefillExtractError.Date_Last_Modified
						

				FROM            vTempPrepMonthlyRefillExtractError INNER JOIN
										 ValidationError ON vTempPrepMonthlyRefillExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
            
            

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
