using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Prep;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller.PrepExtractDetails
{
    [Produces("application/json")]
    [Route("api/PrepLab")]
    public class PrepLabController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempPrepLabExtractRepository _tempPrepLabExtractRepository;
        private readonly IPrepLabExtractRepository _otzExtractRepository;
        private readonly ITempPrepLabExtractErrorSummaryRepository _errorSummaryRepository;

        public PrepLabController(ITempPrepLabExtractRepository tempPrepLabExtractRepository, IPrepLabExtractRepository PrepLabExtractRepository, ITempPrepLabExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempPrepLabExtractRepository = tempPrepLabExtractRepository;
            _otzExtractRepository = PrepLabExtractRepository;
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
                var tempPrepLabExtracts = await _otzExtractRepository.GetAll(page,pageSize);
                return Ok(tempPrepLabExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid PrepLab Extracts";
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
                var tempPrepLabExtracts = _tempPrepLabExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempPrepLabExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading PrepLab Extracts with errors";
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
