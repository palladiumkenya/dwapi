using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Prep;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller.PrepExtractDetails
{
    [Produces("application/json")]
    [Route("api/PrepCareTermination")]
    public class PrepCareTerminationController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempPrepCareTerminationExtractRepository _tempPrepCareTerminationExtractRepository;
        private readonly IPrepCareTerminationExtractRepository _otzExtractRepository;
        private readonly ITempPrepCareTerminationExtractErrorSummaryRepository _errorSummaryRepository;

        public PrepCareTerminationController(ITempPrepCareTerminationExtractRepository tempPrepCareTerminationExtractRepository, IPrepCareTerminationExtractRepository PrepCareTerminationExtractRepository, ITempPrepCareTerminationExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempPrepCareTerminationExtractRepository = tempPrepCareTerminationExtractRepository;
            _otzExtractRepository = PrepCareTerminationExtractRepository;
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
                var tempPrepCareTerminationExtracts = await _otzExtractRepository.GetAll(page,pageSize);
                return Ok(tempPrepCareTerminationExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid PrepCareTermination Extracts";
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
                var tempPrepCareTerminationExtracts = _tempPrepCareTerminationExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempPrepCareTerminationExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading PrepCareTermination Extracts with errors";
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
