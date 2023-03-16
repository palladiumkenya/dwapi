using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class PMTCTImmunizationsAndMnchUpdatesViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

             migrationBuilder.Sql(@"alter view vTempMnchArtExtractError as SELECT * FROM TempMnchArtExtracts WHERE (CheckError = 1)");

	        migrationBuilder.Sql(@"
					alter view vTempMnchArtExtractErrorSummary
					AS
					SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,
				vTempMnchArtExtractError.Date_Created,
				vTempMnchArtExtractError.Date_Last_Modified,
				vTempMnchArtExtractError.DateExtracted,
				vTempMnchArtExtractError.Emr,
				vTempMnchArtExtractError.ErrorType,
				vTempMnchArtExtractError.FacilityId,
				vTempMnchArtExtractError.FacilityName,
				vTempMnchArtExtractError.LastARTDate,
				vTempMnchArtExtractError.LastRegimen,
				vTempMnchArtExtractError.LastRegimenLine,
				vTempMnchArtExtractError.PatientHeiID,
				vTempMnchArtExtractError.PatientID,
				vTempMnchArtExtractError.PatientMnchID,
				vTempMnchArtExtractError.PatientPK,
				vTempMnchArtExtractError.Pkv,
				vTempMnchArtExtractError.Project,
				vTempMnchArtExtractError.RegistrationAtCCC,
				vTempMnchArtExtractError.SiteCode,
				vTempMnchArtExtractError.StartARTDate,
				vTempMnchArtExtractError.StartRegimen,
				vTempMnchArtExtractError.StartRegimenLine,
				vTempMnchArtExtractError.StatusAtCCC,
				vTempMnchArtExtractError.FacilityReceivingARTCare
				                        
				FROM            vTempMnchArtExtractError INNER JOIN
										 ValidationError ON vTempMnchArtExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
			                ");
	        
	        
	        
	        
	        migrationBuilder.Sql(@"alter view vTempMatVisitExtractError as SELECT * FROM TempMatVisitExtracts WHERE (CheckError = 1)");

	        migrationBuilder.Sql(@"
					alter view vTempMatVisitExtractErrorSummary
					AS
					SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,

				vTempMatVisitExtractError.AdmissionNumber,
				vTempMatVisitExtractError.ANCVisits,
				vTempMatVisitExtractError.ApgarScore1,
				vTempMatVisitExtractError.ApgarScore10,
				vTempMatVisitExtractError.ApgarScore5,
				vTempMatVisitExtractError.BabyBirthNumber,
				vTempMatVisitExtractError.BabyGivenProphylaxis,
				vTempMatVisitExtractError.BirthOutcome,
				vTempMatVisitExtractError.BirthWeight,
				vTempMatVisitExtractError.BirthWithDeformity,
				vTempMatVisitExtractError.BloodLoss,
				vTempMatVisitExtractError.BloodLossVisual,
				vTempMatVisitExtractError.ChlorhexidineApplied,
				vTempMatVisitExtractError.ClinicalNotes,
				vTempMatVisitExtractError.ConditonAfterDelivery,
				vTempMatVisitExtractError.CounselledOn,
				vTempMatVisitExtractError.Date_Created,
				vTempMatVisitExtractError.Date_Last_Modified,
				vTempMatVisitExtractError.DateExtracted,
				vTempMatVisitExtractError.DateOfDelivery,
				vTempMatVisitExtractError.DeliveryComplications,
				vTempMatVisitExtractError.DurationOfDelivery,
				vTempMatVisitExtractError.Emr,
				vTempMatVisitExtractError.ErrorType,
				vTempMatVisitExtractError.FacilityId,
				vTempMatVisitExtractError.FacilityName,
				vTempMatVisitExtractError.GestationAtBirth,
				vTempMatVisitExtractError.HIV1Results,
				vTempMatVisitExtractError.HIV2Results,
				vTempMatVisitExtractError.HIVStatusLastANC,
				vTempMatVisitExtractError.HIVTest1,
				vTempMatVisitExtractError.HIVTest2,
				vTempMatVisitExtractError.HIVTestFinalResult,
				vTempMatVisitExtractError.HIVTestingDone,
				vTempMatVisitExtractError.InitiatedBF,
				vTempMatVisitExtractError.KangarooCare,
				vTempMatVisitExtractError.MaternalDeath,
				vTempMatVisitExtractError.ModeOfDelivery,
				vTempMatVisitExtractError.MotherDischargeDate,
				vTempMatVisitExtractError.MotherGivenCTX,
				vTempMatVisitExtractError.NoBabiesDelivered,
				vTempMatVisitExtractError.OnARTANC,
				vTempMatVisitExtractError.PartnerHIVStatusMAT,
				vTempMatVisitExtractError.PartnerHIVTestingMAT,
				vTempMatVisitExtractError.PatientID,
				vTempMatVisitExtractError.PatientMnchID,
				vTempMatVisitExtractError.PatientPK,
				vTempMatVisitExtractError.PlacentaComplete,
				vTempMatVisitExtractError.Project,
				vTempMatVisitExtractError.ReferredFrom,
				vTempMatVisitExtractError.ReferredTo,
				vTempMatVisitExtractError.SexBaby,
				vTempMatVisitExtractError.SiteCode,
				vTempMatVisitExtractError.StatusBabyDischarge,
				vTempMatVisitExtractError.SyphilisTestResults,
				vTempMatVisitExtractError.TetracyclineGiven,
				vTempMatVisitExtractError.UterotonicGiven,
				vTempMatVisitExtractError.VaginalExamination,
				vTempMatVisitExtractError.VisitDate,
				vTempMatVisitExtractError.VisitID,
				vTempMatVisitExtractError.VitaminKGiven,
				vTempMatVisitExtractError.LMP,
				vTempMatVisitExtractError.EDD,
				vTempMatVisitExtractError.MaternalDeathAudited,
				vTempMatVisitExtractError.OnARTMat,
				vTempMatVisitExtractError.ReferralReason
				                        
				FROM            vTempMatVisitExtractError INNER JOIN
										 ValidationError ON vTempMatVisitExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
			                ");
	        
	        
	        
	        migrationBuilder.Sql(@"alter view vTempPncVisitExtractError as SELECT * FROM TempPncVisitExtracts WHERE (CheckError = 1)");

	        migrationBuilder.Sql(@"
					alter view vTempPncVisitExtractErrorSummary
					AS
				    SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,

				vTempPncVisitExtractError.BabyConditon,
				vTempPncVisitExtractError.BabyFeeding,
				vTempPncVisitExtractError.BP,
				vTempPncVisitExtractError.Breast,
				vTempPncVisitExtractError.BreastExam,
				vTempPncVisitExtractError.CACxScreenMethod,
				vTempPncVisitExtractError.CACxScreenResults,
				vTempPncVisitExtractError.ClientScreenedCACx,
				vTempPncVisitExtractError.ClinicalNotes,
				vTempPncVisitExtractError.CounselledOnFP,
				vTempPncVisitExtractError.CoupleCounselled,
				vTempPncVisitExtractError.CSScar,
				vTempPncVisitExtractError.Date_Created,
				vTempPncVisitExtractError.Date_Last_Modified,
				vTempPncVisitExtractError.DateExtracted,
				vTempPncVisitExtractError.DeliveryDate,
				vTempPncVisitExtractError.DeliveryOutcome,
				vTempPncVisitExtractError.Emr,
				vTempPncVisitExtractError.Episiotomy,
				vTempPncVisitExtractError.ErrorType,
				vTempPncVisitExtractError.FacilityId,
				vTempPncVisitExtractError.Fistula,
				vTempPncVisitExtractError.GeneralCondition,
				vTempPncVisitExtractError.HaematinicsGiven,
				vTempPncVisitExtractError.HasPallor,
				vTempPncVisitExtractError.Height,
				vTempPncVisitExtractError.HIVTest1,
				vTempPncVisitExtractError.HIVTest1Result,
				vTempPncVisitExtractError.HIVTest2,
				vTempPncVisitExtractError.HIVTest2Result,
				vTempPncVisitExtractError.HIVTestFinalResult,
				vTempPncVisitExtractError.HIVTestingDone,
				vTempPncVisitExtractError.Immunization,
				vTempPncVisitExtractError.InfantFeeding,
				vTempPncVisitExtractError.InfantProphylaxisGiven,
				vTempPncVisitExtractError.Lochia,
				vTempPncVisitExtractError.MaternalComplications,
				vTempPncVisitExtractError.ModeOfDelivery,
				vTempPncVisitExtractError.MotherProphylaxisGiven,
				vTempPncVisitExtractError.MUAC,
				vTempPncVisitExtractError.NextAppointmentPNC,
				vTempPncVisitExtractError.OxygenSaturation,
				vTempPncVisitExtractError.Pallor,
				vTempPncVisitExtractError.PartnerHIVResultPNC,
				vTempPncVisitExtractError.PartnerHIVTestingPNC,
				vTempPncVisitExtractError.PatientID,
				vTempPncVisitExtractError.PatientMnchID,
				vTempPncVisitExtractError.PatientPK,
				vTempPncVisitExtractError.PlaceOfDelivery,
				vTempPncVisitExtractError.PNCRegisterNumber,
				vTempPncVisitExtractError.PNCVisitNo,
				vTempPncVisitExtractError.PPH,
				vTempPncVisitExtractError.PreventiveServices,
				vTempPncVisitExtractError.PriorHIVStatus,
				vTempPncVisitExtractError.Project,
				vTempPncVisitExtractError.PulseRate,
				vTempPncVisitExtractError.ReceivedFP,
				vTempPncVisitExtractError.ReferredFrom,
				vTempPncVisitExtractError.ReferredTo,
				vTempPncVisitExtractError.RespiratoryRate,
				vTempPncVisitExtractError.SiteCode,
				vTempPncVisitExtractError.TBScreening,
				vTempPncVisitExtractError.Temp,
				vTempPncVisitExtractError.UmbilicalCord,
				vTempPncVisitExtractError.UterusInvolution,
				vTempPncVisitExtractError.VisitDate,
				vTempPncVisitExtractError.VisitID,
				vTempPncVisitExtractError.Weight,
				vTempPncVisitExtractError.VisitTimingMother,
				vTempPncVisitExtractError.VisitTimingBaby,
				vTempPncVisitExtractError.MotherCameForHIVTest,
				vTempPncVisitExtractError.InfactCameForHAART,
				vTempPncVisitExtractError.MotherGivenHAART
				                        
				FROM            vTempPncVisitExtractError INNER JOIN
										 ValidationError ON vTempPncVisitExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
			                ");
	        
	        
             
             migrationBuilder.Sql(@"alter view vTempCwcVisitExtractError as SELECT * FROM TempCwcVisitExtracts WHERE (CheckError = 1)");

             migrationBuilder.Sql(@"
						alter view vTempCwcVisitExtractErrorSummary
						AS
						SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,
					vTempCwcVisitExtractError.DangerSigns,
					vTempCwcVisitExtractError.Date_Created,
					vTempCwcVisitExtractError.Date_Last_Modified,
					vTempCwcVisitExtractError.DateExtracted,
					vTempCwcVisitExtractError.Dewormed,
					vTempCwcVisitExtractError.Disability,
					vTempCwcVisitExtractError.Emr,
					vTempCwcVisitExtractError.ErrorType,
					vTempCwcVisitExtractError.FacilityId,
					vTempCwcVisitExtractError.FacilityName,
					vTempCwcVisitExtractError.FollowUP,
					vTempCwcVisitExtractError.Height,
					vTempCwcVisitExtractError.Immunization,
					vTempCwcVisitExtractError.InfantFeeding,
					vTempCwcVisitExtractError.MedicationGiven,
					vTempCwcVisitExtractError.Milestones,
					vTempCwcVisitExtractError.MNPsSupplementation,
					vTempCwcVisitExtractError.MUAC,
					vTempCwcVisitExtractError.NextAppointment,
					vTempCwcVisitExtractError.OxygenSaturation,
					vTempCwcVisitExtractError.PatientID,
					vTempCwcVisitExtractError.PatientMnchID,
					vTempCwcVisitExtractError.PatientPK,
					vTempCwcVisitExtractError.Project,
					vTempCwcVisitExtractError.PulseRate,
					vTempCwcVisitExtractError.ReceivedMosquitoNet,
					vTempCwcVisitExtractError.ReferralReasons,
					vTempCwcVisitExtractError.ReferredFrom,
					vTempCwcVisitExtractError.ReferredTo,
					vTempCwcVisitExtractError.RespiratoryRate,
					vTempCwcVisitExtractError.SiteCode,
					vTempCwcVisitExtractError.Stunted,
					vTempCwcVisitExtractError.TBAssessment,
					vTempCwcVisitExtractError.Temp,
					vTempCwcVisitExtractError.VisitDate,
					vTempCwcVisitExtractError.VisitID,
					vTempCwcVisitExtractError.VitaminA,
					vTempCwcVisitExtractError.Weight,
					vTempCwcVisitExtractError.WeightCategory,
					vTempCwcVisitExtractError.RevisitThisYear,
					vTempCwcVisitExtractError.Refferred,
					vTempCwcVisitExtractError.HeightLength
					                        
					FROM            vTempCwcVisitExtractError INNER JOIN
											 ValidationError ON vTempCwcVisitExtractError.Id = ValidationError.RecordId INNER JOIN
											 Validator ON ValidationError.ValidatorId = Validator.Id
				                ");
             
             
             
             migrationBuilder.Sql(@"alter table MnchImmunizationExtracts convert to character set utf8 collate utf8_unicode_ci;");
             migrationBuilder.Sql(@"alter table TempMnchImmunizationExtracts convert to character set utf8 collate utf8_unicode_ci;");
             migrationBuilder.Sql(@"create view vTempMnchImmunizationExtractError as SELECT * FROM TempMnchImmunizationExtracts WHERE (CheckError = 1)");

             migrationBuilder.Sql(@"
				CREATE VIEW vTempMnchImmunizationExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,	
						vTempMnchImmunizationExtractError.DateExtracted,
						vTempMnchImmunizationExtractError.Emr,
						vTempMnchImmunizationExtractError.ErrorType,
						vTempMnchImmunizationExtractError.FacilityId,
						vTempMnchImmunizationExtractError.FacilityName,						
						vTempMnchImmunizationExtractError.BCG,
						vTempMnchImmunizationExtractError.OPVatBirth,
						vTempMnchImmunizationExtractError.OPV1,
						vTempMnchImmunizationExtractError.OPV2,
						vTempMnchImmunizationExtractError.OPV3,
						vTempMnchImmunizationExtractError.IPV,
						vTempMnchImmunizationExtractError.DPTHepBHIB1,
						vTempMnchImmunizationExtractError.DPTHepBHIB2,
						vTempMnchImmunizationExtractError.DPTHepBHIB3,
						vTempMnchImmunizationExtractError.PCV101,
						vTempMnchImmunizationExtractError.PCV102,
						vTempMnchImmunizationExtractError.PCV103,
						vTempMnchImmunizationExtractError.ROTA1,
						vTempMnchImmunizationExtractError.MeaslesReubella1,
						vTempMnchImmunizationExtractError.YellowFever,
						vTempMnchImmunizationExtractError.MeaslesReubella2,
						vTempMnchImmunizationExtractError.MeaslesAt6Months,
						vTempMnchImmunizationExtractError.ROTA2,
						vTempMnchImmunizationExtractError.DateofNextVisit,
						vTempMnchImmunizationExtractError.BCGScarChecked,
						vTempMnchImmunizationExtractError.DateChecked,
						vTempMnchImmunizationExtractError.DateBCGrepeated,
						vTempMnchImmunizationExtractError.VitaminAAt6Months,
						vTempMnchImmunizationExtractError.VitaminAAt1Yr,
						vTempMnchImmunizationExtractError.VitaminAAt18Months,
						vTempMnchImmunizationExtractError.VitaminAAt2Years,
						vTempMnchImmunizationExtractError.VitaminAAt2To5Years,
						vTempMnchImmunizationExtractError.FullyImmunizedChild
				FROM            vTempMnchImmunizationExtractError INNER JOIN
										 ValidationError ON vTempMnchImmunizationExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
             
             
             
             
             
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
