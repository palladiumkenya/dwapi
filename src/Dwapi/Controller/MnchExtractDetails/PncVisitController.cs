using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller.MnchExtractDetails
{
    [Produces("application/json")]
    [Route("api/PncVisit")]
    public class PncVisitController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempPncVisitExtractRepository _tempPncVisitExtractRepository;
        private readonly IPncVisitExtractRepository _otzExtractRepository;
        private readonly ITempPncVisitExtractErrorSummaryRepository _errorSummaryRepository;

        public PncVisitController(ITempPncVisitExtractRepository tempPncVisitExtractRepository, IPncVisitExtractRepository PncVisitExtractRepository, ITempPncVisitExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempPncVisitExtractRepository = tempPncVisitExtractRepository;
            _otzExtractRepository = PncVisitExtractRepository;
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
                var tempPncVisitExtracts = await _otzExtractRepository.GetAll(page,pageSize);
                return Ok(tempPncVisitExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid PncVisit Extracts";
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
                var tempPncVisitExtracts = _tempPncVisitExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempPncVisitExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading PncVisit Extracts with errors";
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
