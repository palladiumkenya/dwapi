﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class AddRiskScoreToEligibilityViews : Migration
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
						vTempHtsEligibilityExtractError.ContactWithTBCase,
						vTempHtsEligibilityExtractError.Disability,
						vTempHtsEligibilityExtractError.DisabilityType,
						vTempHtsEligibilityExtractError.HTSStrategy,
						vTempHtsEligibilityExtractError.HTSEntryPoint,
						vTempHtsEligibilityExtractError.HIVRiskCategory,
						vTempHtsEligibilityExtractError.ReasonRefferredForTesting  ,
						vTempHtsEligibilityExtractError.ReasonNotReffered,
						vTempHtsEligibilityExtractError.DateCreated,
						vTempHtsEligibilityExtractError.DateLastModified,
						vTempHtsEligibilityExtractError.HtsRiskScore

				FROM            vTempHtsEligibilityExtractError INNER JOIN
										 ValidationError ON vTempHtsEligibilityExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
            ");
            
            
            
            migrationBuilder.Sql(@"alter view vTempHtsClientTestsExtractError as SELECT  * FROM    TempHtsClientTestsExtracts WHERE   (CheckError = 1)");
             migrationBuilder.Sql(@"
                        ALTER VIEW vTempHtsClientTestsExtractErrorSummary
                        AS
                        SELECT ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, 
                        ValidationError.RecordId, 
                        vTempHtsClientTestsExtractError.FacilityName,
                        vTempHtsClientTestsExtractError.HtsNumber,
                        vTempHtsClientTestsExtractError.PatientPK,
                        vTempHtsClientTestsExtractError.SiteCode,
                        vTempHtsClientTestsExtractError.EncounterId,
                        vTempHtsClientTestsExtractError.TestDate,
                        vTempHtsClientTestsExtractError.EverTestedForHiv,
                        vTempHtsClientTestsExtractError.MonthsSinceLastTest,
                        vTempHtsClientTestsExtractError.ClientTestedAs,
                        vTempHtsClientTestsExtractError.EntryPoint,
                        vTempHtsClientTestsExtractError.TestStrategy,
                        vTempHtsClientTestsExtractError.TestResult1,
                        vTempHtsClientTestsExtractError.TestResult2,
                        vTempHtsClientTestsExtractError.FinalTestResult,
                        vTempHtsClientTestsExtractError.PatientGivenResult,
                        vTempHtsClientTestsExtractError.TbScreening,
                        vTempHtsClientTestsExtractError.ClientSelfTested,
                        vTempHtsClientTestsExtractError.CoupleDiscordant,
                        vTempHtsClientTestsExtractError.TestType,
                        vTempHtsClientTestsExtractError.Consent,
						vTempHtsClientTestsExtractError.Approach,
						vTempHtsClientTestsExtractError.Date_Created,
						vTempHtsClientTestsExtractError.Date_Last_Modified,
						vTempHtsClientTestsExtractError.HtsRiskCategory,
						vTempHtsClientTestsExtractError.HtsRiskScore,
						vTempHtsClientTestsExtractError.Setting,
						vTempHtsClientTestsExtractError.OtherReferredServices,
						vTempHtsClientTestsExtractError.ReferredForServices,
						vTempHtsClientTestsExtractError.ReferredServices
                        FROM vTempHtsClientTestsExtractError 
                        INNER JOIN ValidationError ON vTempHtsClientTestsExtractError.Id = ValidationError.RecordId 
                        INNER JOIN Validator ON ValidationError.ValidatorId = Validator.Id");
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
