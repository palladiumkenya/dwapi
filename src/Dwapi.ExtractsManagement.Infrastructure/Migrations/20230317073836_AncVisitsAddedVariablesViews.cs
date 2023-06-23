using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class AncVisitsAddedVariablesViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	        migrationBuilder.Sql(@"alter view vTempAncVisitExtractError as SELECT * FROM TempAncVisitExtracts WHERE (CheckError = 1)");

            migrationBuilder.Sql(@"
							alter view vTempAncVisitExtractErrorSummary
							AS
							SELECT        ValidationError.Id, Validator.Extract, Validator.Field, Validator.Type, Validator.Summary, ValidationError.DateGenerated,ValidationError.RecordId,

						vTempAncVisitExtractError.ANCClinicNumber,
						vTempAncVisitExtractError.ANCVisitNo,
						vTempAncVisitExtractError.AntenatalExercises,
						vTempAncVisitExtractError.AZTBabyDispense,
						vTempAncVisitExtractError.BP,
						vTempAncVisitExtractError.BreastExam,
						vTempAncVisitExtractError.CACxScreen,
						vTempAncVisitExtractError.CACxScreenMethod,
						vTempAncVisitExtractError.ChronicIllness,
						vTempAncVisitExtractError.ClinicalNotes,
						vTempAncVisitExtractError.CounselledOn,
						vTempAncVisitExtractError.Date_Created,
						vTempAncVisitExtractError.Date_Last_Modified,
						vTempAncVisitExtractError.DateExtracted,
						vTempAncVisitExtractError.Deworming,
						vTempAncVisitExtractError.DiabetesTest,
						vTempAncVisitExtractError.Emr,
						vTempAncVisitExtractError.ErrorType,
						vTempAncVisitExtractError.FacilityId,
						vTempAncVisitExtractError.FacilityName,
						vTempAncVisitExtractError.FGM,
						vTempAncVisitExtractError.FGMComplications,
						vTempAncVisitExtractError.GestationWeeks,
						vTempAncVisitExtractError.Haemoglobin,
						vTempAncVisitExtractError.Height,
						vTempAncVisitExtractError.HIVStatusBeforeANC,
						vTempAncVisitExtractError.HIVTest1,
						vTempAncVisitExtractError.HIVTest1Result,
						vTempAncVisitExtractError.HIVTest2,
						vTempAncVisitExtractError.HIVTest2Result,
						vTempAncVisitExtractError.HIVTestFinalResult,
						vTempAncVisitExtractError.HIVTestingDone,
						vTempAncVisitExtractError.HIVTestType,
						vTempAncVisitExtractError.IronSupplementsGiven,
						vTempAncVisitExtractError.MalariaProphylaxis,
						vTempAncVisitExtractError.MotherGivenHAART,
						vTempAncVisitExtractError.MotherProphylaxisGiven,
						vTempAncVisitExtractError.MUAC,
						vTempAncVisitExtractError.NextAppointmentANC,
						vTempAncVisitExtractError.NVPBabyDispense,
						vTempAncVisitExtractError.OxygenSaturation,
						vTempAncVisitExtractError.PartnerHIVStatusANC,
						vTempAncVisitExtractError.PartnerHIVTestingANC,
						vTempAncVisitExtractError.PatientID,
						vTempAncVisitExtractError.PatientMnchID,
						vTempAncVisitExtractError.PatientPK,
						vTempAncVisitExtractError.PostParturmFP,
						vTempAncVisitExtractError.PreventiveServices,
						vTempAncVisitExtractError.Project,
						vTempAncVisitExtractError.PulseRate,
						vTempAncVisitExtractError.ReceivedMosquitoNet,
						vTempAncVisitExtractError.ReferralReasons,
						vTempAncVisitExtractError.ReferredFrom,
						vTempAncVisitExtractError.ReferredTo,
						vTempAncVisitExtractError.RespiratoryRate,
						vTempAncVisitExtractError.SiteCode,
						vTempAncVisitExtractError.SyphilisTestDone,
						vTempAncVisitExtractError.SyphilisTestResults,
						vTempAncVisitExtractError.SyphilisTestType,
						vTempAncVisitExtractError.SyphilisTreated,
						vTempAncVisitExtractError.SyphilisTreatment,
						vTempAncVisitExtractError.TBScreening,
						vTempAncVisitExtractError.Temp,
						vTempAncVisitExtractError.TetanusDose,
						vTempAncVisitExtractError.UrinalysisVariables,
						vTempAncVisitExtractError.VisitDate,
						vTempAncVisitExtractError.VisitID,
						vTempAncVisitExtractError.VLDate,
						vTempAncVisitExtractError.VLResult,
						vTempAncVisitExtractError.VLSampleTaken,
						vTempAncVisitExtractError.Weight,
						vTempAncVisitExtractError.WHOStaging,
						vTempAncVisitExtractError.HepatitisBScreening,
				        vTempAncVisitExtractError.TreatedHepatitisB,
				        vTempAncVisitExtractError.PresumptiveTreatmentGiven,
				        vTempAncVisitExtractError.PresumptiveTreatmentDose,
				        vTempAncVisitExtractError.MiminumPackageOfCareReceived,
				        vTempAncVisitExtractError.MiminumPackageOfCareServices
						                        
							FROM            vTempAncVisitExtractError INNER JOIN
							 ValidationError ON vTempAncVisitExtractError.Id = ValidationError.RecordId INNER JOIN
							 Validator ON ValidationError.ValidatorId = Validator.Id
                ");

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
