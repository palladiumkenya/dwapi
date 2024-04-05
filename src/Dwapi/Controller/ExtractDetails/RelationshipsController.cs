using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dwapi.Controller.ExtractDetails
{
    [Produces("application/json")]
    [Route("api/Relationships")]
    public class RelationshipsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempRelationshipsExtractRepository _tempRelationshipsExtractRepository;
        private readonly IRelationshipsExtractRepository _relationshipsExtractRepository;
        private readonly ITempRelationshipsExtractErrorSummaryRepository _errorSummaryRepository;

        public RelationshipsController(ITempRelationshipsExtractRepository tempRelationshipsExtractRepository, IRelationshipsExtractRepository RelationshipsExtractRepository, ITempRelationshipsExtractErrorSummaryRepository errorSummaryRepository)
        {
            _tempRelationshipsExtractRepository = tempRelationshipsExtractRepository;
            _relationshipsExtractRepository = RelationshipsExtractRepository;
            _errorSummaryRepository = errorSummaryRepository;
        }

        [HttpGet("ValidCount")]
        public async Task<IActionResult> GetValidCount()
        {
            try
            {
                var count = await _relationshipsExtractRepository.GetCount();
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
                var tempRelationshipsExtracts = await _relationshipsExtractRepository.GetAll(page,pageSize);
                return Ok(tempRelationshipsExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Relationships Extracts";
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
                var tempRelationshipsExtracts = _tempRelationshipsExtractRepository.GetAll().Where(n => n.CheckError).ToList();
                return Ok(tempRelationshipsExtracts);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Relationships Extracts with errors";
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
                var msg = $"Error loading Patient art fast track error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }
    }
}
