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

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/Upload")]
    public class UploadController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediator _mediator;
        private readonly IExtractStatusService _extractStatusService;
        private readonly IHubContext<CrsActivity> _hubContext;
        private readonly IHubContext<CrsSendActivity> _hubSendContext;
        private readonly IClientRegistryExtractRepository _clientRegistryExtractRepository;
        private readonly ICrsSendService _crsSendService;
        private readonly ICrsSearchService _crsSearchService;
        private readonly string _version;
        private readonly string _endPoint;
        public HttpClient Client { get; set; }
        private IHostingEnvironment _hostingEnvironment;



        public UploadController(IMediator mediator, IExtractStatusService extractStatusService, 
            IHubContext<CrsActivity> hubContext, IClientRegistryExtractRepository clientRegistryExtractRepository, 
            ICrsSendService crsSendService, IHubContext<CrsSendActivity> hubSendContext, ICrsSearchService crsSearchService,IHostingEnvironment hostingEnvironment)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _extractStatusService = extractStatusService;
            _clientRegistryExtractRepository = clientRegistryExtractRepository;
            _crsSendService = crsSendService;           
            _crsSearchService = crsSearchService;
            _hostingEnvironment = hostingEnvironment;           
            _endPoint = "http://localhost:10707/api/Crs/sendF";
            Startup.CrsSendHubContext = _hubSendContext = hubSendContext;
            Startup.CrsHubContext = _hubContext = hubContext;
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
                    //string fullPath = Path.Combine(newPath, fileName);
                    //using (var stream = new FileStream(fullPath, FileMode.Create))
                    //{
                    //    await file.CopyToAsync(stream);
                    //}
                    if (partToExtract != null)
                    {
                        if (partToExtract == "Crs")
                        {                          

                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:10707")
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
                        if (partToExtract == "Prep")
                        {
                            

                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:9797")
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
                            //string result = "";
                            var url = "http://localhost:21751/api/file/patients";
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:21751/api/file/patients")
                            };
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                            client.Timeout = TimeSpan.FromMinutes(59);
                            using (var content = new MultipartFormDataContent())
                            {
                                content.Add(new StreamContent(file.OpenReadStream())
                                {
                                    Headers =
                                        {
                                            ContentLength = file.Length,
                                            ContentType = new MediaTypeHeaderValue(file.ContentType)
                                        }
                                }, "File", fileName);

                                var response = await client.PostAsync(url, content);
                                if (response.IsSuccessStatusCode)
                                {
                                    var url1 = "http://localhost:21751/api/file";
                                    var client1 = new HttpClient
                                    {
                                        BaseAddress = new Uri("http://localhost:21751/api/file")
                                    };
                                    client1.DefaultRequestHeaders.Accept.Clear();
                                    client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                                    client1.Timeout = TimeSpan.FromMinutes(59);
                                    using (var content1 = new MultipartFormDataContent())
                                    {
                                        content1.Add(new StreamContent(file.OpenReadStream())
                                        {
                                            Headers =
                                        {
                                            ContentLength = file.Length,
                                            ContentType = new MediaTypeHeaderValue(file.ContentType)
                                        }
                                        }, "File", fileName);

                                        var response1 = await client1.PostAsync(url1, content1);

                                    }
                                }


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
