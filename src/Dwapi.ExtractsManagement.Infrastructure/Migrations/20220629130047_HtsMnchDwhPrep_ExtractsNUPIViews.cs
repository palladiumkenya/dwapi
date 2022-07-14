using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class HtsMnchDwhPrep_ExtractsNUPIViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	        migrationBuilder.Sql(@"alter view vTempPatientPrepExtractError as SELECT * FROM TempPatientPrepExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"
						ALTER VIEW vTempPatientPrepExtractErrorSummary
						AS
						SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, ValidationError.RecordId,

					vTempPatientPrepExtractError.PatientPK,
								vTempPatientPrepExtractError.FacilityId,
								vTempPatientPrepExtractError.PatientID, 
								vTempPatientPrepExtractError.SiteCode,
								vTempPatientPrepExtractError.Emr,
								vTempPatientPrepExtractError.Project,

								vTempPatientPrepExtractError.PrepNumber,
								vTempPatientPrepExtractError.HtsNumber,
								vTempPatientPrepExtractError.PrepEnrollmentDate,
								vTempPatientPrepExtractError.Sex,
								vTempPatientPrepExtractError.DateofBirth,
								vTempPatientPrepExtractError.CountyofBirth,
								vTempPatientPrepExtractError.County,
								vTempPatientPrepExtractError.SubCounty,
								vTempPatientPrepExtractError.Location,
								vTempPatientPrepExtractError.LandMark,
								vTempPatientPrepExtractError.Ward,
								vTempPatientPrepExtractError.ClientType,
								vTempPatientPrepExtractError.ReferralPoint,
								vTempPatientPrepExtractError.MaritalStatus,
								vTempPatientPrepExtractError.Inschool,
								vTempPatientPrepExtractError.PopulationType,
								vTempPatientPrepExtractError.KeyPopulationType,
								vTempPatientPrepExtractError.Refferedfrom,
								vTempPatientPrepExtractError.TransferIn,
								vTempPatientPrepExtractError.TransferInDate,
								vTempPatientPrepExtractError.TransferFromFacility,
								vTempPatientPrepExtractError.DatefirstinitiatedinPrepCare,
								vTempPatientPrepExtractError.DateStartedPrEPattransferringfacility,
								vTempPatientPrepExtractError.ClientPreviouslyonPrep,
								vTempPatientPrepExtractError.PrevPrepReg,
								vTempPatientPrepExtractError.DateLastUsedPrev,

								vTempPatientPrepExtractError.Date_Created,
								vTempPatientPrepExtractError.Date_Last_Modified,
								vTempPatientPrepExtractError.NUPI
					 
					FROM            vTempPatientPrepExtractError INNER JOIN
										 ValidationError ON vTempPatientPrepExtractError.Id = ValidationError.RecordId INNER JOIN
										 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
            
            migrationBuilder.Sql(@"alter view vTempPatientMnchExtractError as SELECT * FROM TempPatientMnchExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"
						ALTER VIEW vTempPatientMnchExtractErrorSummary
						AS
						SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, ValidationError.RecordId,

						vTempPatientMnchExtractError.Date_Created,
						vTempPatientMnchExtractError.Date_Last_Modified,
						vTempPatientMnchExtractError.DateExtracted,
						vTempPatientMnchExtractError.DOB,
						vTempPatientMnchExtractError.EducationLevel,
						vTempPatientMnchExtractError.Emr,
						vTempPatientMnchExtractError.ErrorType,
						vTempPatientMnchExtractError.FacilityId,
						vTempPatientMnchExtractError.FacilityName,
						vTempPatientMnchExtractError.FirstEnrollmentAtMnch,
						vTempPatientMnchExtractError.Gender,

						vTempPatientMnchExtractError.InSchool,
						vTempPatientMnchExtractError.MaritalStatus,
						vTempPatientMnchExtractError.Occupation,
						vTempPatientMnchExtractError.PatientHeiID,
						vTempPatientMnchExtractError.PatientID,
						vTempPatientMnchExtractError.PatientMnchID,
						vTempPatientMnchExtractError.PatientPK,
						vTempPatientMnchExtractError.PatientResidentCounty,
						vTempPatientMnchExtractError.PatientResidentSubCounty,
						vTempPatientMnchExtractError.PatientResidentWard,
						vTempPatientMnchExtractError.Pkv,
						vTempPatientMnchExtractError.Project,
						vTempPatientMnchExtractError.SiteCode,
						vTempPatientMnchExtractError.NUPI
						             
				FROM            vTempPatientMnchExtractError INNER JOIN
							 ValidationError ON vTempPatientMnchExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");
            
            
              migrationBuilder.Sql(@"alter view vTempPatientExtractError as SELECT * FROM TempPatientExtracts WHERE (CheckError = 1)");
            migrationBuilder.Sql(@"

						ALTER VIEW vTempPatientExtractErrorSummary
						AS
						SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated, ValidationError.RecordId,

				        vTempPatientExtractError.PatientID,
					    vTempPatientExtractError.FacilityName, 
				        vTempPatientExtractError.Gender, 
				        vTempPatientExtractError.DOB, 
				        vTempPatientExtractError.RegistrationDate, 
				        vTempPatientExtractError.RegistrationAtCCC, 
				        vTempPatientExtractError.RegistrationATPMTCT, 
				        vTempPatientExtractError.RegistrationAtTBClinic, 
				        vTempPatientExtractError.PatientSource, 
				        vTempPatientExtractError.Region, 
				        vTempPatientExtractError.District, 
				        vTempPatientExtractError.Village, 
				        vTempPatientExtractError.ContactRelation, 
				        vTempPatientExtractError.LastVisit, 
				        vTempPatientExtractError.MaritalStatus, 
				        vTempPatientExtractError.EducationLevel, 
				        vTempPatientExtractError.DateConfirmedHIVPositive, 
				        vTempPatientExtractError.PreviousARTExposure,
				        vTempPatientExtractError.StatusAtCCC,
				        vTempPatientExtractError.StatusAtPMTCT,
				        vTempPatientExtractError.StatusAtTBClinic, 

				        vTempPatientExtractError.Orphan, 
				        vTempPatientExtractError.Inschool, 
				        vTempPatientExtractError.PatientType, 
				        vTempPatientExtractError.PopulationType, 
				        vTempPatientExtractError.KeyPopulationType, 
				        vTempPatientExtractError.PatientResidentCounty, 
				        vTempPatientExtractError.PatientResidentSubCounty, 
				        vTempPatientExtractError.PatientResidentLocation, 
				        vTempPatientExtractError.PatientResidentSubLocation, 
				        vTempPatientExtractError.PatientResidentWard ,
				        vTempPatientExtractError.PatientResidentVillage, 
				        vTempPatientExtractError.TransferInDate, 
				        vTempPatientExtractError.Date_Created,
				        vTempPatientExtractError.Date_Last_Modified, 
				        vTempPatientExtractError.Pkv,
				        vTempPatientExtractError.Occupation,
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
