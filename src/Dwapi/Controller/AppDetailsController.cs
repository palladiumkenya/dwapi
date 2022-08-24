using System;
using System.Threading.Tasks;
using Dwapi.SettingsManagement.Core.Application.Checks.Queries;
using Dwapi.SettingsManagement.Core.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/appDetails")]
    public class AppDetailsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediator _mediator;

        public AppDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        // GET: api/appDetails/version
        [HttpGet("version")]
        public IActionResult Version()
        {
            var ver = GetType().Assembly.GetName().Version;
            string version = $"{ver.Major}.{ver.Minor}.{ver.Build}{ver.Revision}";
            return Ok(version);
        }

        // GET: api/appDetails/version
        [HttpGet("checkUpdate")]
        public async Task<IActionResult> CheckUpdate()
        {
            try
            {
                var ver = GetType().Assembly.GetName().Version;
                string version = $"{ver.Major}.{ver.Minor}.{ver.Build}{ver.Revision}";
                var result = await _mediator.Send(new CheckLiveUpdate(version));
                if (result.IsSuccess)
                    return Ok(result.Value.HasUpdates);

                throw new Exception(result.Error);
            }
            catch (Exception e)
            {
                var msg = $"Error checking Updates";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        // GET: api/appDetails/version
        [HttpGet("checkOs")]
        public async Task<IActionResult> CheckOs()
        {
            string os = "UnKnown";
            try
            {

                if (InDocker)
                {
                    os = "Docker";
                    return Ok(os);
                }


                os = Environment.OSVersion.Platform.ToString();

                return Ok(os);

            }
            catch (Exception e)
            {
                var msg = $"Error checking OS";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        private bool InDocker { get { return Environment.GetEnvironmentVariable("DWAPI_RUNNING_IN_CONTAINER") == "true";} }
    }
}
