using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class NewViewzReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	        migrationBuilder.Sql(@"ALTER view vTempAllergiesChronicIllnessExtractError as SELECT * FROM TempAllergiesChronicIllnessExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempIptExtractError as SELECT * FROM TempIptExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempDepressionScreeningExtractError as SELECT * FROM TempDepressionScreeningExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempContactListingExtractError as SELECT * FROM TempContactListingExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempGbvScreeningExtractError as SELECT * FROM TempGbvScreeningExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempEnhancedAdherenceCounsellingExtractError as SELECT * FROM TempEnhancedAdherenceCounsellingExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempDrugAlcoholScreeningExtractError as SELECT * FROM TempDrugAlcoholScreeningExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempOvcExtractError as SELECT * FROM TempOvcExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"ALTER view vTempOtzExtractError as SELECT * FROM TempOtzExtracts WHERE (CheckError = 1)");


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
vTempAllergiesChronicIllnessExtractError.Date_Last_Modified



	FROM            vTempAllergiesChronicIllnessExtractError INNER JOIN
							 ValidationError ON vTempAllergiesChronicIllnessExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");


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
vTempContactListingExtractError.Date_Last_Modified
 
	FROM            vTempContactListingExtractError INNER JOIN
							 ValidationError ON vTempContactListingExtractError.Id = ValidationError.RecordId INNER JOIN
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
vTempDepressionScreeningExtractError.Date_Last_Modified

	FROM            vTempDepressionScreeningExtractError INNER JOIN
							 ValidationError ON vTempDepressionScreeningExtractError.Id = ValidationError.RecordId INNER JOIN
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
vTempDrugAlcoholScreeningExtractError.Date_Last_Modified

	FROM            vTempDrugAlcoholScreeningExtractError INNER JOIN
							 ValidationError ON vTempDrugAlcoholScreeningExtractError.Id = ValidationError.RecordId INNER JOIN
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
vTempEnhancedAdherenceCounsellingExtractError.Date_Last_Modified

	FROM            vTempEnhancedAdherenceCounsellingExtractError INNER JOIN
							 ValidationError ON vTempEnhancedAdherenceCounsellingExtractError.Id = ValidationError.RecordId INNER JOIN
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
vTempGbvScreeningExtractError.Date_Last_Modified

	FROM            vTempGbvScreeningExtractError INNER JOIN
							 ValidationError ON vTempGbvScreeningExtractError.Id = ValidationError.RecordId INNER JOIN
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
vTempOtzExtractError.Date_Last_Modified

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
vTempOvcExtractError.Date_Last_Modified

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
vTempIptExtractError.Date_Last_Modified

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
