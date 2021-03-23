using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/DepressionScreening")]
    public class DepressionScreeningController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempDepressionScreeningExtractRepository _tempDepressionScreeningExtractRepository;
        private readonly IDepressionScreeningExtractRepository _depressionScreeningExtractRepository;
        private readonly ITempDepressionScreeningExtractErrorSummaryRepository _errorSummaryRepository;

        public DepressionScreeningController(ITempDepressionScreeningExtractRepository tempDepressionScreeningExtractRepository, IDepressionScreeningExtractRepository DepressionScreeningExtractRepository, ITempDepressionScreeningExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempDepressionScreeningExtractRepository = tempDepressionScreeningExtractRepository;
            _depressionScreeningExtractRepository = DepressionScreeningExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("ValidCount")]
        public async Task<IActionResult> GetValidCount()
        {
            try
            {
                var count = await _depressionScreeningExtractRepository.GetCount();
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
                var tempDepressionScreeningExtracts = await _depressionScreeningExtractRepository.GetAll(page,pageSize);
                return Ok(tempDepressionScreeningExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid DepressionScreening Extracts";
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
                var tempDepressionScreeningExtracts = _tempDepressionScreeningExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempDepressionScreeningExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading DepressionScreening Extracts with errors";
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
