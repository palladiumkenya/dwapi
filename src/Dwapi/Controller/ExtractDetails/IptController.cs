using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/Ipt")]
    public class IptController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempIptExtractRepository _tempIptExtractRepository;
        private readonly IIptExtractRepository _iptExtractRepository;
        private readonly ITempIptExtractErrorSummaryRepository _errorSummaryRepository;

        public IptController(ITempIptExtractRepository tempIptExtractRepository, IIptExtractRepository IptExtractRepository, ITempIptExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempIptExtractRepository = tempIptExtractRepository;
            _iptExtractRepository = IptExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("ValidCount")]
        public async Task<IActionResult> GetValidCount()
        {
            try
            {
                var count = await _iptExtractRepository.GetCount();
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
                var tempIptExtracts = await _iptExtractRepository.GetAll(page,pageSize);
                return Ok(tempIptExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Ipt Extracts";
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
                var tempIptExtracts = _tempIptExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempIptExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Ipt Extracts with errors";
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
