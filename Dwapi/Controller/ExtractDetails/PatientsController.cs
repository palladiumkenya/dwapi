using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/Patients")]
    public class PatientsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempPatientExtractRepository _tempPatientExtractRepository;

        public PatientsController(ITempPatientExtractRepository tempPatientExtractRepository)
        {
            _tempPatientExtractRepository = tempPatientExtractRepository;
        }

        [HttpGet("LoadValid")]
        public IActionResult LoadValid()
        {
            try
            {
                var tempPatientExtracts = _tempPatientExtractRepository.GetAll().Where(n => !n.CheckError).ToList();
                return Ok(tempPatientExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Patient Extracts";
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
                var tempPatientExtracts = _tempPatientExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempPatientExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Patient Extracts with errors";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }
    }
}
