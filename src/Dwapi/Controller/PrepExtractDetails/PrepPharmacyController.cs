using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Prep;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller.PrepExtractDetails
{
    [Produces("application/json")]
    [Route("api/PrepPharmacy")]
    public class PrepPharmacyController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempPrepPharmacyExtractRepository _tempPrepPharmacyExtractRepository;
        private readonly IPrepPharmacyExtractRepository _otzExtractRepository;
        private readonly ITempPrepPharmacyExtractErrorSummaryRepository _errorSummaryRepository;

        public PrepPharmacyController(ITempPrepPharmacyExtractRepository tempPrepPharmacyExtractRepository, IPrepPharmacyExtractRepository PrepPharmacyExtractRepository, ITempPrepPharmacyExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempPrepPharmacyExtractRepository = tempPrepPharmacyExtractRepository;
            _otzExtractRepository = PrepPharmacyExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("ValidCount")]
        public async Task<IActionResult> GetValidCount()
        {
            try
            {
                var count = await _otzExtractRepository.GetCount();
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
                var tempPrepPharmacyExtracts = await _otzExtractRepository.GetAll(page,pageSize);
                return Ok(tempPrepPharmacyExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid PrepPharmacy Extracts";
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
                var tempPrepPharmacyExtracts = _tempPrepPharmacyExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempPrepPharmacyExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading PrepPharmacy Extracts with errors";
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
