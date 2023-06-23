using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HtsEligibilityAddedDisabilityViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
 migrationBuilder.Sql(@"alter view vTempHtsEligibilityExtractError as SELECT * FROM TempHtsEligibilityExtracts WHERE (CheckError = 1)");

	        migrationBuilder.Sql(@"
				ALTER VIEW vTempHtsEligibilityExtractErrorSummary
				AS
         		SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, ValidationError.RecordId,
 
					vTempHtsEligibilityExtractError.PatientPK,
					vTempHtsEligibilityExtractError.SiteCode,
					vTempHtsEligibilityExtractError.FacilityName,
					vTempHtsEligibilityExtractError.Emr,
					vTempHtsEligibilityExtractError.Project,
					vTempHtsEligibilityExtractError.HtsNumber,
					vTempHtsEligibilityExtractError.VisitID,
					vTempHtsEligibilityExtractError.EncounterId,
					vTempHtsEligibilityExtractError.VisitDate,
					vTempHtsEligibilityExtractError.PopulationType,
					vTempHtsEligibilityExtractError.KeyPopulation,
					vTempHtsEligibilityExtractError.PriorityPopulation,
					vTempHtsEligibilityExtractError.Department,
					vTempHtsEligibilityExtractError.PatientType,
					vTempHtsEligibilityExtractError.IsHealthWorker,
					vTempHtsEligibilityExtractError.RelationshipWithContact,
					vTempHtsEligibilityExtractError.TestedHIVBefore,
					vTempHtsEligibilityExtractError.WhoPerformedTest,
					vTempHtsEligibilityExtractError.ResultOfHIV,
					vTempHtsEligibilityExtractError.StartedOnART,
					vTempHtsEligibilityExtractError.CCCNumber,
					vTempHtsEligibilityExtractError.EverHadSex,
					vTempHtsEligibilityExtractError.SexuallyActive,
					vTempHtsEligibilityExtractError.NewPartner,
					vTempHtsEligibilityExtractError.PartnerHivStatus,
					vTempHtsEligibilityExtractError.CoupleDiscordant,
					vTempHtsEligibilityExtractError.MultiplePartners,
					vTempHtsEligibilityExtractError.NumberOfPartners,
					vTempHtsEligibilityExtractError.AlcoholSex,
					vTempHtsEligibilityExtractError.MoneySex,
					vTempHtsEligibilityExtractError.CondomBurst,
					vTempHtsEligibilityExtractError.UnknownStatusPartner,
					vTempHtsEligibilityExtractError.KnownStatusPartner,
					vTempHtsEligibilityExtractError.Pregnant,
					vTempHtsEligibilityExtractError.BreastfeedingMother,
					vTempHtsEligibilityExtractError.ExperiencedGBV,
					vTempHtsEligibilityExtractError.EverOnPrep,
					vTempHtsEligibilityExtractError.CurrentlyOnPrep,
					vTempHtsEligibilityExtractError.EverOnPep,
					vTempHtsEligibilityExtractError.CurrentlyOnPep,
					vTempHtsEligibilityExtractError.EverHadSTI,
					vTempHtsEligibilityExtractError.CurrentlyHasSTI,
					vTempHtsEligibilityExtractError.EverHadTB,
					vTempHtsEligibilityExtractError.SharedNeedle,
					vTempHtsEligibilityExtractError.NeedleStickInjuries,
					vTempHtsEligibilityExtractError.TraditionalProcedures,
					vTempHtsEligibilityExtractError.ChildReasonsForIneligibility,
					vTempHtsEligibilityExtractError.EligibleForTest,
					vTempHtsEligibilityExtractError.ReasonsForIneligibility,
					vTempHtsEligibilityExtractError.SpecificReasonForIneligibility,
					vTempHtsEligibilityExtractError.MothersStatus,
					vTempHtsEligibilityExtractError.DateTestedSelf,
					vTempHtsEligibilityExtractError.ResultOfHIVSelf,
					vTempHtsEligibilityExtractError.DateTestedProvider,
					vTempHtsEligibilityExtractError.ScreenedTB,
					vTempHtsEligibilityExtractError.Cough,
					vTempHtsEligibilityExtractError.Fever,
					vTempHtsEligibilityExtractError.WeightLoss,
					vTempHtsEligibilityExtractError.NightSweats,
					vTempHtsEligibilityExtractError.Lethargy,
					vTempHtsEligibilityExtractError.TBStatus,
					vTempHtsEligibilityExtractError.ReferredForTesting,
					vTempHtsEligibilityExtractError.AssessmentOutcome,
					vTempHtsEligibilityExtractError.TypeGBV,
					vTempHtsEligibilityExtractError.ForcedSex,
					vTempHtsEligibilityExtractError.ReceivedServices,
					vTempHtsEligibilityExtractError.DateCreated,
					vTempHtsEligibilityExtractError.DateLastModified,
					vTempHtsEligibilityExtractError.ContactWithTBCase,
					vTempHtsEligibilityExtractError.Disability,
					vTempHtsEligibilityExtractError.DisabilityType,
					vTempHtsEligibilityExtractError.HTSStrategy,
					vTempHtsEligibilityExtractError.HTSEntryPoint,
					vTempHtsEligibilityExtractError.HIVRiskCategory,
					vTempHtsEligibilityExtractError.ReasonRefferredForTesting,
					vTempHtsEligibilityExtractError.ReasonNotReffered

				FROM            vTempHtsEligibilityExtractError INNER JOIN
										 ValidationError ON vTempHtsEligibilityExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
            ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
