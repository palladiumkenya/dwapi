using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller.MnchExtractDetails
{
    [Produces("application/json")]
    [Route("api/MatVisit")]
    public class MatVisitController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempMatVisitExtractRepository _tempMatVisitExtractRepository;
        private readonly IMatVisitExtractRepository _otzExtractRepository;
        private readonly ITempMatVisitExtractErrorSummaryRepository _errorSummaryRepository;

        public MatVisitController(ITempMatVisitExtractRepository tempMatVisitExtractRepository, IMatVisitExtractRepository MatVisitExtractRepository, ITempMatVisitExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempMatVisitExtractRepository = tempMatVisitExtractRepository;
            _otzExtractRepository = MatVisitExtractRepository;
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
                var tempMatVisitExtracts = await _otzExtractRepository.GetAll(page,pageSize);
                return Ok(tempMatVisitExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid MatVisit Extracts";
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
                var tempMatVisitExtracts = _tempMatVisitExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempMatVisitExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading MatVisit Extracts with errors";
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
