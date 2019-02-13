using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/PatientAdverseEvent")]
    public class PatientAdverseEventController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempPatientAdverseEventExtractRepository _tempPatientAdverseEventExtractRepository;
        private readonly IPatientAdverseEventExtractRepository _patientAdverseEventExtractRepository;
        private readonly ITempPatientAdverseEventExtractErrorSummaryRepository _errorSummaryRepository;

        public PatientAdverseEventController(ITempPatientAdverseEventExtractRepository tempPatientAdverseEventExtractRepository, IPatientAdverseEventExtractRepository patientAdverseEventExtractRepository, ITempPatientAdverseEventExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempPatientAdverseEventExtractRepository = tempPatientAdverseEventExtractRepository;
            _patientAdverseEventExtractRepository = patientAdverseEventExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("LoadValid")]
        public IActionResult LoadValid()
        {
            try
            {
                var tempPatientAdverseEventExtracts = _patientAdverseEventExtractRepository.GetAll().ToList();
                return Ok(tempPatientAdverseEventExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid PatientAdverseEvent Extracts";
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
                var tempPatientAdverseEventExtracts = _tempPatientAdverseEventExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempPatientAdverseEventExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading PatientAdverseEvent Extracts with errors";
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
                var msg = $"Error loading PatientAdverseEvent error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }
    }
}
