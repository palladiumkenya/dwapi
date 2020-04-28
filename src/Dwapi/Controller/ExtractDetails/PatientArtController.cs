using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/PatientArt")]
    public class PatientArtController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempPatientArtExtractRepository _tempPatientArtExtractRepository;
        private readonly IPatientArtExtractRepository _patientArtExtractRepository;
        private readonly ITempPatientArtExtractErrorSummaryRepository _errorSummaryRepository;

        public PatientArtController(ITempPatientArtExtractRepository tempPatientArtExtractRepository, IPatientArtExtractRepository patientArtExtractRepository, ITempPatientArtExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempPatientArtExtractRepository = tempPatientArtExtractRepository;
            _patientArtExtractRepository = patientArtExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("ValidCount")]
        public async Task<IActionResult> GetValidCount()
        {
            try
            {
                var count = await _patientArtExtractRepository.GetCount();
                return Ok(count);
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Patient Extracts";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }


        [HttpGet("LoadValid/{page}/{pageSize}")]
        public async Task<IActionResult> LoadValid(int? page,int pageSize)
        {
            try
            {
                var tempPatientArtExtracts =await  _patientArtExtractRepository.GetAll(page,pageSize);
                return Ok(tempPatientArtExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid PatientArt Extracts";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("LoadErrors")]
        public IActionResult LoadErrors()
        {
            try
            {
                var tempPatientArtExtracts = _tempPatientArtExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempPatientArtExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading PatientArt Extracts with errors";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("LoadValidations")]
        public IActionResult LoadValidations()
        {
            try
            {

                var sql = "SELECT v.Id, v.Extract, v.Field, v.Type, v.Summary, v.DateGenerated, v.PatientPK, v.FacilityId, v.PatientID, v.SiteCode, " +
                    "v.FacilityName, v.RecordId, v.DOB, v.Gender, v.PatientSource, v.RegistrationDate, v.AgeLastVisit, v.PreviousARTStartDate, " +
                    "v.PreviousARTRegimen, v.StartARTAtThisFacility, v.StartARTDate, v.StartRegimen, v.StartRegimenLine, v.LastARTDate, " +
                    "v.LastRegimen, v.LastRegimenLine, v.LastVisit, v.ExitReason, v.ExitDate, t.ExpectedReturn FROM " +
                    "vTempPatientArtExtractErrorSummary AS v INNER JOIN vTempPatientArtExtractError AS t ON v.PatientPK = t.PatientPK " +
                    "AND v.SiteCode = t.SiteCode";

                var errorSummary = _tempPatientArtExtractRepository.ExecQueryMulti<dynamic>(sql).OrderByDescending(x=>x.Type).ToList();

                return Ok(errorSummary);
            }
            catch (Exception e)
            {
                var msg = $"Error loading PatientArt error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }
    }
}
