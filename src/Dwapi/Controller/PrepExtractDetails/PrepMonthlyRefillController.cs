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
    [Route("api/PrepMonthlyRefill")]
    public class PrepMonthlyRefillController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempPrepMonthlyRefillExtractRepository _tempPrepMonthlyRefillExtractRepository;
        private readonly IPrepMonthlyRefillExtractRepository _otzExtractRepository;
        private readonly ITempPrepMonthlyRefillExtractErrorSummaryRepository _errorSummaryRepository;

        public PrepMonthlyRefillController(ITempPrepMonthlyRefillExtractRepository tempPrepMonthlyRefillExtractRepository, IPrepMonthlyRefillExtractRepository PrepMonthlyRefillExtractRepository, ITempPrepMonthlyRefillExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempPrepMonthlyRefillExtractRepository = tempPrepMonthlyRefillExtractRepository;
            _otzExtractRepository = PrepMonthlyRefillExtractRepository;
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
                var tempPrepMonthlyRefillExtracts = await _otzExtractRepository.GetAll(page,pageSize);
                return Ok(tempPrepMonthlyRefillExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid PrepMonthlyRefill Extracts";
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
                var tempPrepMonthlyRefillExtracts = _tempPrepMonthlyRefillExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempPrepMonthlyRefillExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading PrepMonthlyRefill Extracts with errors";
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
                var msg = $"Error loading MonthlyRefill error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }
    }
}
