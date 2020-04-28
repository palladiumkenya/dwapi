using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/PatientPharmacy")]
    public class PatientPharmacyController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempPatientPharmacyExtractRepository _tempPatientPharmacyExtractRepository;
        private readonly IPatientPharmacyExtractRepository _patientPharmacyExtractRepository;
        private readonly ITempPatientPharmacyExtractErrorSummaryRepository _errorSummaryRepository;

        public PatientPharmacyController(ITempPatientPharmacyExtractRepository tempPatientPharmacyExtractRepository, IPatientPharmacyExtractRepository patientPharmacyExtractRepository, ITempPatientPharmacyExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempPatientPharmacyExtractRepository = tempPatientPharmacyExtractRepository;
            _patientPharmacyExtractRepository = patientPharmacyExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("ValidCount")]
        public async Task<IActionResult> GetValidCount()
        {
            try
            {
                var count = await _patientPharmacyExtractRepository.GetCount();
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
                var tempPatientPharmacyExtracts =await _patientPharmacyExtractRepository.GetAll(page, pageSize);
                return Ok(tempPatientPharmacyExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid PatientPharmacy Extracts";
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
                var tempPatientPharmacyExtracts = _tempPatientPharmacyExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempPatientPharmacyExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading PatientPharmacy Extracts with errors";
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
                var msg = $"Error loading Patient Pharmacy error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }
    }
}
