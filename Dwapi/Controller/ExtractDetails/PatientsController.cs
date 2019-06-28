using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/Patients")]
    public class PatientsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempPatientExtractRepository _tempPatientExtractRepository;
        private readonly ITempPatientExtractErrorSummaryRepository _errorSummaryRepository;

        public PatientsController(ITempPatientExtractRepository tempPatientExtractRepository, ITempPatientExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempPatientExtractRepository = tempPatientExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("LoadValid")]
        public IActionResult LoadValid()
        {
            try
            {
                var tempPatientExtracts = _tempPatientExtractRepository.GetAll().Where(n => !n.CheckError).ToList();
                return Ok(tempPatientExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Patient Extracts";
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
                var tempPatientExtracts = _tempPatientExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempPatientExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Patient Extracts with errors";
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
                var errorSummary = _errorSummaryRepository.GetAll().OrderByDescending(x=>x.Type).ToList();
                return Ok(errorSummary);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Patient error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }
    }
}
