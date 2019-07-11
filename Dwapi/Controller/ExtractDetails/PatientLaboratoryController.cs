using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/PatientLaboratory")]
    public class PatientLaboratoryController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempPatientLaboratoryExtractRepository _tempPatientLaboratoryExtractRepository;
        private readonly IPatientLaboratoryExtractRepository _patientLaboratoryExtractRepository;
        private readonly ITempPatientLaboratoryExtractErrorSummaryRepository _errorSummaryRepository;

        public PatientLaboratoryController(ITempPatientLaboratoryExtractRepository tempPatientLaboratoryExtractRepository, IPatientLaboratoryExtractRepository patientLaboratoryExtractRepository, ITempPatientLaboratoryExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempPatientLaboratoryExtractRepository = tempPatientLaboratoryExtractRepository;
            _patientLaboratoryExtractRepository = patientLaboratoryExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("ValidCount")]
        public async Task<IActionResult> GetValidCount()
        {
            try
            {
                var count = await _patientLaboratoryExtractRepository.GetCount();
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
                var tempPatientLaboratoryExtracts = await _patientLaboratoryExtractRepository.GetAll(page,pageSize);
                return Ok(tempPatientLaboratoryExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid PatientLaboratory Extracts";
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
                var tempPatientLaboratoryExtracts = _tempPatientLaboratoryExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempPatientLaboratoryExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading PatientLaboratory Extracts with errors";
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
                var msg = $"Error loading Patient Laboratory error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }
    }
}
