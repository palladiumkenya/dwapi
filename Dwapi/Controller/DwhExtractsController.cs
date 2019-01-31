using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Commands.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.Hubs.Dwh;
using Dwapi.Models;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Interfaces.Services.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Services.Dwh;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/DwhExtracts")]
    public class DwhExtractsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediator _mediator;
        private readonly IExtractStatusService _extractStatusService;
        private readonly IHubContext<ExtractActivity> _hubContext;
        private readonly IHubContext<DwhSendActivity> _hubSendContext;
        private readonly IDwhSendService _dwhSendService;
        private readonly ICbsSendService _cbsSendService;

        public DwhExtractsController(IMediator mediator, IExtractStatusService extractStatusService, IHubContext<ExtractActivity> hubContext, IDwhSendService dwhSendService, IHubContext<DwhSendActivity> hubSendContext, ICbsSendService cbsSendService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _extractStatusService = extractStatusService;
            _dwhSendService = dwhSendService;
            _cbsSendService = cbsSendService;
            Startup.DwhSendHubContext= _hubSendContext = hubSendContext;
            Startup.HubContext= _hubContext = hubContext;
        }


        [HttpPost("extract")]
        public async Task<IActionResult> Load([FromBody]ExtractPatient request)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _mediator.Send(request, HttpContext.RequestAborted);
            return Ok(result);
        }

        [HttpPost("extractAll")]
        public async Task<IActionResult> Load([FromBody]LoadExtracts request)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (!request.LoadMpi)
            {
                var result = await _mediator.Send(request.LoadFromEmrCommand, HttpContext.RequestAborted);
                return Ok(result);
            }

            var dwhExtractsTask = Task.Run(() => _mediator.Send(request.LoadFromEmrCommand, HttpContext.RequestAborted));
            var mpiExtractsTask = Task.Run(() => _mediator.Send(request.ExtractMpi, HttpContext.RequestAborted));
            var extractTasks = new List<Task<bool>> { mpiExtractsTask, dwhExtractsTask};
            // wait for both tasks but doesn't throw an error for mpi load
            var result1 = await Task.WhenAll(extractTasks);
            return Ok(dwhExtractsTask);
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
                if (!packageDto.SendMpi)
                {
                    var result = await _dwhSendService.SendManifestAsync(packageDto.DwhPackage);
                    return Ok(result);
                }
                var mpiTask = _cbsSendService.SendManifestAsync(packageDto.MpiPackage);
                var dwhTask = await _dwhSendService.SendManifestAsync(packageDto.DwhPackage);
                return Ok();
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
                if (!packageDto.SendMpi)
                {
                    var jobId = BackgroundJob.Enqueue(() => _dwhSendService.SendExtractsAsync(packageDto.DwhPackage));
                    return Ok();
                }
                var j1 = BackgroundJob.Enqueue(() => _dwhSendService.SendExtractsAsync(packageDto.DwhPackage));
                var j2 = BackgroundJob.Enqueue(() => _cbsSendService.SendMpiAsync(packageDto.MpiPackage));
                return Ok();

            }
            catch (Exception e)
            {
                var msg = $"Error sending to DWH {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
    }
}
