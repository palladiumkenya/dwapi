using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller.MnchExtractDetails
{
    [Produces("application/json")]
    [Route("api/PatientMnch")]
    public class PatientMnchController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IPatientMnchExtractRepository _patientExtractRepository;
        private readonly ITempPatientMnchExtractRepository _tempPatientMnchExtractRepository;
        private readonly ITempPatientMnchExtractErrorSummaryRepository _errorSummaryRepository;

        public PatientMnchController(IPatientMnchExtractRepository patientExtractRepository, ITempPatientMnchExtractErrorSummaryRepository errorSummaryRepository, ITempPatientMnchExtractRepository tempPatientMnchExtractRepository)
        {
            _patientExtractRepository = patientExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
            _tempPatientMnchExtractRepository = tempPatientMnchExtractRepository;
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
                var tempPatientMnchExtracts = await _patientExtractRepository.GetAll(page,pageSize);
                return Ok(tempPatientMnchExtracts.ToList());
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
                var tempPatientMnchExtracts = _tempPatientMnchExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempPatientMnchExtracts);
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
                    "FROM vTempPatientMnchExtractErrorSummary AS v INNER JOIN TempPatientMnchExtracts AS t ON v.PatientPK = t.PatientPK " +
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
