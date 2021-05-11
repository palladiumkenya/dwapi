using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller.MnchExtractDetails
{
    [Produces("application/json")]
    [Route("api/AncVisit")]
    public class AncVisitController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempAncVisitExtractRepository _tempAncVisitExtractRepository;
        private readonly IAncVisitExtractRepository _otzExtractRepository;
        private readonly ITempAncVisitExtractErrorSummaryRepository _errorSummaryRepository;

        public AncVisitController(ITempAncVisitExtractRepository tempAncVisitExtractRepository, IAncVisitExtractRepository AncVisitExtractRepository, ITempAncVisitExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempAncVisitExtractRepository = tempAncVisitExtractRepository;
            _otzExtractRepository = AncVisitExtractRepository;
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
                var tempAncVisitExtracts = await _otzExtractRepository.GetAll(page,pageSize);
                return Ok(tempAncVisitExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid AncVisit Extracts";
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
                var tempAncVisitExtracts = _tempAncVisitExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempAncVisitExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading AncVisit Extracts with errors";
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
