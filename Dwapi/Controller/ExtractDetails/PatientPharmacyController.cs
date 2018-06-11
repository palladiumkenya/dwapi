using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/PatientPharmacy")]
    public class PatientPharmacyController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempPatientPharmacyExtractRepository _tempPatientPharmacyExtractRepository;
        private readonly IPatientPharmacyExtractRepository _patientPharmacyExtractRepository;

        public PatientPharmacyController(ITempPatientPharmacyExtractRepository tempPatientPharmacyExtractRepository, IPatientPharmacyExtractRepository patientPharmacyExtractRepository)
        {
            _tempPatientPharmacyExtractRepository = tempPatientPharmacyExtractRepository;
            _patientPharmacyExtractRepository = patientPharmacyExtractRepository;
        }

        [HttpGet("LoadValid")]
        public IActionResult LoadValid()
        {
            try
            {
                var tempPatientPharmacyExtracts = _patientPharmacyExtractRepository.GetAll().ToList();
                return Ok(tempPatientPharmacyExtracts);
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
    }
}
