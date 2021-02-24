using System;
using System.Threading.Tasks;
using Dwapi.SettingsManagement.Core.Application.Checks.Commands;
using Dwapi.SettingsManagement.Core.Application.Metrics.Queries;
using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Enum;
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
        private readonly IEmrManagerService _emrManagerService;
        public DashboardController(IMediator mediator, IEmrManagerService emrManagerService)
        {
            _mediator = mediator;
            _emrManagerService = emrManagerService;
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

        [HttpGet("LiveUpdate")]
        public async Task<IActionResult> LiveUpdate()
        {
            try
            {
                var emr = _emrManagerService.GetDefault();

                string version = GetType().Assembly.GetName().Version.ToString();

                var result= await _mediator.Send(new InitAppVerCheck());
                if(result.IsSuccess)
                    await _mediator.Send(new PerformSingleCheck(emr.Id, CheckStage.PreSend, version,new Guid("d05864e8-678a-11eb-ae93-0242ac130002")));
            }
            catch (Exception e)
            {
                Log.Warning(e.Message);
            }

            return Ok();
        }
    }
}
