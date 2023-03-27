using System;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/RefreshETL")]
    public class AutoloadController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IAutoloadService _autoloadService;
        private readonly IEmrMetricsService _metricsService;

        public AutoloadController(IAutoloadService autoloadService, IEmrMetricsService metricsService)
        {
            _autoloadService = autoloadService;
            _metricsService = metricsService;
        }

        [HttpPost]
        public dynamic refreshETL([FromBody] DatabaseProtocol entity)
        {
            // return $"here there ";

            try
            {
                var list = _autoloadService.refreshEMRETL(entity);
                return list;
            }
            catch (Exception e)
            {
                var msg = $"Error refreshing EMR tables {e}";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet]
        public dynamic test()
        {

            return "here there";
        }

    }
}
