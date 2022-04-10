using System;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Prep;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.Hubs.Prep;
using Dwapi.Models;
using Dwapi.SettingsManagement.Core.Application.Metrics.Events;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Interfaces.Services.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Services.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Services.Prep;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/Prep")]
    public class PrepController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediator _mediator;
        private readonly IExtractStatusService _extractStatusService;
        private readonly IHubContext<PrepActivity> _hubContext;
        private readonly IPrepSendService _prepSendService;
        private readonly ICbsSendService _cbsSendService;
        private readonly ICTSendService _ctSendService;
        private readonly IExtractRepository _extractRepository;
        private readonly string _version;

        public PrepController(IMediator mediator, IExtractStatusService extractStatusService,
            IHubContext<PrepActivity> hubContext, IPrepSendService prepSendService, ICbsSendService cbsSendService,
            ICTSendService ctSendService, IExtractRepository extractRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _extractStatusService = extractStatusService;
            _prepSendService = prepSendService;
            _cbsSendService = cbsSendService;
            _ctSendService = ctSendService;
            _extractRepository = extractRepository;
            Startup.PrepHubContext = _hubContext = hubContext;
            _version = GetType().Assembly.GetName().Version.ToString();
        }

        [HttpPost("extract")]
        public async Task<IActionResult> Load([FromBody] ExtractPatientPrep request)
        {
            if (!request.IsValid())
                return BadRequest();

            var result = await _mediator.Send(request, HttpContext.RequestAborted);
            return Ok(result);
        }

        [HttpPost("extractAll")]
        public async Task<IActionResult> Load([FromBody] LoadPrepExtracts request)
        {
            if (!request.IsValid())
                return BadRequest();

            string version = GetType().Assembly.GetName().Version.ToString();

            var result = await _mediator.Send(request.LoadPrepFromEmrCommand, HttpContext.RequestAborted);

            await _mediator.Publish(new ExtractLoaded("PREP", version));

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
        public async Task<IActionResult> SendManifest([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();

            string version = GetType().Assembly.GetName().Version.ToString();
            await _mediator.Publish(new ExtractSent("PrepService", version));

            try
            {
                var result = await _prepSendService.SendManifestAsync(packageDto, version);
                return Ok(result);
            }
            catch (Exception e)
            {
                var msg = $"Error sending  Manifest {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        [HttpPost("PatientPreps")]
        public IActionResult SendPatientPrepsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _prepSendService.SendPatientPrepsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        [HttpPost("PrepAdverseEvents")]
        public IActionResult SendPrepEnrolmentsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _prepSendService.SendPrepAdverseEventAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }


        [HttpPost("PrepBehaviourRisks")]
        public IActionResult SendAncVisitsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _prepSendService.SendPrepBehaviourRisksAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        [HttpPost("PrepCareTerminations")]
        public IActionResult SendMatVisitsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _prepSendService.SendPrepCareTerminationsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        [HttpPost("PrepLabs")]
        public IActionResult SendPrepLabsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _prepSendService.SendPrepLabsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        [HttpPost("PrepPharmacys")]
        public IActionResult SendPrepArtsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _prepSendService.SendPrepPharmacysAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }



        [HttpPost("PrepVisits")]
        public IActionResult SendPncVisitsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _prepSendService.SendPrepVisitsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }





        // POST: api/DwhExtracts/patients
        [HttpPost("endsession")]
        public IActionResult SendEndSession([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                string version = GetType().Assembly.GetName().Version.ToString();
                _prepSendService.NotifyPostSending(packageDto, version);
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
