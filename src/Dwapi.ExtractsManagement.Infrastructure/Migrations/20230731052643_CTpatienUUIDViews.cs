using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class CTpatienUUIDViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"alter view vTempContactListingExtractError as SELECT * FROM TempContactListingExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"

						ALTER VIEW vTempContactListingExtractErrorSummary
						AS
						SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, ValidationError.RecordId,

					vTempContactListingExtractError.PatientPK,
					vTempContactListingExtractError.PatientID,
					vTempContactListingExtractError.FacilityId,
					vTempContactListingExtractError.SiteCode,
					vTempContactListingExtractError.DateExtracted,
					vTempContactListingExtractError.Emr,
					vTempContactListingExtractError.Project,
					vTempContactListingExtractError.CheckError,
					vTempContactListingExtractError.ErrorType,
					vTempContactListingExtractError.FacilityName,
					vTempContactListingExtractError.PartnerPersonID,
					vTempContactListingExtractError.ContactAge,
					vTempContactListingExtractError.ContactSex,
					vTempContactListingExtractError.ContactMaritalStatus,
					vTempContactListingExtractError.RelationshipWithPatient,
					vTempContactListingExtractError.ScreenedForIpv,
					vTempContactListingExtractError.IpvScreening,
					vTempContactListingExtractError.IPVScreeningOutcome,
					vTempContactListingExtractError.CurrentlyLivingWithIndexClient,
					vTempContactListingExtractError.KnowledgeOfHivStatus,
					vTempContactListingExtractError.PnsApproach,
					vTempContactListingExtractError.Date_Created,
					vTempContactListingExtractError.Date_Last_Modified,
					vTempContactListingExtractError.ContactPatientPK,
					vTempContactListingExtractError.RecordUUID
					 
					FROM vTempContactListingExtractError INNER JOIN
							 ValidationError ON vTempContactListingExtractError.Id = ValidationError.RecordId INNER JOIN
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
							vTempPatientVisitExtractError.StabilityAssessment,
							vTempPatientVisitExtractError.RecordUUID
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
						vTempPatientExtractError.FacilityId,
						vTempPatientExtractError.RecordUUID
									 
					FROM            vTempPatientExtractError INNER JOIN
										 ValidationError ON vTempPatientExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
            
            

            migrationBuilder.Sql(@"ALTER view vTempPatientArtExtractError as SELECT * FROM TempPatientArtExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempPatientBaselinesExtractError as SELECT * FROM TempPatientBaselinesExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempPatientLaboratoryExtractError as SELECT * FROM TempPatientLaboratoryExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempPatientPharmacyExtractError as SELECT * FROM TempPatientPharmacyExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempPatientStatusExtractError as SELECT * FROM TempPatientStatusExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempPatientAdverseEventExtractError as SELECT * FROM TempPatientAdverseEventExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempCervicalCancerScreeningExtractError as SELECT * FROM TempCervicalCancerScreeningExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempCovidExtractError as SELECT * FROM TempCovidExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempDefaulterTracingExtractError as SELECT * FROM TempDefaulterTracingExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempOvcExtractError as SELECT * FROM TempOvcExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempOtzExtractError as SELECT * FROM TempOtzExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempIptExtractError as SELECT * FROM TempIptExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempAllergiesChronicIllnessExtractError as SELECT * FROM TempAllergiesChronicIllnessExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempGbvScreeningExtractError as SELECT * FROM TempGbvScreeningExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempEnhancedAdherenceCounsellingExtractError as SELECT * FROM TempEnhancedAdherenceCounsellingExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempDrugAlcoholScreeningExtractError as SELECT * FROM TempDrugAlcoholScreeningExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempDepressionScreeningExtractError as SELECT * FROM TempDepressionScreeningExtracts WHERE (CheckError = 1)");

            
            
			migrationBuilder.Sql(@"
				ALTER VIEW vTempPatientArtExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientArtExtractError.PatientPK,vTempPatientArtExtractError.FacilityId,
										 vTempPatientArtExtractError.PatientID, vTempPatientArtExtractError.SiteCode, vTempPatientArtExtractError.FacilityName, ValidationError.RecordId,
						 
							 vTempPatientArtExtractError.DOB, 
							 vTempPatientArtExtractError.Gender, 
							 vTempPatientArtExtractError.PatientSource, 
							 vTempPatientArtExtractError.RegistrationDate, 
							 vTempPatientArtExtractError.AgeLastVisit, 
							 vTempPatientArtExtractError.PreviousARTStartDate, 
							 vTempPatientArtExtractError.PreviousARTRegimen, 
							 vTempPatientArtExtractError.StartARTAtThisFacility, 
							 vTempPatientArtExtractError.StartARTDate, 
							 vTempPatientArtExtractError.StartRegimen, 
							 vTempPatientArtExtractError.StartRegimenLine, 
							 vTempPatientArtExtractError.LastARTDate, 
							 vTempPatientArtExtractError.LastRegimen, 
							 vTempPatientArtExtractError.LastRegimenLine, 
							 vTempPatientArtExtractError.LastVisit, 
							 vTempPatientArtExtractError.ExitReason, 
							 vTempPatientArtExtractError.ExitDate,
							vTempPatientArtExtractError.Date_Created,
							vTempPatientArtExtractError.Date_Last_Modified,
							vTempPatientArtExtractError.RecordUUID
	FROM            vTempPatientArtExtractError INNER JOIN
							 ValidationError ON vTempPatientArtExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");


            migrationBuilder.Sql(@"
	ALTER VIEW vTempPatientBaselinesExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientBaselinesExtractError.PatientPK,vTempPatientBaselinesExtractError.FacilityId,
							 vTempPatientBaselinesExtractError.PatientID, vTempPatientBaselinesExtractError.SiteCode, ValidationError.RecordId,
						 
							 vTempPatientBaselinesExtractError.bCD4, 
							 vTempPatientBaselinesExtractError.bCD4Date, 
							 vTempPatientBaselinesExtractError.bWAB, 
							 vTempPatientBaselinesExtractError.bWABDate, 
							 vTempPatientBaselinesExtractError.bWHO, 
							 vTempPatientBaselinesExtractError.bWHODate, 
							 vTempPatientBaselinesExtractError.eWAB, 
							 vTempPatientBaselinesExtractError.eWABDate, 
							 vTempPatientBaselinesExtractError.eCD4,
							 vTempPatientBaselinesExtractError.eCD4Date, 
							 vTempPatientBaselinesExtractError.eWHO, 
							 vTempPatientBaselinesExtractError.eWHODate, 
							 vTempPatientBaselinesExtractError.lastWHO, 
							 vTempPatientBaselinesExtractError.lastWHODate, 
							 vTempPatientBaselinesExtractError.lastCD4, 
							 vTempPatientBaselinesExtractError.lastCD4Date, 
							 vTempPatientBaselinesExtractError.lastWAB, 
							 vTempPatientBaselinesExtractError.lastWABDate, 
							 vTempPatientBaselinesExtractError.m12CD4, 
							 vTempPatientBaselinesExtractError.m12CD4Date, 
							 vTempPatientBaselinesExtractError.m6CD4, 
							 vTempPatientBaselinesExtractError.m6CD4Date,
							vTempPatientBaselinesExtractError.Date_Created,
							vTempPatientBaselinesExtractError.Date_Last_Modified,
							vTempPatientBaselinesExtractError.RecordUUID

	FROM            vTempPatientBaselinesExtractError INNER JOIN
							 ValidationError ON vTempPatientBaselinesExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

            migrationBuilder.Sql(@"
	ALTER view vTempPatientLaboratoryExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientLaboratoryExtractError.PatientPK,vTempPatientLaboratoryExtractError.FacilityId,
							 vTempPatientLaboratoryExtractError.PatientID, vTempPatientLaboratoryExtractError.SiteCode, vTempPatientLaboratoryExtractError.FacilityName, ValidationError.RecordId,

							 vTempPatientLaboratoryExtractError.OrderedByDate, 
							 vTempPatientLaboratoryExtractError.ReportedByDate, 
							 vTempPatientLaboratoryExtractError.TestName, 
							 vTempPatientLaboratoryExtractError.EnrollmentTest, 
							 vTempPatientLaboratoryExtractError.TestResult, 
							 vTempPatientLaboratoryExtractError.VisitId,
							vTempPatientLaboratoryExtractError.Reason,
							vTempPatientLaboratoryExtractError.Date_Created,
							vTempPatientLaboratoryExtractError.Date_Last_Modified,
							vTempPatientLaboratoryExtractError.RecordUUID

	FROM            vTempPatientLaboratoryExtractError INNER JOIN
							 ValidationError ON vTempPatientLaboratoryExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

            migrationBuilder.Sql(@"
	ALTER VIEW vTempPatientPharmacyExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientPharmacyExtractError.PatientPK,vTempPatientPharmacyExtractError.FacilityId,
							 vTempPatientPharmacyExtractError.PatientID, vTempPatientPharmacyExtractError.SiteCode,  ValidationError.RecordId,

							 vTempPatientPharmacyExtractError.Drug, 
							 vTempPatientPharmacyExtractError.DispenseDate,
							 vTempPatientPharmacyExtractError.Duration, 
							 vTempPatientPharmacyExtractError.ExpectedReturn, 
							 vTempPatientPharmacyExtractError.TreatmentType, 
							 vTempPatientPharmacyExtractError.RegimenLine, 
							 vTempPatientPharmacyExtractError.PeriodTaken, 
							 vTempPatientPharmacyExtractError.ProphylaxisType, 
							 vTempPatientPharmacyExtractError.Provider, 
							 vTempPatientPharmacyExtractError.VisitID,
							vTempPatientPharmacyExtractError.Date_Created,
							vTempPatientPharmacyExtractError.Date_Last_Modified,
							vTempPatientPharmacyExtractError.RecordUUID

	FROM            vTempPatientPharmacyExtractError INNER JOIN
							 ValidationError ON vTempPatientPharmacyExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

            migrationBuilder.Sql(@"
	ALTER VIEW vTempPatientStatusExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientStatusExtractError.PatientPK,vTempPatientStatusExtractError.FacilityId,
							 vTempPatientStatusExtractError.PatientID, vTempPatientStatusExtractError.SiteCode, vTempPatientStatusExtractError.FacilityName, ValidationError.RecordId,

							 vTempPatientStatusExtractError.ExitDescription, 
							 vTempPatientStatusExtractError.ExitDate, 
							 vTempPatientStatusExtractError.ExitReason,
							vTempPatientStatusExtractError.Date_Created,
							vTempPatientStatusExtractError.Date_Last_Modified,
							vTempPatientStatusExtractError.RecordUUID

	FROM            vTempPatientStatusExtractError INNER JOIN
							 ValidationError ON vTempPatientStatusExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");



            migrationBuilder.Sql(@"
			ALTER VIEW vTempPatientAdverseEventExtractErrorSummary
			AS
			SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientAdverseEventExtractError.PatientPK,vTempPatientAdverseEventExtractError.FacilityId,
									 vTempPatientAdverseEventExtractError.PatientID, vTempPatientAdverseEventExtractError.SiteCode, ValidationError.RecordId,

									 vTempPatientAdverseEventExtractError.AdverseEvent, 
									 vTempPatientAdverseEventExtractError.AdverseEventStartDate, 
									 vTempPatientAdverseEventExtractError.AdverseEventEndDate,
		                             vTempPatientAdverseEventExtractError.Severity,
		                             vTempPatientAdverseEventExtractError.VisitDate,
									'' AS FacilityName,
									vTempPatientAdverseEventExtractError.Date_Created,
									vTempPatientAdverseEventExtractError.Date_Last_Modified,
									vTempPatientAdverseEventExtractError.RecordUUID


			FROM            vTempPatientAdverseEventExtractError INNER JOIN
							 ValidationError ON vTempPatientAdverseEventExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
            
				 

            		migrationBuilder.Sql(@"
				ALTER VIEW vTempCervicalCancerScreeningExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,

							vTempCervicalCancerScreeningExtractError.PatientPK,
							vTempCervicalCancerScreeningExtractError.SiteCode,
							vTempCervicalCancerScreeningExtractError.PatientID,
							vTempCervicalCancerScreeningExtractError.FacilityID,
							vTempCervicalCancerScreeningExtractError.Emr,
							vTempCervicalCancerScreeningExtractError.Project,
							vTempCervicalCancerScreeningExtractError.FacilityName,
							vTempCervicalCancerScreeningExtractError.VisitType,
							vTempCervicalCancerScreeningExtractError.VisitID,
							vTempCervicalCancerScreeningExtractError.VisitDate,
							vTempCervicalCancerScreeningExtractError.ScreeningMethod,
							vTempCervicalCancerScreeningExtractError.TreatmentToday,
							vTempCervicalCancerScreeningExtractError.ReferredOut,
							vTempCervicalCancerScreeningExtractError.NextAppointmentDate,
							vTempCervicalCancerScreeningExtractError.ScreeningType,
							vTempCervicalCancerScreeningExtractError.ScreeningResult,
					        vTempCervicalCancerScreeningExtractError.PostTreatmentComplicationCause,
					        vTempCervicalCancerScreeningExtractError.OtherPostTreatmentComplication,
					        vTempCervicalCancerScreeningExtractError.ReferralReason,
							vTempCervicalCancerScreeningExtractError.Date_Created,
							vTempCervicalCancerScreeningExtractError.Date_Last_Modified	,
							vTempCervicalCancerScreeningExtractError.RecordUUID	
				FROM            vTempCervicalCancerScreeningExtractError INNER JOIN
										 ValidationError ON vTempCervicalCancerScreeningExtractError.Id = ValidationError.RecordId INNER JOIN
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
							vTempDefaulterTracingExtractError.RecordUUID	

			                   
				FROM            vTempDefaulterTracingExtractError INNER JOIN
										 ValidationError ON vTempDefaulterTracingExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
			                ");

						migrationBuilder.Sql(@"
				ALTER VIEW vTempCovidExtractErrorSummary
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
			vTempCovidExtractError.COVID19TestResult,
			vTempCovidExtractError.Sequence,
			vTempCovidExtractError.Date_Created,
			vTempCovidExtractError.Date_Last_Modified,
			vTempCovidExtractError.RecordUUID	
			                        
				FROM            vTempCovidExtractError INNER JOIN
										 ValidationError ON vTempCovidExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");


						migrationBuilder.Sql(@"
					ALTER VIEW vTempOtzExtractErrorSummary
					AS
					SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, ValidationError.RecordId,

				vTempOtzExtractError.PatientPK,
				vTempOtzExtractError.PatientID,
				vTempOtzExtractError.FacilityId,
				vTempOtzExtractError.SiteCode,
				vTempOtzExtractError.DateExtracted,
				vTempOtzExtractError.Emr,
				vTempOtzExtractError.Project,
				vTempOtzExtractError.CheckError,
				vTempOtzExtractError.ErrorType,
				vTempOtzExtractError.FacilityName,
				vTempOtzExtractError.VisitID,
				vTempOtzExtractError.VisitDate,
				vTempOtzExtractError.OTZEnrollmentDate,
				vTempOtzExtractError.TransferInStatus,
				vTempOtzExtractError.ModulesPreviouslyCovered,
				vTempOtzExtractError.ModulesCompletedToday,
				vTempOtzExtractError.SupportGroupInvolvement,
				vTempOtzExtractError.Remarks,
				vTempOtzExtractError.TransitionAttritionReason,
				vTempOtzExtractError.OutcomeDate,
				vTempOtzExtractError.Date_Created,
				vTempOtzExtractError.Date_Last_Modified,
			vTempOtzExtractError.RecordUUID	
					FROM            vTempOtzExtractError INNER JOIN
											 ValidationError ON vTempOtzExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

						
						migrationBuilder.Sql(@"

	ALTER VIEW vTempOvcExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, ValidationError.RecordId,

					vTempOvcExtractError.PatientPK,
					vTempOvcExtractError.PatientID,
					vTempOvcExtractError.FacilityId,
					vTempOvcExtractError.SiteCode,
					vTempOvcExtractError.DateExtracted,
					vTempOvcExtractError.Emr,
					vTempOvcExtractError.Project,
					vTempOvcExtractError.CheckError,
					vTempOvcExtractError.ErrorType,
					vTempOvcExtractError.FacilityName,
					vTempOvcExtractError.VisitID,
					vTempOvcExtractError.VisitDate,
					vTempOvcExtractError.OVCEnrollmentDate,
					vTempOvcExtractError.RelationshipToClient,
					vTempOvcExtractError.EnrolledinCPIMS,
					vTempOvcExtractError.CPIMSUniqueIdentifier,
					vTempOvcExtractError.PartnerOfferingOVCServices,
					vTempOvcExtractError.OVCExitReason,
					vTempOvcExtractError.ExitDate,
					vTempOvcExtractError.Date_Created,
					vTempOvcExtractError.Date_Last_Modified,
			vTempOvcExtractError.RecordUUID	


				FROM            vTempOvcExtractError INNER JOIN
										 ValidationError ON vTempOvcExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");


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
			vTempIptExtractError.RecordUUID	


						FROM            vTempIptExtractError INNER JOIN
												 ValidationError ON vTempIptExtractError.Id = ValidationError.RecordId INNER JOIN
												 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
             
             
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
			vTempAllergiesChronicIllnessExtractError.RecordUUID	
						FROM            vTempAllergiesChronicIllnessExtractError INNER JOIN
							 ValidationError ON vTempAllergiesChronicIllnessExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
             
             
             migrationBuilder.Sql(@"

					ALTER VIEW vTempGbvScreeningExtractErrorSummary
					AS
					SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,

				vTempGbvScreeningExtractError.PatientPK,
				vTempGbvScreeningExtractError.PatientID,
				vTempGbvScreeningExtractError.FacilityId,
				vTempGbvScreeningExtractError.SiteCode,
				vTempGbvScreeningExtractError.DateExtracted,
				vTempGbvScreeningExtractError.Emr,
				vTempGbvScreeningExtractError.Project,
				vTempGbvScreeningExtractError.CheckError,
				vTempGbvScreeningExtractError.ErrorType,
				vTempGbvScreeningExtractError.FacilityName,
				vTempGbvScreeningExtractError.VisitID,
				vTempGbvScreeningExtractError.VisitDate,
				vTempGbvScreeningExtractError.IPV,
				vTempGbvScreeningExtractError.PhysicalIPV,
				vTempGbvScreeningExtractError.EmotionalIPV,
				vTempGbvScreeningExtractError.SexualIPV,
				vTempGbvScreeningExtractError.IPVRelationship,
				vTempGbvScreeningExtractError.Date_Created,
				vTempGbvScreeningExtractError.Date_Last_Modified,
			vTempGbvScreeningExtractError.RecordUUID	


					FROM            vTempGbvScreeningExtractError INNER JOIN
											 ValidationError ON vTempGbvScreeningExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
             
             migrationBuilder.Sql(@"

				ALTER VIEW vTempEnhancedAdherenceCounsellingExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, ValidationError.RecordId,

			vTempEnhancedAdherenceCounsellingExtractError.PatientPK,
			vTempEnhancedAdherenceCounsellingExtractError.PatientID,
			vTempEnhancedAdherenceCounsellingExtractError.FacilityId,
			vTempEnhancedAdherenceCounsellingExtractError.SiteCode,
			vTempEnhancedAdherenceCounsellingExtractError.DateExtracted,
			vTempEnhancedAdherenceCounsellingExtractError.Emr,
			vTempEnhancedAdherenceCounsellingExtractError.Project,
			vTempEnhancedAdherenceCounsellingExtractError.CheckError,
			vTempEnhancedAdherenceCounsellingExtractError.ErrorType,
			vTempEnhancedAdherenceCounsellingExtractError.FacilityName,
			vTempEnhancedAdherenceCounsellingExtractError.VisitID,
			vTempEnhancedAdherenceCounsellingExtractError.VisitDate,
			vTempEnhancedAdherenceCounsellingExtractError.SessionNumber,
			vTempEnhancedAdherenceCounsellingExtractError.DateOfFirstSession,
			vTempEnhancedAdherenceCounsellingExtractError.PillCountAdherence,
			vTempEnhancedAdherenceCounsellingExtractError.MMAS4_1,
			vTempEnhancedAdherenceCounsellingExtractError.MMAS4_2,
			vTempEnhancedAdherenceCounsellingExtractError.MMAS4_3,
			vTempEnhancedAdherenceCounsellingExtractError.MMAS4_4,
			vTempEnhancedAdherenceCounsellingExtractError.MMSA8_1,
			vTempEnhancedAdherenceCounsellingExtractError.MMSA8_2,
			vTempEnhancedAdherenceCounsellingExtractError.MMSA8_3,
			vTempEnhancedAdherenceCounsellingExtractError.MMSA8_4,
			vTempEnhancedAdherenceCounsellingExtractError.MMSAScore,
			vTempEnhancedAdherenceCounsellingExtractError.EACRecievedVL,
			vTempEnhancedAdherenceCounsellingExtractError.EACVL,
			vTempEnhancedAdherenceCounsellingExtractError.EACVLConcerns,
			vTempEnhancedAdherenceCounsellingExtractError.EACVLThoughts,
			vTempEnhancedAdherenceCounsellingExtractError.EACWayForward,
			vTempEnhancedAdherenceCounsellingExtractError.EACCognitiveBarrier,
			vTempEnhancedAdherenceCounsellingExtractError.EACBehaviouralBarrier_1,
			vTempEnhancedAdherenceCounsellingExtractError.EACBehaviouralBarrier_2,
			vTempEnhancedAdherenceCounsellingExtractError.EACBehaviouralBarrier_3,
			vTempEnhancedAdherenceCounsellingExtractError.EACBehaviouralBarrier_4,
			vTempEnhancedAdherenceCounsellingExtractError.EACBehaviouralBarrier_5,
			vTempEnhancedAdherenceCounsellingExtractError.EACEmotionalBarriers_1,
			vTempEnhancedAdherenceCounsellingExtractError.EACEmotionalBarriers_2,
			vTempEnhancedAdherenceCounsellingExtractError.EACEconBarrier_1,
			vTempEnhancedAdherenceCounsellingExtractError.EACEconBarrier_2,
			vTempEnhancedAdherenceCounsellingExtractError.EACEconBarrier_3,
			vTempEnhancedAdherenceCounsellingExtractError.EACEconBarrier_4,
			vTempEnhancedAdherenceCounsellingExtractError.EACEconBarrier_5,
			vTempEnhancedAdherenceCounsellingExtractError.EACEconBarrier_6,
			vTempEnhancedAdherenceCounsellingExtractError.EACEconBarrier_7,
			vTempEnhancedAdherenceCounsellingExtractError.EACEconBarrier_8,
			vTempEnhancedAdherenceCounsellingExtractError.EACReviewImprovement,
			vTempEnhancedAdherenceCounsellingExtractError.EACReviewMissedDoses,
			vTempEnhancedAdherenceCounsellingExtractError.EACReviewStrategy,
			vTempEnhancedAdherenceCounsellingExtractError.EACReferral,
			vTempEnhancedAdherenceCounsellingExtractError.EACReferralApp,
			vTempEnhancedAdherenceCounsellingExtractError.EACReferralExperience,
			vTempEnhancedAdherenceCounsellingExtractError.EACHomevisit,
			vTempEnhancedAdherenceCounsellingExtractError.EACAdherencePlan,
			vTempEnhancedAdherenceCounsellingExtractError.EACFollowupDate,
			vTempEnhancedAdherenceCounsellingExtractError.Date_Created,
			vTempEnhancedAdherenceCounsellingExtractError.Date_Last_Modified,
			vTempEnhancedAdherenceCounsellingExtractError.RecordUUID	


				FROM            vTempEnhancedAdherenceCounsellingExtractError INNER JOIN
										 ValidationError ON vTempEnhancedAdherenceCounsellingExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
             
             migrationBuilder.Sql(@"

					ALTER VIEW vTempDrugAlcoholScreeningExtractErrorSummary
					AS
					SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,

				vTempDrugAlcoholScreeningExtractError.PatientPK,
				vTempDrugAlcoholScreeningExtractError.PatientID,
				vTempDrugAlcoholScreeningExtractError.FacilityId,
				vTempDrugAlcoholScreeningExtractError.SiteCode,
				vTempDrugAlcoholScreeningExtractError.DateExtracted,
				vTempDrugAlcoholScreeningExtractError.Emr,
				vTempDrugAlcoholScreeningExtractError.Project,
				vTempDrugAlcoholScreeningExtractError.CheckError,
				vTempDrugAlcoholScreeningExtractError.ErrorType,
				vTempDrugAlcoholScreeningExtractError.FacilityName,
				vTempDrugAlcoholScreeningExtractError.VisitID,
				vTempDrugAlcoholScreeningExtractError.VisitDate,
				vTempDrugAlcoholScreeningExtractError.DrinkingAlcohol,
				vTempDrugAlcoholScreeningExtractError.Smoking,
				vTempDrugAlcoholScreeningExtractError.DrugUse,
				vTempDrugAlcoholScreeningExtractError.Date_Created,
				vTempDrugAlcoholScreeningExtractError.Date_Last_Modified,
			vTempDrugAlcoholScreeningExtractError.RecordUUID	


					FROM            vTempDrugAlcoholScreeningExtractError INNER JOIN
							 ValidationError ON vTempDrugAlcoholScreeningExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
             
             
             migrationBuilder.Sql(@"

				ALTER VIEW vTempDepressionScreeningExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, ValidationError.RecordId,
										 
			vTempDepressionScreeningExtractError.PatientPK,
			vTempDepressionScreeningExtractError.PatientID,
			vTempDepressionScreeningExtractError.FacilityId,
			vTempDepressionScreeningExtractError.SiteCode,
			vTempDepressionScreeningExtractError.DateExtracted,
			vTempDepressionScreeningExtractError.Emr,
			vTempDepressionScreeningExtractError.Project,
			vTempDepressionScreeningExtractError.CheckError,
			vTempDepressionScreeningExtractError.ErrorType,
			vTempDepressionScreeningExtractError.FacilityName,
			vTempDepressionScreeningExtractError.VisitID,
			vTempDepressionScreeningExtractError.VisitDate,
			vTempDepressionScreeningExtractError.PHQ9_1,
			vTempDepressionScreeningExtractError.PHQ9_2,
			vTempDepressionScreeningExtractError.PHQ9_3,
			vTempDepressionScreeningExtractError.PHQ9_4,
			vTempDepressionScreeningExtractError.PHQ9_5,
			vTempDepressionScreeningExtractError.PHQ9_6,
			vTempDepressionScreeningExtractError.PHQ9_7,
			vTempDepressionScreeningExtractError.PHQ9_8,
			vTempDepressionScreeningExtractError.PHQ9_9,
			vTempDepressionScreeningExtractError.PHQ_9_rating,
			vTempDepressionScreeningExtractError.DepressionAssesmentScore,
			vTempDepressionScreeningExtractError.Date_Created,
			vTempDepressionScreeningExtractError.Date_Last_Modified,
			vTempDepressionScreeningExtractError.RecordUUID


				FROM            vTempDepressionScreeningExtractError INNER JOIN
										 ValidationError ON vTempDepressionScreeningExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
