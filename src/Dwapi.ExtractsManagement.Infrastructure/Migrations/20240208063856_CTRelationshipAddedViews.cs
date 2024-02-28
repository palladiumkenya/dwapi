using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class CTRelationshipAddedViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"alter table RelationshipsExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"alter table TempRelationshipsExtracts convert to character set utf8 collate utf8_unicode_ci;");
            }

            migrationBuilder.Sql(@"create view vTempRelationshipsExtractError as SELECT * FROM TempRelationshipsExtracts WHERE (CheckError = 1)");

            migrationBuilder.Sql(@"
				CREATE VIEW vTempRelationshipsExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,

							vTempRelationshipsExtractError.PatientPK,
							vTempRelationshipsExtractError.SiteCode,
							vTempRelationshipsExtractError.PatientID,
							vTempRelationshipsExtractError.FacilityID,
							vTempRelationshipsExtractError.Emr,
							vTempRelationshipsExtractError.Project,
							vTempRelationshipsExtractError.FacilityName,

							vTempRelationshipsExtractError.RelationshipToPatient,
							vTempRelationshipsExtractError.StartDate,
							vTempRelationshipsExtractError.EndDate,

							
							vTempRelationshipsExtractError.RecordUUID,
							vTempRelationshipsExtractError.Voided,
							vTempRelationshipsExtractError.Date_Created,
							vTempRelationshipsExtractError.Date_Last_Modified			

				FROM            vTempRelationshipsExtractError INNER JOIN
					ValidationError ON vTempRelationshipsExtractError.Id = ValidationError.RecordId INNER JOIN
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
							vTempPatientVisitExtractError.RecordUUID,
							vTempPatientVisitExtractError.Voided,
							vTempPatientVisitExtractError.WHOStagingOI

				FROM            vTempPatientVisitExtractError INNER JOIN
										 ValidationError ON vTempPatientVisitExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
				 
				 
				 
				 migrationBuilder.Sql(@"ALTER view vTempHtsPartnerNotificationServicesExtractError as SELECT  * FROM TempHtsPartnerNotificationServicesExtracts WHERE   (CheckError = 1)");
				 migrationBuilder.Sql(@"
                        ALTER VIEW vTempHtsPartnerNotificationServicesExtractErrorSummary
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
					vTempHtsPartnerNotificationServicesExtractError.Voided,
					vTempHtsPartnerNotificationServicesExtractError.IndexPatientPk
                        FROM vTempHtsPartnerNotificationServicesExtractError 
                        INNER JOIN ValidationError ON vTempHtsPartnerNotificationServicesExtractError.Id = ValidationError.RecordId 
                        INNER JOIN Validator ON ValidationError.ValidatorId = Validator.Id");

            
				 
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
