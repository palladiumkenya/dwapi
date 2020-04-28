using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet("ValidCount")]
        public async Task<IActionResult> GetValidCount()
        {
            try
            {
                var count = await _patientStatusExtractRepository.GetCount();
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
                var tempPatientStatusExtracts = await _patientStatusExtractRepository.GetAll(page,pageSize);
                return Ok(tempPatientStatusExtracts.ToList());
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
                var errorSummary = _errorSummaryRepository.GetAll().OrderByDescending(x=>x.Type).ToList();
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
