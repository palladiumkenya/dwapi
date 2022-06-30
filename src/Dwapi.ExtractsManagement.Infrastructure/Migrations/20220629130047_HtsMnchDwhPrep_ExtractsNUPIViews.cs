using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HtsMnchDwhPrep_ExtractsNUPIViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
              migrationBuilder.Sql(@"alter view vTempPatientExtractError as SELECT * FROM TempPatientExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"

						ALTER VIEW vTempPatientExtractErrorSummary
						AS
						SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, ValidationError.RecordId,

					vTempPatientExtractError.FacilityName 
				        vTempPatientExtractError.Gender 
				        vTempPatientExtractError.DOB 
				        vTempPatientExtractError.RegistrationDate 
				        vTempPatientExtractError.RegistrationAtCCC 
				        vTempPatientExtractError.RegistrationATPMTCT 
				        vTempPatientExtractError.RegistrationAtTBClinic 
				        vTempPatientExtractError.PatientSource 
				        vTempPatientExtractError.Region 
				        vTempPatientExtractError.District 
				        vTempPatientExtractError.Village 
				        vTempPatientExtractError.ContactRelation 
				        vTempPatientExtractError.LastVisit 
				        vTempPatientExtractError.MaritalStatus 
				        vTempPatientExtractError.EducationLevel 
				        vTempPatientExtractError.DateConfirmedHIVPositive 
				        vTempPatientExtractError.PreviousARTExposure 
				        vTempPatientExtractError.DatePreviousARTStart 
				        vTempPatientExtractError.StatusAtCCC 
				        vTempPatientExtractError.StatusAtPMTCT 
				        vTempPatientExtractError.StatusAtTBClinic 

				        vTempPatientExtractError.Orphan 
				        vTempPatientExtractError.Inschool 
				        vTempPatientExtractError.PatientType 
				        vTempPatientExtractError.PopulationType 
				        vTempPatientExtractError.KeyPopulationType 
				        vTempPatientExtractError.PatientResidentCounty 
				        vTempPatientExtractError.PatientResidentSubCounty 
				        vTempPatientExtractError.PatientResidentLocation 
				        vTempPatientExtractError.PatientResidentSubLocation 
				        vTempPatientExtractError.PatientResidentWard 
				        vTempPatientExtractError.PatientResidentVillage 
				        vTempPatientExtractError.TransferInDate 
				        vTempPatientExtractError.Date_Created 
				        vTempPatientExtractError.Date_Last_Modified 
				        vTempPatientExtractError.Pkv 
				        vTempPatientExtractError.Occupation 
				        vTempPatientExtractError.NUPI 
									 
					FROM            vTempPatientExtractError INNER JOIN
										 ValidationError ON vTempPatientExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
