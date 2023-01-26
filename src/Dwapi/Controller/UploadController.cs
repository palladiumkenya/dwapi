using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Crs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Crs;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.ExtractsManagement.Core.Model.Destination.Crs;
using Dwapi.Hubs.Crs;
using Dwapi.SettingsManagement.Core.Application.Metrics.Events;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;

using Dwapi.UploadManagement.Core.Interfaces.Services.Crs;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;
using Serilog;
using System.Net.Http.Headers;
using Hangfire;
using System.Text;

using Dwapi.UploadManagement.Core.Interfaces.Services;
using Dwapi.UploadManagement.Core.Exchange.Crs;
using Dwapi.UploadManagement.Infrastructure.Data;
using AutoMapper;
using Dwapi.Models;
using Dwapi.UploadManagement.Core.Interfaces.Services.Dwh;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Infrastructure;
using Dwapi.Hubs.Dwh;
using Dwapi.UploadManagement.Core.Services.Prep;
using Dwapi.UploadManagement.Core.Interfaces.Services.Prep;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/Upload")]
    public class UploadController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediator _mediator;
        private readonly IExtractStatusService _extractStatusService;      
        private readonly IHubContext<ExtractActivity> _hubContext;
        private readonly IClientRegistryExtractRepository _clientRegistryExtractRepository;
        private readonly ICrsSendService _crsSendService;
        private readonly ICrsSearchService _crsSearchService;
        private readonly string _version;
        private readonly string _endPoint;
        public HttpClient Client { get; set; }
        private IHostingEnvironment _hostingEnvironment;
        private readonly ICTExportService _ctExportService;
        private readonly IPrepExportService _prepExportService;



        public UploadController(IMediator mediator, IExtractStatusService extractStatusService,
             IHubContext<ExtractActivity> hubContext, IClientRegistryExtractRepository clientRegistryExtractRepository, 
            ICrsSendService crsSendService, ICrsSearchService crsSearchService,IHostingEnvironment hostingEnvironment, ICTExportService ctExportService, IPrepExportService prepExportService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _extractStatusService = extractStatusService;
            _clientRegistryExtractRepository = clientRegistryExtractRepository;
            _crsSendService = crsSendService;           
            _crsSearchService = crsSearchService;
            _ctExportService = ctExportService;
            _prepExportService = prepExportService;
            _hostingEnvironment = hostingEnvironment;           
            _endPoint = "http://localhost:10707/api/Crs/sendF";
            Startup.HubContext = _hubContext = hubContext;
            var ver = GetType().Assembly.GetName().Version;
            _version = $"{ver.Major}.{ver.Minor}.{ver.Build}";
        }

        [HttpPost]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public async Task<IActionResult> Index()
        {
            try
            {
                var file = Request.Form.Files[0];
                string folderName = "Upload";
                string webRootPath = _hostingEnvironment.ContentRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    String partToExtract = fileName.Split('-', '.')[1];
                    
                    if (partToExtract != null)
                    {                       
                        if (partToExtract == "Prep")
                        {


                            try
                            {
                                string fullPath = Path.Combine(newPath, fileName);
                                using (var stream = new FileStream(fullPath, FileMode.Create))
                                {
                                    await file.CopyToAsync(stream);
                                }
                                var result = await _prepExportService.SendPrepFiles(file);
                                return Ok(result);
                                
                            }
                            catch (Exception e)
                            {
                                var msg = $"Error sending  {e.Message}";
                                Log.Error(e, msg);
                                return StatusCode(500, msg);
                            }


                        }
                        if (partToExtract == "Hts")
                        {
                           

                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:7777")
                            };

                            var stream = file.OpenReadStream();
                            var request = new HttpRequestMessage(HttpMethod.Post, "file");
                            var content = new MultipartFormDataContent
                            {
                                { new StreamContent(stream), "file", fileName }
                            };

                            request.Content = content;

                            await client.SendAsync(request);

                        }
                        if (partToExtract == "Mnch")
                        {
                            
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:8787")
                            };

                            var stream = file.OpenReadStream();
                            var request = new HttpRequestMessage(HttpMethod.Post, "file");
                            var content = new MultipartFormDataContent
                            {
                                { new StreamContent(stream), "file", fileName }
                            };

                            request.Content = content;

                            await client.SendAsync(request);

                        }
                        if (partToExtract == "CT")
                        {
                           
                            
                            try
                            {
                                string fullPath = Path.Combine(newPath, fileName);
                                using (var stream = new FileStream(fullPath, FileMode.Create))
                                {
                                    await file.CopyToAsync(stream);
                                }

                                var result = _ctExportService.SendFileManifest(file, "3").Result;                             

                                return Ok(result);
                            }
                            catch (Exception e)
                            {
                                var msg = $"Error sending  {e.Message}";
                                Log.Error(e, msg);
                                return StatusCode(500, msg);
                            }

                        }

                    }
                }
                return Json("Upload Successful.");
            }
            catch (Exception ex)
            {
                return Json("Upload Failed: " + ex.Message);
            }
        }
        
    }
}
