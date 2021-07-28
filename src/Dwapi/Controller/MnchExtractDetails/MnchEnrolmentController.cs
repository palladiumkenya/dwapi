using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller.MnchExtractDetails
{
    [Produces("application/json")]
    [Route("api/MnchEnrolment")]
    public class MnchEnrolmentController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempMnchEnrolmentExtractRepository _tempMnchEnrolmentExtractRepository;
        private readonly IMnchEnrolmentExtractRepository _otzExtractRepository;
        private readonly ITempMnchEnrolmentExtractErrorSummaryRepository _errorSummaryRepository;

        public MnchEnrolmentController(ITempMnchEnrolmentExtractRepository tempMnchEnrolmentExtractRepository, IMnchEnrolmentExtractRepository MnchEnrolmentExtractRepository, ITempMnchEnrolmentExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempMnchEnrolmentExtractRepository = tempMnchEnrolmentExtractRepository;
            _otzExtractRepository = MnchEnrolmentExtractRepository;
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
                var tempMnchEnrolmentExtracts = await _otzExtractRepository.GetAll(page,pageSize);
                return Ok(tempMnchEnrolmentExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid MnchEnrolment Extracts";
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
                var tempMnchEnrolmentExtracts = _tempMnchEnrolmentExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempMnchEnrolmentExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading MnchEnrolment Extracts with errors";
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
