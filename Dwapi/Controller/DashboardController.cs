using System;
using System.Threading.Tasks;
using Dwapi.Models;
using Dwapi.SettingsManagement.Core.Application.Metrics.Events;
using Dwapi.SettingsManagement.Core.Application.Metrics.Queries;
using Dwapi.SettingsManagement.Core.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/Dashboard")]
    public class DashboardController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _mediator.Send(new GetAppMetric(), HttpContext.RequestAborted);
                return Ok(result);
            }
            catch (Exception e)
            {
                var msg = $"Error loading {nameof(AppMetric)}(s)";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }

        }
    }
}
