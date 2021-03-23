using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/GbvScreening")]
    public class GbvScreeningController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempGbvScreeningExtractRepository _tempGbvScreeningExtractRepository;
        private readonly IGbvScreeningExtractRepository _gbvScreeningExtractRepository;
        private readonly ITempGbvScreeningExtractErrorSummaryRepository _errorSummaryRepository;

        public GbvScreeningController(ITempGbvScreeningExtractRepository tempGbvScreeningExtractRepository, IGbvScreeningExtractRepository GbvScreeningExtractRepository, ITempGbvScreeningExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempGbvScreeningExtractRepository = tempGbvScreeningExtractRepository;
            _gbvScreeningExtractRepository = GbvScreeningExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("ValidCount")]
        public async Task<IActionResult> GetValidCount()
        {
            try
            {
                var count = await _gbvScreeningExtractRepository.GetCount();
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
                var tempGbvScreeningExtracts = await _gbvScreeningExtractRepository.GetAll(page,pageSize);
                return Ok(tempGbvScreeningExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid GbvScreening Extracts";
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
                var tempGbvScreeningExtracts = _tempGbvScreeningExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempGbvScreeningExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading GbvScreening Extracts with errors";
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
