using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/Patients")]
    public class PatientsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IPatientExtractRepository _patientExtractRepository;
        private readonly ITempPatientExtractRepository _tempPatientExtractRepository;
        private readonly ITempPatientExtractErrorSummaryRepository _errorSummaryRepository;

        public PatientsController(IPatientExtractRepository patientExtractRepository, ITempPatientExtractErrorSummaryRepository errorSummaryRepository, ITempPatientExtractRepository tempPatientExtractRepository)
        {
            _patientExtractRepository = patientExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
            _tempPatientExtractRepository = tempPatientExtractRepository;
        }

        [HttpGet("ValidCount")]
        public async Task<IActionResult> GetValidCount()
        {
            try
            {
                var count = await _patientExtractRepository.GetCount();
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
                var tempPatientExtracts = await _patientExtractRepository.GetAll(page,pageSize);
                return Ok(tempPatientExtracts.ToList());
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
        [HttpGet("LoadValidations")]
        public IActionResult LoadValidations()
        {
            try
            {

                var sql = "SELECT v.Id, v.Extract, v.Field, v.Type, v.Summary, v.DateGenerated, v.PatientPK, v.FacilityId, " +
                    "v.PatientID, v.SiteCode, v.FacilityName, v.RecordId, t.DOB, t.Gender, t.LastVisit, t.RegistrationAtCCC " +
                    "FROM vTempPatientExtractErrorSummary AS v INNER JOIN TempPatientExtracts AS t ON v.PatientPK = t.PatientPK " +
                    "AND v.SiteCode = t.SiteCode";

                var errorSummary = _patientExtractRepository.ExecQueryMulti<dynamic>(sql).OrderByDescending(x=>x.Type).ToList();

                return Ok(errorSummary);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Patient error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }
    }
}
