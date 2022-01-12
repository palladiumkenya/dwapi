using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/DefaulterTracing")]
    public class DefaulterTracingController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempDefaulterTracingExtractRepository _tempDefaulterTracingExtractRepository;
        private readonly IDefaulterTracingExtractRepository _defaulterTracingExtractRepository;
        private readonly ITempDefaulterTracingExtractErrorSummaryRepository _errorSummaryRepository;

        public DefaulterTracingController(ITempDefaulterTracingExtractRepository tempDefaulterTracingExtractRepository, IDefaulterTracingExtractRepository DefaulterTracingExtractRepository, ITempDefaulterTracingExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempDefaulterTracingExtractRepository = tempDefaulterTracingExtractRepository;
            _defaulterTracingExtractRepository = DefaulterTracingExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("ValidCount")]
        public async Task<IActionResult> GetValidCount()
        {
            try
            {
                var count = await _defaulterTracingExtractRepository.GetCount();
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
                var tempDefaulterTracingExtracts = await _defaulterTracingExtractRepository.GetAll(page,pageSize);
                return Ok(tempDefaulterTracingExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid DefaulterTracing Extracts";
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
                var tempDefaulterTracingExtracts = _tempDefaulterTracingExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempDefaulterTracingExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading DefaulterTracing Extracts with errors";
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
