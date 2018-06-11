using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/PatientLaboratory")]
    public class PatientLaboratoryController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempPatientLaboratoryExtractRepository _tempPatientLaboratoryExtractRepository;
        private readonly IPatientLaboratoryExtractRepository _patientLaboratoryExtractRepository;

        public PatientLaboratoryController(ITempPatientLaboratoryExtractRepository tempPatientLaboratoryExtractRepository, IPatientLaboratoryExtractRepository patientLaboratoryExtractRepository)
        {
            _tempPatientLaboratoryExtractRepository = tempPatientLaboratoryExtractRepository;
            _patientLaboratoryExtractRepository = patientLaboratoryExtractRepository;
        }

        [HttpGet("LoadValid")]
        public IActionResult LoadValid()
        {
            try
            {
                var tempPatientLaboratoryExtracts = _patientLaboratoryExtractRepository.GetAll().ToList();
                return Ok(tempPatientLaboratoryExtracts);
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
    }
}
