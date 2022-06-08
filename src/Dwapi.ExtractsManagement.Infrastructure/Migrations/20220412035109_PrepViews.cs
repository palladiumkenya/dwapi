using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class PrepViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

	        migrationBuilder.Sql(@"ALTER view vTempPatientStatusExtractError as SELECT * FROM TempPatientStatusExtracts WHERE (CheckError = 1)");

            migrationBuilder.Sql(@"create view vTempPatientPrepExtractError as SELECT * FROM TempPatientPrepExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"create view vTempPrepAdverseEventExtractError as SELECT * FROM TempPrepAdverseEventExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"create view vTempPrepBehaviourRiskExtractError as SELECT * FROM TempPrepBehaviourRiskExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"create view vTempPrepCareTerminationExtractError as SELECT * FROM TempPrepCareTerminationExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"create view vTempPrepLabExtractError as SELECT * FROM TempPrepLabExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"create view vTempPrepPharmacyExtractError as SELECT * FROM TempPrepPharmacyExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"create view vTempPrepVisitExtractError as SELECT * FROM TempPrepVisitExtracts WHERE (CheckError = 1)");

            migrationBuilder.Sql(@"
					ALTER VIEW vTempPatientStatusExtractErrorSummary
					AS
					SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientStatusExtractError.PatientPK,vTempPatientStatusExtractError.FacilityId,
											 vTempPatientStatusExtractError.PatientID, vTempPatientStatusExtractError.SiteCode, vTempPatientStatusExtractError.FacilityName, ValidationError.RecordId,

											 vTempPatientStatusExtractError.ExitDescription, 
											 vTempPatientStatusExtractError.EffectiveDiscontinuationDate,
											 vTempPatientStatusExtractError.ExitDate, 
											 vTempPatientStatusExtractError.ExitReason,
											 vTempPatientStatusExtractError.ReasonForDeath,
											 vTempPatientStatusExtractError.SpecificDeathReason,
											 vTempPatientStatusExtractError.DeathDate,
											 vTempPatientStatusExtractError.Date_Created,
											 vTempPatientStatusExtractError.Date_Last_Modified

					FROM            vTempPatientStatusExtractError INNER JOIN
											 ValidationError ON vTempPatientStatusExtractError.Id = ValidationError.RecordId INNER JOIN
											 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

            		migrationBuilder.Sql(@"
				CREATE VIEW vTempPatientPrepExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,


								vTempPatientPrepExtractError.PatientPK,
								vTempPatientPrepExtractError.FacilityId,
								vTempPatientPrepExtractError.PatientID, 
								vTempPatientPrepExtractError.SiteCode,
								vTempPatientPrepExtractError.Emr,
								vTempPatientPrepExtractError.Project,

								vTempPatientPrepExtractError.PrepNumber,
								vTempPatientPrepExtractError.HtsNumber,
								vTempPatientPrepExtractError.PrepEnrollmentDate,
								vTempPatientPrepExtractError.Sex,
								vTempPatientPrepExtractError.DateofBirth,
								vTempPatientPrepExtractError.CountyofBirth,
								vTempPatientPrepExtractError.County,
								vTempPatientPrepExtractError.SubCounty,
								vTempPatientPrepExtractError.Location,
								vTempPatientPrepExtractError.LandMark,
								vTempPatientPrepExtractError.Ward,
								vTempPatientPrepExtractError.ClientType,
								vTempPatientPrepExtractError.ReferralPoint,
								vTempPatientPrepExtractError.MaritalStatus,
								vTempPatientPrepExtractError.Inschool,
								vTempPatientPrepExtractError.PopulationType,
								vTempPatientPrepExtractError.KeyPopulationType,
								vTempPatientPrepExtractError.Refferedfrom,
								vTempPatientPrepExtractError.TransferIn,
								vTempPatientPrepExtractError.TransferInDate,
								vTempPatientPrepExtractError.TransferFromFacility,
								vTempPatientPrepExtractError.DatefirstinitiatedinPrepCare,
								vTempPatientPrepExtractError.DateStartedPrEPattransferringfacility,
								vTempPatientPrepExtractError.ClientPreviouslyonPrep,
								vTempPatientPrepExtractError.PrevPrepReg,
								vTempPatientPrepExtractError.DateLastUsedPrev,

								vTempPatientPrepExtractError.Date_Created,
								vTempPatientPrepExtractError.Date_Last_Modified
				FROM            vTempPatientPrepExtractError INNER JOIN
										 ValidationError ON vTempPatientPrepExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");


                    migrationBuilder.Sql(@"
				CREATE VIEW vTempPrepAdverseEventExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,


								vTempPrepAdverseEventExtractError.PatientPK,
								vTempPrepAdverseEventExtractError.FacilityId,
								vTempPrepAdverseEventExtractError.PatientID, 
								vTempPrepAdverseEventExtractError.SiteCode,
								vTempPrepAdverseEventExtractError.Emr,
								vTempPrepAdverseEventExtractError.Project,
vTempPrepAdverseEventExtractError.PREPNumber,
vTempPrepAdverseEventExtractError.AdverseEvent,
vTempPrepAdverseEventExtractError.AdverseEventStartDate,
vTempPrepAdverseEventExtractError.AdverseEventEndDate,
vTempPrepAdverseEventExtractError.Severity,
vTempPrepAdverseEventExtractError.VisitDate,
vTempPrepAdverseEventExtractError.AdverseEventActionTaken,
vTempPrepAdverseEventExtractError.AdverseEventClinicalOutcome,
vTempPrepAdverseEventExtractError.AdverseEventIsPregnant,
vTempPrepAdverseEventExtractError.AdverseEventCause,
vTempPrepAdverseEventExtractError.AdverseEventRegimen,
vTempPrepAdverseEventExtractError.FacilityName,


								vTempPrepAdverseEventExtractError.Date_Created,
								vTempPrepAdverseEventExtractError.Date_Last_Modified
				FROM            vTempPrepAdverseEventExtractError INNER JOIN
										 ValidationError ON vTempPrepAdverseEventExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

                    migrationBuilder.Sql(@"
				CREATE VIEW vTempPrepBehaviourRiskExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,


								vTempPrepBehaviourRiskExtractError.PatientPK,
								vTempPrepBehaviourRiskExtractError.FacilityId,
								vTempPrepBehaviourRiskExtractError.PatientID, 
								vTempPrepBehaviourRiskExtractError.SiteCode,
								vTempPrepBehaviourRiskExtractError.Emr,
								vTempPrepBehaviourRiskExtractError.Project,

vTempPrepBehaviourRiskExtractError.PrepNumber,
vTempPrepBehaviourRiskExtractError.HtsNumber,
vTempPrepBehaviourRiskExtractError.VisitDate,
vTempPrepBehaviourRiskExtractError.VisitID,
vTempPrepBehaviourRiskExtractError.SexPartnerHIVStatus,
vTempPrepBehaviourRiskExtractError.IsHIVPositivePartnerCurrentonART,
vTempPrepBehaviourRiskExtractError.IsPartnerHighrisk,
vTempPrepBehaviourRiskExtractError.PartnerARTRisk,
vTempPrepBehaviourRiskExtractError.ClientAssessments,
vTempPrepBehaviourRiskExtractError.ClientRisk,
vTempPrepBehaviourRiskExtractError.ClientWillingToTakePrep,
vTempPrepBehaviourRiskExtractError.PrEPDeclineReason,
vTempPrepBehaviourRiskExtractError.RiskReductionEducationOffered,
vTempPrepBehaviourRiskExtractError.ReferralToOtherPrevServices,
vTempPrepBehaviourRiskExtractError.FirstEstablishPartnerStatus,
vTempPrepBehaviourRiskExtractError.PartnerEnrolledtoCCC,
vTempPrepBehaviourRiskExtractError.HIVPartnerCCCnumber,
vTempPrepBehaviourRiskExtractError.HIVPartnerARTStartDate,
vTempPrepBehaviourRiskExtractError.MonthsknownHIVSerodiscordant,
vTempPrepBehaviourRiskExtractError.SexWithoutCondom,
vTempPrepBehaviourRiskExtractError.NumberofchildrenWithPartner,
vTempPrepBehaviourRiskExtractError.FacilityName,


								vTempPrepBehaviourRiskExtractError.Date_Created,
								vTempPrepBehaviourRiskExtractError.Date_Last_Modified
				FROM            vTempPrepBehaviourRiskExtractError INNER JOIN
										 ValidationError ON vTempPrepBehaviourRiskExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

                    migrationBuilder.Sql(@"
				CREATE VIEW vTempPrepCareTerminationExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,


								vTempPrepCareTerminationExtractError.PatientPK,
								vTempPrepCareTerminationExtractError.FacilityId,
								vTempPrepCareTerminationExtractError.PatientID, 
								vTempPrepCareTerminationExtractError.SiteCode,
								vTempPrepCareTerminationExtractError.Emr,
								vTempPrepCareTerminationExtractError.Project,

vTempPrepCareTerminationExtractError.PrepNumber,
vTempPrepCareTerminationExtractError.HtsNumber,
vTempPrepCareTerminationExtractError.ExitDate,
vTempPrepCareTerminationExtractError.ExitReason,
vTempPrepCareTerminationExtractError.DateOfLastPrepDose,
vTempPrepCareTerminationExtractError.FacilityName,


								vTempPrepCareTerminationExtractError.Date_Created,
								vTempPrepCareTerminationExtractError.Date_Last_Modified
				FROM            vTempPrepCareTerminationExtractError INNER JOIN
										 ValidationError ON vTempPrepCareTerminationExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

                    migrationBuilder.Sql(@"
				CREATE VIEW vTempPrepLabExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,


								vTempPrepLabExtractError.PatientPK,
								vTempPrepLabExtractError.FacilityId,
								vTempPrepLabExtractError.PatientID, 
								vTempPrepLabExtractError.SiteCode,
								vTempPrepLabExtractError.Emr,
								vTempPrepLabExtractError.Project,
vTempPrepLabExtractError.PrepNumber,
vTempPrepLabExtractError.HtsNumber,
vTempPrepLabExtractError.VisitID,
vTempPrepLabExtractError.TestName,
vTempPrepLabExtractError.TestResult,
vTempPrepLabExtractError.SampleDate,
vTempPrepLabExtractError.TestResultDate,
vTempPrepLabExtractError.Reason,
vTempPrepLabExtractError.FacilityName,



								vTempPrepLabExtractError.Date_Created,
								vTempPrepLabExtractError.Date_Last_Modified
				FROM            vTempPrepLabExtractError INNER JOIN
										 ValidationError ON vTempPrepLabExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

                    migrationBuilder.Sql(@"
				CREATE VIEW vTempPrepPharmacyExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,


								vTempPrepPharmacyExtractError.PatientPK,
								vTempPrepPharmacyExtractError.FacilityId,
								vTempPrepPharmacyExtractError.PatientID, 
								vTempPrepPharmacyExtractError.SiteCode,
								vTempPrepPharmacyExtractError.Emr,
								vTempPrepPharmacyExtractError.Project,

vTempPrepPharmacyExtractError.PrepNumber,
vTempPrepPharmacyExtractError.HtsNumber,
vTempPrepPharmacyExtractError.VisitID,
vTempPrepPharmacyExtractError.RegimenPrescribed,
vTempPrepPharmacyExtractError.DispenseDate,
vTempPrepPharmacyExtractError.Duration,
vTempPrepPharmacyExtractError.FacilityName,


								vTempPrepPharmacyExtractError.Date_Created,
								vTempPrepPharmacyExtractError.Date_Last_Modified
				FROM            vTempPrepPharmacyExtractError INNER JOIN
										 ValidationError ON vTempPrepPharmacyExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

                    migrationBuilder.Sql(@"
				CREATE VIEW vTempPrepVisitExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,


								vTempPrepVisitExtractError.PatientPK,
								vTempPrepVisitExtractError.FacilityId,
								vTempPrepVisitExtractError.PatientID, 
								vTempPrepVisitExtractError.SiteCode,
								vTempPrepVisitExtractError.Emr,
								vTempPrepVisitExtractError.Project,
vTempPrepVisitExtractError.PrepNumber,
vTempPrepVisitExtractError.HtsNumber,
vTempPrepVisitExtractError.EncounterId,
vTempPrepVisitExtractError.VisitID,
vTempPrepVisitExtractError.VisitDate,
vTempPrepVisitExtractError.BloodPressure,
vTempPrepVisitExtractError.Temperature,
vTempPrepVisitExtractError.Weight,
vTempPrepVisitExtractError.Height,
vTempPrepVisitExtractError.BMI,
vTempPrepVisitExtractError.STIScreening,
vTempPrepVisitExtractError.STISymptoms,
vTempPrepVisitExtractError.STITreated,
vTempPrepVisitExtractError.Circumcised,
vTempPrepVisitExtractError.VMMCReferral,
vTempPrepVisitExtractError.LMP,
vTempPrepVisitExtractError.MenopausalStatus,
vTempPrepVisitExtractError.PregnantAtThisVisit,
vTempPrepVisitExtractError.EDD,
vTempPrepVisitExtractError.PlanningToGetPregnant,
vTempPrepVisitExtractError.PregnancyPlanned,
vTempPrepVisitExtractError.PregnancyEnded,
vTempPrepVisitExtractError.PregnancyEndDate,
vTempPrepVisitExtractError.PregnancyOutcome,
vTempPrepVisitExtractError.BirthDefects,
vTempPrepVisitExtractError.Breastfeeding,
vTempPrepVisitExtractError.FamilyPlanningStatus,
vTempPrepVisitExtractError.FPMethods,
vTempPrepVisitExtractError.AdherenceDone,
vTempPrepVisitExtractError.AdherenceOutcome,
vTempPrepVisitExtractError.AdherenceReasons,
vTempPrepVisitExtractError.SymptomsAcuteHIV,
vTempPrepVisitExtractError.ContraindicationsPrep,
vTempPrepVisitExtractError.PrepTreatmentPlan,
vTempPrepVisitExtractError.PrepPrescribed,
vTempPrepVisitExtractError.RegimenPrescribed,
vTempPrepVisitExtractError.MonthsPrescribed,
vTempPrepVisitExtractError.CondomsIssued,
vTempPrepVisitExtractError.Tobegivennextappointment,
vTempPrepVisitExtractError.Reasonfornotgivingnextappointment,
vTempPrepVisitExtractError.HepatitisBPositiveResult,
vTempPrepVisitExtractError.HepatitisCPositiveResult,
vTempPrepVisitExtractError.VaccinationForHepBStarted,
vTempPrepVisitExtractError.TreatedForHepB,
vTempPrepVisitExtractError.VaccinationForHepCStarted,
vTempPrepVisitExtractError.TreatedForHepC,
vTempPrepVisitExtractError.NextAppointment,
vTempPrepVisitExtractError.ClinicalNotes,
vTempPrepVisitExtractError.FacilityName,

								vTempPrepVisitExtractError.Date_Created,
								vTempPrepVisitExtractError.Date_Last_Modified
				FROM            vTempPrepVisitExtractError INNER JOIN
										 ValidationError ON vTempPrepVisitExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
