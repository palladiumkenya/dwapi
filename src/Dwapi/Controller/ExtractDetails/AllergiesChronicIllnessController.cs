using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/AllergiesChronicIllness")]
    public class AllergiesChronicIllnessController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempAllergiesChronicIllnessExtractRepository _tempAllergiesChronicIllnessExtractRepository;
        private readonly IAllergiesChronicIllnessExtractRepository _allergiesChronicIllnessExtractRepository;
        private readonly ITempAllergiesChronicIllnessExtractErrorSummaryRepository _errorSummaryRepository;

        public AllergiesChronicIllnessController(ITempAllergiesChronicIllnessExtractRepository tempAllergiesChronicIllnessExtractRepository, IAllergiesChronicIllnessExtractRepository AllergiesChronicIllnessExtractRepository, ITempAllergiesChronicIllnessExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempAllergiesChronicIllnessExtractRepository = tempAllergiesChronicIllnessExtractRepository;
            _allergiesChronicIllnessExtractRepository = AllergiesChronicIllnessExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("ValidCount")]
        public async Task<IActionResult> GetValidCount()
        {
            try
            {
                var count = await _allergiesChronicIllnessExtractRepository.GetCount();
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
                var tempAllergiesChronicIllnessExtracts = await _allergiesChronicIllnessExtractRepository.GetAll(page,pageSize);
                return Ok(tempAllergiesChronicIllnessExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid AllergiesChronicIllness Extracts";
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
                var tempAllergiesChronicIllnessExtracts = _tempAllergiesChronicIllnessExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempAllergiesChronicIllnessExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading AllergiesChronicIllness Extracts with errors";
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
