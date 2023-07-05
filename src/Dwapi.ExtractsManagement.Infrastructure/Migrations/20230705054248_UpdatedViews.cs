using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class UpdatedViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"alter view vTempMotherBabyPairExtractError as SELECT * FROM TempMotherBabyPairExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"
				alter VIEW vTempMotherBabyPairExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,

			vTempMotherBabyPairExtractError.BabyPatientMncHeiID,
			vTempMotherBabyPairExtractError.BabyPatientPK,
			vTempMotherBabyPairExtractError.Date_Created,
			vTempMotherBabyPairExtractError.Date_Last_Modified,
			vTempMotherBabyPairExtractError.DateExtracted,
			vTempMotherBabyPairExtractError.Emr,
			vTempMotherBabyPairExtractError.ErrorType,
			vTempMotherBabyPairExtractError.FacilityId,
			vTempMotherBabyPairExtractError.MotherPatientMncHeiID,
			vTempMotherBabyPairExtractError.MotherPatientPK,
			vTempMotherBabyPairExtractError.PatientID,
			vTempMotherBabyPairExtractError.PatientIDCCC,
			vTempMotherBabyPairExtractError.PatientPK,
			vTempMotherBabyPairExtractError.Project,
			vTempMotherBabyPairExtractError.SiteCode,
			vTempMotherBabyPairExtractError.FacilityName
			                    
				FROM            vTempMotherBabyPairExtractError INNER JOIN
										 ValidationError ON vTempMotherBabyPairExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
			                ");
            
            
            migrationBuilder.Sql(@"alter view vTempPncVisitExtractError as SELECT * FROM TempPncVisitExtracts WHERE (CheckError = 1)");
			migrationBuilder.Sql(@"
				alter VIEW vTempPncVisitExtractErrorSummary
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
			vTempPncVisitExtractError.FacilityName,
			vTempPncVisitExtractError.InfactCameForHAART,
			vTempPncVisitExtractError.MotherCameForHIVTest,
			vTempPncVisitExtractError.MotherGivenHAART,
			vTempPncVisitExtractError.VisitTimingBaby,
			vTempPncVisitExtractError.VisitTimingMother


			                        
				FROM            vTempPncVisitExtractError INNER JOIN
										 ValidationError ON vTempPncVisitExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
			                ");
			
			
			migrationBuilder.Sql(@"alter view vTempCwcEnrolmentExtractError as SELECT * FROM TempCwcEnrolmentExtracts WHERE (CheckError = 1)");
			migrationBuilder.Sql(@"
				alter VIEW vTempCwcEnrolmentExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,
			vTempCwcEnrolmentExtractError.ARTMother,
			vTempCwcEnrolmentExtractError.ARTRegimenMother,
			vTempCwcEnrolmentExtractError.ARTStartDateMother,
			vTempCwcEnrolmentExtractError.BirthLength,
			vTempCwcEnrolmentExtractError.BirthOrder,
			vTempCwcEnrolmentExtractError.BirthType,
			vTempCwcEnrolmentExtractError.BirthWeight,
			vTempCwcEnrolmentExtractError.BreastFeeding,
			vTempCwcEnrolmentExtractError.Date_Created,
			vTempCwcEnrolmentExtractError.Date_Last_Modified,
			vTempCwcEnrolmentExtractError.DateExtracted,
			vTempCwcEnrolmentExtractError.Emr,
			vTempCwcEnrolmentExtractError.ErrorType,
			vTempCwcEnrolmentExtractError.FacilityId,
			vTempCwcEnrolmentExtractError.Gestation,
			vTempCwcEnrolmentExtractError.HEI,
			vTempCwcEnrolmentExtractError.HEIDate,
			vTempCwcEnrolmentExtractError.HEIID,
			vTempCwcEnrolmentExtractError.ModeOfDelivery,
			vTempCwcEnrolmentExtractError.MotherAlive,
			vTempCwcEnrolmentExtractError.MothersCCCNo,
			vTempCwcEnrolmentExtractError.MothersPkv,
			vTempCwcEnrolmentExtractError.NVP,
			vTempCwcEnrolmentExtractError.PatientID,
			vTempCwcEnrolmentExtractError.PatientIDCWC,
			vTempCwcEnrolmentExtractError.PatientPK,
			vTempCwcEnrolmentExtractError.Pkv,
			vTempCwcEnrolmentExtractError.PlaceOfDelivery,
			vTempCwcEnrolmentExtractError.Project,
			vTempCwcEnrolmentExtractError.ReferredFrom,
			vTempCwcEnrolmentExtractError.RegistrationAtCWC,
			vTempCwcEnrolmentExtractError.RegistrationAtHEI,
			vTempCwcEnrolmentExtractError.SiteCode,
			vTempCwcEnrolmentExtractError.SpecialCare,
			vTempCwcEnrolmentExtractError.SpecialNeeds,
			vTempCwcEnrolmentExtractError.TransferIn,
			vTempCwcEnrolmentExtractError.TransferInDate,
			vTempCwcEnrolmentExtractError.TransferredFrom,
			vTempCwcEnrolmentExtractError.VisitID,
			vTempCwcEnrolmentExtractError.FacilityName


			                        
				FROM            vTempCwcEnrolmentExtractError INNER JOIN
										 ValidationError ON vTempCwcEnrolmentExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
			                ");


				migrationBuilder.Sql(@"alter view vTempHeiExtractError as SELECT * FROM TempHeiExtracts WHERE (CheckError = 1)");
				migrationBuilder.Sql(@"
					ALTER VIEW vTempHeiExtractErrorSummary
					AS
					SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,
				vTempHeiExtractError.BasellineVL,
				vTempHeiExtractError.BasellineVLDate,
				vTempHeiExtractError.ConfirmatoryPCR,
				vTempHeiExtractError.ConfirmatoryPCRDate,
				vTempHeiExtractError.Date_Created,
				vTempHeiExtractError.Date_Last_Modified,
				vTempHeiExtractError.DateExtracted,
				vTempHeiExtractError.DNAPCR1,
				vTempHeiExtractError.DNAPCR1Date,
				vTempHeiExtractError.DNAPCR2,
				vTempHeiExtractError.DNAPCR2Date,
				vTempHeiExtractError.DNAPCR3,
				vTempHeiExtractError.DNAPCR3Date,
				vTempHeiExtractError.Emr,
				vTempHeiExtractError.ErrorType,
				vTempHeiExtractError.FacilityId,
				vTempHeiExtractError.FacilityName,
				vTempHeiExtractError.FinalyAntibody,
				vTempHeiExtractError.FinalyAntibodyDate,
				vTempHeiExtractError.HEIExitCritearia,
				vTempHeiExtractError.HEIExitDate,
				vTempHeiExtractError.HEIHIVStatus,
				vTempHeiExtractError.PatientID,
				vTempHeiExtractError.PatientMnchID,
				vTempHeiExtractError.PatientPK,
				vTempHeiExtractError.Project,
				vTempHeiExtractError.SiteCode,
				vTempHeiExtractError.PatientHeiId

				                        
					FROM            vTempHeiExtractError INNER JOIN
											 ValidationError ON vTempHeiExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

				migrationBuilder.Sql(@"alter view vTempCwcVisitExtractError as SELECT * FROM TempCwcVisitExtracts WHERE (CheckError = 1)");
				 migrationBuilder.Sql(@"
					alter VIEW vTempCwcVisitExtractErrorSummary
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
				vTempCwcVisitExtractError.HeightLength,
				vTempCwcVisitExtractError.Refferred,
				vTempCwcVisitExtractError.RevisitThisYear,
				vTempCwcVisitExtractError.ZScore,
				vTempCwcVisitExtractError.ZScoreAbsolute

				                        
					FROM            vTempCwcVisitExtractError INNER JOIN
											 ValidationError ON vTempCwcVisitExtractError.Id = ValidationError.RecordId INNER JOIN
											 Validator ON ValidationError.ValidatorId = Validator.Id
				                ");
				 
				 
				 
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
							vTempPatientVisitExtractError.StabilityAssessment


				FROM            vTempPatientVisitExtractError INNER JOIN
										 ValidationError ON vTempPatientVisitExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
				 
				 
				 migrationBuilder.Sql(@"alter view vTempPatientExtractError as SELECT * FROM TempPatientExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"

						ALTER VIEW vTempPatientExtractErrorSummary
						AS
						SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, ValidationError.RecordId,

				        vTempPatientExtractError.PatientID,
					    vTempPatientExtractError.FacilityName, 
				        vTempPatientExtractError.Gender, 
				        vTempPatientExtractError.DOB, 
				        vTempPatientExtractError.RegistrationDate, 
				        vTempPatientExtractError.RegistrationAtCCC, 
				        vTempPatientExtractError.RegistrationATPMTCT, 
				        vTempPatientExtractError.RegistrationAtTBClinic, 
				        vTempPatientExtractError.PatientSource, 
				        vTempPatientExtractError.Region, 
				        vTempPatientExtractError.District, 
				        vTempPatientExtractError.Village, 
				        vTempPatientExtractError.ContactRelation, 
				        vTempPatientExtractError.LastVisit, 
				        vTempPatientExtractError.MaritalStatus, 
				        vTempPatientExtractError.EducationLevel, 
				        vTempPatientExtractError.DateConfirmedHIVPositive, 
				        vTempPatientExtractError.PreviousARTExposure,
				        vTempPatientExtractError.StatusAtCCC,
				        vTempPatientExtractError.StatusAtPMTCT,
				        vTempPatientExtractError.StatusAtTBClinic, 

				        vTempPatientExtractError.Orphan, 
				        vTempPatientExtractError.Inschool, 
				        vTempPatientExtractError.PatientType, 
				        vTempPatientExtractError.PopulationType, 
				        vTempPatientExtractError.KeyPopulationType, 
				        vTempPatientExtractError.PatientResidentCounty, 
				        vTempPatientExtractError.PatientResidentSubCounty, 
				        vTempPatientExtractError.PatientResidentLocation, 
				        vTempPatientExtractError.PatientResidentSubLocation, 
				        vTempPatientExtractError.PatientResidentWard ,
				        vTempPatientExtractError.PatientResidentVillage, 
				        vTempPatientExtractError.TransferInDate, 
				        vTempPatientExtractError.Date_Created,
				        vTempPatientExtractError.Date_Last_Modified, 
				        vTempPatientExtractError.Pkv,
				        vTempPatientExtractError.Occupation,
				        vTempPatientExtractError.NUPI ,
						vTempPatientExtractError.PatientPK,
						vTempPatientExtractError.SiteCode,
						vTempPatientExtractError.FacilityId

									 
					FROM            vTempPatientExtractError INNER JOIN
										 ValidationError ON vTempPatientExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
            
            migrationBuilder.Sql(@"alter view vTempMnchImmunizationExtractError as SELECT * FROM TempMnchImmunizationExtracts WHERE (CheckError = 1)");

             migrationBuilder.Sql(@"
				alter VIEW vTempMnchImmunizationExtractErrorSummary
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
						vTempMnchImmunizationExtractError.FullyImmunizedChild,
						vTempMnchImmunizationExtractError.Date_Created,
						vTempMnchImmunizationExtractError.Date_Last_Modified,
						vTempMnchImmunizationExtractError.PatientMnchID,
						vTempMnchImmunizationExtractError.PatientPK,
						vTempMnchImmunizationExtractError.SiteCode
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