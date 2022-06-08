using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Prep;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller.PrepExtractDetails
{
    [Produces("application/json")]
    [Route("api/PatientPrep")]
    public class PatientPrepController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IPatientPrepExtractRepository _patientExtractRepository;
        private readonly ITempPatientPrepExtractRepository _tempPatientPrepExtractRepository;
        private readonly ITempPatientPrepExtractErrorSummaryRepository _errorSummaryRepository;

        public PatientPrepController(IPatientPrepExtractRepository patientExtractRepository, ITempPatientPrepExtractErrorSummaryRepository errorSummaryRepository, ITempPatientPrepExtractRepository tempPatientPrepExtractRepository)
        {
            _patientExtractRepository = patientExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
            _tempPatientPrepExtractRepository = tempPatientPrepExtractRepository;
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
                var tempPatientPrepExtracts = await _patientExtractRepository.GetAll(page,pageSize);
                return Ok(tempPatientPrepExtracts.ToList());
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
                var tempPatientPrepExtracts = _tempPatientPrepExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempPatientPrepExtracts);
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
                    "v.PatientID, v.SiteCode, v.FacilityName, v.RecordId, t.DOB, t.Gender " +
                    "FROM vTempPatientPrepExtractErrorSummary AS v INNER JOIN TempPatientPrepExtracts AS t ON v.PatientPK = t.PatientPK " +
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
