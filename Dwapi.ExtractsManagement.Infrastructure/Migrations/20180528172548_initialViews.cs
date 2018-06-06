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
SELECT        dbo.ValidationError.Id, dbo.Validator.Extract, dbo.Validator.Field, dbo.Validator.Type, dbo.Validator.Summary, dbo.ValidationError.DateGenerated, dbo.vTempPatientExtractError.PatientPK,dbo.vTempPatientExtractError.FacilityId,
                         dbo.vTempPatientExtractError.PatientID, dbo.vTempPatientExtractError.SiteCode, dbo.vTempPatientExtractError.FacilityName, dbo.ValidationError.RecordId
FROM            dbo.vTempPatientExtractError INNER JOIN
                         dbo.ValidationError ON dbo.vTempPatientExtractError.Id = dbo.ValidationError.RecordId INNER JOIN
                         dbo.Validator ON dbo.ValidationError.ValidatorId = dbo.Validator.Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW vTempPatientExtractErrorSummary");
            migrationBuilder.Sql(@"DROP VIEW vTempPatientExtractError");
        }
    }
}
