using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller.MnchExtractDetails
{
    [Produces("application/json")]
    [Route("api/MnchImmunization")]
    public class MnchImmunizationController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempMnchImmunizationExtractRepository _tempMnchImmunizationExtractRepository;
        private readonly IMnchImmunizationExtractRepository _mnchImmunizationExtractRepository;
        private readonly ITempMnchImmunizationExtractErrorSummaryRepository _errorSummaryRepository;

        public MnchImmunizationController(ITempMnchImmunizationExtractRepository tempMnchImmunizationExtractRepository, IMnchImmunizationExtractRepository MnchImmunizationExtractRepository, ITempMnchImmunizationExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempMnchImmunizationExtractRepository = tempMnchImmunizationExtractRepository;
            _mnchImmunizationExtractRepository = MnchImmunizationExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("ValidCount")]
        public async Task<IActionResult> GetValidCount()
        {
            try
            {
                var count = await _mnchImmunizationExtractRepository.GetCount();
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
                var tempMnchImmunizationExtracts = await _mnchImmunizationExtractRepository.GetAll(page,pageSize);
                return Ok(tempMnchImmunizationExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid MnchImmunization Extracts";
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
                var tempMnchImmunizationExtracts = _tempMnchImmunizationExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempMnchImmunizationExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading MnchImmunization Extracts with errors";
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
                var msg = $"Error loading Mnch Immunization error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }
    }
}
