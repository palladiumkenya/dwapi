using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/Covid")]
    public class CovidController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempCovidExtractRepository _tempCovidExtractRepository;
        private readonly ICovidExtractRepository _covidExtractRepository;
        private readonly ITempCovidExtractErrorSummaryRepository _errorSummaryRepository;

        public CovidController(ITempCovidExtractRepository tempCovidExtractRepository, ICovidExtractRepository CovidExtractRepository, ITempCovidExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempCovidExtractRepository = tempCovidExtractRepository;
            _covidExtractRepository = CovidExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("ValidCount")]
        public async Task<IActionResult> GetValidCount()
        {
            try
            {
                var count = await _covidExtractRepository.GetCount();
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
                var tempCovidExtracts = await _covidExtractRepository.GetAll(page,pageSize);
                return Ok(tempCovidExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Covid Extracts";
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
                var tempCovidExtracts = _tempCovidExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempCovidExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Covid Extracts with errors";
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
