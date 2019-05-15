using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Cbs;
using Dwapi.ExtractsManagement.Core.Commands.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.Hubs.Cbs;
using Dwapi.Hubs.Dwh;
using Dwapi.Hubs.Hts;
using Dwapi.Models;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Interfaces.Services.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Services.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Services.Hts;
using Hangfire;
using Hangfire.States;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/Hts")]
    public class HtsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediator _mediator;
        private readonly IExtractStatusService _extractStatusService;
        private readonly IHubContext<HtsActivity> _hubContext;
        private readonly IHubContext<HtsSendActivity> _hubSendContext;
        private readonly IHtsSendService _dwhSendService;

        public HtsController(IMediator mediator, IExtractStatusService extractStatusService, IHubContext<HtsActivity> hubContext, IHtsSendService dwhSendService, IHubContext<HtsSendActivity> hubSendContext)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _extractStatusService = extractStatusService;
            _dwhSendService = dwhSendService;
            Startup.HtsSendHubContext= _hubSendContext = hubSendContext;
            Startup.HtsHubContext= _hubContext = hubContext;
        }


        [HttpPost("extract")]
        public async Task<IActionResult> Load([FromBody]ExtractPatient request)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _mediator.Send(request, HttpContext.RequestAborted);
            return Ok(result);
        }

        [HttpPost("extractAll")]
        public async Task<IActionResult> Load([FromBody] LoadHtsExtracts request)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _mediator.Send(request.LoadHtsFromEmrCommand, HttpContext.RequestAborted);
            return Ok(result);
        }


        // GET: api/DwhExtracts/status/id
        [HttpGet("status/{id}")]
        public IActionResult GetStatus(Guid id)
        {
            if (id.IsNullOrEmpty())
                return BadRequest();
            try
            {
                var eventExtract = _extractStatusService.GetStatus(id);
                if (null == eventExtract)
                    return NotFound();

                return Ok(eventExtract);
            }
            catch (Exception e)
            {
                var msg = $"Error loading {nameof(Extract)}(s)";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        // POST: api/DwhExtracts/manifest
        [HttpPost("manifest")]
        public async Task<IActionResult> SendManifest([FromBody] CombinedSendManifestDto packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                var result = await _dwhSendService.SendManifestAsync(packageDto.DwhPackage);
                return Ok(result);
            }
            catch (Exception e)
            {
                var msg = $"Error sending  Manifest {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }


        // POST: api/DwhExtracts/patients
        [HttpPost("patients")]
        public IActionResult SendPatientExtracts([FromBody] CombinedSendManifestDto packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                _dwhSendService.SendClientsAsync(packageDto.DwhPackage);
                    return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
    }
}
