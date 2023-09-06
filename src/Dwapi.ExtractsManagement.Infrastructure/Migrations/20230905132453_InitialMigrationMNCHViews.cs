using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class InitialMigrationMNCHViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
	        {
		        migrationBuilder.Sql(@"ALTER table PatientMnchExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        migrationBuilder.Sql(@"ALTER table TempPatientMnchExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        
		        migrationBuilder.Sql(@"ALTER table MnchEnrolmentExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        migrationBuilder.Sql(@"ALTER table TempMnchEnrolmentExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        
		        migrationBuilder.Sql(@"ALTER table MnchArtExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        migrationBuilder.Sql(@"ALTER table TempMnchArtExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        
		        migrationBuilder.Sql(@"ALTER table AncVisitExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        migrationBuilder.Sql(@"ALTER table TempAncVisitExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        
		        migrationBuilder.Sql(@"ALTER table MatVisitExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        migrationBuilder.Sql(@"ALTER table TempMatVisitExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        
		        migrationBuilder.Sql(@"ALTER table PncVisitExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        migrationBuilder.Sql(@"ALTER table TempPncVisitExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        
		        migrationBuilder.Sql(@"ALTER table MotherBabyPairExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        migrationBuilder.Sql(@"ALTER table TempMotherBabyPairExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        
		        migrationBuilder.Sql(@"ALTER table HeiExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        migrationBuilder.Sql(@"ALTER table TempHeiExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        
		        migrationBuilder.Sql(@"ALTER table MnchLabExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        migrationBuilder.Sql(@"ALTER table TempMnchLabExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        
		        migrationBuilder.Sql(@"ALTER table CwcEnrolmentExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        migrationBuilder.Sql(@"ALTER table TempCwcEnrolmentExtracts convert to character set utf8 collate utf8_unicode_ci;");

		        migrationBuilder.Sql(@"ALTER table CwcVisitExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        migrationBuilder.Sql(@"ALTER table TempCwcVisitExtracts convert to character set utf8 collate utf8_unicode_ci;");

		        migrationBuilder.Sql(@"ALTER table MnchImmunizationExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        migrationBuilder.Sql(@"ALTER table TempMnchImmunizationExtracts convert to character set utf8 collate utf8_unicode_ci;");
		        
	        }


            migrationBuilder.Sql(@"CREATE  view vTempPatientMnchExtractError as SELECT * FROM TempPatientMnchExtracts WHERE (CheckError = 1)");
	        migrationBuilder.Sql(@"CREATE  view vTempMnchEnrolmentExtractError as SELECT * FROM TempMnchEnrolmentExtracts WHERE (CheckError = 1)");
	        migrationBuilder.Sql(@"CREATE  view vTempMnchArtExtractError as SELECT * FROM TempMnchArtExtracts WHERE (CheckError = 1)");
	        migrationBuilder.Sql(@"CREATE  view vTempAncVisitExtractError as SELECT * FROM TempAncVisitExtracts WHERE (CheckError = 1)");
	        migrationBuilder.Sql(@"CREATE  view vTempMatVisitExtractError as SELECT * FROM TempMatVisitExtracts WHERE (CheckError = 1)");
	        migrationBuilder.Sql(@"CREATE  view vTempPncVisitExtractError as SELECT * FROM TempPncVisitExtracts WHERE (CheckError = 1)");
	        migrationBuilder.Sql(@"CREATE  view vTempMotherBabyPairExtractError as SELECT * FROM TempMotherBabyPairExtracts WHERE (CheckError = 1)");
	        migrationBuilder.Sql(@"CREATE  view vTempCwcEnrolmentExtractError as SELECT * FROM TempCwcEnrolmentExtracts WHERE (CheckError = 1)");
	        migrationBuilder.Sql(@"CREATE  view vTempCwcVisitExtractError as SELECT * FROM TempCwcVisitExtracts WHERE (CheckError = 1)");
	        migrationBuilder.Sql(@"CREATE  view vTempHeiExtractError as SELECT * FROM TempHeiExtracts WHERE (CheckError = 1)");
	        migrationBuilder.Sql(@"CREATE  view vTempMnchLabExtractError as SELECT * FROM TempMnchLabExtracts WHERE (CheckError = 1)");
	        migrationBuilder.Sql(@"CREATE  view vTempMnchImmunizationExtractError as SELECT * FROM TempMnchImmunizationExtracts WHERE (CheckError = 1)");

	        
	        migrationBuilder.Sql(@"
					CREATE  VIEW vTempPatientMnchExtractErrorSummary
					AS
					SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId, 
				vTempPatientMnchExtractError.Date_Created,
				vTempPatientMnchExtractError.Date_Last_Modified,
				vTempPatientMnchExtractError.DateExtracted,
				vTempPatientMnchExtractError.DOB,
				vTempPatientMnchExtractError.EducationLevel,
				vTempPatientMnchExtractError.Emr,
				vTempPatientMnchExtractError.ErrorType,
				vTempPatientMnchExtractError.FacilityId,
				vTempPatientMnchExtractError.FacilityName,
				vTempPatientMnchExtractError.FirstEnrollmentAtMnch,
				vTempPatientMnchExtractError.Gender,
				vTempPatientMnchExtractError.InSchool,
				vTempPatientMnchExtractError.MaritalStatus,
				vTempPatientMnchExtractError.Occupation,
				vTempPatientMnchExtractError.PatientHeiID,
				vTempPatientMnchExtractError.PatientID,
				vTempPatientMnchExtractError.PatientMnchID,
				vTempPatientMnchExtractError.PatientPK,
				vTempPatientMnchExtractError.PatientResidentCounty,
				vTempPatientMnchExtractError.PatientResidentSubCounty,
				vTempPatientMnchExtractError.PatientResidentWard,
				vTempPatientMnchExtractError.Pkv,
				vTempPatientMnchExtractError.Project,
				vTempPatientMnchExtractError.SiteCode,	
					vTempPatientMnchExtractError.RecordUUID,
					vTempPatientMnchExtractError.Voided
				             
					FROM            vTempPatientMnchExtractError INNER JOIN
											 ValidationError ON vTempPatientMnchExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
	        
	        
	        
	        migrationBuilder.Sql(@"
					CREATE  VIEW vTempMnchLabExtractErrorSummary
					AS
					SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,
				vTempMnchLabExtractError.Date_Created,
				vTempMnchLabExtractError.Date_Last_Modified,
				vTempMnchLabExtractError.DateExtracted,
				vTempMnchLabExtractError.Emr,
				vTempMnchLabExtractError.ErrorType,
				vTempMnchLabExtractError.FacilityId,
				vTempMnchLabExtractError.FacilityName,
				vTempMnchLabExtractError.LabReason,
				vTempMnchLabExtractError.OrderedbyDate,
				vTempMnchLabExtractError.PatientID,
				vTempMnchLabExtractError.PatientMNCH_ID,
				vTempMnchLabExtractError.PatientPK,
				vTempMnchLabExtractError.Project,
				vTempMnchLabExtractError.ReportedbyDate,
				vTempMnchLabExtractError.SatelliteName,
				vTempMnchLabExtractError.SiteCode,
				vTempMnchLabExtractError.TestName,
				vTempMnchLabExtractError.TestResult,
				vTempMnchLabExtractError.VisitID,	
					vTempMnchLabExtractError.RecordUUID,
					vTempMnchLabExtractError.Voided				                        
					FROM            vTempMnchLabExtractError INNER JOIN
											 ValidationError ON vTempMnchLabExtractError.Id = ValidationError.RecordId INNER JOIN
											 Validator ON ValidationError.ValidatorId = Validator.Id
				                ");

	        
	        migrationBuilder.Sql(@"
					CREATE  VIEW vTempMnchEnrolmentExtractErrorSummary
					AS
					SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,

				vTempMnchEnrolmentExtractError.BloodGroup,
				vTempMnchEnrolmentExtractError.Date_Created,
				vTempMnchEnrolmentExtractError.Date_Last_Modified,
				vTempMnchEnrolmentExtractError.DateExtracted,
				vTempMnchEnrolmentExtractError.EDDFromLMP,
				vTempMnchEnrolmentExtractError.Emr,
				vTempMnchEnrolmentExtractError.EnrollmentDateAtMnch,
				vTempMnchEnrolmentExtractError.ErrorType,
				vTempMnchEnrolmentExtractError.FacilityId,
				vTempMnchEnrolmentExtractError.FacilityName,
				vTempMnchEnrolmentExtractError.FirstVisitAnc,
				vTempMnchEnrolmentExtractError.Gravidae,
				vTempMnchEnrolmentExtractError.HIVStatusBeforeANC,
				vTempMnchEnrolmentExtractError.HIVTestDate,
				vTempMnchEnrolmentExtractError.LMP,
				vTempMnchEnrolmentExtractError.MnchNumber,
				vTempMnchEnrolmentExtractError.Parity,
				vTempMnchEnrolmentExtractError.PartnerHIVStatus,
				vTempMnchEnrolmentExtractError.PartnerHIVTestDate,
				vTempMnchEnrolmentExtractError.PatientID,
				vTempMnchEnrolmentExtractError.PatientMnchID,
				vTempMnchEnrolmentExtractError.PatientPK,
				vTempMnchEnrolmentExtractError.Project,
				vTempMnchEnrolmentExtractError.ServiceType,
				vTempMnchEnrolmentExtractError.SiteCode,
				vTempMnchEnrolmentExtractError.StatusAtMnch,	
					vTempMnchEnrolmentExtractError.RecordUUID,
					vTempMnchEnrolmentExtractError.Voided

					FROM            vTempMnchEnrolmentExtractError INNER JOIN
											 ValidationError ON vTempMnchEnrolmentExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

	        

	        migrationBuilder.Sql(@"
					CREATE  view vTempMnchArtExtractErrorSummary
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
				vTempMnchArtExtractError.FacilityReceivingARTCare,	
					vTempMnchArtExtractError.RecordUUID,
					vTempMnchArtExtractError.Voided
				                        
				FROM            vTempMnchArtExtractError INNER JOIN
										 ValidationError ON vTempMnchArtExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
			                ");
	        
	        
	        
	        migrationBuilder.Sql(@"
					CREATE  view vTempMatVisitExtractErrorSummary
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
				vTempMatVisitExtractError.ReferralReason,	
					vTempMatVisitExtractError.RecordUUID,
					vTempMatVisitExtractError.Voided
				                        
				FROM            vTempMatVisitExtractError INNER JOIN
										 ValidationError ON vTempMatVisitExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
			                ");
	        
	        
	        
            migrationBuilder.Sql(@"
							CREATE  view vTempAncVisitExtractErrorSummary
							AS
							SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,

						vTempAncVisitExtractError.ANCClinicNumber,
						vTempAncVisitExtractError.ANCVisitNo,
						vTempAncVisitExtractError.AntenatalExercises,
						vTempAncVisitExtractError.AZTBabyDispense,
						vTempAncVisitExtractError.BP,
						vTempAncVisitExtractError.BreastExam,
						vTempAncVisitExtractError.CACxScreen,
						vTempAncVisitExtractError.CACxScreenMethod,
						vTempAncVisitExtractError.ChronicIllness,
						vTempAncVisitExtractError.ClinicalNotes,
						vTempAncVisitExtractError.CounselledOn,
						vTempAncVisitExtractError.Date_Created,
						vTempAncVisitExtractError.Date_Last_Modified,
						vTempAncVisitExtractError.DateExtracted,
						vTempAncVisitExtractError.Deworming,
						vTempAncVisitExtractError.DiabetesTest,
						vTempAncVisitExtractError.Emr,
						vTempAncVisitExtractError.ErrorType,
						vTempAncVisitExtractError.FacilityId,
						vTempAncVisitExtractError.FacilityName,
						vTempAncVisitExtractError.FGM,
						vTempAncVisitExtractError.FGMComplications,
						vTempAncVisitExtractError.GestationWeeks,
						vTempAncVisitExtractError.Haemoglobin,
						vTempAncVisitExtractError.Height,
						vTempAncVisitExtractError.HIVStatusBeforeANC,
						vTempAncVisitExtractError.HIVTest1,
						vTempAncVisitExtractError.HIVTest1Result,
						vTempAncVisitExtractError.HIVTest2,
						vTempAncVisitExtractError.HIVTest2Result,
						vTempAncVisitExtractError.HIVTestFinalResult,
						vTempAncVisitExtractError.HIVTestingDone,
						vTempAncVisitExtractError.HIVTestType,
						vTempAncVisitExtractError.IronSupplementsGiven,
						vTempAncVisitExtractError.MalariaProphylaxis,
						vTempAncVisitExtractError.MotherGivenHAART,
						vTempAncVisitExtractError.MotherProphylaxisGiven,
						vTempAncVisitExtractError.MUAC,
						vTempAncVisitExtractError.NextAppointmentANC,
						vTempAncVisitExtractError.NVPBabyDispense,
						vTempAncVisitExtractError.OxygenSaturation,
						vTempAncVisitExtractError.PartnerHIVStatusANC,
						vTempAncVisitExtractError.PartnerHIVTestingANC,
						vTempAncVisitExtractError.PatientID,
						vTempAncVisitExtractError.PatientMnchID,
						vTempAncVisitExtractError.PatientPK,
						vTempAncVisitExtractError.PostParturmFP,
						vTempAncVisitExtractError.PreventiveServices,
						vTempAncVisitExtractError.Project,
						vTempAncVisitExtractError.PulseRate,
						vTempAncVisitExtractError.ReceivedMosquitoNet,
						vTempAncVisitExtractError.ReferralReasons,
						vTempAncVisitExtractError.ReferredFrom,
						vTempAncVisitExtractError.ReferredTo,
						vTempAncVisitExtractError.RespiratoryRate,
						vTempAncVisitExtractError.SiteCode,
						vTempAncVisitExtractError.SyphilisTestDone,
						vTempAncVisitExtractError.SyphilisTestResults,
						vTempAncVisitExtractError.SyphilisTestType,
						vTempAncVisitExtractError.SyphilisTreated,
						vTempAncVisitExtractError.SyphilisTreatment,
						vTempAncVisitExtractError.TBScreening,
						vTempAncVisitExtractError.Temp,
						vTempAncVisitExtractError.TetanusDose,
						vTempAncVisitExtractError.UrinalysisVariables,
						vTempAncVisitExtractError.VisitDate,
						vTempAncVisitExtractError.VisitID,
						vTempAncVisitExtractError.VLDate,
						vTempAncVisitExtractError.VLResult,
						vTempAncVisitExtractError.VLSampleTaken,
						vTempAncVisitExtractError.Weight,
						vTempAncVisitExtractError.WHOStaging,
						vTempAncVisitExtractError.HepatitisBScreening,
				        vTempAncVisitExtractError.TreatedHepatitisB,
				        vTempAncVisitExtractError.PresumptiveTreatmentGiven,
				        vTempAncVisitExtractError.PresumptiveTreatmentDose,
				        vTempAncVisitExtractError.MiminumPackageOfCareReceived,
				        vTempAncVisitExtractError.MiminumPackageOfCareServices,	
					vTempAncVisitExtractError.RecordUUID,
					vTempAncVisitExtractError.Voided
						                        
							FROM            vTempAncVisitExtractError INNER JOIN
							 ValidationError ON vTempAncVisitExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");


            
            migrationBuilder.Sql(@"
				CREATE  VIEW vTempMotherBabyPairExtractErrorSummary
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
			vTempMotherBabyPairExtractError.FacilityName,	
					vTempMotherBabyPairExtractError.RecordUUID,
					vTempMotherBabyPairExtractError.Voided
			                    
				FROM            vTempMotherBabyPairExtractError INNER JOIN
										 ValidationError ON vTempMotherBabyPairExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
			                ");
            
            
			migrationBuilder.Sql(@"
				CREATE  VIEW vTempPncVisitExtractErrorSummary
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
			vTempPncVisitExtractError.VisitTimingMother,	
					vTempPncVisitExtractError.RecordUUID,
					vTempPncVisitExtractError.Voided


			                        
				FROM            vTempPncVisitExtractError INNER JOIN
										 ValidationError ON vTempPncVisitExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
			                ");
			
			
			migrationBuilder.Sql(@"
				CREATE  VIEW vTempCwcEnrolmentExtractErrorSummary
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
			vTempCwcEnrolmentExtractError.FacilityName,	
					vTempCwcEnrolmentExtractError.RecordUUID,
					vTempCwcEnrolmentExtractError.Voided


			                        
				FROM            vTempCwcEnrolmentExtractError INNER JOIN
										 ValidationError ON vTempCwcEnrolmentExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
			                ");


	        migrationBuilder.Sql(@"
					CREATE  VIEW vTempHeiExtractErrorSummary
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
				vTempHeiExtractError.PatientHeiId,	
					vTempHeiExtractError.RecordUUID,
					vTempHeiExtractError.Voided

				                        
					FROM            vTempHeiExtractError INNER JOIN
											 ValidationError ON vTempHeiExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");


				
	        migrationBuilder.Sql(@"
					CREATE  VIEW vTempCwcVisitExtractErrorSummary
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
				vTempCwcVisitExtractError.ZScoreAbsolute,	
					vTempCwcVisitExtractError.RecordUUID,
					vTempCwcVisitExtractError.Voided

				                        
					FROM            vTempCwcVisitExtractError INNER JOIN
											 ValidationError ON vTempCwcVisitExtractError.Id = ValidationError.RecordId INNER JOIN
											 Validator ON ValidationError.ValidatorId = Validator.Id
				                ");
				 
				 

             migrationBuilder.Sql(@"
				CREATE  VIEW vTempMnchImmunizationExtractErrorSummary
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
						vTempMnchImmunizationExtractError.SiteCode,	
					vTempMnchImmunizationExtractError.RecordUUID,
					vTempMnchImmunizationExtractError.Voided
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
