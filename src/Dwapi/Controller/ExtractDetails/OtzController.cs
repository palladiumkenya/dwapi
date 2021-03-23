using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/Otz")]
    public class OtzController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempOtzExtractRepository _tempOtzExtractRepository;
        private readonly IOtzExtractRepository _otzExtractRepository;
        private readonly ITempOtzExtractErrorSummaryRepository _errorSummaryRepository;

        public OtzController(ITempOtzExtractRepository tempOtzExtractRepository, IOtzExtractRepository OtzExtractRepository, ITempOtzExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempOtzExtractRepository = tempOtzExtractRepository;
            _otzExtractRepository = OtzExtractRepository;
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
                var tempOtzExtracts = await _otzExtractRepository.GetAll(page,pageSize);
                return Ok(tempOtzExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Otz Extracts";
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
                var tempOtzExtracts = _tempOtzExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempOtzExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Otz Extracts with errors";
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
