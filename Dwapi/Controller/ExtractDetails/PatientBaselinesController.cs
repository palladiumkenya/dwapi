using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/PatientBaselines")]
    public class PatientBaselinesController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempPatientBaselinesExtractRepository _tempPatientBaselinesExtractRepository;
        private readonly IPatientBaselinesExtractRepository _patientBaselinesExtractRepository;

        public PatientBaselinesController(ITempPatientBaselinesExtractRepository tempPatientBaselinesExtractRepository, IPatientBaselinesExtractRepository patientBaselinesExtractRepository)
        {
            _tempPatientBaselinesExtractRepository = tempPatientBaselinesExtractRepository;
            _patientBaselinesExtractRepository = patientBaselinesExtractRepository;
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
    }
}
