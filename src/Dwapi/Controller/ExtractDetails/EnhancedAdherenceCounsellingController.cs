using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/EnhancedAdherenceCounselling")]
    public class EnhancedAdherenceCounsellingController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempEnhancedAdherenceCounsellingExtractRepository _tempEnhancedAdherenceCounsellingExtractRepository;
        private readonly IEnhancedAdherenceCounsellingExtractRepository _enhancedAdherenceCounsellingExtractRepository;
        private readonly ITempEnhancedAdherenceCounsellingExtractErrorSummaryRepository _errorSummaryRepository;

        public EnhancedAdherenceCounsellingController(ITempEnhancedAdherenceCounsellingExtractRepository tempEnhancedAdherenceCounsellingExtractRepository, IEnhancedAdherenceCounsellingExtractRepository EnhancedAdherenceCounsellingExtractRepository, ITempEnhancedAdherenceCounsellingExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempEnhancedAdherenceCounsellingExtractRepository = tempEnhancedAdherenceCounsellingExtractRepository;
            _enhancedAdherenceCounsellingExtractRepository = EnhancedAdherenceCounsellingExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("ValidCount")]
        public async Task<IActionResult> GetValidCount()
        {
            try
            {
                var count = await _enhancedAdherenceCounsellingExtractRepository.GetCount();
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
                var tempEnhancedAdherenceCounsellingExtracts = await _enhancedAdherenceCounsellingExtractRepository.GetAll(page,pageSize);
                return Ok(tempEnhancedAdherenceCounsellingExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid EnhancedAdherenceCounselling Extracts";
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
                var tempEnhancedAdherenceCounsellingExtracts = _tempEnhancedAdherenceCounsellingExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempEnhancedAdherenceCounsellingExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading EnhancedAdherenceCounselling Extracts with errors";
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
