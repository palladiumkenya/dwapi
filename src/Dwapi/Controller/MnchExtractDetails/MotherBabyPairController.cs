using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/MotherBabyPair")]
    public class MotherBabyPairController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempMotherBabyPairExtractRepository _tempMotherBabyPairExtractRepository;
        private readonly IMotherBabyPairExtractRepository _otzExtractRepository;
        private readonly ITempMotherBabyPairExtractErrorSummaryRepository _errorSummaryRepository;

        public MotherBabyPairController(ITempMotherBabyPairExtractRepository tempMotherBabyPairExtractRepository, IMotherBabyPairExtractRepository MotherBabyPairExtractRepository, ITempMotherBabyPairExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempMotherBabyPairExtractRepository = tempMotherBabyPairExtractRepository;
            _otzExtractRepository = MotherBabyPairExtractRepository;
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
                var tempMotherBabyPairExtracts = await _otzExtractRepository.GetAll(page,pageSize);
                return Ok(tempMotherBabyPairExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid MotherBabyPair Extracts";
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
                var tempMotherBabyPairExtracts = _tempMotherBabyPairExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempMotherBabyPairExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading MotherBabyPair Extracts with errors";
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
