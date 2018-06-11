using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/PatientVisit")]
    public class PatientVisitController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempPatientVisitExtractRepository _tempPatientVisitExtractRepository;
        private readonly IPatientVisitExtractRepository _patientVisitExtractRepository;
        public PatientVisitController(ITempPatientVisitExtractRepository tempPatientVisitExtractRepository, IPatientVisitExtractRepository patientVisitExtractRepository)
        {
            _tempPatientVisitExtractRepository = tempPatientVisitExtractRepository;
            _patientVisitExtractRepository = patientVisitExtractRepository;
        }

        [HttpGet("LoadValid")]
        public IActionResult LoadValid()
        {
            try
            {
                var tempPatientVisitExtracts = _patientVisitExtractRepository.GetAll().ToList();
                return Ok(tempPatientVisitExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid PatientVisit Extracts";
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
                var tempPatientVisitExtracts = _tempPatientVisitExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempPatientVisitExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading PatientVisit Extracts with errors";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }
    }
}
