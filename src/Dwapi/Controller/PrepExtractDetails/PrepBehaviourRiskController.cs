using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Prep;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller.PrepExtractDetails
{
    [Produces("application/json")]
    [Route("api/PrepBehaviourRisk")]
    public class PrepBehaviourRiskController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempPrepBehaviourRiskExtractRepository _tempPrepBehaviourRiskExtractRepository;
        private readonly IPrepBehaviourRiskExtractRepository _otzExtractRepository;
        private readonly ITempPrepBehaviourRiskExtractErrorSummaryRepository _errorSummaryRepository;

        public PrepBehaviourRiskController(ITempPrepBehaviourRiskExtractRepository tempPrepBehaviourRiskExtractRepository, IPrepBehaviourRiskExtractRepository PrepBehaviourRiskExtractRepository, ITempPrepBehaviourRiskExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempPrepBehaviourRiskExtractRepository = tempPrepBehaviourRiskExtractRepository;
            _otzExtractRepository = PrepBehaviourRiskExtractRepository;
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
                var tempPrepBehaviourRiskExtracts = await _otzExtractRepository.GetAll(page,pageSize);
                return Ok(tempPrepBehaviourRiskExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid PrepBehaviourRisk Extracts";
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
                var tempPrepBehaviourRiskExtracts = _tempPrepBehaviourRiskExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempPrepBehaviourRiskExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading PrepBehaviourRisk Extracts with errors";
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
