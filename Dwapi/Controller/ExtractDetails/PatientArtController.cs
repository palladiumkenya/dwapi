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
        private readonly ITempPatientArtExtractErrorSummaryRepository _errorSummaryRepository;

        public PatientArtController(ITempPatientArtExtractRepository tempPatientArtExtractRepository, IPatientArtExtractRepository patientArtExtractRepository, ITempPatientArtExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempPatientArtExtractRepository = tempPatientArtExtractRepository;
            _patientArtExtractRepository = patientArtExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("LoadValid")]
        public IActionResult LoadValid()
        {
            try
            {
                var tempPatientArtExtracts = _patientArtExtractRepository.BatchGet().ToList();
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

        [HttpGet("LoadValidations")]
        public IActionResult LoadValidations()
        {
            try
            {
                var errorSummary = _errorSummaryRepository.GetAll()
                    .OrderByDescending(x=>x.Type).ToList();
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
