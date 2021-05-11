using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller.MnchExtractDetails
{
    [Produces("application/json")]
    [Route("api/MnchLab")]
    public class MnchLabController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempMnchLabExtractRepository _tempMnchLabExtractRepository;
        private readonly IMnchLabExtractRepository _otzExtractRepository;
        private readonly ITempMnchLabExtractErrorSummaryRepository _errorSummaryRepository;

        public MnchLabController(ITempMnchLabExtractRepository tempMnchLabExtractRepository, IMnchLabExtractRepository MnchLabExtractRepository, ITempMnchLabExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempMnchLabExtractRepository = tempMnchLabExtractRepository;
            _otzExtractRepository = MnchLabExtractRepository;
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
                var tempMnchLabExtracts = await _otzExtractRepository.GetAll(page,pageSize);
                return Ok(tempMnchLabExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid MnchLab Extracts";
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
                var tempMnchLabExtracts = _tempMnchLabExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempMnchLabExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading MnchLab Extracts with errors";
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
