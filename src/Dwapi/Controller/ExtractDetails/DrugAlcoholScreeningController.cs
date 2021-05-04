using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/DrugAlcoholScreening")]
    public class DrugAlcoholScreeningController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempDrugAlcoholScreeningExtractRepository _tempDrugAlcoholScreeningExtractRepository;
        private readonly IDrugAlcoholScreeningExtractRepository _drugAlcoholScreeningExtractRepository;
        private readonly ITempDrugAlcoholScreeningExtractErrorSummaryRepository _errorSummaryRepository;

        public DrugAlcoholScreeningController(ITempDrugAlcoholScreeningExtractRepository tempDrugAlcoholScreeningExtractRepository, IDrugAlcoholScreeningExtractRepository DrugAlcoholScreeningExtractRepository, ITempDrugAlcoholScreeningExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempDrugAlcoholScreeningExtractRepository = tempDrugAlcoholScreeningExtractRepository;
            _drugAlcoholScreeningExtractRepository = DrugAlcoholScreeningExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("ValidCount")]
        public async Task<IActionResult> GetValidCount()
        {
            try
            {
                var count = await _drugAlcoholScreeningExtractRepository.GetCount();
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
                var tempDrugAlcoholScreeningExtracts = await _drugAlcoholScreeningExtractRepository.GetAll(page,pageSize);
                return Ok(tempDrugAlcoholScreeningExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid DrugAlcoholScreening Extracts";
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
                var tempDrugAlcoholScreeningExtracts = _tempDrugAlcoholScreeningExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempDrugAlcoholScreeningExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading DrugAlcoholScreening Extracts with errors";
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
