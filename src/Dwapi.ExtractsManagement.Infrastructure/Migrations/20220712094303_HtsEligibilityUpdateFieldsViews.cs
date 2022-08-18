using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HtsEligibilityUpdateFieldsViews : Migration
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
					vTempHtsEligibilityExtractError.DateTested ,
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
					vTempHtsEligibilityExtractError.PhysicalViolence,
					vTempHtsEligibilityExtractError.SexualViolence,
					vTempHtsEligibilityExtractError.EverOnPrep,
					vTempHtsEligibilityExtractError.CurrentlyOnPrep,
					vTempHtsEligibilityExtractError.EverOnPep,
					vTempHtsEligibilityExtractError.CurrentlyOnPep,
					vTempHtsEligibilityExtractError.EverHadSTI,
					vTempHtsEligibilityExtractError.CurrentlyHasSTI,
					vTempHtsEligibilityExtractError.EverHadTB,
					vTempHtsEligibilityExtractError.CurrentlyHasTB,
					vTempHtsEligibilityExtractError.SharedNeedle,
					vTempHtsEligibilityExtractError.NeedleStickInjuries,
					vTempHtsEligibilityExtractError.TraditionalProcedures,
					vTempHtsEligibilityExtractError.ChildReasonsForIneligibility,
					vTempHtsEligibilityExtractError.EligibleForTest,
					vTempHtsEligibilityExtractError.ReasonsForIneligibility,
					vTempHtsEligibilityExtractError.SpecificReasonForIneligibility

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
