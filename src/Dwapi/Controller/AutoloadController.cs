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
        private readonly IEmrManagerService _emrManagerService;
        private readonly IEmrMetricsService _metricsService;

        public AutoloadController(IEmrManagerService emrManagerService, IEmrMetricsService metricsService)
        {
            _emrManagerService = emrManagerService;
            _metricsService = metricsService;
        }

        [HttpGet]
        public IActionResult refreshETL()
        {
            return Ok("hdhccncn here");

            // try
            // {
            //     var list = _emrManagerService.GetAllEmrs();
            //     return Ok(list);
            // }
            // catch (Exception e)
            // {
            //     var msg = $"Error refreshing EMR tables";
            //     Log.Error(msg);
            //     Log.Error($"{e}");
            //     return StatusCode(500, msg);
            // }
        }

    }
}
