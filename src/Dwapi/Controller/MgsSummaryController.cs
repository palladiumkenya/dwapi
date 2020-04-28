using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mgs;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/MgsSummary")]
    public class MgsSummaryController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITempMetricMigrationExtractRepository _tempMetricMigrationExtractRepository;
        private readonly IMetricMigrationExtractRepository _metricMigrationExtractRepository;

        private readonly ITempMetricMigrationExtractErrorSummaryRepository
            _tempMetricMigrationExtractErrorSummaryRepository;

        public MgsSummaryController(ITempMetricMigrationExtractRepository tempMetricMigrationExtractRepository,
            IMetricMigrationExtractRepository metricMigrationExtractRepository,
            ITempMetricMigrationExtractErrorSummaryRepository tempMetricMigrationExtractErrorSummaryRepository)
        {
            _tempMetricMigrationExtractRepository = tempMetricMigrationExtractRepository;
            _metricMigrationExtractRepository = metricMigrationExtractRepository;
            _tempMetricMigrationExtractErrorSummaryRepository = tempMetricMigrationExtractErrorSummaryRepository;
        }

        [HttpGet("migrationcount")]
        public async Task<IActionResult> GetValidCount()
        {
            try
            {
                var count = await _metricMigrationExtractRepository.GetCount();
                return Ok(count);
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Migration";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("migration/{page}/{pageSize}")]
        public async Task<IActionResult> LoadClientValid(int? page, int pageSize)
        {
            try
            {
                var tempClientExtracts = await _metricMigrationExtractRepository.GetAll(page, pageSize);
                return Ok(tempClientExtracts.ToList());
            }
            catch (Exception e)
            {
                var msg = $"Error loading valid Migrations";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("migrationvalidations")]
        public IActionResult LoadClientValidations()
        {
            try
            {
                var errorSummary = _tempMetricMigrationExtractErrorSummaryRepository.GetAll().ToList();
                return Ok(errorSummary);
            }
            catch (Exception e)
            {
                var msg = $"Error loading Migrations error summary";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }
    }
}
