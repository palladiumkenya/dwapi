using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class UpdatedTablesCollation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"alter table HtsEligibilityExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempHtsEligibilityExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table ClientRegistryExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempClientRegistryExtracts convert to character set utf8 collate utf8_unicode_ci;");
            }
            
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
					vTempHtsEligibilityExtractError.ContactWithTBCase

				FROM            vTempHtsEligibilityExtractError INNER JOIN
										 ValidationError ON vTempHtsEligibilityExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
            ");
	        
				migrationBuilder.Sql(@"alter view vTempClientRegistryExtractError as SELECT * FROM TempClientRegistryExtracts WHERE (CheckError = 1)");

            		migrationBuilder.Sql(@"
				ALTER VIEW vTempClientRegistryExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,

								vTempClientRegistryExtractError.AlienIdNo,
								vTempClientRegistryExtractError.AlternativePhoneNumber,
								vTempClientRegistryExtractError.BirthCertificateNumber,
								vTempClientRegistryExtractError.CCCNumber,
								vTempClientRegistryExtractError.County,
								vTempClientRegistryExtractError.DateOfBirth,
								vTempClientRegistryExtractError.DateOfInitiation,
								vTempClientRegistryExtractError.DateOfLastEncounter,
								vTempClientRegistryExtractError.DateOfLastViralLoad,
								vTempClientRegistryExtractError.DrivingLicenseNumber,
								vTempClientRegistryExtractError.Emr,
								vTempClientRegistryExtractError.FacilityId,
								vTempClientRegistryExtractError.FacilityName,
								vTempClientRegistryExtractError.FirstName,
								vTempClientRegistryExtractError.HighestLevelOfEducation,
								vTempClientRegistryExtractError.HudumaNumber,
								vTempClientRegistryExtractError.Landmark,
								vTempClientRegistryExtractError.LastName,
								vTempClientRegistryExtractError.LastRegimen,
								vTempClientRegistryExtractError.LastRegimenLine,
								vTempClientRegistryExtractError.Location,
								vTempClientRegistryExtractError.MaritalStatus,
								vTempClientRegistryExtractError.MFLCode,
								vTempClientRegistryExtractError.MiddleName,
								vTempClientRegistryExtractError.NameOfNextOfKin,
								vTempClientRegistryExtractError.NationalId,
								vTempClientRegistryExtractError.NextAppointmentDate,
								vTempClientRegistryExtractError.NextOfKinRelationship,
								vTempClientRegistryExtractError.NextOfKinTelNo,
								vTempClientRegistryExtractError.Occupation,
								vTempClientRegistryExtractError.Passport,
								vTempClientRegistryExtractError.PatientClinicNumber,
								vTempClientRegistryExtractError.PatientPK,
								vTempClientRegistryExtractError.PhoneNumber,
								vTempClientRegistryExtractError.Project,
								vTempClientRegistryExtractError.Sex,
								vTempClientRegistryExtractError.SiteCode,
								vTempClientRegistryExtractError.SpousePhoneNumber,
								vTempClientRegistryExtractError.SubCounty,
								vTempClientRegistryExtractError.TreatmentOutcome,
								vTempClientRegistryExtractError.Village,
								vTempClientRegistryExtractError.Ward,
								vTempClientRegistryExtractError.CurrentOnART,
								vTempClientRegistryExtractError.DateOfHIVdiagnosis,
								vTempClientRegistryExtractError.LastViralLoadResult						


				FROM            vTempClientRegistryExtractError INNER JOIN
										 ValidationError ON vTempClientRegistryExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
