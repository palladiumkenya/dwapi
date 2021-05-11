using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller.MnchExtractDetails
{
    [Produces("application/json")]
    [Route("api/CwcEnrolment")]
    public class CwcEnrolmentController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempCwcEnrolmentExtractRepository _tempCwcEnrolmentExtractRepository;
        private readonly ICwcEnrolmentExtractRepository _otzExtractRepository;
        private readonly ITempCwcEnrolmentExtractErrorSummaryRepository _errorSummaryRepository;

        public CwcEnrolmentController(ITempCwcEnrolmentExtractRepository tempCwcEnrolmentExtractRepository, ICwcEnrolmentExtractRepository CwcEnrolmentExtractRepository, ITempCwcEnrolmentExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempCwcEnrolmentExtractRepository = tempCwcEnrolmentExtractRepository;
            _otzExtractRepository = CwcEnrolmentExtractRepository;
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
                var tempCwcEnrolmentExtracts = await _otzExtractRepository.GetAll(page,pageSize);
                return Ok(tempCwcEnrolmentExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid CwcEnrolment Extracts";
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
                var tempCwcEnrolmentExtracts = _tempCwcEnrolmentExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempCwcEnrolmentExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading CwcEnrolment Extracts with errors";
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
