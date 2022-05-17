using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class CrsInitialViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create view vTempClientRegistryExtractError as SELECT * FROM TempClientRegistryExtracts WHERE (CheckError = 1)");

            		migrationBuilder.Sql(@"
				CREATE VIEW vTempClientRegistryExtractErrorSummary
				AS
				SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,

								vTempClientRegistryExtractError.AlienIdNo,
								vTempClientRegistryExtractError.AlternativePhoneNumber,
								vTempClientRegistryExtractError.BirthCertificateNumber,
								vTempClientRegistryExtractError.CCCNumber,
								vTempClientRegistryExtractError.County,
								vTempClientRegistryExtractError.DateOfBirth,
								vTempClientRegistryExtractError.DateOfInitiation,
								vTempClientRegistryExtractError.DateOfLastEncounter,
								vTempClientRegistryExtractError.DateOfLastViralLoad,
								vTempClientRegistryExtractError.DrivingLicenseNumber,
								vTempClientRegistryExtractError.Emr,
								vTempClientRegistryExtractError.FacilityId,
								vTempClientRegistryExtractError.FacilityName,
								vTempClientRegistryExtractError.FirstName,
								vTempClientRegistryExtractError.HighestLevelOfEducation,
								vTempClientRegistryExtractError.HudumaNumber,
								vTempClientRegistryExtractError.Landmark,
								vTempClientRegistryExtractError.LastName,
								vTempClientRegistryExtractError.LastRegimen,
								vTempClientRegistryExtractError.LastRegimenLine,
.								vTempClientRegistryExtractError.Location,
								vTempClientRegistryExtractError.MaritalStatus,
								vTempClientRegistryExtractError.MFLCode,
								vTempClientRegistryExtractError.MiddleName,
								vTempClientRegistryExtractError.NameOfNextOfKin,
								vTempClientRegistryExtractError.NationalId,
								vTempClientRegistryExtractError.NextAppointmentDate,
								vTempClientRegistryExtractError.NextOfKinRelationship,
								vTempClientRegistryExtractError.NextOfKinTelNo,
								vTempClientRegistryExtractError.Occupation,
								vTempClientRegistryExtractError.Passport,
								vTempClientRegistryExtractError.PatientClinicNumber,
								vTempClientRegistryExtractError.PatientPK,
								vTempClientRegistryExtractError.PhoneNumber,
								vTempClientRegistryExtractError.Project,
								vTempClientRegistryExtractError.Sex,
								vTempClientRegistryExtractError.SiteCode,
								vTempClientRegistryExtractError.SpousePhoneNumber,
								vTempClientRegistryExtractError.SubCounty,
								vTempClientRegistryExtractError.TreatmentOutcome,
								vTempClientRegistryExtractError.Village,
								vTempClientRegistryExtractError.Ward,
								vTempClientRegistryExtractError.CurrentOnART,
								vTempClientRegistryExtractError.DateOfHIVdiagnosis,
								vTempClientRegistryExtractError.LastViralLoadResult						


				FROM            vTempClientRegistryExtractError INNER JOIN
										 ValidationError ON vTempClientRegistryExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
