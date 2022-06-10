using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Prep;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller.PrepExtractDetails
{
    [Produces("application/json")]
    [Route("api/PrepVisit")]
    public class PrepVisitController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempPrepVisitExtractRepository _tempPrepVisitExtractRepository;
        private readonly IPrepVisitExtractRepository _otzExtractRepository;
        private readonly ITempPrepVisitExtractErrorSummaryRepository _errorSummaryRepository;

        public PrepVisitController(ITempPrepVisitExtractRepository tempPrepVisitExtractRepository, IPrepVisitExtractRepository PrepVisitExtractRepository, ITempPrepVisitExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempPrepVisitExtractRepository = tempPrepVisitExtractRepository;
            _otzExtractRepository = PrepVisitExtractRepository;
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
                var tempPrepVisitExtracts = await _otzExtractRepository.GetAll(page,pageSize);
                return Ok(tempPrepVisitExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid PrepVisit Extracts";
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
                var tempPrepVisitExtracts = _tempPrepVisitExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempPrepVisitExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading PrepVisit Extracts with errors";
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
