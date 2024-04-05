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
using Dwapi.UploadManagement.Core.Interfaces.Services.Hts;
using Dwapi.UploadManagement.Core.Interfaces.Services.Mnch;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/Upload")]
    public class UploadController : Microsoft.AspNetCore.Mvc.Controller
    {
       
        public HttpClient Client { get; set; }
        private IHostingEnvironment _hostingEnvironment;
        private readonly ICTExportService _ctExportService;
        private readonly IPrepExportService _prepExportService;
        private readonly IHtsExportService _htsExportService;
        private readonly IMnchExportService _mnchExportService;



        public UploadController(IHostingEnvironment hostingEnvironment, ICTExportService ctExportService,
           IHtsExportService htsExportService, IMnchExportService mnchExportService, IPrepExportService prepExportService)
        {
           
            _ctExportService = ctExportService;
            _prepExportService = prepExportService;
            _hostingEnvironment = hostingEnvironment;           
            _htsExportService= htsExportService;
            _mnchExportService= mnchExportService;
            
            var ver = GetType().Assembly.GetName().Version;
           
        }

        [HttpPost]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public async Task<IActionResult> Index()
        {
            var file = Request.Form.Files[0];
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.ContentRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            try
            {
                
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
                                var msg = "Error Sending Prep Extracts "+e.Message;
                                Log.Error(e, msg);
                                return StatusCode(500, msg);
                            }


                        }
                        if (partToExtract == "Hts")
                        {
                            try
                            {
                                string fullPath = Path.Combine(newPath, fileName);
                                using (var stream = new FileStream(fullPath, FileMode.Create))
                                {
                                    await file.CopyToAsync(stream);
                                }
                                var result = await _htsExportService.SendHtsFiles(file);
                                return Ok(result);

                            }
                            catch (Exception e)
                            {
                                var msg = $"Error sending Hts Extracts "+e.Message;
                                Log.Error(e, msg);
                                return StatusCode(500, msg);
                            }

                        }
                        if (partToExtract == "Mnch")
                        {

                            try
                            {
                                string fullPath = Path.Combine(newPath, fileName);
                                using (var stream = new FileStream(fullPath, FileMode.Create))
                                {
                                    await file.CopyToAsync(stream);
                                }
                                var result = await _mnchExportService.SendMnchFiles(file);
                                return Ok(result);

                            }
                            catch (Exception e)
                            {
                                var msg = $"Error sending Mnch Extracts  {e.Message}";
                                Log.Error(e, msg);
                                return StatusCode(500, msg);
                            }

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
                                var msg = $"Error sending Care & Treatment Extracts  {e.Message}";
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
