using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class initialViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create view vTempPatientExtractError as
SELECT        *
FROM            TempPatientExtracts
WHERE        (CheckError = 1)");
            migrationBuilder.Sql(@"CREATE VIEW vTempPatientExtractErrorSummary
AS
SELECT         ValidationError.Id,  Validator.Extract,  Validator.Field,  Validator.Type,  Validator.Summary,  ValidationError.DateGenerated,  vTempPatientExtractError.PatientPK, vTempPatientExtractError.FacilityId,
                          vTempPatientExtractError.PatientID,  vTempPatientExtractError.SiteCode,  vTempPatientExtractError.FacilityName,  ValidationError.RecordId
FROM             vTempPatientExtractError INNER JOIN
                          ValidationError ON  vTempPatientExtractError.Id =  ValidationError.RecordId INNER JOIN
                          Validator ON  ValidationError.ValidatorId =  Validator.Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW vTempPatientExtractErrorSummary");
            migrationBuilder.Sql(@"DROP VIEW vTempPatientExtractError");
        }
    }
}
