using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Crs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Crs;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.ExtractsManagement.Core.Model.Destination.Crs;
using Dwapi.Hubs.Crs;
using Dwapi.SettingsManagement.Core.Application.Metrics.Events;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Interfaces.Services.Crs;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/Crs")]
    public class CrsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediator _mediator;
        private readonly IExtractStatusService _extractStatusService;
        private readonly IHubContext<CrsActivity> _hubContext;
        private readonly IHubContext<CrsSendActivity> _hubSendContext;
        private readonly IClientRegistryExtractRepository _clientRegistryExtractRepository;
        private readonly ICrsSendService _crsSendService;
        private readonly ICrsSearchService _crsSearchService;
        private readonly string _version;
        private IHostingEnvironment _hostingEnvironment;

        public CrsController(IMediator mediator, IExtractStatusService extractStatusService,
            IHubContext<CrsActivity> hubContext, IClientRegistryExtractRepository clientRegistryExtractRepository,
            ICrsSendService crsSendService, IHubContext<CrsSendActivity> hubSendContext, ICrsSearchService crsSearchService, IHostingEnvironment hostingEnvironment)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _extractStatusService = extractStatusService;
            _clientRegistryExtractRepository = clientRegistryExtractRepository;
            _crsSendService = crsSendService;
            _crsSearchService = crsSearchService;
            _hostingEnvironment = hostingEnvironment;
            Startup.CrsSendHubContext= _hubSendContext = hubSendContext;
            Startup.CrsHubContext = _hubContext = hubContext;
            var ver = GetType().Assembly.GetName().Version;
            _version = $"{ver.Major}.{ver.Minor}.{ver.Build}";
        }


        [HttpPost("extract")]
        public async Task<IActionResult> Load([FromBody] ExtractClientRegistryExtract request)
        {

            string version = GetType().Assembly.GetName().Version.ToString();

            var result = await _mediator.Send(request, HttpContext.RequestAborted);
            await _mediator.Publish(new ExtractLoaded("ClientRegistryExtract", version));

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

        [HttpGet("allcount")]
        public IActionResult GetAllExtractCount()
        {
            try
            {
                var count = _clientRegistryExtractRepository.GetAll().Count();
                return Ok(count);
            }
            catch (Exception e)
            {
                var msg = $"Error loading {nameof(Extract)}(s)";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("crssummary")]
        public IActionResult GetCrsSummery()
        {
            try
            {
                var count = _clientRegistryExtractRepository.GetSummery();
                return Ok(count);
            }
            catch (Exception e)
            {
                var msg = $"Error loading {nameof(Extract)}(s)";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet("all")]
        public IActionResult GetAllExtracts()
        {
            try
            {
                var eventExtract = _clientRegistryExtractRepository.GetAll().ToList();
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



        [HttpGet("count")]
        public IActionResult GetExtractCount()
        {
            try
            {
                var count = _clientRegistryExtractRepository.GetView().Select(x => x.Id).Count();
                return Ok(count);
            }
            catch (Exception e)
            {
                var msg = $"Error loading {nameof(Extract)}(s)";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

        [HttpGet]
        public IActionResult GetExtracts()
        {
            try
            {
                var eventExtract = _clientRegistryExtractRepository.GetAll().ToList();
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

        // POST: api/Crs/manifest
        [HttpPost("manifest")]
        public async Task<IActionResult> SendManifest([FromBody] SendManifestPackageDTO packageDTO)
        {
            if (!packageDTO.IsValid())
                return BadRequest();

            string version = GetType().Assembly.GetName().Version.ToString();

            await _mediator.Publish(new ExtractSent("ClientRegistryExtract", version));


            try
            {
                await _crsSendService.SendManifestAsync(packageDTO,_version);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending {nameof(ClientRegistryExtract)} Manifest {e.Message}";
                Log.Error(e,msg);
                return StatusCode(500, msg);
            }
        }


        // POST: api/Crs/manifest
        [HttpPost("crs")]
        public async Task<IActionResult> SendCrs([FromBody] SendManifestPackageDTO packageDTO)
        {
            if (!packageDTO.IsValid())
                return BadRequest();
            try
            {
                await _crsSendService.SendCrsAsync(packageDTO);
                await _crsSendService.NotifyPostSending(packageDTO, _version);
                return Ok();
            }
            catch (Exception e)
            {
                var msg = $"Error sending {nameof(ClientRegistryExtract)} {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        // POST: api/Crs/crsSearchPackage
        [HttpPost("crsSearch")]
        public async Task<IActionResult> SearchCrs([FromBody] CrsSearchPackageDto crsSearchPackage)
        {
            if (!crsSearchPackage.IsValid())
                return BadRequest();
            try
            {
                var result = await _crsSearchService.SearchCrsAsync(crsSearchPackage);
                return Ok(result);
            }
            catch (Exception e)
            {
                var msg = $"Error getting {nameof(ClientRegistryExtract)} search results. {e.Message}";
                Log.Error(e, msg);
                return StatusCode(500, msg);
            }
        }

        [HttpGet("displayFiles")]
        public IActionResult GetExportedFiles()
        {
            try
            {
                string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\exports";

                string[] filePaths = Directory.GetFiles(projectPath, "*.json",
                                     SearchOption.AllDirectories);

                string[] filesnumber = Directory.GetFiles(projectPath, "*.json");

                return Ok(filePaths);



            }
            catch (Exception e)
            {
                var msg = $"Error loading {nameof(Extract)}(s)";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }
        [HttpGet]
        [Route("files")]
        public IActionResult Files()
        {
            var result = new List<string>();
            var uploads =  "exports";
            string extension = ".zip";
            if (Directory.Exists(uploads))
            {
                
                foreach (string fileName in Directory.GetFiles(uploads))
                {
                    if (Path.GetExtension(fileName) == extension)
                    {
                        var fileInfo = Path.GetFileName(fileName);

                        result.Add(fileInfo);
                    }

                    
                }
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> Download([FromQuery] string file)
        {
            var uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "exports");
            var filePath = Path.Combine(uploads, file);
            if (!System.IO.File.Exists(filePath))
                return NotFound();


            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(filePath), file);
        }
        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }


    }
}
