using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class CTREview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Breastfeeding",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CTXAdherence",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClinicalNotes",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentRegimen",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EverHadMenses",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HCWConcern",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Menopausal",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Muac",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoFPReason",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NutritionalStatus",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OxygenSaturation",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProphylaxisUsed",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PulseRate",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RespiratoryRate",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TCAReason",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Temp",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitBy",
                table: "TempPatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TOVerified",
                table: "TempPatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TOVerifiedDate",
                table: "TempPatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegimenChangeSwitchReason",
                table: "TempPatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegimenChangedSwitched",
                table: "TempPatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StopRegimenDate",
                table: "TempPatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StopRegimenReason",
                table: "TempPatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSampleTaken",
                table: "TempPatientLaboratoryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SampleType",
                table: "TempPatientLaboratoryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "TempPatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pkv",
                table: "TempPatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Breastfeeding",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CTXAdherence",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClinicalNotes",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentRegimen",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EverHadMenses",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HCWConcern",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Menopausal",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Muac",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoFPReason",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NutritionalStatus",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OxygenSaturation",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProphylaxisUsed",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PulseRate",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RespiratoryRate",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TCAReason",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Temp",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitBy",
                table: "PatientVisitExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TOVerified",
                table: "PatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TOVerifiedDate",
                table: "PatientStatusExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegimenChangeSwitchReason",
                table: "PatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegimenChangedSwitched",
                table: "PatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StopRegimenDate",
                table: "PatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StopRegimenReason",
                table: "PatientPharmacyExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSampleTaken",
                table: "PatientLaboratoryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SampleType",
                table: "PatientLaboratoryExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "PatientExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pkv",
                table: "PatientExtracts",
                nullable: true);

            if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 0;");

                migrationBuilder.Sql(
                    @"alter table PatientArtExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(
                    @"alter table PatientExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(
                    @"alter table PatientLaboratoryExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(
                    @"alter table PatientPharmacyExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(
                    @"alter table PatientStatusExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(
                    @"alter table PatientVisitExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(
                    @"alter table TempPatientArtExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(
                    @"alter table TempPatientExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(
                    @"alter table TempPatientLaboratoryExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(
                    @"alter table TempPatientPharmacyExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(
                    @"alter table TempPatientStatusExtracts convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(
                    @"alter table TempPatientVisitExtracts convert to character set utf8 collate utf8_unicode_ci;");

                migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 1;");


                migrationBuilder.Sql(@"
	            ALTER view vTempPatientLaboratoryExtractError as
	            SELECT        *
	            FROM            TempPatientLaboratoryExtracts
	            WHERE        (CheckError = 1)
                            ");

                migrationBuilder.Sql(@"
	            ALTER view vTempPatientLaboratoryExtractErrorSummary
	            AS
	            SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientLaboratoryExtractError.PatientPK,vTempPatientLaboratoryExtractError.FacilityId,
							             vTempPatientLaboratoryExtractError.PatientID, vTempPatientLaboratoryExtractError.SiteCode, vTempPatientLaboratoryExtractError.FacilityName, ValidationError.RecordId,

							             vTempPatientLaboratoryExtractError.OrderedByDate, 
							             vTempPatientLaboratoryExtractError.ReportedByDate, 
							             vTempPatientLaboratoryExtractError.TestName, 
							             vTempPatientLaboratoryExtractError.EnrollmentTest, 
							             vTempPatientLaboratoryExtractError.TestResult, 
                                         vTempPatientLaboratoryExtractError.Reason, 
							             vTempPatientLaboratoryExtractError.VisitId

	            FROM            vTempPatientLaboratoryExtractError INNER JOIN
							             ValidationError ON vTempPatientLaboratoryExtractError.Id = ValidationError.RecordId INNER JOIN
							             Validator ON ValidationError.ValidatorId = Validator.Id
                            ");


                //Patient Extract Errors

            migrationBuilder.Sql(@"
                        create view vTempPatientExtractError as
                        SELECT  *
                        FROM    TempPatientExtracts
                        WHERE   (CheckError = 1)");

            migrationBuilder.Sql(@"
                        CREATE VIEW vTempPatientExtractErrorSummary
                        AS
                        SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientExtractError.PatientPK,vTempPatientExtractError.FacilityId,
                                                 vTempPatientExtractError.PatientID, vTempPatientExtractError.SiteCode, vTempPatientExtractError.FacilityName, ValidationError.RecordId
                        FROM            vTempPatientExtractError INNER JOIN
                                                 ValidationError ON vTempPatientExtractError.Id = ValidationError.RecordId INNER JOIN
                                                 Validator ON ValidationError.ValidatorId = Validator.Id");

            //Patient ART Errors

            migrationBuilder.Sql(@"
	create view vTempPatientArtExtractError as
	SELECT        *
	FROM            TempPatientArtExtracts
	WHERE        (CheckError = 1)
                ");
            migrationBuilder.Sql(@"
	CREATE VIEW vTempPatientArtExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientArtExtractError.PatientPK,vTempPatientArtExtractError.FacilityId,
							 vTempPatientArtExtractError.PatientID, vTempPatientArtExtractError.SiteCode, vTempPatientArtExtractError.FacilityName, ValidationError.RecordId,
						 
							 vTempPatientArtExtractError.DOB, 
							 vTempPatientArtExtractError.Gender, 
							 vTempPatientArtExtractError.PatientSource, 
							 vTempPatientArtExtractError.RegistrationDate, 
							 vTempPatientArtExtractError.AgeLastVisit, 
							 vTempPatientArtExtractError.PreviousARTStartDate, 
							 vTempPatientArtExtractError.PreviousARTRegimen, 
							 vTempPatientArtExtractError.StartARTAtThisFacility, 
							 vTempPatientArtExtractError.StartARTDate, 
							 vTempPatientArtExtractError.StartRegimen, 
							 vTempPatientArtExtractError.StartRegimenLine, 
							 vTempPatientArtExtractError.LastARTDate, 
							 vTempPatientArtExtractError.LastRegimen, 
							 vTempPatientArtExtractError.LastRegimenLine, 
							 vTempPatientArtExtractError.LastVisit, 
							 vTempPatientArtExtractError.ExitReason, 
							 vTempPatientArtExtractError.ExitDate
	FROM            vTempPatientArtExtractError INNER JOIN
							 ValidationError ON vTempPatientArtExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");


             // Patient Pharmacy Errors

            migrationBuilder.Sql(@"
	create view vTempPatientPharmacyExtractError as
	SELECT        *
	FROM            TempPatientPharmacyExtracts
	WHERE        (CheckError = 1)
                ");
            migrationBuilder.Sql(@"
	CREATE VIEW vTempPatientPharmacyExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientPharmacyExtractError.PatientPK,vTempPatientPharmacyExtractError.FacilityId,
							 vTempPatientPharmacyExtractError.PatientID, vTempPatientPharmacyExtractError.SiteCode,  ValidationError.RecordId,

							 vTempPatientPharmacyExtractError.Drug, 
							 vTempPatientPharmacyExtractError.DispenseDate,
							 vTempPatientPharmacyExtractError.Duration, 
							 vTempPatientPharmacyExtractError.ExpectedReturn, 
							 vTempPatientPharmacyExtractError.TreatmentType, 
							 vTempPatientPharmacyExtractError.RegimenLine, 
							 vTempPatientPharmacyExtractError.PeriodTaken, 
							 vTempPatientPharmacyExtractError.ProphylaxisType, 
							 vTempPatientPharmacyExtractError.Provider, 
							 vTempPatientPharmacyExtractError.VisitID

	FROM            vTempPatientPharmacyExtractError INNER JOIN
							 ValidationError ON vTempPatientPharmacyExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

            //Patient Status Errors

            migrationBuilder.Sql(@"
	create view vTempPatientStatusExtractError as
	SELECT        *
	FROM            TempPatientStatusExtracts
	WHERE        (CheckError = 1)
                ");
            migrationBuilder.Sql(@"
	CREATE VIEW vTempPatientStatusExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientStatusExtractError.PatientPK,vTempPatientStatusExtractError.FacilityId,
							 vTempPatientStatusExtractError.PatientID, vTempPatientStatusExtractError.SiteCode, vTempPatientStatusExtractError.FacilityName, ValidationError.RecordId,

							 vTempPatientStatusExtractError.ExitDescription, 
							 vTempPatientStatusExtractError.ExitDate, 
							 vTempPatientStatusExtractError.ExitReason

	FROM            vTempPatientStatusExtractError INNER JOIN
							 ValidationError ON vTempPatientStatusExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

            //Patient visit errors

            migrationBuilder.Sql(@"
	create view vTempPatientVisitExtractError as
	SELECT        *
	FROM            TempPatientVisitExtracts
	WHERE        (CheckError = 1)
                ");
            migrationBuilder.Sql(@"

	CREATE VIEW vTempPatientVisitExtractErrorSummary
	AS
	SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, vTempPatientVisitExtractError.PatientPK,vTempPatientVisitExtractError.FacilityId,
							 vTempPatientVisitExtractError.PatientID, vTempPatientVisitExtractError.SiteCode, vTempPatientVisitExtractError.FacilityName, ValidationError.RecordId,

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
							 vTempPatientVisitExtractError.VisitId

	FROM            vTempPatientVisitExtractError INNER JOIN
							 ValidationError ON vTempPatientVisitExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Breastfeeding",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "CTXAdherence",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "ClinicalNotes",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "CurrentRegimen",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "EverHadMenses",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "HCWConcern",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Menopausal",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Muac",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "NoFPReason",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "NutritionalStatus",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "OxygenSaturation",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "ProphylaxisUsed",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "PulseRate",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "RespiratoryRate",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "TCAReason",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Temp",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "VisitBy",
                table: "TempPatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "TOVerified",
                table: "TempPatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "TOVerifiedDate",
                table: "TempPatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "RegimenChangeSwitchReason",
                table: "TempPatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "RegimenChangedSwitched",
                table: "TempPatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "StopRegimenDate",
                table: "TempPatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "StopRegimenReason",
                table: "TempPatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "DateSampleTaken",
                table: "TempPatientLaboratoryExtracts");

            migrationBuilder.DropColumn(
                name: "SampleType",
                table: "TempPatientLaboratoryExtracts");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "TempPatientExtracts");

            migrationBuilder.DropColumn(
                name: "Pkv",
                table: "TempPatientExtracts");

            migrationBuilder.DropColumn(
                name: "Breastfeeding",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "CTXAdherence",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "ClinicalNotes",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "CurrentRegimen",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "EverHadMenses",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "HCWConcern",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Menopausal",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Muac",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "NoFPReason",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "NutritionalStatus",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "OxygenSaturation",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "ProphylaxisUsed",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "PulseRate",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "RespiratoryRate",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "TCAReason",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "Temp",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "VisitBy",
                table: "PatientVisitExtracts");

            migrationBuilder.DropColumn(
                name: "TOVerified",
                table: "PatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "TOVerifiedDate",
                table: "PatientStatusExtracts");

            migrationBuilder.DropColumn(
                name: "RegimenChangeSwitchReason",
                table: "PatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "RegimenChangedSwitched",
                table: "PatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "StopRegimenDate",
                table: "PatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "StopRegimenReason",
                table: "PatientPharmacyExtracts");

            migrationBuilder.DropColumn(
                name: "DateSampleTaken",
                table: "PatientLaboratoryExtracts");

            migrationBuilder.DropColumn(
                name: "SampleType",
                table: "PatientLaboratoryExtracts");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "PatientExtracts");

            migrationBuilder.DropColumn(
                name: "Pkv",
                table: "PatientExtracts");
        }
    }
}
