using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/PatientStatus")]
    public class PatientStatusController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempPatientStatusExtractRepository _tempPatientStatusExtractRepository;
        private readonly IPatientStatusExtractRepository _patientStatusExtractRepository;
        private readonly ITempPatientStatusExtractErrorSummaryRepository _errorSummaryRepository;

        public PatientStatusController(ITempPatientStatusExtractRepository tempPatientStatusExtractRepository, IPatientStatusExtractRepository patientStatusExtractRepository, ITempPatientStatusExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempPatientStatusExtractRepository = tempPatientStatusExtractRepository;
            _patientStatusExtractRepository = patientStatusExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("LoadValid")]
        public IActionResult LoadValid()
        {
            try
            {
                var tempPatientStatusExtracts = _patientStatusExtractRepository.GetAll().ToList();
                return Ok(tempPatientStatusExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid PatientStatus Extracts";
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
                var tempPatientStatusExtracts = _tempPatientStatusExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempPatientStatusExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading PatientStatus Extracts with errors";
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
                var errorSummary = _errorSummaryRepository.GetAll().ToList();
                return Ok(errorSummary);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Patient Status error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }
    }
}
