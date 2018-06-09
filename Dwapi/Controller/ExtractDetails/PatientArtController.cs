using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/PatientArt")]
    public class PatientArtController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempPatientArtExtractRepository _tempPatientArtExtractRepository;
        private readonly IPatientArtExtractRepository _patientArtExtractRepository;

        public PatientArtController(ITempPatientArtExtractRepository tempPatientArtExtractRepository, IPatientArtExtractRepository patientArtExtractRepository)
        {
            _tempPatientArtExtractRepository = tempPatientArtExtractRepository;
            _patientArtExtractRepository = patientArtExtractRepository;
        }

        [HttpGet("LoadValid")]
        public IActionResult LoadValid()
        {
            try
            {
                var tempPatientArtExtracts = _patientArtExtractRepository.GetAll().ToList();
                return Ok(tempPatientArtExtracts);
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
    }
}
