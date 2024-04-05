using System;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.Hubs.Hts;
using Dwapi.Models;
using Dwapi.SettingsManagement.Core.Application.Metrics.Events;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Interfaces.Services.Hts;
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
        private readonly IHtsSendService _htsSendService;
        private readonly IHtsExportService _htsExportService;

        public HtsController(IMediator mediator, IExtractStatusService extractStatusService,
            IHubContext<HtsActivity> hubContext, IHtsSendService htsSendService, IHtsExportService htsExportService,
            IHubContext<HtsSendActivity> hubSendContext)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _extractStatusService = extractStatusService;
            _htsSendService = htsSendService;
            _htsExportService = htsExportService;
            Startup.HtsSendHubContext = _hubSendContext = hubSendContext;
            Startup.HtsHubContext = _hubContext = hubContext;
        }

        [HttpPost("extractAll")]
        public async Task<IActionResult> Load([FromBody] LoadHtsExtracts request)
        {
            string version = GetType().Assembly.GetName().Version.ToString();
            var result = await _mediator.Send(request.LoadHtsFromEmrCommand, HttpContext.RequestAborted);
            await _mediator.Publish(new ExtractLoaded("HivTestingService", version));
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
            await _mediator.Publish(new ExtractSent("HivTestingService", version));

            try
            {
                var result = await _htsSendService.SendManifestAsync(packageDto,version);
                return Ok(result);
            }
            catch (Exception e)
            {
                var msg = $"Error sending  Manifest {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
        [HttpPost("exportmanifest")]
        public async Task<IActionResult> ExportManifest([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();

            string version = GetType().Assembly.GetName().Version.ToString();
            await _mediator.Publish(new ExtractSent("HivTestingService", version));

            try
            {
                var result = await _htsExportService.ExportManifestAsync(packageDto, version);
                return Ok(result);
            }
            catch (Exception e)
            {
                var msg = $"Error exporting  Manifest {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }



        // POST: api/DwhExtracts/patients
        [HttpPost("clients")]
        public IActionResult sendClientsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                _htsSendService.SendClientsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
        [HttpPost("exportclients")]
        public IActionResult exportClientsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                _htsExportService.ExportClientsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error exporting Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        // POST: api/DwhExtracts/patients
        [HttpPost("clienttests")]
        public IActionResult sendClientTestsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                _htsSendService.SendClientTestsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
        [HttpPost("exportclienttests")]
        public IActionResult exportClientTestsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                _htsExportService.ExportClientTestsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error exporting Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        // POST: api/DwhExtracts/patients
        [HttpPost("testkits")]
        public IActionResult sendTestKitsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                _htsSendService.SendTestKitsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
        [HttpPost("exporttestkits")]
        public IActionResult exportTestKitsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                _htsExportService.ExportTestKitsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error exporting Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }


        // POST: api/DwhExtracts/patients
        [HttpPost("clienttracing")]
        public IActionResult sendClientTracingExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                _htsSendService.SendClientTracingAsync(packageDto);
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
        [HttpPost("exportclienttracing")]
        public IActionResult exportClientTracingExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                _htsExportService.ExportClientTracingAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error exporting Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }


        // POST: api/DwhExtracts/patients
        [HttpPost("partnertracing")]
        public IActionResult sendPartnerTracingExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                _htsSendService.SendPartnerTracingAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
        [HttpPost("exportpartnertracing")]
        public IActionResult exportPartnerTracingExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                _htsExportService.ExportPartnerTracingAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error exporting Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        // POST: api/DwhExtracts/patients
        [HttpPost("partnernotificationservices")]
        public IActionResult sendPartnerNotificationServicesExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                _htsSendService.SendPartnerNotificationServicesAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
        [HttpPost("exportpartnernotificationservices")]
        public IActionResult exportPartnerNotificationServicesExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                _htsExportService.ExportPartnerNotificationServicesAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error exporting Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        // POST: api/DwhExtracts/patients
        [HttpPost("clientslinkage")]
        public IActionResult SendClientLinkageExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                _htsSendService.SendClientsLinkagesAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
        [HttpPost("exportclientslinkage")]
        public IActionResult ExportClientLinkageExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                _htsExportService.ExportClientsLinkagesAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error exporting Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }


        // POST: api/DwhExtracts/patients
        [HttpPost("htseligibilityextract")]
        public IActionResult SendHtsEligibilityExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                _htsSendService.SendHtsEligibilityExtractsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
        [HttpPost("exporthtseligibilityextract")]
        public IActionResult ExportHtsEligibilityExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                _htsExportService.ExportHtsEligibilityExtractsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error exporting Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        [HttpPost("zipfiles")]
        public IActionResult ZipFiles([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid())
                return BadRequest();
            try
            {
                string version = GetType().Assembly.GetName().Version.ToString();
                _htsExportService.ZipExtractsAsync(packageDto, version);
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
                _htsSendService.NotifyPostSending(packageDto,version);
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
