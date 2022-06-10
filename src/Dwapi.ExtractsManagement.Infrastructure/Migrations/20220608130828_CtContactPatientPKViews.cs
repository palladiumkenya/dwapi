using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class CtContactPatientPKViews : Migration
    {
         protected override void Up(MigrationBuilder migrationBuilder)
        {
	        migrationBuilder.Sql(@"alter view vTempContactListingExtractError as SELECT * FROM vTempContactListingExtracts WHERE (CheckError = 1)");
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
					vTempContactListingExtractError.ContactPatientPK
					 
					FROM vTempContactListingExtractError INNER JOIN
							 ValidationError ON vTempContactListingExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
