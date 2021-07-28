using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller.MnchExtractDetails
{
    [Produces("application/json")]
    [Route("api/CwcVisit")]
    public class CwcVisitController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempCwcVisitExtractRepository _tempCwcVisitExtractRepository;
        private readonly ICwcVisitExtractRepository _otzExtractRepository;
        private readonly ITempCwcVisitExtractErrorSummaryRepository _errorSummaryRepository;

        public CwcVisitController(ITempCwcVisitExtractRepository tempCwcVisitExtractRepository, ICwcVisitExtractRepository CwcVisitExtractRepository, ITempCwcVisitExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempCwcVisitExtractRepository = tempCwcVisitExtractRepository;
            _otzExtractRepository = CwcVisitExtractRepository;
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
                var tempCwcVisitExtracts = await _otzExtractRepository.GetAll(page,pageSize);
                return Ok(tempCwcVisitExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid CwcVisit Extracts";
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
                var tempCwcVisitExtracts = _tempCwcVisitExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempCwcVisitExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading CwcVisit Extracts with errors";
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
