using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HtsClientsPkvViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
 
            migrationBuilder.Sql(@"alter view vTempHtsClientsExtractError as SELECT * FROM TempHtsClientsExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"

						ALTER VIEW vTempHtsClientsExtractErrorSummary
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
						vTempHtsClientsExtractError.NUPI,
						vTempHtsClientsExtractError.Pkv
                        FROM            vTempHtsClientsExtractError INNER JOIN
										 ValidationError ON vTempHtsClientsExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
