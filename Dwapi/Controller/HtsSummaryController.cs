using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    public class HtsSummaryController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempHTSClientExtractRepository _tempPatientArtExtractRepository;
        private readonly IHTSClientExtractRepository _patientArtExtractRepository;
        private readonly ITempHTSClientExtractErrorSummaryRepository _errorSummaryRepository;

        public HtsSummaryController(ITempHTSClientExtractRepository tempPatientArtExtractRepository, IHTSClientExtractRepository patientArtExtractRepository, ITempHTSClientExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempPatientArtExtractRepository = tempPatientArtExtractRepository;
            _patientArtExtractRepository = patientArtExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("client")]
        public IActionResult LoadClientValid()
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

        [HttpGet("clienterrors")]
        public IActionResult LoadClientErrors()
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

        [HttpGet("clientvalidations")]
        public IActionResult LoadClientValidations()
        {
            try
            {
                var errorSummary = _errorSummaryRepository.GetAll().ToList();
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

        [HttpGet("linkage")]
        public IActionResult LoadLinkageValid()
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

        [HttpGet("linkageerrors")]
        public IActionResult LoadLinkageErrors()
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

        [HttpGet("linkagevalidations")]
        public IActionResult LoadLinkageValidations()
        {
            try
            {
                var errorSummary = _errorSummaryRepository.GetAll().ToList();
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


        [HttpGet("partner")]
        public IActionResult LoadPartnerValid()
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

        [HttpGet("partnererrors")]
        public IActionResult LoadPartnerErrors()
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

        [HttpGet("partnervalidations")]
        public IActionResult LoadPartnerValidations()
        {
            try
            {
                var errorSummary = _errorSummaryRepository.GetAll().ToList();
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
