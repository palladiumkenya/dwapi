using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class InitialMigrationHTSViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
 if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"alter table TempHtsClientsExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempHtsClientTestsExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempHtsTestKitsExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempHtsClientTracingExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempHtsClientTestsExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempHtsPartnerTracingExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempHtsClientsLinkageExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempHtsEligibilityExtracts convert to character set utf8 collate utf8_unicode_ci;");


                migrationBuilder.Sql(@"alter table HtsClientsExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table HtsClientTestsExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table HtsTestKitsExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table HtsClientTracingExtracts convert to character set utf8 collate utf8_unicode_ci;"); 
                migrationBuilder.Sql(@"alter table HtsPartnerTracingExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table HtsPartnerNotificationServicesExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table HtsClientsLinkageExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table HtsEligibilityExtracts convert to character set utf8 collate utf8_unicode_ci;");

            }
 
 
            migrationBuilder.Sql(@"CREATE  view vTempHtsClientsExtractError as SELECT * FROM TempHtsClientsExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"						CREATE  VIEW vTempHtsClientsExtractErrorSummary
						AS
						SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, ValidationError.RecordId,
						vTempHtsClientsExtractError.FacilityName,
                        vTempHtsClientsExtractError.HtsNumber,
                        vTempHtsClientsExtractError.PatientPK,
                        vTempHtsClientsExtractError.SiteCode,
                        vTempHtsClientsExtractError.Dob,
                        vTempHtsClientsExtractError.Gender,
                        vTempHtsClientsExtractError.MaritalStatus,
                        vTempHtsClientsExtractError.PopulationType,
                        vTempHtsClientsExtractError.KeyPopulationType,
                        vTempHtsClientsExtractError.PatientDisabled,
                        vTempHtsClientsExtractError.County,
                        vTempHtsClientsExtractError.SubCounty,
                        vTempHtsClientsExtractError.Ward,                        
						vTempHtsClientsExtractError.HtsRecencyId,
						vTempHtsClientsExtractError.Occupation,
						vTempHtsClientsExtractError.PriorityPopulationType,
						vTempHtsClientsExtractError.NUPI,
						vTempHtsClientsExtractError.Pkv,	
					vTempHtsClientsExtractError.RecordUUID,
					vTempHtsClientsExtractError.Voided

                        FROM            vTempHtsClientsExtractError INNER JOIN
										 ValidationError ON vTempHtsClientsExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
            
            
             migrationBuilder.Sql($@"CREATE  view vTempHtsClientsLinkageExtractError as SELECT  * FROM    TempHtsClientsLinkageExtracts WHERE   (CheckError = 1)");
             migrationBuilder.Sql(@"
                        CREATE  VIEW vTempHtsClientsLinkageExtractErrorSummary
                        AS
                        SELECT
                                ValidationError.Id,Validator.Extract,Validator.Field,Validator.Type,Validator.Summary,ValidationError.DateGenerated,ValidationError.RecordId,
									    vTempHtsClientsLinkageExtractError.FacilityName,
				                        vTempHtsClientsLinkageExtractError.HtsNumber,
				                        vTempHtsClientsLinkageExtractError.PatientPK,
				                        vTempHtsClientsLinkageExtractError.SiteCode,
				                        vTempHtsClientsLinkageExtractError.DatePrefferedToBeEnrolled,
				                        vTempHtsClientsLinkageExtractError.FacilityReferredTo,
				                        vTempHtsClientsLinkageExtractError.HandedOverTo,
				                        vTempHtsClientsLinkageExtractError.HandedOverToCadre,
				                        vTempHtsClientsLinkageExtractError.EnrolledFacilityName,
				                        vTempHtsClientsLinkageExtractError.ReferralDate,
				                        vTempHtsClientsLinkageExtractError.DateEnrolled,
				                        vTempHtsClientsLinkageExtractError.ReportedCCCNumber,
				                        vTempHtsClientsLinkageExtractError.ReportedStartARTDate,                                 
									vTempHtsClientsLinkageExtractError.Date_Created,
									vTempHtsClientsLinkageExtractError.Date_Last_Modified,	
					vTempHtsClientsLinkageExtractError.RecordUUID,
					vTempHtsClientsLinkageExtractError.Voided
                        FROM vTempHtsClientsLinkageExtractError 
                        INNER JOIN ValidationError ON vTempHtsClientsLinkageExtractError.Id = ValidationError.RecordId 
                        INNER JOIN Validator ON ValidationError.ValidatorId = Validator.Id");
             
            
            migrationBuilder.Sql(@"CREATE  view vTempHtsPartnerTracingExtractError as SELECT  * FROM TempHtsPartnerTracingExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"
                        CREATE  VIEW vTempHtsPartnerTracingExtractErrorSummary
                        AS
                        SELECT ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, 
                        ValidationError.RecordId, 
						vTempHtsPartnerTracingExtractError.FacilityName,
                        vTempHtsPartnerTracingExtractError.HtsNumber,
                        vTempHtsPartnerTracingExtractError.PatientPK,
                        vTempHtsPartnerTracingExtractError.SiteCode,
                        vTempHtsPartnerTracingExtractError.TraceType,
                        vTempHtsPartnerTracingExtractError.PartnerPersonId,
                        vTempHtsPartnerTracingExtractError.TraceDate,
                        vTempHtsPartnerTracingExtractError.TraceOutcome,
                        vTempHtsPartnerTracingExtractError.BookingDate,
	                        vTempHtsPartnerTracingExtractError.Date_Created,
							vTempHtsPartnerTracingExtractError.Date_Last_Modified,	
					vTempHtsPartnerTracingExtractError.RecordUUID,
					vTempHtsPartnerTracingExtractError.Voided
                        FROM vTempHtsPartnerTracingExtractError 
                        INNER JOIN ValidationError ON vTempHtsPartnerTracingExtractError.Id = ValidationError.RecordId 
                        INNER JOIN Validator ON ValidationError.ValidatorId = Validator.Id");
            
            
            migrationBuilder.Sql(@"CREATE  view vTempHtsClientTracingExtractError as  SELECT * FROM TempHtsClientTracingExtracts WHERE   (CheckError = 1)");
            migrationBuilder.Sql(@"
                        CREATE  VIEW vTempHtsClientTracingExtractErrorSummary
                        AS
                        SELECT ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, 
                        ValidationError.RecordId, 
								vTempHtsClientTracingExtractError.FacilityName,
		                        vTempHtsClientTracingExtractError.HtsNumber,
		                        vTempHtsClientTracingExtractError.PatientPK,
		                        vTempHtsClientTracingExtractError.SiteCode,
		                        vTempHtsClientTracingExtractError.TracingType,
		                        vTempHtsClientTracingExtractError.TracingDate,
		                        vTempHtsClientTracingExtractError.TracingOutcome,
							vTempHtsClientTracingExtractError.Date_Created,
							vTempHtsClientTracingExtractError.Date_Last_Modified,	
					vTempHtsClientTracingExtractError.RecordUUID,
					vTempHtsClientTracingExtractError.Voided
                        FROM vTempHtsClientTracingExtractError 
                        INNER JOIN ValidationError ON vTempHtsClientTracingExtractError.Id = ValidationError.RecordId 
                        INNER JOIN Validator ON ValidationError.ValidatorId = Validator.Id");
            
            
            migrationBuilder.Sql(@"CREATE  view vTempHtsTestKitsExtractError as SELECT  *  FROM    TempHtsTestKitsExtracts WHERE   (CheckError = 1)");
            migrationBuilder.Sql(@"
                        CREATE  VIEW vTempHtsTestKitsExtractErrorSummary
                        AS
                        SELECT ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, 
                        ValidationError.RecordId, 
                        vTempHtsTestKitsExtractError.FacilityName,
                        vTempHtsTestKitsExtractError.HtsNumber,
                        vTempHtsTestKitsExtractError.PatientPK,
                        vTempHtsTestKitsExtractError.SiteCode,
                        vTempHtsTestKitsExtractError.EncounterId,
                        vTempHtsTestKitsExtractError.TestKitName1,
                        vTempHtsTestKitsExtractError.TestKitLotNumber1,
                        vTempHtsTestKitsExtractError.TestKitExpiry1,
                        vTempHtsTestKitsExtractError.TestResult1,
                        vTempHtsTestKitsExtractError.TestKitName2,
                        vTempHtsTestKitsExtractError.TestKitLotNumber2,
                        vTempHtsTestKitsExtractError.TestKitExpiry2,
                        vTempHtsTestKitsExtractError.TestResult2,
						vTempHtsTestKitsExtractError.Date_Created,
						vTempHtsTestKitsExtractError.Date_Last_Modified,
						vTempHtsTestKitsExtractError.SyphilisResult,	
					vTempHtsTestKitsExtractError.RecordUUID,
					vTempHtsTestKitsExtractError.Voided
                        FROM vTempHtsTestKitsExtractError 
                        INNER JOIN ValidationError ON vTempHtsTestKitsExtractError.Id = ValidationError.RecordId 
                        INNER JOIN Validator ON ValidationError.ValidatorId = Validator.Id");
            

			migrationBuilder.Sql(@"CREATE  view vTempHtsPartnerNotificationServicesExtractError as SELECT  * FROM TempHtsPartnerNotificationServicesExtracts WHERE   (CheckError = 1)");

            migrationBuilder.Sql(@"
                        CREATE  VIEW vTempHtsPartnerNotificationServicesExtractErrorSummary
                        AS
                        SELECT ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, 
                        ValidationError.RecordId, 
                        vTempHtsPartnerNotificationServicesExtractError.FacilityName,
                        vTempHtsPartnerNotificationServicesExtractError.HtsNumber,
                        vTempHtsPartnerNotificationServicesExtractError.PatientPK,
                        vTempHtsPartnerNotificationServicesExtractError.SiteCode,
                        vTempHtsPartnerNotificationServicesExtractError.PartnerPatientPk,
                        vTempHtsPartnerNotificationServicesExtractError.PartnerPersonID,
                        vTempHtsPartnerNotificationServicesExtractError.Age,
                        vTempHtsPartnerNotificationServicesExtractError.Sex,
                        vTempHtsPartnerNotificationServicesExtractError.RelationsipToIndexClient,
                        vTempHtsPartnerNotificationServicesExtractError.ScreenedForIpv,
                        vTempHtsPartnerNotificationServicesExtractError.IpvScreeningOutcome,
                        vTempHtsPartnerNotificationServicesExtractError.CurrentlyLivingWithIndexClient,
                        vTempHtsPartnerNotificationServicesExtractError.KnowledgeOfHivStatus,
                        vTempHtsPartnerNotificationServicesExtractError.PnsApproach,
                        vTempHtsPartnerNotificationServicesExtractError.PnsConsent,
                        vTempHtsPartnerNotificationServicesExtractError.LinkedToCare,
                        vTempHtsPartnerNotificationServicesExtractError.LinkDateLinkedToCare,
                        vTempHtsPartnerNotificationServicesExtractError.CccNumber,
                        vTempHtsPartnerNotificationServicesExtractError.FacilityLinkedTo,
                        vTempHtsPartnerNotificationServicesExtractError.Dob,
                        vTempHtsPartnerNotificationServicesExtractError.DateElicited,
                        vTempHtsPartnerNotificationServicesExtractError.MaritalStatus,
						vTempHtsPartnerNotificationServicesExtractError.Date_Created,
						vTempHtsPartnerNotificationServicesExtractError.Date_Last_Modified,	
					vTempHtsPartnerNotificationServicesExtractError.RecordUUID,
					vTempHtsPartnerNotificationServicesExtractError.Voided
                        FROM vTempHtsPartnerNotificationServicesExtractError 
                        INNER JOIN ValidationError ON vTempHtsPartnerNotificationServicesExtractError.Id = ValidationError.RecordId 
                        INNER JOIN Validator ON ValidationError.ValidatorId = Validator.Id");

            
             migrationBuilder.Sql(@"CREATE  view vTempHtsClientTestsExtractError as SELECT  * FROM    TempHtsClientTestsExtracts WHERE   (CheckError = 1)");
             migrationBuilder.Sql(@"
                        CREATE  VIEW vTempHtsClientTestsExtractErrorSummary
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
		                vTempHtsClientTestsExtractError.ReferredForServices,
		                vTempHtsClientTestsExtractError.ReferredServices,
		                vTempHtsClientTestsExtractError.OtherReferredServices,	
					vTempHtsClientTestsExtractError.RecordUUID,
					vTempHtsClientTestsExtractError.Voided

                        FROM vTempHtsClientTestsExtractError 
                        INNER JOIN ValidationError ON vTempHtsClientTestsExtractError.Id = ValidationError.RecordId 
                        INNER JOIN Validator ON ValidationError.ValidatorId = Validator.Id");
             
             
             migrationBuilder.Sql(@"CREATE  view vTempHtsEligibilityExtractError as SELECT * FROM TempHtsEligibilityExtracts WHERE (CheckError = 1)");

	        migrationBuilder.Sql(@"
				CREATE  VIEW vTempHtsEligibilityExtractErrorSummary
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
						vTempHtsEligibilityExtractError.HtsRiskScore,	
					vTempHtsEligibilityExtractError.RecordUUID,
					vTempHtsEligibilityExtractError.Voided

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
