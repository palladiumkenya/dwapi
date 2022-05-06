using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Prep;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/PrepAdverseEvent")]
    public class PrepAdverseEventController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempPrepAdverseEventExtractRepository _tempPrepAdverseEventExtractRepository;
        private readonly IPrepAdverseEventExtractRepository _otzExtractRepository;
        private readonly ITempPrepAdverseEventExtractErrorSummaryRepository _errorSummaryRepository;

        public PrepAdverseEventController(ITempPrepAdverseEventExtractRepository tempPrepAdverseEventExtractRepository, IPrepAdverseEventExtractRepository PrepAdverseEventExtractRepository, ITempPrepAdverseEventExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempPrepAdverseEventExtractRepository = tempPrepAdverseEventExtractRepository;
            _otzExtractRepository = PrepAdverseEventExtractRepository;
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
                var tempPrepAdverseEventExtracts = await _otzExtractRepository.GetAll(page,pageSize);
                return Ok(tempPrepAdverseEventExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid PrepAdverseEvent Extracts";
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
                var tempPrepAdverseEventExtracts = _tempPrepAdverseEventExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempPrepAdverseEventExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading PrepAdverseEvent Extracts with errors";
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
