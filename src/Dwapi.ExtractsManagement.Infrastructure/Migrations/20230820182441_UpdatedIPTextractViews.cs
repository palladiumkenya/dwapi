using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class UpdatedIPTextractViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	        migrationBuilder.Sql(@"ALTER view vTempIptExtractError as SELECT * FROM TempIptExtracts WHERE (CheckError = 1)");

	        migrationBuilder.Sql(@"

						ALTER VIEW vTempIptExtractErrorSummary
						AS
						SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,

					vTempIptExtractError.PatientPK,
					vTempIptExtractError.PatientID,
					vTempIptExtractError.FacilityId,
					vTempIptExtractError.SiteCode,
					vTempIptExtractError.DateExtracted,
					vTempIptExtractError.Emr,
					vTempIptExtractError.Project,
					vTempIptExtractError.CheckError,
					vTempIptExtractError.ErrorType,
					vTempIptExtractError.FacilityName,
					vTempIptExtractError.VisitID,
					vTempIptExtractError.VisitDate,
					vTempIptExtractError.OnTBDrugs,
					vTempIptExtractError.OnIPT,
					vTempIptExtractError.EverOnIPT,
					vTempIptExtractError.Cough,
					vTempIptExtractError.Fever,
					vTempIptExtractError.NoticeableWeightLoss,
					vTempIptExtractError.NightSweats,
					vTempIptExtractError.Lethargy,
					vTempIptExtractError.ICFActionTaken,
					vTempIptExtractError.TestResult,
					vTempIptExtractError.TBClinicalDiagnosis,
					vTempIptExtractError.ContactsInvited,
					vTempIptExtractError.EvaluatedForIPT,
					vTempIptExtractError.StartAntiTBs,
					vTempIptExtractError.TBRxStartDate,
					vTempIptExtractError.TBScreening,
					vTempIptExtractError.IPTClientWorkUp,
					vTempIptExtractError.StartIPT,
					vTempIptExtractError.IndicationForIPT,
					vTempIptExtractError.Date_Created,
					vTempIptExtractError.Date_Last_Modified,
					vTempIptExtractError.RecordUUID	,
					vTempIptExtractError.TPTInitiationDate	,
					vTempIptExtractError.IPTDiscontinuation	,
					vTempIptExtractError.DateOfDiscontinuation	


						FROM            vTempIptExtractError INNER JOIN
												 ValidationError ON vTempIptExtractError.Id = ValidationError.RecordId INNER JOIN
												 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
