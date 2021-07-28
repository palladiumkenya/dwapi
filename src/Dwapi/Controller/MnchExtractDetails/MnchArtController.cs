using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller.MnchExtractDetails
{
    [Produces("application/json")]
    [Route("api/MnchArt")]
    public class MnchArtController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempMnchArtExtractRepository _tempMnchArtExtractRepository;
        private readonly IMnchArtExtractRepository _otzExtractRepository;
        private readonly ITempMnchArtExtractErrorSummaryRepository _errorSummaryRepository;

        public MnchArtController(ITempMnchArtExtractRepository tempMnchArtExtractRepository, IMnchArtExtractRepository MnchArtExtractRepository, ITempMnchArtExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempMnchArtExtractRepository = tempMnchArtExtractRepository;
            _otzExtractRepository = MnchArtExtractRepository;
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
                var tempMnchArtExtracts = await _otzExtractRepository.GetAll(page,pageSize);
                return Ok(tempMnchArtExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid MnchArt Extracts";
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
                var tempMnchArtExtracts = _tempMnchArtExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempMnchArtExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading MnchArt Extracts with errors";
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
