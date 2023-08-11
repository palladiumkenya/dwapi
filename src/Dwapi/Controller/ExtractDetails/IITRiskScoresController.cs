using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/IITRiskScores")]
    public class IITRiskScoresController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempIITRiskScoresExtractRepository _tempIITRiskScoresExtractRepository;
        private readonly IIITRiskScoresExtractRepository _iitRiskScoresExtractRepository;
        private readonly ITempIITRiskScoresExtractErrorSummaryRepository _errorSummaryRepository;

        public IITRiskScoresController(ITempIITRiskScoresExtractRepository tempIITRiskScoresExtractRepository, IIITRiskScoresExtractRepository IITRiskScoresExtractRepository, ITempIITRiskScoresExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempIITRiskScoresExtractRepository = tempIITRiskScoresExtractRepository;
            _iitRiskScoresExtractRepository = IITRiskScoresExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("ValidCount")]
        public async Task<IActionResult> GetValidCount()
        {
            try
            {
                var count = await _iitRiskScoresExtractRepository.GetCount();
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
                var tempIITRiskScoresExtracts = await _iitRiskScoresExtractRepository.GetAll(page,pageSize);
                return Ok(tempIITRiskScoresExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid IITRiskScores Extracts";
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
                var tempIITRiskScoresExtracts = _tempIITRiskScoresExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempIITRiskScoresExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading IITRiskScores Extracts with errors";
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
