using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller.MnchExtractDetails
{
    [Produces("application/json")]
    [Route("api/Hei")]
    public class HeiController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempHeiExtractRepository _tempHeiExtractRepository;
        private readonly IHeiExtractRepository _otzExtractRepository;
        private readonly ITempHeiExtractErrorSummaryRepository _errorSummaryRepository;

        public HeiController(ITempHeiExtractRepository tempHeiExtractRepository, IHeiExtractRepository HeiExtractRepository, ITempHeiExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempHeiExtractRepository = tempHeiExtractRepository;
            _otzExtractRepository = HeiExtractRepository;
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
                var tempHeiExtracts = await _otzExtractRepository.GetAll(page,pageSize);
                return Ok(tempHeiExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Hei Extracts";
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
                var tempHeiExtracts = _tempHeiExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempHeiExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Hei Extracts with errors";
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
