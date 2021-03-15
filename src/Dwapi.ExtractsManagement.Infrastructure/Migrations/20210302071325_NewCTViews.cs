using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class NewCTViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create view vTempAllergiesChronicIllnessExtractError as SELECT * FROM TempAllergiesChronicIllnessExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"create view vTempIptExtractError as SELECT * FROM TempIptExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"create view vTempDepressionScreeningExtractError as SELECT * FROM TempDepressionScreeningExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"create view vTempContactListingExtractError as SELECT * FROM TempContactListingExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"create view vTempGbvScreeningExtractError as SELECT * FROM TempGbvScreeningExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"create view vTempEnhancedAdherenceCounsellingExtractError as SELECT * FROM TempEnhancedAdherenceCounsellingExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"create view vTempDrugAlcoholScreeningExtractError as SELECT * FROM TempDrugAlcoholScreeningExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"create view vTempOvcExtractError as SELECT * FROM TempOvcExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"create view vTempOtzExtractError as SELECT * FROM TempOtzExtracts WHERE (CheckError = 1)");


             migrationBuilder.Sql(@"

	CREATE VIEW vTempAllergiesChronicIllnessExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempAllergiesChronicIllnessExtractError.PatientPK,vTempAllergiesChronicIllnessExtractError.FacilityId,
							 vTempAllergiesChronicIllnessExtractError.PatientID, vTempAllergiesChronicIllnessExtractError.SiteCode, vTempAllergiesChronicIllnessExtractError.FacilityName, ValidationError.RecordId

	FROM            vTempAllergiesChronicIllnessExtractError INNER JOIN
							 ValidationError ON vTempAllergiesChronicIllnessExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");


             migrationBuilder.Sql(@"

	CREATE VIEW vTempContactListingExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempContactListingExtractError.PatientPK,vTempContactListingExtractError.FacilityId,
							 vTempContactListingExtractError.PatientID, vTempContactListingExtractError.SiteCode, vTempContactListingExtractError.FacilityName, ValidationError.RecordId

	FROM            vTempContactListingExtractError INNER JOIN
							 ValidationError ON vTempContactListingExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");


             migrationBuilder.Sql(@"

	CREATE VIEW vTempDepressionScreeningExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempDepressionScreeningExtractError.PatientPK,vTempDepressionScreeningExtractError.FacilityId,
							 vTempDepressionScreeningExtractError.PatientID, vTempDepressionScreeningExtractError.SiteCode, vTempDepressionScreeningExtractError.FacilityName, ValidationError.RecordId

	FROM            vTempDepressionScreeningExtractError INNER JOIN
							 ValidationError ON vTempDepressionScreeningExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");


             migrationBuilder.Sql(@"

	CREATE VIEW vTempDrugAlcoholScreeningExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempDrugAlcoholScreeningExtractError.PatientPK,vTempDrugAlcoholScreeningExtractError.FacilityId,
							 vTempDrugAlcoholScreeningExtractError.PatientID, vTempDrugAlcoholScreeningExtractError.SiteCode, vTempDrugAlcoholScreeningExtractError.FacilityName, ValidationError.RecordId

	FROM            vTempDrugAlcoholScreeningExtractError INNER JOIN
							 ValidationError ON vTempDrugAlcoholScreeningExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");


             migrationBuilder.Sql(@"

	CREATE VIEW vTempEnhancedAdherenceCounsellingExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempEnhancedAdherenceCounsellingExtractError.PatientPK,vTempEnhancedAdherenceCounsellingExtractError.FacilityId,
							 vTempEnhancedAdherenceCounsellingExtractError.PatientID, vTempEnhancedAdherenceCounsellingExtractError.SiteCode, vTempEnhancedAdherenceCounsellingExtractError.FacilityName, ValidationError.RecordId

	FROM            vTempEnhancedAdherenceCounsellingExtractError INNER JOIN
							 ValidationError ON vTempEnhancedAdherenceCounsellingExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");


             migrationBuilder.Sql(@"

	CREATE VIEW vTempGbvScreeningExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempGbvScreeningExtractError.PatientPK,vTempGbvScreeningExtractError.FacilityId,
							 vTempGbvScreeningExtractError.PatientID, vTempGbvScreeningExtractError.SiteCode, vTempGbvScreeningExtractError.FacilityName, ValidationError.RecordId

	FROM            vTempGbvScreeningExtractError INNER JOIN
							 ValidationError ON vTempGbvScreeningExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");


             migrationBuilder.Sql(@"

	CREATE VIEW vTempOtzExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempOtzExtractError.PatientPK,vTempOtzExtractError.FacilityId,
							 vTempOtzExtractError.PatientID, vTempOtzExtractError.SiteCode, vTempOtzExtractError.FacilityName, ValidationError.RecordId

	FROM            vTempOtzExtractError INNER JOIN
							 ValidationError ON vTempOtzExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");


             migrationBuilder.Sql(@"

	CREATE VIEW vTempOvcExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempOvcExtractError.PatientPK,vTempOvcExtractError.FacilityId,
							 vTempOvcExtractError.PatientID, vTempOvcExtractError.SiteCode, vTempOvcExtractError.FacilityName, ValidationError.RecordId

	FROM            vTempOvcExtractError INNER JOIN
							 ValidationError ON vTempOvcExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");


             migrationBuilder.Sql(@"

	CREATE VIEW vTempIptExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempIptExtractError.PatientPK,vTempIptExtractError.FacilityId,
							 vTempIptExtractError.PatientID, vTempIptExtractError.SiteCode, vTempIptExtractError.FacilityName, ValidationError.RecordId

	FROM            vTempIptExtractError INNER JOIN
							 ValidationError ON vTempIptExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
	        migrationBuilder.Sql(@"drop view vTempAllergiesChronicIllnessExtractErrorSummary");
	        migrationBuilder.Sql(@"drop view vTempIptExtractErrorSummary");
	        migrationBuilder.Sql(@"drop view vTempDepressionScreeningExtractErrorSummary");
	        migrationBuilder.Sql(@"drop view vTempContactListingExtractErrorSummary");
	        migrationBuilder.Sql(@"drop view vTempGbvScreeningExtractErrorSummary");
	        migrationBuilder.Sql(@"drop view vTempEnhancedAdherenceCounsellingExtractErrorSummary");
	        migrationBuilder.Sql(@"drop view vTempDrugAlcoholScreeningExtractErrorSummary");
	        migrationBuilder.Sql(@"drop view vTempOvcExtractErrorSummary");
	        migrationBuilder.Sql(@"drop view vTempOtzExtractErrorSummary");

	        migrationBuilder.Sql(@"drop view vTempAllergiesChronicIllnessExtractError");
	        migrationBuilder.Sql(@"drop view vTempIptExtractError");
	        migrationBuilder.Sql(@"drop view vTempDepressionScreeningExtractError");
	        migrationBuilder.Sql(@"drop view vTempContactListingExtractError");
	        migrationBuilder.Sql(@"drop view vTempGbvScreeningExtractError");
	        migrationBuilder.Sql(@"drop view vTempEnhancedAdherenceCounsellingExtractError");
	        migrationBuilder.Sql(@"drop view vTempDrugAlcoholScreeningExtractError");
	        migrationBuilder.Sql(@"drop view vTempOvcExtractError");
	        migrationBuilder.Sql(@"drop view vTempOtzExtractError");
        }
    }
}
