using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/PatientBaselines")]
    public class PatientBaselinesController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempPatientBaselinesExtractRepository _tempPatientBaselinesExtractRepository;
        private readonly IPatientBaselinesExtractRepository _patientBaselinesExtractRepository;
        private readonly ITempPatientBaselinesExtractErrorSummaryRepository _errorSummaryRepository;

        public PatientBaselinesController(ITempPatientBaselinesExtractRepository tempPatientBaselinesExtractRepository, IPatientBaselinesExtractRepository patientBaselinesExtractRepository, ITempPatientBaselinesExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempPatientBaselinesExtractRepository = tempPatientBaselinesExtractRepository;
            _patientBaselinesExtractRepository = patientBaselinesExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("LoadValid")]
        public IActionResult LoadValid()
        {
            try
            {
                var tempPatientBaselinesExtracts = _patientBaselinesExtractRepository.GetAll().ToList();
                return Ok(tempPatientBaselinesExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid PatientBaselines Extracts";
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
                var tempPatientBaselinesExtracts = _tempPatientBaselinesExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempPatientBaselinesExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading PatientBaselines Extracts with errors";
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
                var errorSummary = _errorSummaryRepository.GetAll().OrderByDescending(x=>x.Type).OrderByDescending(x=>x.Type).ToList();
                return Ok(errorSummary);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Patient Baselines error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }
    }
}
