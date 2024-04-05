using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class AddPersonBPatientPkToRelationshipsViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"alter view vTempRelationshipsExtractError as SELECT * FROM TempRelationshipsExtracts WHERE (CheckError = 1)");

            migrationBuilder.Sql(@"
				alter VIEW vTempRelationshipsExtractErrorSummary
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
							vTempRelationshipsExtractError.PatientRelationshipToOther,
							vTempRelationshipsExtractError.PersonAPatientPk,
							vTempRelationshipsExtractError.PersonBPatientPk,							
							vTempRelationshipsExtractError.RecordUUID,
							vTempRelationshipsExtractError.Voided,
							vTempRelationshipsExtractError.Date_Created,
							vTempRelationshipsExtractError.Date_Last_Modified			

				FROM            vTempRelationshipsExtractError INNER JOIN
					ValidationError ON vTempRelationshipsExtractError.Id = ValidationError.RecordId INNER JOIN
					Validator ON ValidationError.ValidatorId = Validator.Id
                ");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
