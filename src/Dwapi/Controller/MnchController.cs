using System;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.Hubs.Mnch;
using Dwapi.Models;
using Dwapi.SettingsManagement.Core.Application.Metrics.Events;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Interfaces.Services.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Services.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Services.Mnch;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/Mnch")]
    public class MnchController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediator _mediator;
        private readonly IExtractStatusService _extractStatusService;
        private readonly IHubContext<MnchActivity> _hubContext;
        private readonly IMnchSendService _mnchSendService;
        private readonly IMnchExportService _mnchExportService;
        private readonly ICbsSendService _cbsSendService;
        private readonly ICTSendService _ctSendService;
        private readonly IExtractRepository _extractRepository;
        private readonly string _version;

        public MnchController(IMediator mediator, IExtractStatusService extractStatusService,
            IHubContext<MnchActivity> hubContext, IMnchSendService mnchSendService, ICbsSendService cbsSendService,
            ICTSendService ctSendService, IExtractRepository extractRepository, IMnchExportService mnchExportService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _extractStatusService = extractStatusService;
            _mnchSendService = mnchSendService;
            _cbsSendService = cbsSendService;
            _ctSendService = ctSendService;
            _extractRepository = extractRepository;
            _mnchExportService = mnchExportService;
            Startup.MnchHubContext = _hubContext = hubContext;
            _version = GetType().Assembly.GetName().Version.ToString();
        }

        [HttpPost("extract")]
        public async Task<IActionResult> Load([FromBody] ExtractPatientMnch request)
        {
            if (!request.IsValid())
                return BadRequest();

            var result = await _mediator.Send(request, HttpContext.RequestAborted);
            return Ok(result);
        }

        [HttpPost("extractAll")]
        public async Task<IActionResult> Load([FromBody] LoadMnchExtracts request)
        {
            if (!request.IsValid())
                return BadRequest();

            string version = GetType().Assembly.GetName().Version.ToString();

            var result = await _mediator.Send(request.LoadMnchFromEmrCommand, HttpContext.RequestAborted);

            await _mediator.Publish(new ExtractLoaded("MNCH", version));

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
            await _mediator.Publish(new ExtractSent("MnchService", version));

            try
            {
                var result = await _mnchSendService.SendManifestAsync(packageDto, version);
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
            await _mediator.Publish(new ExtractSent("MnchService", version));

            try
            {
                var result = await _mnchExportService.ExportManifestAsync(packageDto, version);
                return Ok(result);
            }
            catch (Exception e)
            {
                var msg = $"Error sending  Manifest {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        [HttpPost("PatientMnchs")]
        public IActionResult SendPatientMnchsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchSendService.SendPatientMnchsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
        [HttpPost("exportpatientmnchs")]
        public IActionResult ExportPatientMnchsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchExportService.ExportPatientMnchsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        [HttpPost("MnchEnrolments")]
        public IActionResult SendMnchEnrolmentsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchSendService.SendMnchEnrolmentsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
        [HttpPost("exportmnchenrolments")]
        public IActionResult ExportMnchEnrolmentsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchExportService.ExportMnchEnrolmentsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error exporting Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        [HttpPost("MnchArts")]
        public IActionResult SendMnchArtsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchSendService.SendMnchArtsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
        [HttpPost("exportmncharts")]
        public IActionResult ExportMnchArtsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchExportService.ExportMnchArtsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error exporting Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        [HttpPost("AncVisits")]
        public IActionResult SendAncVisitsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchSendService.SendAncVisitsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
        [HttpPost("exportancvisits")]
        public IActionResult ExportAncVisitsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchExportService.ExportAncVisitsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error exporting Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        [HttpPost("MatVisits")]
        public IActionResult SendMatVisitsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchSendService.SendMatVisitsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
        [HttpPost("exportmatvisits")]
        public IActionResult ExportMatVisitsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchExportService.ExportMatVisitsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error exporting Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        [HttpPost("PncVisits")]
        public IActionResult SendPncVisitsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchSendService.SendPncVisitsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
        [HttpPost("exportpncvisits")]
        public IActionResult ExportPncVisitsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchExportService.ExportPncVisitsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error exporting Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        [HttpPost("MotherBabyPairs")]
        public IActionResult SendMotherBabyPairsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchSendService.SendMotherBabyPairsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
        [HttpPost("exportmotherbabypairs")]
        public IActionResult ExportMotherBabyPairsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchExportService.ExportMotherBabyPairsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error exporting Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        [HttpPost("CwcEnrolments")]
        public IActionResult SendCwcEnrolmentsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchSendService.SendCwcEnrolmentsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
        [HttpPost("exportcwcenrolments")]
        public IActionResult ExportCwcEnrolmentsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchExportService.ExportCwcEnrolmentsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error exporting Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }


        [HttpPost("CwcVisits")]
        public IActionResult SendCwcVisitsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchSendService.SendCwcVisitsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
        [HttpPost("exportcwcvisits")]
        public IActionResult ExportCwcVisitsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchExportService.ExportCwcVisitsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error exporting Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        [HttpPost("Heis")]
        public IActionResult SendHeisExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchSendService.SendHeisAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
        [HttpPost("exportheis")]
        public IActionResult ExportHeisExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchExportService.ExportHeisAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }


        [HttpPost("MnchLabs")]
        public IActionResult SendMnchLabsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchSendService.SendMnchLabsAsync(packageDto);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending Extracts {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }
        [HttpPost("exportmnchlabs")]
        public IActionResult ExportMnchLabsExtracts([FromBody] SendManifestPackageDTO packageDto)
        {
            if (!packageDto.IsValid()) return BadRequest();
            try
            {
                _mnchExportService.ExportMnchLabsAsync(packageDto);
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
                _mnchExportService.ZipExtractsAsync(packageDto, version);
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
                _mnchSendService.NotifyPostSending(packageDto, version);
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
