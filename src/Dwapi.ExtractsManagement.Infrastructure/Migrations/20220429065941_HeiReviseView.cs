using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HeiReviseView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
	ALTER VIEW vTempHeiExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,
vTempHeiExtractError.BasellineVL,
vTempHeiExtractError.BasellineVLDate,
vTempHeiExtractError.ConfirmatoryPCR,
vTempHeiExtractError.ConfirmatoryPCRDate,
vTempHeiExtractError.Date_Created,
vTempHeiExtractError.Date_Last_Modified,
vTempHeiExtractError.DateExtracted,
vTempHeiExtractError.DNAPCR1,
vTempHeiExtractError.DNAPCR1Date,
vTempHeiExtractError.DNAPCR2,
vTempHeiExtractError.DNAPCR2Date,
vTempHeiExtractError.DNAPCR3,
vTempHeiExtractError.DNAPCR3Date,
vTempHeiExtractError.Emr,
vTempHeiExtractError.ErrorType,
vTempHeiExtractError.FacilityId,
vTempHeiExtractError.FacilityName,
vTempHeiExtractError.FinalyAntibody,
vTempHeiExtractError.FinalyAntibodyDate,
vTempHeiExtractError.HEIExitCritearia,
vTempHeiExtractError.HEIExitDate,
vTempHeiExtractError.HEIHIVStatus,
vTempHeiExtractError.PatientID,
vTempHeiExtractError.PatientMnchID,
vTempHeiExtractError.PatientPK,
vTempHeiExtractError.Project,
vTempHeiExtractError.SiteCode

                        
	FROM            vTempHeiExtractError INNER JOIN
							 ValidationError ON vTempHeiExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
