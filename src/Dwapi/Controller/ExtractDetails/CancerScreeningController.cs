using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/CancerScreening")]
    public class CancerScreeningController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempCancerScreeningExtractRepository _tempCancerScreeningExtractRepository;
        private readonly ICancerScreeningExtractRepository _CancerScreeningExtractRepository;
        private readonly ITempCancerScreeningExtractErrorSummaryRepository _errorSummaryRepository;

        public CancerScreeningController(ITempCancerScreeningExtractRepository tempCancerScreeningExtractRepository, ICancerScreeningExtractRepository CancerScreeningExtractRepository, ITempCancerScreeningExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempCancerScreeningExtractRepository = tempCancerScreeningExtractRepository;
            _CancerScreeningExtractRepository = CancerScreeningExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("ValidCount")]
        public async Task<IActionResult> GetValidCount()
        {
            try
            {
                var count = await _CancerScreeningExtractRepository.GetCount();
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
                var tempCancerScreeningExtracts = await _CancerScreeningExtractRepository.GetAll(page,pageSize);
                return Ok(tempCancerScreeningExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid CancerScreening Extracts";
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
                var tempCancerScreeningExtracts = _tempCancerScreeningExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempCancerScreeningExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading CancerScreening Extracts with errors";
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
