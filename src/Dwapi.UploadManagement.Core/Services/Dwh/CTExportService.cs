using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Dwapi.Contracts.Exchange;
using Dwapi.ExtractsManagement.Core.Application.Events;
using Dwapi.ExtractsManagement.Core.Commands.Mts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SettingsManagement.Core.Application.Metrics.Events;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Event.Dwh;
using Dwapi.UploadManagement.Core.Exceptions;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Exchange.Dwh.Smart;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Services.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Dwapi.UploadManagement.Core.Notifications.Dwh;
using Hangfire.Common;
using Humanizer;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using static System.Net.Mime.MediaTypeNames;

namespace Dwapi.UploadManagement.Core.Services.Dwh
{
    public class CTExportService : ICTExportService
    {
        private readonly string _endPoint;
        private readonly IDwhPackager _packager;
        private readonly IMediator _mediator;
        private IEmrMetricReader _reader;
        private readonly ITransportLogRepository _transportLogRepository;
        private IHostingEnvironment _hostingEnvironment;

        public HttpClient Client { get; set; }

        public CTExportService(IDwhPackager packager, IMediator mediator, IEmrMetricReader reader, ITransportLogRepository transportLogRepository, IHostingEnvironment hostingEnvironment)
        {
            _packager = packager;
            _mediator = mediator;
            _reader = reader;
            _transportLogRepository = transportLogRepository;
            _endPoint = "api/";
            _hostingEnvironment = hostingEnvironment;
        }  

        public Task<List<SendDhwManifestResponse>> ExportManifestAsync(SendManifestPackageDTO sendTo, string version, string apiVersion = "")
        {
            return ExportManifestAsync(sendTo, DwhManifestMessageBag.Create(_packager.GenerateWithMetrics(sendTo.GetEmrDto()).ToList()), version, apiVersion);
        }
        public async Task<List<SendDhwManifestResponse>> ExportManifestAsync(SendManifestPackageDTO sendTo, DwhManifestMessageBag messageBag, string version, string apiVersion = "")
        {
            var responses = new List<SendDhwManifestResponse>();

            await _mediator.Publish(new HandshakeStart("CTSendStart", version, messageBag.Session));

            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            var client = Client ?? new HttpClient(handler);
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            foreach (var message in messageBag.Messages)
            {
                try
                {
                    var start = DateTime.Now;
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}spot", apiVersion), message.Manifest);

                    var msg = JsonConvert.SerializeObject(message);
                    var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                    var Base64Manifest = Convert.ToBase64String(plainTextBytes);
                    string projectPath = Path.Combine(_hostingEnvironment.ContentRootPath + "\\exports");
                    string folderName = Path.Combine(projectPath, Convert.ToString(message.Manifest.SiteCode) + "-CT");
                    Directory.CreateDirectory(folderName);                    // Write that JSON to txt file,  
                    File.WriteAllText(folderName + "\\" + "manifest.dump" + ".json", msg);

                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
            }
            return responses;
        }

        public Task<List<SendDhwManifestResponse>> ExportSmartManifestAsync(SendManifestPackageDTO sendTo, string version, string apiVersion = "")
        {
            return ExportSmartManifestAsync(sendTo,
                DwhManifestMessageBag.Create(_packager.GenerateWithMetrics(sendTo.GetEmrDto()).ToList()), version,
                apiVersion);
        }
        public async Task<List<SendDhwManifestResponse>> ExportSmartManifestAsync(SendManifestPackageDTO sendTo, DwhManifestMessageBag messageBag, string version,
         string apiVersion = "")
        {
            var responses = new List<SendDhwManifestResponse>();

            await _mediator.Publish(new HandshakeStart("CTSendStart", version, messageBag.Session));

            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            var client = Client ?? new HttpClient(handler);
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            foreach (var message in messageBag.Messages)
            {
                try
                {
                    
                    var start = DateTime.Now;
                    var msg = JsonConvert.SerializeObject(message.Manifest);
                    var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                    var Base64Manifest = Convert.ToBase64String(plainTextBytes);
                    string projectPath = Path.Combine(_hostingEnvironment.ContentRootPath + "\\exports");
                    string folderName = Path.Combine(projectPath, Convert.ToString(message.Manifest.SiteCode) + "-CT");
                    Directory.CreateDirectory(folderName);
                    File.WriteAllText(folderName + "\\" + "manifest.dump" + ".json", Base64Manifest);

                   
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
            }
            return responses;
        }
      
        public void NotifyPreSending()
        {
            DomainEvents.Dispatch(new DwExporthMessageNotification(false, $"Exporting started..."));

        }

        public async Task<List<SendCTResponse>> ExportBatchExtractsAsync<T>(
            SendManifestPackageDTO sendTo,
            int batchSize,
            IMessageBag<T> messageBag)
            where T : ClientExtract
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            var client = Client ?? new HttpClient(handler);

            var responses = new List<SendCTResponse>();
            var packageInfo = _packager.GetPackageInfo<T>(batchSize);
            int sendCound = 0;
            int count = 0;
            int total = packageInfo.PageCount;
            int overall = 0;

            DomainEvents.Dispatch(new CTStatusNotification(sendTo.ExtractId, sendTo.GetExtractId(messageBag.ExtractName), ExtractStatus.Sending));
            long recordCount = 0;

            try
            {
                for (int page = 1; page <= packageInfo.PageCount; page++)
                {
                    count++;
                    var extracts = _packager.GenerateBatchExtracts<T>(page, packageInfo.PageSize).ToList();
                    recordCount = recordCount + extracts.Count;
                    Log.Debug(
                        $">>>> Sending {messageBag.ExtractName} {recordCount}/{packageInfo.TotalRecords} Page:{page} of {packageInfo.PageCount}");
                    messageBag = messageBag.Generate(extracts);
                    var message = messageBag.Messages;
                    try
                    {

                        bool allowSend = true;
                        while (allowSend)
                        {
                            if (messageBag.ExtractName == "DefaulterTracingExtract")
                            {
                                var msg = JsonConvert.SerializeObject(message);
                                var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                                var Base64Extract = Convert.ToBase64String(plainTextBytes);
                                string projectPath = Path.Combine(_hostingEnvironment.ContentRootPath + "\\exports");
                                string folderName = Path.Combine(projectPath, message[0].Extracts[0].SiteCode + "-CT" + "\\extracts");

                                string path = folderName + "\\" + messageBag.ExtractName + ".dump" + ".json";
                                if (File.Exists(path))
                                {
                                    File.AppendAllText(folderName + "\\" + messageBag.ExtractName + ".dump" + ".json", msg);
                                    // File.WriteAllText(folderName + "\\" + messageBag.ExtractName + ".dump" + ".json", msg);
                                    allowSend = false;
                                }
                                else
                                {
                                    File.WriteAllText(folderName + "\\" + messageBag.ExtractName + ".dump" + ".json", msg);
                                    allowSend = false;

                                }

                                string startPath = Path.Combine(projectPath, message[0].Extracts[0].SiteCode + "-CT");
                                string zipPath = Path.Combine(projectPath, message[0].Extracts[0].SiteCode + "-CT" + ".zip");

                                if (File.Exists(zipPath))
                                    File.Delete(zipPath);

                                ZipFile.CreateFromDirectory(startPath, zipPath, CompressionLevel.Fastest, true);
                            }
                            else
                            {
                                var msg = JsonConvert.SerializeObject(message);
                                var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                                var Base64Extract = Convert.ToBase64String(plainTextBytes);
                                string projectPath = Path.Combine(_hostingEnvironment.ContentRootPath + "\\exports");
                                string folderName = Path.Combine(projectPath, message[0].Extracts[0].SiteCode + "-CT" + "\\extracts");

                                if (!Directory.Exists(folderName))
                                    Directory.CreateDirectory(folderName);
                                string path = folderName + "\\" + messageBag.ExtractName + ".dump" + ".json";

                                if (File.Exists(path))
                                {
                                    File.AppendAllText(folderName + "\\" + messageBag.ExtractName + ".dump" + ".json", msg);
                                    // File.WriteAllText(folderName + "\\" + messageBag.ExtractName + ".dump" + ".json", msg);
                                    allowSend = false;
                                }
                                else
                                {
                                    File.WriteAllText(folderName + "\\" + messageBag.ExtractName + ".dump" + ".json", msg);
                                    allowSend = false;

                                }
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error");
                        throw;
                    }

                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName, messageBag.GetProgress(count, total), recordCount)));

                }

                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));
            }
            catch (Exception e)
            {
                Log.Error(e, $"Send Extracts {messageBag.ExtractName} Error");
                throw;
            }

            DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName,
                messageBag.GetProgress(count, total), recordCount, true)));

            DomainEvents.Dispatch(new CTStatusNotification(sendTo.ExtractId, sendTo.GetExtractId(messageBag.ExtractName), ExtractStatus.Sent, sendCound)
            { UpdatePatient = (messageBag is ArtMessageBag || messageBag is BaselineMessageBag || messageBag is StatusMessageBag) }
            );

            return responses;
        }

        public async Task<List<SendCTResponse>> ExportSmartBatchExtractsAsync<T>(SendManifestPackageDTO sendTo, int batchSize, IMessageSourceBag<T> messageBag) where T : ClientExtract
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            string startPath = "";

            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            var client = Client ?? new HttpClient(handler);

            var responses = new List<SendCTResponse>();
            var packageInfo = _packager.GetPackageInfo<T>(batchSize);
            int sendCound = 0;
            int count = 0;
            int total = count;
            int overall = 0;

           DomainEvents.Dispatch(new CTStatusNotification(sendTo.ExtractId, sendTo.GetExtractId(messageBag.ExtractName), ExtractStatus.Exporting));
            long recordCount = 0;

            if (messageBag.ExtractName == "PatientArtExtract")
            {
                string x = "ss";
            }


            try
            {
                string jobId = string.Empty; Guid manifestId = new Guid(); Guid facilityId = new Guid();
       


                if (packageInfo.PageCount > 0)
                {

                    int page = 1;
                    count++;
                    var extracts = _packager.GenerateSmartBatchExtracts<T>(page, Convert.ToInt32(packageInfo.TotalRecords)).ToList();
                    recordCount = recordCount + extracts.Count;
                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                    var message = messageBag;


                    try
                    {
                        int retryCount = 0;
                        bool allowSend = true;
                        while (allowSend)
                        {
                       
                            if (message.ExtractName == "DefaulterTracingExtract")
                            {

                                var msg = JsonConvert.SerializeObject(message);
                                var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                                var Base64Extract = Convert.ToBase64String(plainTextBytes);
                                string projectPath = Path.Combine(_hostingEnvironment.ContentRootPath + "\\exports");
                                string folderName = Path.Combine(projectPath, message.Extracts[0].SiteCode + "-CT" + "\\extracts");

                                string path = folderName + "\\" + messageBag.ExtractName + ".dump" + ".json";
                               
                                await File.WriteAllTextAsync(path, msg);
                                allowSend = false;

                                var sentIds = messageBag.SendIds;
                                sendCound += sentIds.Count;

                                DomainEvents.Dispatch(new CTExtractSentEvent(sentIds, SendStatus.Exported,
                                   messageBag.ExtractType));

                               

                                startPath = Path.Combine(projectPath, message.Extracts[0].SiteCode + "-CT");
                                string zipPath = Path.Combine(projectPath, message.Extracts[0].SiteCode + "-CT" + ".zip");

                                DirectoryInfo di = new DirectoryInfo(startPath);

                                foreach (DirectoryInfo dir in di.GetDirectories())
                                {
                                    foreach (FileInfo extractsfile in dir.GetFiles("*.json"))
                                    {
                                        byte[] bytes = Encoding.UTF8.GetBytes(await File.ReadAllTextAsync(extractsfile.FullName));
                                        string base64 = Convert.ToBase64String(bytes);
                                        await File.WriteAllTextAsync(extractsfile.FullName, base64);
                                    }

                                }
                                if (File.Exists(zipPath))
                                    File.Delete(zipPath);
                                ZipFile.CreateFromDirectory(startPath, zipPath, CompressionLevel.Fastest, true);



                            }
                            else
                            {
                                var msg = JsonConvert.SerializeObject(message);
                                var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                                var Base64Extract = Convert.ToBase64String(plainTextBytes);
                                string projectPath = Path.Combine(_hostingEnvironment.ContentRootPath + "\\exports");
                                string folderName = Path.Combine(projectPath, message.Extracts[0].SiteCode + "-CT" + "\\extracts");

                                if (!Directory.Exists(folderName))
                                    Directory.CreateDirectory(folderName);

                                string path = folderName + "\\" + messageBag.ExtractName + ".dump" + ".json";

                                await File.WriteAllTextAsync(path, msg);
                                allowSend = false;

                                var sentIds = messageBag.SendIds;
                                sendCound += sentIds.Count;

                                DomainEvents.Dispatch(new CTExtractSentEvent(sentIds, SendStatus.Exported,
                                   messageBag.ExtractType));

                            }
                        }

                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Export Extracts{messageBag.ExtractName} Error");
                        throw;
                    }

                    DomainEvents.Dispatch(new CTExportNotification(new SendProgress(messageBag.ExtractName, messageBag.GetProgress(count, total), recordCount)));

                }           


                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));
            }
            catch (Exception e)
            {
                Log.Error(e, $"Export Extracts {messageBag.ExtractName} Error");
                throw;
            }

            DomainEvents.Dispatch(new CTExportNotification(new SendProgress(messageBag.ExtractName,
                messageBag.GetProgress(count, total), recordCount, true)));

            DomainEvents.Dispatch(new CTStatusNotification(sendTo.ExtractId, sendTo.GetExtractId(messageBag.ExtractName), ExtractStatus.exported, sendCound)
            { UpdatePatient = (messageBag is ArtMessageSourceBag || messageBag is BaselineMessageSourceBag || messageBag is StatusMessageSourceBag) }
            );
            stopWatch.Stop();
            Log.Debug(new string('*', 40));
            Log.Debug($"Sent {recordCount} | {messageBag.ExtractName} in [{stopWatch.ElapsedMilliseconds / 1000}] s");
            Log.Debug(new string('*', 40));
            return responses;
        }

       

        public async Task NotifyPostSending(SendManifestPackageDTO sendTo, string version)
        {
            int maxRetries = 4;
            int retries = 0;
            var notificationend = new HandshakeEnd("CTSendEnd", version);
            DomainEvents.Dispatch(new DwExporthMessageNotification(false, $"Exporting completed"));
            await _mediator.Publish(new HandshakeEnd("CTSendEnd", version));

            Thread.Sleep(3000);

            var client = Client ?? new HttpClient();

            while (retries < maxRetries)
            {
                try
                {
                    var session = _reader.GetSession(notificationend.EndName);
                    var response =
                        await client.PostAsync(
                            sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}Handshake?session={session}"), null);

                    if (!session.IsNullOrEmpty())
                    {
                        Log.Debug(new string('*', 50));
                        Log.Debug("SUCCESS Sent Handshake");
                        Log.Debug(new string('*', 50));
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Handshake Error");
                }
                retries++;
            }
        }

        //BoardRoom Uploads

        public async Task<List<SendCTResponse>> SendFileManifest(IFormFile file)
        {
            var responses = new List<SendCTResponse>();
            string folderName = "Upload";
            string tempfolderName = "Temp";
            string webRootPath = _hostingEnvironment.ContentRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            string tempPath = Path.Combine(webRootPath, tempfolderName);
            
            string text;            
            int count = 0;
            string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            string fullPath = Path.Combine(newPath, fileName);
            String partToExtract = fileName.Split('.')[0];
            string tempFullPath = Path.Combine(tempPath, partToExtract);
            try
            {


                using (ZipArchive archive = ZipFile.OpenRead(fullPath))
                {
                   
                        for (int i = 0; i < archive.Entries.Count; i++)
                       {
                        if (archive.Entries[i].Name == "manifest.dump.json")
                        {
                            int retryCount = 0;
                            bool allowSend = true;

                            //allowSend = false;
                            //var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                            //responses.Add(res);
                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                            if (!Directory.Exists(tempFullPath))
                                Directory.CreateDirectory(tempFullPath);

                            archive.Entries[i].ExtractToFile(destinationPath,true);

                            //string result = "";
                            var url = "http://localhost:21751/api/file/manifest";
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:21751/api/file/manifest")
                            };
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                            client.Timeout = TimeSpan.FromMinutes(59);
                            var filestream = File.OpenRead(destinationPath);
                            using (var content = new MultipartFormDataContent())
                            {
                                content.Add(new StreamContent(filestream)
                                {
                                    Headers =
                                                {
                                                    ContentLength = archive.Entries[i].Length
                                                    
                                                }
                                }, "File", fileName);

                                var response = await client.PostAsync(url, content);
                                if (response.IsSuccessStatusCode)
                                {
                                    allowSend = false;
                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);                                    
                                }
                            }
                        }
                        break;


                    }
                    for (int i = 1; i < archive.Entries.Count; i++)
                    {
                        if (archive.Entries[i].Name == "PatientExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                        {
                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                            if (!Directory.Exists(tempFullPath))
                                Directory.CreateDirectory(tempFullPath);

                            archive.Entries[i].ExtractToFile(destinationPath, true);

                            //string result = "";
                            var url = "http://localhost:21751/api/file/patients";
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:21751/api/file/patients")
                            };
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                            client.Timeout = TimeSpan.FromMinutes(59);
                            var filestream = File.OpenRead(destinationPath);
                            using (var content = new MultipartFormDataContent())
                            {
                                content.Add(new StreamContent(filestream)
                                {
                                    Headers =
                                                {
                                                    ContentLength = archive.Entries[i].Length

                                                }
                                }, "File", fileName);

                                var response = await client.PostAsync(url, content);
                                if (response.IsSuccessStatusCode)
                                {
                                    
                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);
                                }
                                else
                                {
                                        var error = await response.Content.ReadAsStringAsync();
                                       
                                        throw new Exception(error);
                                    
                                }
                            }
                        }
                       
                    }
                    for (int i = 1; i < archive.Entries.Count; i++)
                    {

                        if (archive.Entries[i].Name == "AllergiesChronicIllnessExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                        {
                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                            if (!Directory.Exists(tempFullPath))
                                Directory.CreateDirectory(tempFullPath);

                            archive.Entries[i].ExtractToFile(destinationPath, true);

                            //string result = "";
                            var url = "http://localhost:21751/api/file/allergiesChronic";
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:21751/api/file/allergiesChronic")
                            };
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                            client.Timeout = TimeSpan.FromMinutes(59);
                            var filestream = File.OpenRead(destinationPath);
                            using (var content = new MultipartFormDataContent())
                            {
                                content.Add(new StreamContent(filestream)
                                {
                                    Headers =
                                                {
                                                    ContentLength = archive.Entries[i].Length

                                                }
                                }, "File", fileName);

                                var response = await client.PostAsync(url, content);
                                if (response.IsSuccessStatusCode)
                                {

                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);
                                }
                            }

                        }
                        else if (archive.Entries[i].Name == "ContactListingExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                        {
                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                            if (!Directory.Exists(tempFullPath))
                                Directory.CreateDirectory(tempFullPath);

                            archive.Entries[i].ExtractToFile(destinationPath, true);

                            //string result = "";
                            var url = "http://localhost:21751/api/file/contactlisting";
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:21751/api/file/contactlisting")
                            };
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                            client.Timeout = TimeSpan.FromMinutes(59);
                            var filestream = File.OpenRead(destinationPath);
                            using (var content = new MultipartFormDataContent())
                            {
                                content.Add(new StreamContent(filestream)
                                {
                                    Headers =
                                                {
                                                    ContentLength = archive.Entries[i].Length

                                                }
                                }, "File", fileName);

                                var response = await client.PostAsync(url, content);
                                if (response.IsSuccessStatusCode)
                                {

                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);
                                }
                            }
                        }
                        else if (archive.Entries[i].Name == "CovidExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                        {

                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                            if (!Directory.Exists(tempFullPath))
                                Directory.CreateDirectory(tempFullPath);

                            archive.Entries[i].ExtractToFile(destinationPath, true);

                            //string result = "";
                            var url = "http://localhost:21751/api/file/covid";
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:21751/api/file/covid")
                            };
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                            client.Timeout = TimeSpan.FromMinutes(59);
                            var filestream = File.OpenRead(destinationPath);
                            using (var content = new MultipartFormDataContent())
                            {
                                content.Add(new StreamContent(filestream)
                                {
                                    Headers =
                                                {
                                                    ContentLength = archive.Entries[i].Length

                                                }
                                }, "File", fileName);

                                var response = await client.PostAsync(url, content);
                                if (response.IsSuccessStatusCode)
                                {

                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);
                                }
                            }
                        }
                        else if (archive.Entries[i].Name == "DefaulterTracingExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                        {

                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                            if (!Directory.Exists(tempFullPath))
                                Directory.CreateDirectory(tempFullPath);

                            archive.Entries[i].ExtractToFile(destinationPath, true);

                            //string result = "";
                            var url = "http://localhost:21751/api/file/defaulttracer";
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:21751/api/file/defaulttracer")
                            };
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                            client.Timeout = TimeSpan.FromMinutes(59);
                            var filestream = File.OpenRead(destinationPath);
                            using (var content = new MultipartFormDataContent())
                            {
                                content.Add(new StreamContent(filestream)
                                {
                                    Headers =
                                                {
                                                    ContentLength = archive.Entries[i].Length

                                                }
                                }, "File", fileName);

                                var response = await client.PostAsync(url, content);
                                if (response.IsSuccessStatusCode)
                                {

                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);
                                }
                            }
                        }
                        else if (archive.Entries[i].Name == "DepressionScreeningExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                        {
                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                            if (!Directory.Exists(tempFullPath))
                                Directory.CreateDirectory(tempFullPath);

                            archive.Entries[i].ExtractToFile(destinationPath, true);

                            //string result = "";
                            var url = "http://localhost:21751/api/file/depressionscreening";
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:21751/api/file/depressionscreening")
                            };
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                            client.Timeout = TimeSpan.FromMinutes(59);
                            var filestream = File.OpenRead(destinationPath);
                            using (var content = new MultipartFormDataContent())
                            {
                                content.Add(new StreamContent(filestream)
                                {
                                    Headers =
                                                {
                                                    ContentLength = archive.Entries[i].Length

                                                }
                                }, "File", fileName);

                                var response = await client.PostAsync(url, content);
                                if (response.IsSuccessStatusCode)
                                {

                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);
                                }
                            }

                        }
                        else if (archive.Entries[i].Name == "DrugAlcoholScreeningExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                        {
                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                            if (!Directory.Exists(tempFullPath))
                                Directory.CreateDirectory(tempFullPath);

                            archive.Entries[i].ExtractToFile(destinationPath, true);

                            //string result = "";
                            var url = "http://localhost:21751/api/file/drugalcoholscreening";
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:21751/api/file/drugalcoholscreening")
                            };
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                            client.Timeout = TimeSpan.FromMinutes(59);
                            var filestream = File.OpenRead(destinationPath);
                            using (var content = new MultipartFormDataContent())
                            {
                                content.Add(new StreamContent(filestream)
                                {
                                    Headers =
                                                {
                                                    ContentLength = archive.Entries[i].Length

                                                }
                                }, "File", fileName);

                                var response = await client.PostAsync(url, content);
                                if (response.IsSuccessStatusCode)
                                {

                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);
                                }
                            }
                        }
                        else if (archive.Entries[i].Name == "EnhancedAdherenceCounsellingExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                        {
                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                            if (!Directory.Exists(tempFullPath))
                                Directory.CreateDirectory(tempFullPath);

                            archive.Entries[i].ExtractToFile(destinationPath, true);

                            //string result = "";
                            var url = "http://localhost:21751/api/file/enhancedadherence";
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:21751/api/file/enhancedadherence")
                            };
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                            client.Timeout = TimeSpan.FromMinutes(59);
                            var filestream = File.OpenRead(destinationPath);
                            using (var content = new MultipartFormDataContent())
                            {
                                content.Add(new StreamContent(filestream)
                                {
                                    Headers =
                                                {
                                                    ContentLength = archive.Entries[i].Length

                                                }
                                }, "File", fileName);

                                var response = await client.PostAsync(url, content);
                                if (response.IsSuccessStatusCode)
                                {

                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);
                                }
                            }
                        }
                        else if (archive.Entries[i].Name == "GbvScreeningExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                        {
                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                            if (!Directory.Exists(tempFullPath))
                                Directory.CreateDirectory(tempFullPath);

                            archive.Entries[i].ExtractToFile(destinationPath, true);

                            //string result = "";
                            var url = "http://localhost:21751/api/file/gbvscreening";
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:21751/api/file/gbvscreening")
                            };
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                            client.Timeout = TimeSpan.FromMinutes(59);
                            var filestream = File.OpenRead(destinationPath);
                            using (var content = new MultipartFormDataContent())
                            {
                                content.Add(new StreamContent(filestream)
                                {
                                    Headers =
                                                {
                                                    ContentLength = archive.Entries[i].Length

                                                }
                                }, "File", fileName);

                                var response = await client.PostAsync(url, content);
                                if (response.IsSuccessStatusCode)
                                {

                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);
                                }
                            }
                        }
                        else if (archive.Entries[i].Name == "IptExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                        {
                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                            if (!Directory.Exists(tempFullPath))
                                Directory.CreateDirectory(tempFullPath);

                            archive.Entries[i].ExtractToFile(destinationPath, true);

                            //string result = "";
                            var url = "http://localhost:21751/api/file/ipt";
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:21751/api/file/ipt")
                            };
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                            client.Timeout = TimeSpan.FromMinutes(59);
                            var filestream = File.OpenRead(destinationPath);
                            using (var content = new MultipartFormDataContent())
                            {
                                content.Add(new StreamContent(filestream)
                                {
                                    Headers =
                                                {
                                                    ContentLength = archive.Entries[i].Length

                                                }
                                }, "File", fileName);

                                var response = await client.PostAsync(url, content);
                                if (response.IsSuccessStatusCode)
                                {

                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);
                                }
                            }

                        }
                        else if (archive.Entries[i].Name == "OtzExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                        {
                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                            if (!Directory.Exists(tempFullPath))
                                Directory.CreateDirectory(tempFullPath);

                            archive.Entries[i].ExtractToFile(destinationPath, true);

                            //string result = "";
                            var url = "http://localhost:21751/api/file/otz";
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:21751/api/file/otz")
                            };
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                            client.Timeout = TimeSpan.FromMinutes(59);
                            var filestream = File.OpenRead(destinationPath);
                            using (var content = new MultipartFormDataContent())
                            {
                                content.Add(new StreamContent(filestream)
                                {
                                    Headers =
                                                {
                                                    ContentLength = archive.Entries[i].Length

                                                }
                                }, "File", fileName);

                                var response = await client.PostAsync(url, content);
                                if (response.IsSuccessStatusCode)
                                {

                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);
                                }
                            }
                        }
                        else if (archive.Entries[i].Name == "OvcExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                        {
                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                            if (!Directory.Exists(tempFullPath))
                                Directory.CreateDirectory(tempFullPath);

                            archive.Entries[i].ExtractToFile(destinationPath, true);

                            //string result = "";
                            var url = "http://localhost:21751/api/file/ovc";
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:21751/api/file/ovc")
                            };
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                            client.Timeout = TimeSpan.FromMinutes(59);
                            var filestream = File.OpenRead(destinationPath);
                            using (var content = new MultipartFormDataContent())
                            {
                                content.Add(new StreamContent(filestream)
                                {
                                    Headers =
                                                {
                                                    ContentLength = archive.Entries[i].Length

                                                }
                                }, "File", fileName);

                                var response = await client.PostAsync(url, content);
                                if (response.IsSuccessStatusCode)
                                {

                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);
                                }
                            }

                        }
                        else if (archive.Entries[i].Name == "PatientAdverseEventExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                        {
                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                            if (!Directory.Exists(tempFullPath))
                                Directory.CreateDirectory(tempFullPath);

                            archive.Entries[i].ExtractToFile(destinationPath, true);

                            //string result = "";
                            var url = "http://localhost:21751/api/file/patientadverse";
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:21751/api/file/patientadverse")
                            };
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                            client.Timeout = TimeSpan.FromMinutes(59);
                            var filestream = File.OpenRead(destinationPath);
                            using (var content = new MultipartFormDataContent())
                            {
                                content.Add(new StreamContent(filestream)
                                {
                                    Headers =
                                                {
                                                    ContentLength = archive.Entries[i].Length

                                                }
                                }, "File", fileName);

                                var response = await client.PostAsync(url, content);
                                if (response.IsSuccessStatusCode)
                                {

                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);
                                }
                            }

                        }
                        else if (archive.Entries[i].Name == "PatientArtExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                        {
                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                            if (!Directory.Exists(tempFullPath))
                                Directory.CreateDirectory(tempFullPath);

                            archive.Entries[i].ExtractToFile(destinationPath, true);

                            //string result = "";
                            var url = "http://localhost:21751/api/file/patientart";
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:21751/api/file/patientart")
                            };
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                            client.Timeout = TimeSpan.FromMinutes(59);
                            var filestream = File.OpenRead(destinationPath);
                            using (var content = new MultipartFormDataContent())
                            {
                                content.Add(new StreamContent(filestream)
                                {
                                    Headers =
                                                {
                                                    ContentLength = archive.Entries[i].Length

                                                }
                                }, "File", fileName);

                                var response = await client.PostAsync(url, content);
                                if (response.IsSuccessStatusCode)
                                {

                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);
                                }
                            }

                        }
                        else if (archive.Entries[i].Name == "PatientBaselineExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                        {
                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                            if (!Directory.Exists(tempFullPath))
                                Directory.CreateDirectory(tempFullPath);

                            archive.Entries[i].ExtractToFile(destinationPath, true);

                            //string result = "";
                            var url = "http://localhost:21751/api/file/patientbaseline";
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:21751/api/file/patientbaseline")
                            };
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                            client.Timeout = TimeSpan.FromMinutes(59);
                            var filestream = File.OpenRead(destinationPath);
                            using (var content = new MultipartFormDataContent())
                            {
                                content.Add(new StreamContent(filestream)
                                {
                                    Headers =
                                                {
                                                    ContentLength = archive.Entries[i].Length

                                                }
                                }, "File", fileName);

                                var response = await client.PostAsync(url, content);
                                if (response.IsSuccessStatusCode)
                                {

                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);
                                }
                            }

                        }
                        else if (archive.Entries[i].Name == "PatientLabExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                        {
                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                            if (!Directory.Exists(tempFullPath))
                                Directory.CreateDirectory(tempFullPath);

                            archive.Entries[i].ExtractToFile(destinationPath, true);

                            //string result = "";
                            var url = "http://localhost:21751/api/file/patientlab";
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:21751/api/file/patientlab")
                            };
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                            client.Timeout = TimeSpan.FromMinutes(59);
                            var filestream = File.OpenRead(destinationPath);
                            using (var content = new MultipartFormDataContent())
                            {
                                content.Add(new StreamContent(filestream)
                                {
                                    Headers =
                                                {
                                                    ContentLength = archive.Entries[i].Length

                                                }
                                }, "File", fileName);

                                var response = await client.PostAsync(url, content);
                                if (response.IsSuccessStatusCode)
                                {

                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);
                                }
                            }

                        }

                        else if (archive.Entries[i].Name == "PatientPharmacyExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                        {
                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                            if (!Directory.Exists(tempFullPath))
                                Directory.CreateDirectory(tempFullPath);

                            archive.Entries[i].ExtractToFile(destinationPath, true);

                            //string result = "";
                            var url = "http://localhost:21751/api/file/patientpharmacy";
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:21751/api/file/patientpharmacy")
                            };
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                            client.Timeout = TimeSpan.FromMinutes(59);
                            var filestream = File.OpenRead(destinationPath);
                            using (var content = new MultipartFormDataContent())
                            {
                                content.Add(new StreamContent(filestream)
                                {
                                    Headers =
                                                {
                                                    ContentLength = archive.Entries[i].Length

                                                }
                                }, "File", fileName);

                                var response = await client.PostAsync(url, content);
                                if (response.IsSuccessStatusCode)
                                {

                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);
                                }
                            }

                        }
                        else if (archive.Entries[i].Name == "PatientStatusExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                        {
                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                            if (!Directory.Exists(tempFullPath))
                                Directory.CreateDirectory(tempFullPath);

                            archive.Entries[i].ExtractToFile(destinationPath, true);

                            //string result = "";
                            var url = "http://localhost:21751/api/file/patientstatus";
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:21751/api/file/patientstatus")
                            };
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                            client.Timeout = TimeSpan.FromMinutes(59);
                            var filestream = File.OpenRead(destinationPath);
                            using (var content = new MultipartFormDataContent())
                            {
                                content.Add(new StreamContent(filestream)
                                {
                                    Headers =
                                                {
                                                    ContentLength = archive.Entries[i].Length

                                                }
                                }, "File", fileName);

                                var response = await client.PostAsync(url, content);
                                if (response.IsSuccessStatusCode)
                                {

                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);
                                }
                            }

                        }
                        else if (archive.Entries[i].Name == "PatientVisitExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                        {
                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                            if (!Directory.Exists(tempFullPath))
                                Directory.CreateDirectory(tempFullPath);

                            archive.Entries[i].ExtractToFile(destinationPath, true);

                           
                            //string result = "";
                            var url = "http://localhost:21751/api/file/patientvisit";
                            var client = new HttpClient
                            {
                                BaseAddress = new Uri("http://localhost:21751/api/file/patientvisit")
                            };
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                            client.Timeout = TimeSpan.FromMinutes(59);
                            var filestream = File.OpenRead(destinationPath);
                            using (var content = new MultipartFormDataContent())
                            {
                                content.Add(new StreamContent(filestream)
                                {
                                        Headers =
                                                    {
                                                        ContentLength = archive.Entries[i].Length

                                                    }
                                }, "File", fileName);

                                var response = await client.PostAsync(url, content);
                                if (response.IsSuccessStatusCode)
                                {                                   
                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);                                  


                                    var tlog = TransportLog.GenerateExtract("NDWH", archive.Entries[i].Name, res.JobId);
                                    _transportLogRepository.CreateLatest(tlog);
                                }
                               // DomainEvents.Dispatch(new CTSendNotification(new SendProgress(archive.Entries[i].Name, IMessageSourceBag<PatientVisitExtract>.GetProgress(count, total), recordCount)));
                            }
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Log.Error(e, $"Send Extracts Error");
                throw;
            }

            return responses;
        }
       

        public async Task<List<SendCTResponse>> SendFileManifest(IFormFile file, string apiVersion = "")
        {
            var responses = new List<SendCTResponse>();
            
            string folderName = "Upload";
            string tempfolderName = "Temp";
            string webRootPath = _hostingEnvironment.ContentRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            string tempPath = Path.Combine(webRootPath, tempfolderName);

            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            var client = Client ?? new HttpClient(handler);
            string text;            
            string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            string fullPath = Path.Combine(newPath, fileName);
            String partToExtract = fileName.Split('.')[0];
            string tempFullPath = Path.Combine(tempPath, partToExtract);
            using (ZipArchive archive = ZipFile.OpenRead(fullPath))
                {
                    for (int i = 0; i < archive.Entries.Count; i++)
                    {
                        if (archive.Entries[i].Name == "manifest.dump.json")
                        {
                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                            var filestream = File.OpenRead(destinationPath);
                            using (StreamReader sr = new StreamReader(filestream))
                            {
                                try
                                {
                                    text = await sr.ReadToEndAsync(); // OK                         

                                    byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                    var Extract = Encoding.UTF8.GetString(base64EncodedBytes);

                                    DwhManifest manifest = JsonConvert.DeserializeObject<DwhManifest>(Extract);

                                    var start = DateTime.Now;
                                    var response = await client.PostAsJsonAsync("http://localhost:21751/api/v3/spot", manifest);
                                    if (response.IsSuccessStatusCode)
                                    {


                                        var content = await response.Content.ReadAsJsonAsync<ManifestResponse>();
                                        responses.Add(new SendCTResponse());

                                        var tlog = TransportLog.GenerateManifest("NDWH", content.JobId,
                                            new Guid(content.ManifestId), content.Code, start, content.FacilityId);
                                        _transportLogRepository.CreateLatest(tlog);
                                        Log.Debug(new string('+', 40));
                                        Log.Debug($"CONNECTED: {"http://localhost:21751/api/v3/spot"}");
                                        Log.Debug(new string('_', 40));
                                        Log.Debug($"RECIEVED: {content}");
                                        Log.Debug(new string('+', 40));

                                    }
                                    else
                                    {
                                        var error = await response.Content.ReadAsStringAsync();
                                        throw new Exception(error);
                                    }                                       
                                    
                                }
                            catch(Exception ex) { }

                                    
                                }
                              
                            }
                    break;
                           
                            
                        }
                for (int i = 1; i < archive.Entries.Count; i++)
                {
                    if (archive.Entries[i].Name == "PatientExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                PatientMessageSourceBag messageBag = JsonConvert.DeserializeObject<PatientMessageSourceBag>(Extract);
                                var batchSize = 2000;
                                
                                var packageInfo = _packager.GetPackageInfo<PatientExtractView>(batchSize);
                                int sendCound = 0;
                                int count = 0;
                                int total = packageInfo.PageCount;
                                int overall = 0;
                                long recordCount = 0;
                                int retryCount = 0;
                                bool allowSend = true;

                                string jobId = string.Empty; Guid manifestId; Guid facilityId;
                                var manifest = _transportLogRepository.GetManifest();

                                if (messageBag.ExtractName == nameof(PatientExtract))
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    if (null == mainExtract)
                                    {
                                        jobId = manifest.JobId;
                                        manifestId = manifest.ManifestId;
                                        facilityId = manifest.FacilityId;
                                    }
                                    else
                                    {
                                        jobId = mainExtract.JobId;
                                        manifestId = mainExtract.ManifestId;
                                        facilityId = mainExtract.FacilityId;
                                    }
                                }
                                else
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    jobId = mainExtract.JobId;
                                    manifestId = mainExtract.ManifestId;
                                    facilityId = mainExtract.FacilityId;
                                }

                                for (int page = 1; page <= packageInfo.PageCount; page++)
                                {
                                    count++;
                                    var extracts = _packager.GenerateSmartBatchExtracts<PatientExtractView>(page, packageInfo.PageSize).ToList();
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    count++;                                    
                                    try
                                    {                                        
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync("http://localhost:21751/api/v3/patient", message);
                                            if (response.IsSuccessStatusCode)
                                            {
                                                allowSend = false;
                                                var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                                responses.Add(res);

                                                var sentIds = messageBag.SendIds;
                                                sendCound += sentIds.Count;
                                                DomainEvents.Dispatch(new CTExtractSentEvent(sentIds, SendStatus.Sent,
                                                    messageBag.ExtractType));

                                                var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, res.JobId);
                                                _transportLogRepository.CreateLatest(tlog);
                                            }
                                            else
                                            {
                                                retryCount++;
                                                if (retryCount == 4)
                                                {
                                                    var sentIds = messageBag.SendIds;
                                                    var error = await response.Content.ReadAsStringAsync();
                                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                                        error));
                                                    throw new Exception(error);
                                                }
                                            }
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error");
                                        throw;
                                    }

                                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName, messageBag.GetProgress(count, total), recordCount)));

                                }

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) { }


                        }
                    }

                }
                for (int i = 1; i < archive.Entries.Count; i++)
                {

                    if (archive.Entries[i].Name == "AllergiesChronicIllnessExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {

                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                AllergiesChronicIllnessMessageSourceBag messageBag = JsonConvert.DeserializeObject<AllergiesChronicIllnessMessageSourceBag>(Extract);
                                var batchSize = 2000;

                                var packageInfo = _packager.GetPackageInfo<AllergiesChronicIllnessExtractView>(batchSize);
                                int sendCound = 0;
                                int count = 0;
                                int total = packageInfo.PageCount;
                                int overall = 0;
                                long recordCount = 0;
                                int retryCount = 0;
                                bool allowSend = true;

                                string jobId = string.Empty; Guid manifestId; Guid facilityId;
                                var manifest = _transportLogRepository.GetManifest();

                                if (messageBag.ExtractName == nameof(PatientExtract))
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    if (null == mainExtract)
                                    {
                                        jobId = manifest.JobId;
                                        manifestId = manifest.ManifestId;
                                        facilityId = manifest.FacilityId;
                                    }
                                    else
                                    {
                                        jobId = mainExtract.JobId;
                                        manifestId = mainExtract.ManifestId;
                                        facilityId = mainExtract.FacilityId;
                                    }
                                }
                                else
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    jobId = mainExtract.JobId;
                                    manifestId = mainExtract.ManifestId;
                                    facilityId = mainExtract.FacilityId;
                                }

                                for (int page = 1; page <= packageInfo.PageCount; page++)
                                {
                                    count++;
                                    var extracts = _packager.GenerateSmartBatchExtracts<AllergiesChronicIllnessExtractView>(page, packageInfo.PageSize).ToList();
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    count++;
                                    try
                                    {
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync("http://localhost:21751/api/v3/allergieschronicIllness", message);
                                            if (response.IsSuccessStatusCode)
                                            {
                                                allowSend = false;
                                                var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                                responses.Add(res);

                                                var sentIds = messageBag.SendIds;
                                                sendCound += sentIds.Count;
                                                DomainEvents.Dispatch(new CTExtractSentEvent(sentIds, SendStatus.Sent,
                                                    messageBag.ExtractType));

                                                var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, res.JobId);
                                                _transportLogRepository.CreateLatest(tlog);
                                            }
                                            else
                                            {
                                                retryCount++;
                                                if (retryCount == 4)
                                                {
                                                    var sentIds = messageBag.SendIds;
                                                    var error = await response.Content.ReadAsStringAsync();
                                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                                        error));
                                                    throw new Exception(error);
                                                }
                                            }
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error");
                                        throw;
                                    }

                                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName, messageBag.GetProgress(count, total), recordCount)));

                                }

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) { }


                        }
                    }
                    else if (archive.Entries[i].Name == "ContactListingExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                ContactListingMessageSourceBag messageBag = JsonConvert.DeserializeObject<ContactListingMessageSourceBag>(Extract);
                                var batchSize = 2000;

                                var packageInfo = _packager.GetPackageInfo<ContactListingExtractView>(batchSize);
                                int sendCound = 0;
                                int count = 0;
                                int total = packageInfo.PageCount;
                                int overall = 0;
                                long recordCount = 0;
                                int retryCount = 0;
                                bool allowSend = true;

                                string jobId = string.Empty; Guid manifestId; Guid facilityId;
                                var manifest = _transportLogRepository.GetManifest();

                                if (messageBag.ExtractName == nameof(PatientExtract))
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    if (null == mainExtract)
                                    {
                                        jobId = manifest.JobId;
                                        manifestId = manifest.ManifestId;
                                        facilityId = manifest.FacilityId;
                                    }
                                    else
                                    {
                                        jobId = mainExtract.JobId;
                                        manifestId = mainExtract.ManifestId;
                                        facilityId = mainExtract.FacilityId;
                                    }
                                }
                                else
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    jobId = mainExtract.JobId;
                                    manifestId = mainExtract.ManifestId;
                                    facilityId = mainExtract.FacilityId;
                                }

                                for (int page = 1; page <= packageInfo.PageCount; page++)
                                {
                                    count++;
                                    var extracts = _packager.GenerateSmartBatchExtracts<ContactListingExtractView>(page, packageInfo.PageSize).ToList();
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    count++;
                                    try
                                    {
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync("http://localhost:21751/api/v3/contactlisting", message);
                                            if (response.IsSuccessStatusCode)
                                            {
                                                allowSend = false;
                                                var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                                responses.Add(res);

                                                var sentIds = messageBag.SendIds;
                                                sendCound += sentIds.Count;                                               

                                                var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, res.JobId);
                                                _transportLogRepository.CreateLatest(tlog);
                                            }
                                            else
                                            {
                                                retryCount++;
                                                if (retryCount == 4)
                                                {
                                                    var sentIds = messageBag.SendIds;
                                                    var error = await response.Content.ReadAsStringAsync();
                                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                                        error));
                                                    throw new Exception(error);
                                                }
                                            }
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error");
                                        throw;
                                    }

                                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName, messageBag.GetProgress(count, total), recordCount)));

                                }

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) { }


                        }
                    }
                    else if (archive.Entries[i].Name == "CovidExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                CovidMessageSourceBag messageBag = JsonConvert.DeserializeObject<CovidMessageSourceBag>(Extract);
                                var batchSize = 2000;

                                var packageInfo = _packager.GetPackageInfo<CovidExtractView>(batchSize);
                                int sendCound = 0;
                                int count = 0;
                                int total = packageInfo.PageCount;
                                int overall = 0;
                                long recordCount = 0;
                                int retryCount = 0;
                                bool allowSend = true;

                                string jobId = string.Empty; Guid manifestId; Guid facilityId;
                                var manifest = _transportLogRepository.GetManifest();

                                if (messageBag.ExtractName == nameof(PatientExtract))
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    if (null == mainExtract)
                                    {
                                        jobId = manifest.JobId;
                                        manifestId = manifest.ManifestId;
                                        facilityId = manifest.FacilityId;
                                    }
                                    else
                                    {
                                        jobId = mainExtract.JobId;
                                        manifestId = mainExtract.ManifestId;
                                        facilityId = mainExtract.FacilityId;
                                    }
                                }
                                else
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    jobId = mainExtract.JobId;
                                    manifestId = mainExtract.ManifestId;
                                    facilityId = mainExtract.FacilityId;
                                }

                                for (int page = 1; page <= packageInfo.PageCount; page++)
                                {
                                    count++;
                                    var extracts = _packager.GenerateSmartBatchExtracts<CovidExtractView>(page, packageInfo.PageSize).ToList();
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    count++;
                                    try
                                    {
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync("http://localhost:21751/api/v3/covid", message);
                                            if (response.IsSuccessStatusCode)
                                            {
                                                allowSend = false;
                                                var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                                responses.Add(res);

                                                var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, res.JobId);
                                                _transportLogRepository.CreateLatest(tlog);
                                            }
                                            else
                                            {
                                                retryCount++;
                                                if (retryCount == 4)
                                                {
                                                    var sentIds = messageBag.SendIds;
                                                    var error = await response.Content.ReadAsStringAsync();
                                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                                        error));
                                                    throw new Exception(error);
                                                }
                                            }
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error");
                                        throw;
                                    }

                                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName, messageBag.GetProgress(count, total), recordCount)));

                                }

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) { }


                        }
                    }
                    else if (archive.Entries[i].Name == "DefaulterTracingExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {

                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                DefaulterTracingMessageSourceBag messageBag = JsonConvert.DeserializeObject<DefaulterTracingMessageSourceBag>(Extract);
                                var batchSize = 2000;

                                var packageInfo = _packager.GetPackageInfo<DefaulterTracingExtractView>(batchSize);
                                int sendCound = 0;
                                int count = 0;
                                int total = packageInfo.PageCount;
                                int overall = 0;
                                long recordCount = 0;
                                int retryCount = 0;
                                bool allowSend = true;

                                string jobId = string.Empty; Guid manifestId; Guid facilityId;
                                var manifest = _transportLogRepository.GetManifest();

                                if (messageBag.ExtractName == nameof(PatientExtract))
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    if (null == mainExtract)
                                    {
                                        jobId = manifest.JobId;
                                        manifestId = manifest.ManifestId;
                                        facilityId = manifest.FacilityId;
                                    }
                                    else
                                    {
                                        jobId = mainExtract.JobId;
                                        manifestId = mainExtract.ManifestId;
                                        facilityId = mainExtract.FacilityId;
                                    }
                                }
                                else
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    jobId = mainExtract.JobId;
                                    manifestId = mainExtract.ManifestId;
                                    facilityId = mainExtract.FacilityId;
                                }

                                for (int page = 1; page <= packageInfo.PageCount; page++)
                                {
                                    count++;
                                    var extracts = _packager.GenerateSmartBatchExtracts<DefaulterTracingExtractView>(page, packageInfo.PageSize).ToList();
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    count++;
                                    try
                                    {
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync("http://localhost:21751/api/v3/defaultertracing", message);
                                            if (response.IsSuccessStatusCode)
                                            {
                                                allowSend = false;
                                                var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                                responses.Add(res);

                                                var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, res.JobId);
                                                _transportLogRepository.CreateLatest(tlog);
                                            }
                                            else
                                            {
                                                retryCount++;
                                                if (retryCount == 4)
                                                {
                                                    var sentIds = messageBag.SendIds;
                                                    var error = await response.Content.ReadAsStringAsync();
                                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                                        error));
                                                    throw new Exception(error);
                                                }
                                            }
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error");
                                        throw;
                                    }

                                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName, messageBag.GetProgress(count, total), recordCount)));

                                }

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) { }


                        }
                    }
                    else if (archive.Entries[i].Name == "DepressionScreeningExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                DepressionScreeningMessageSourceBag messageBag = JsonConvert.DeserializeObject<DepressionScreeningMessageSourceBag>(Extract);
                                var batchSize = 2000;

                                var packageInfo = _packager.GetPackageInfo<DepressionScreeningExtractView>(batchSize);
                                int sendCound = 0;
                                int count = 0;
                                int total = packageInfo.PageCount;
                                int overall = 0;
                                long recordCount = 0;
                                int retryCount = 0;
                                bool allowSend = true;

                                string jobId = string.Empty; Guid manifestId; Guid facilityId;
                                var manifest = _transportLogRepository.GetManifest();

                                if (messageBag.ExtractName == nameof(PatientExtract))
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    if (null == mainExtract)
                                    {
                                        jobId = manifest.JobId;
                                        manifestId = manifest.ManifestId;
                                        facilityId = manifest.FacilityId;
                                    }
                                    else
                                    {
                                        jobId = mainExtract.JobId;
                                        manifestId = mainExtract.ManifestId;
                                        facilityId = mainExtract.FacilityId;
                                    }
                                }
                                else
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    jobId = mainExtract.JobId;
                                    manifestId = mainExtract.ManifestId;
                                    facilityId = mainExtract.FacilityId;
                                }

                                for (int page = 1; page <= packageInfo.PageCount; page++)
                                {
                                    count++;
                                    var extracts = _packager.GenerateSmartBatchExtracts<DepressionScreeningExtractView>(page, packageInfo.PageSize).ToList();
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    count++;
                                    try
                                    {
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync("http://localhost:21751/api/v3/depressionscreening", message);
                                            if (response.IsSuccessStatusCode)
                                            {
                                                allowSend = false;
                                                var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                                responses.Add(res);

                                                var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, res.JobId);
                                                _transportLogRepository.CreateLatest(tlog);
                                            }
                                            else
                                            {
                                                retryCount++;
                                                if (retryCount == 4)
                                                {
                                                    var sentIds = messageBag.SendIds;
                                                    var error = await response.Content.ReadAsStringAsync();
                                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                                        error));
                                                    throw new Exception(error);
                                                }
                                            }
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error");
                                        throw;
                                    }

                                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName, messageBag.GetProgress(count, total), recordCount)));

                                }

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) { }


                        }

                    }
                    else if (archive.Entries[i].Name == "DrugAlcoholScreeningExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                DrugAlcoholScreeningMessageSourceBag messageBag = JsonConvert.DeserializeObject<DrugAlcoholScreeningMessageSourceBag>(Extract);
                                var batchSize = 2000;

                                var packageInfo = _packager.GetPackageInfo<DrugAlcoholScreeningExtractView>(batchSize);
                                int sendCound = 0;
                                int count = 0;
                                int total = packageInfo.PageCount;
                                int overall = 0;
                                long recordCount = 0;
                                int retryCount = 0;
                                bool allowSend = true;

                                string jobId = string.Empty; Guid manifestId; Guid facilityId;
                                var manifest = _transportLogRepository.GetManifest();

                                if (messageBag.ExtractName == nameof(PatientExtract))
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    if (null == mainExtract)
                                    {
                                        jobId = manifest.JobId;
                                        manifestId = manifest.ManifestId;
                                        facilityId = manifest.FacilityId;
                                    }
                                    else
                                    {
                                        jobId = mainExtract.JobId;
                                        manifestId = mainExtract.ManifestId;
                                        facilityId = mainExtract.FacilityId;
                                    }
                                }
                                else
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    jobId = mainExtract.JobId;
                                    manifestId = mainExtract.ManifestId;
                                    facilityId = mainExtract.FacilityId;
                                }

                                for (int page = 1; page <= packageInfo.PageCount; page++)
                                {
                                    count++;
                                    var extracts = _packager.GenerateSmartBatchExtracts<DrugAlcoholScreeningExtractView>(page, packageInfo.PageSize).ToList();
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    count++;
                                    try
                                    {
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync("http://localhost:21751/api/v3/DrugAlcoholScreening", message);
                                            if (response.IsSuccessStatusCode)
                                            {
                                                allowSend = false;
                                                var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                                responses.Add(res);

                                                var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, res.JobId);
                                                _transportLogRepository.CreateLatest(tlog);
                                            }
                                            else
                                            {
                                                retryCount++;
                                                if (retryCount == 4)
                                                {
                                                    var sentIds = messageBag.SendIds;
                                                    var error = await response.Content.ReadAsStringAsync();
                                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                                        error));
                                                    throw new Exception(error);
                                                }
                                            }
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error");
                                        throw;
                                    }

                                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName, messageBag.GetProgress(count, total), recordCount)));

                                }

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) { }


                        }
                    }
                    else if (archive.Entries[i].Name == "EnhancedAdherenceCounsellingExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                EnhancedAdherenceCounsellingMessageSourceBag messageBag = JsonConvert.DeserializeObject<EnhancedAdherenceCounsellingMessageSourceBag>(Extract);
                                var batchSize = 2000;

                                var packageInfo = _packager.GetPackageInfo<EnhancedAdherenceCounsellingExtractView>(batchSize);
                                int sendCound = 0;
                                int count = 0;
                                int total = packageInfo.PageCount;
                                int overall = 0;
                                long recordCount = 0;
                                int retryCount = 0;
                                bool allowSend = true;

                                string jobId = string.Empty; Guid manifestId; Guid facilityId;
                                var manifest = _transportLogRepository.GetManifest();

                                if (messageBag.ExtractName == nameof(PatientExtract))
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    if (null == mainExtract)
                                    {
                                        jobId = manifest.JobId;
                                        manifestId = manifest.ManifestId;
                                        facilityId = manifest.FacilityId;
                                    }
                                    else
                                    {
                                        jobId = mainExtract.JobId;
                                        manifestId = mainExtract.ManifestId;
                                        facilityId = mainExtract.FacilityId;
                                    }
                                }
                                else
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    jobId = mainExtract.JobId;
                                    manifestId = mainExtract.ManifestId;
                                    facilityId = mainExtract.FacilityId;
                                }

                                for (int page = 1; page <= packageInfo.PageCount; page++)
                                {
                                    count++;
                                    var extracts = _packager.GenerateSmartBatchExtracts<EnhancedAdherenceCounsellingExtractView>(page, packageInfo.PageSize).ToList();
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    count++;
                                    try
                                    {
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync("http://localhost:21751/api/v3/EnhancedAdherenceCounselling", message);
                                            if (response.IsSuccessStatusCode)
                                            {
                                                allowSend = false;
                                                var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                                responses.Add(res);

                                                var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, res.JobId);
                                                _transportLogRepository.CreateLatest(tlog);
                                            }
                                            else
                                            {
                                                retryCount++;
                                                if (retryCount == 4)
                                                {
                                                    var sentIds = messageBag.SendIds;
                                                    var error = await response.Content.ReadAsStringAsync();
                                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                                        error));
                                                    throw new Exception(error);
                                                }
                                            }
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error");
                                        throw;
                                    }

                                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName, messageBag.GetProgress(count, total), recordCount)));

                                }

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) { }


                        }
                    }
                    else if (archive.Entries[i].Name == "GbvScreeningExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                GbvScreeningMessageSourceBag messageBag = JsonConvert.DeserializeObject<GbvScreeningMessageSourceBag>(Extract);
                                var batchSize = 2000;

                                var packageInfo = _packager.GetPackageInfo<GbvScreeningExtractView>(batchSize);
                                int sendCound = 0;
                                int count = 0;
                                int total = packageInfo.PageCount;
                                int overall = 0;
                                long recordCount = 0;
                                int retryCount = 0;
                                bool allowSend = true;

                                string jobId = string.Empty; Guid manifestId; Guid facilityId;
                                var manifest = _transportLogRepository.GetManifest();

                                if (messageBag.ExtractName == nameof(PatientExtract))
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    if (null == mainExtract)
                                    {
                                        jobId = manifest.JobId;
                                        manifestId = manifest.ManifestId;
                                        facilityId = manifest.FacilityId;
                                    }
                                    else
                                    {
                                        jobId = mainExtract.JobId;
                                        manifestId = mainExtract.ManifestId;
                                        facilityId = mainExtract.FacilityId;
                                    }
                                }
                                else
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    jobId = mainExtract.JobId;
                                    manifestId = mainExtract.ManifestId;
                                    facilityId = mainExtract.FacilityId;
                                }

                                for (int page = 1; page <= packageInfo.PageCount; page++)
                                {
                                    count++;
                                    var extracts = _packager.GenerateSmartBatchExtracts<GbvScreeningExtractView>(page, packageInfo.PageSize).ToList();
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    count++;
                                    try
                                    {
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync("http://localhost:21751/api/v3/GbvScreening", message);
                                            if (response.IsSuccessStatusCode)
                                            {
                                                allowSend = false;
                                                var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                                responses.Add(res);

                                                var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, res.JobId);
                                                _transportLogRepository.CreateLatest(tlog);
                                            }
                                            else
                                            {
                                                retryCount++;
                                                if (retryCount == 4)
                                                {
                                                    var sentIds = messageBag.SendIds;
                                                    var error = await response.Content.ReadAsStringAsync();
                                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                                        error));
                                                    throw new Exception(error);
                                                }
                                            }
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error");
                                        throw;
                                    }

                                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName, messageBag.GetProgress(count, total), recordCount)));

                                }

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) { }


                        }
                    }
                    else if (archive.Entries[i].Name == "IptExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                IptMessageSourceBag messageBag = JsonConvert.DeserializeObject<IptMessageSourceBag>(Extract);
                                var batchSize = 2000;

                                var packageInfo = _packager.GetPackageInfo<IptExtractView>(batchSize);
                                int sendCound = 0;
                                int count = 0;
                                int total = packageInfo.PageCount;
                                int overall = 0;
                                long recordCount = 0;
                                int retryCount = 0;
                                bool allowSend = true;

                                string jobId = string.Empty; Guid manifestId; Guid facilityId;
                                var manifest = _transportLogRepository.GetManifest();

                                if (messageBag.ExtractName == nameof(PatientExtract))
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    if (null == mainExtract)
                                    {
                                        jobId = manifest.JobId;
                                        manifestId = manifest.ManifestId;
                                        facilityId = manifest.FacilityId;
                                    }
                                    else
                                    {
                                        jobId = mainExtract.JobId;
                                        manifestId = mainExtract.ManifestId;
                                        facilityId = mainExtract.FacilityId;
                                    }
                                }
                                else
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    jobId = mainExtract.JobId;
                                    manifestId = mainExtract.ManifestId;
                                    facilityId = mainExtract.FacilityId;
                                }

                                for (int page = 1; page <= packageInfo.PageCount; page++)
                                {
                                    count++;
                                    var extracts = _packager.GenerateSmartBatchExtracts<IptExtractView>(page, packageInfo.PageSize).ToList();
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    count++;
                                    try
                                    {
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync("http://localhost:21751/api/v3/Ipt", message);
                                            if (response.IsSuccessStatusCode)
                                            {
                                                allowSend = false;
                                                var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                                responses.Add(res);

                                                var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, res.JobId);
                                                _transportLogRepository.CreateLatest(tlog);
                                            }
                                            else
                                            {
                                                retryCount++;
                                                if (retryCount == 4)
                                                {
                                                    var sentIds = messageBag.SendIds;
                                                    var error = await response.Content.ReadAsStringAsync();
                                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                                        error));
                                                    throw new Exception(error);
                                                }
                                            }
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error");
                                        throw;
                                    }

                                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName, messageBag.GetProgress(count, total), recordCount)));

                                }

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) { }


                        }

                    }
                    else if (archive.Entries[i].Name == "OtzExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                OtzMessageSourceBag messageBag = JsonConvert.DeserializeObject<OtzMessageSourceBag>(Extract);
                                var batchSize = 2000;

                                var packageInfo = _packager.GetPackageInfo<OtzExtractView>(batchSize);
                                int sendCound = 0;
                                int count = 0;
                                int total = packageInfo.PageCount;
                                int overall = 0;
                                long recordCount = 0;
                                int retryCount = 0;
                                bool allowSend = true;

                                string jobId = string.Empty; Guid manifestId; Guid facilityId;
                                var manifest = _transportLogRepository.GetManifest();

                                if (messageBag.ExtractName == nameof(PatientExtract))
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    if (null == mainExtract)
                                    {
                                        jobId = manifest.JobId;
                                        manifestId = manifest.ManifestId;
                                        facilityId = manifest.FacilityId;
                                    }
                                    else
                                    {
                                        jobId = mainExtract.JobId;
                                        manifestId = mainExtract.ManifestId;
                                        facilityId = mainExtract.FacilityId;
                                    }
                                }
                                else
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    jobId = mainExtract.JobId;
                                    manifestId = mainExtract.ManifestId;
                                    facilityId = mainExtract.FacilityId;
                                }

                                for (int page = 1; page <= packageInfo.PageCount; page++)
                                {
                                    count++;
                                    var extracts = _packager.GenerateSmartBatchExtracts<OtzExtractView>(page, packageInfo.PageSize).ToList();
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    count++;
                                    try
                                    {
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync("http://localhost:21751/api/v3/Otz", message);
                                            if (response.IsSuccessStatusCode)
                                            {
                                                allowSend = false;
                                                var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                                responses.Add(res);

                                                var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, res.JobId);
                                                _transportLogRepository.CreateLatest(tlog);
                                            }
                                            else
                                            {
                                                retryCount++;
                                                if (retryCount == 4)
                                                {
                                                    var sentIds = messageBag.SendIds;
                                                    var error = await response.Content.ReadAsStringAsync();
                                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                                        error));
                                                    throw new Exception(error);
                                                }
                                            }
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error");
                                        throw;
                                    }

                                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName, messageBag.GetProgress(count, total), recordCount)));

                                }

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) { }


                        }
                    }
                    else if (archive.Entries[i].Name == "OvcExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                OvcMessageSourceBag messageBag = JsonConvert.DeserializeObject<OvcMessageSourceBag>(Extract);
                                var batchSize = 2000;

                                var packageInfo = _packager.GetPackageInfo<OvcExtractView>(batchSize);
                                int sendCound = 0;
                                int count = 0;
                                int total = packageInfo.PageCount;
                                int overall = 0;
                                long recordCount = 0;
                                int retryCount = 0;
                                bool allowSend = true;

                                string jobId = string.Empty; Guid manifestId; Guid facilityId;
                                var manifest = _transportLogRepository.GetManifest();

                                if (messageBag.ExtractName == nameof(PatientExtract))
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    if (null == mainExtract)
                                    {
                                        jobId = manifest.JobId;
                                        manifestId = manifest.ManifestId;
                                        facilityId = manifest.FacilityId;
                                    }
                                    else
                                    {
                                        jobId = mainExtract.JobId;
                                        manifestId = mainExtract.ManifestId;
                                        facilityId = mainExtract.FacilityId;
                                    }
                                }
                                else
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    jobId = mainExtract.JobId;
                                    manifestId = mainExtract.ManifestId;
                                    facilityId = mainExtract.FacilityId;
                                }

                                for (int page = 1; page <= packageInfo.PageCount; page++)
                                {
                                    count++;
                                    var extracts = _packager.GenerateSmartBatchExtracts<OvcExtractView>(page, packageInfo.PageSize).ToList();
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    count++;
                                    try
                                    {
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync("http://localhost:21751/api/v3/Ovc", message);
                                            if (response.IsSuccessStatusCode)
                                            {
                                                allowSend = false;
                                                var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                                responses.Add(res);

                                                var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, res.JobId);
                                                _transportLogRepository.CreateLatest(tlog);
                                            }
                                            else
                                            {
                                                retryCount++;
                                                if (retryCount == 4)
                                                {
                                                    var sentIds = messageBag.SendIds;
                                                    var error = await response.Content.ReadAsStringAsync();
                                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                                        error));
                                                    throw new Exception(error);
                                                }
                                            }
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error");
                                        throw;
                                    }

                                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName, messageBag.GetProgress(count, total), recordCount)));

                                }

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) { }


                        }

                    }
                    else if (archive.Entries[i].Name == "PatientAdverseEventExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                AdverseEventMessageSourceBag messageBag = JsonConvert.DeserializeObject<AdverseEventMessageSourceBag>(Extract);
                                var batchSize = 2000;

                                var packageInfo = _packager.GetPackageInfo<PatientAdverseEventView>(batchSize);
                                int sendCound = 0;
                                int count = 0;
                                int total = packageInfo.PageCount;
                                int overall = 0;
                                long recordCount = 0;
                                int retryCount = 0;
                                bool allowSend = true;

                                string jobId = string.Empty; Guid manifestId; Guid facilityId;
                                var manifest = _transportLogRepository.GetManifest();

                                if (messageBag.ExtractName == nameof(PatientExtract))
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    if (null == mainExtract)
                                    {
                                        jobId = manifest.JobId;
                                        manifestId = manifest.ManifestId;
                                        facilityId = manifest.FacilityId;
                                    }
                                    else
                                    {
                                        jobId = mainExtract.JobId;
                                        manifestId = mainExtract.ManifestId;
                                        facilityId = mainExtract.FacilityId;
                                    }
                                }
                                else
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    jobId = mainExtract.JobId;
                                    manifestId = mainExtract.ManifestId;
                                    facilityId = mainExtract.FacilityId;
                                }

                                for (int page = 1; page <= packageInfo.PageCount; page++)
                                {
                                    count++;
                                    var extracts = _packager.GenerateSmartBatchExtracts<PatientAdverseEventView>(page, packageInfo.PageSize).ToList();
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    count++;
                                    try
                                    {
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync("http://localhost:21751/api/v3/PatientAdverseEvents", message);
                                            if (response.IsSuccessStatusCode)
                                            {
                                                allowSend = false;
                                                var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                                responses.Add(res);

                                                var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, res.JobId);
                                                _transportLogRepository.CreateLatest(tlog);
                                            }
                                            else
                                            {
                                                retryCount++;
                                                if (retryCount == 4)
                                                {
                                                    var sentIds = messageBag.SendIds;
                                                    var error = await response.Content.ReadAsStringAsync();
                                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                                        error));
                                                    throw new Exception(error);
                                                }
                                            }
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error");
                                        throw;
                                    }

                                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName, messageBag.GetProgress(count, total), recordCount)));

                                }

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) { }


                        }

                    }
                    else if (archive.Entries[i].Name == "PatientArtExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                ArtMessageSourceBag messageBag = JsonConvert.DeserializeObject<ArtMessageSourceBag>(Extract);
                                var batchSize = 2000;

                                var packageInfo = _packager.GetPackageInfo<PatientArtExtractView>(batchSize);
                                int sendCound = 0;
                                int count = 0;
                                int total = packageInfo.PageCount;
                                int overall = 0;
                                long recordCount = 0;
                                int retryCount = 0;
                                bool allowSend = true;

                                string jobId = string.Empty; Guid manifestId; Guid facilityId;
                                var manifest = _transportLogRepository.GetManifest();

                                if (messageBag.ExtractName == nameof(PatientExtract))
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    if (null == mainExtract)
                                    {
                                        jobId = manifest.JobId;
                                        manifestId = manifest.ManifestId;
                                        facilityId = manifest.FacilityId;
                                    }
                                    else
                                    {
                                        jobId = mainExtract.JobId;
                                        manifestId = mainExtract.ManifestId;
                                        facilityId = mainExtract.FacilityId;
                                    }
                                }
                                else
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    jobId = mainExtract.JobId;
                                    manifestId = mainExtract.ManifestId;
                                    facilityId = mainExtract.FacilityId;
                                }

                                for (int page = 1; page <= packageInfo.PageCount; page++)
                                {
                                    count++;
                                    var extracts = _packager.GenerateSmartBatchExtracts<PatientArtExtractView>(page, packageInfo.PageSize).ToList();
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    count++;
                                    try
                                    {
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync("http://localhost:21751/api/v3/PatientArt", message);
                                            if (response.IsSuccessStatusCode)
                                            {
                                                allowSend = false;
                                                var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                                responses.Add(res);

                                                var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, res.JobId);
                                                _transportLogRepository.CreateLatest(tlog);
                                            }
                                            else
                                            {
                                                retryCount++;
                                                if (retryCount == 4)
                                                {
                                                    var sentIds = messageBag.SendIds;
                                                    var error = await response.Content.ReadAsStringAsync();
                                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                                        error));
                                                    throw new Exception(error);
                                                }
                                            }
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error");
                                        throw;
                                    }

                                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName, messageBag.GetProgress(count, total), recordCount)));

                                }

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) { }


                        }

                    }
                    else if (archive.Entries[i].Name == "PatientBaselineExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                BaselineMessageSourceBag messageBag = JsonConvert.DeserializeObject<BaselineMessageSourceBag>(Extract);
                                var batchSize = 2000;

                                var packageInfo = _packager.GetPackageInfo<PatientBaselinesExtractView>(batchSize);
                                int sendCound = 0;
                                int count = 0;
                                int total = packageInfo.PageCount;
                                int overall = 0;
                                long recordCount = 0;
                                int retryCount = 0;
                                bool allowSend = true;

                                string jobId = string.Empty; Guid manifestId; Guid facilityId;
                                var manifest = _transportLogRepository.GetManifest();

                                if (messageBag.ExtractName == nameof(PatientExtract))
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    if (null == mainExtract)
                                    {
                                        jobId = manifest.JobId;
                                        manifestId = manifest.ManifestId;
                                        facilityId = manifest.FacilityId;
                                    }
                                    else
                                    {
                                        jobId = mainExtract.JobId;
                                        manifestId = mainExtract.ManifestId;
                                        facilityId = mainExtract.FacilityId;
                                    }
                                }
                                else
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    jobId = mainExtract.JobId;
                                    manifestId = mainExtract.ManifestId;
                                    facilityId = mainExtract.FacilityId;
                                }

                                for (int page = 1; page <= packageInfo.PageCount; page++)
                                {
                                    count++;
                                    var extracts = _packager.GenerateSmartBatchExtracts<PatientBaselinesExtractView>(page, packageInfo.PageSize).ToList();
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    count++;
                                    try
                                    {
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync("http://localhost:21751/api/v3/PatientBaselines", message);
                                            if (response.IsSuccessStatusCode)
                                            {
                                                allowSend = false;
                                                var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                                responses.Add(res);

                                                var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, res.JobId);
                                                _transportLogRepository.CreateLatest(tlog);
                                            }
                                            else
                                            {
                                                retryCount++;
                                                if (retryCount == 4)
                                                {
                                                    var sentIds = messageBag.SendIds;
                                                    var error = await response.Content.ReadAsStringAsync();
                                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                                        error));
                                                    throw new Exception(error);
                                                }
                                            }
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error");
                                        throw;
                                    }

                                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName, messageBag.GetProgress(count, total), recordCount)));

                                }

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) { }


                        }
                    }
                    else if (archive.Entries[i].Name == "PatientLabExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                LaboratoryMessageSourceBag messageBag = JsonConvert.DeserializeObject<LaboratoryMessageSourceBag>(Extract);
                                var batchSize = 2000;

                                var packageInfo = _packager.GetPackageInfo<PatientLaboratoryExtractView>(batchSize);
                                int sendCound = 0;
                                int count = 0;
                                int total = packageInfo.PageCount;
                                int overall = 0;
                                long recordCount = 0;
                                int retryCount = 0;
                                bool allowSend = true;

                                string jobId = string.Empty; Guid manifestId; Guid facilityId;
                                var manifest = _transportLogRepository.GetManifest();

                                if (messageBag.ExtractName == nameof(PatientExtract))
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    if (null == mainExtract)
                                    {
                                        jobId = manifest.JobId;
                                        manifestId = manifest.ManifestId;
                                        facilityId = manifest.FacilityId;
                                    }
                                    else
                                    {
                                        jobId = mainExtract.JobId;
                                        manifestId = mainExtract.ManifestId;
                                        facilityId = mainExtract.FacilityId;
                                    }
                                }
                                else
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    jobId = mainExtract.JobId;
                                    manifestId = mainExtract.ManifestId;
                                    facilityId = mainExtract.FacilityId;
                                }

                                for (int page = 1; page <= packageInfo.PageCount; page++)
                                {
                                    count++;
                                    var extracts = _packager.GenerateSmartBatchExtracts<PatientLaboratoryExtractView>(page, packageInfo.PageSize).ToList();
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    count++;
                                    try
                                    {
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync("http://localhost:21751/api/v3/PatientLabs", message);
                                            if (response.IsSuccessStatusCode)
                                            {
                                                allowSend = false;
                                                var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                                responses.Add(res);

                                                var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, res.JobId);
                                                _transportLogRepository.CreateLatest(tlog);
                                            }
                                            else
                                            {
                                                retryCount++;
                                                if (retryCount == 4)
                                                {
                                                    var sentIds = messageBag.SendIds;
                                                    var error = await response.Content.ReadAsStringAsync();
                                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                                        error));
                                                    throw new Exception(error);
                                                }
                                            }
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error");
                                        throw;
                                    }

                                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName, messageBag.GetProgress(count, total), recordCount)));

                                }

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) { }


                        }
                    }

                    else if (archive.Entries[i].Name == "PatientPharmacyExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {

                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                PharmacyMessageSourceBag messageBag = JsonConvert.DeserializeObject<PharmacyMessageSourceBag>(Extract);
                                var batchSize = 2000;

                                var packageInfo = _packager.GetPackageInfo<PatientPharmacyExtractView>(batchSize);
                                int sendCound = 0;
                                int count = 0;
                                int total = packageInfo.PageCount;
                                int overall = 0;
                                long recordCount = 0;
                                int retryCount = 0;
                                bool allowSend = true;

                                string jobId = string.Empty; Guid manifestId; Guid facilityId;
                                var manifest = _transportLogRepository.GetManifest();

                                if (messageBag.ExtractName == nameof(PatientExtract))
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    if (null == mainExtract)
                                    {
                                        jobId = manifest.JobId;
                                        manifestId = manifest.ManifestId;
                                        facilityId = manifest.FacilityId;
                                    }
                                    else
                                    {
                                        jobId = mainExtract.JobId;
                                        manifestId = mainExtract.ManifestId;
                                        facilityId = mainExtract.FacilityId;
                                    }
                                }
                                else
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    jobId = mainExtract.JobId;
                                    manifestId = mainExtract.ManifestId;
                                    facilityId = mainExtract.FacilityId;
                                }

                                for (int page = 1; page <= packageInfo.PageCount; page++)
                                {
                                    count++;
                                    var extracts = _packager.GenerateSmartBatchExtracts<PatientPharmacyExtractView>(page, packageInfo.PageSize).ToList();
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    count++;
                                    try
                                    {
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync("http://localhost:21751/api/v3/PatientPharmacy", message);
                                            if (response.IsSuccessStatusCode)
                                            {
                                                allowSend = false;
                                                var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                                responses.Add(res);

                                                var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, res.JobId);
                                                _transportLogRepository.CreateLatest(tlog);
                                            }
                                            else
                                            {
                                                retryCount++;
                                                if (retryCount == 4)
                                                {
                                                    var sentIds = messageBag.SendIds;
                                                    var error = await response.Content.ReadAsStringAsync();
                                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                                        error));
                                                    throw new Exception(error);
                                                }
                                            }
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error");
                                        throw;
                                    }

                                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName, messageBag.GetProgress(count, total), recordCount)));

                                }

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) { }


                        }
                    }
                    else if (archive.Entries[i].Name == "PatientStatusExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                StatusMessageSourceBag messageBag = JsonConvert.DeserializeObject<StatusMessageSourceBag>(Extract);
                                var batchSize = 2000;

                                var packageInfo = _packager.GetPackageInfo<PatientStatusExtractView>(batchSize);
                                int sendCound = 0;
                                int count = 0;
                                int total = packageInfo.PageCount;
                                int overall = 0;
                                long recordCount = 0;
                                int retryCount = 0;
                                bool allowSend = true;

                                string jobId = string.Empty; Guid manifestId; Guid facilityId;
                                var manifest = _transportLogRepository.GetManifest();

                                if (messageBag.ExtractName == nameof(PatientExtract))
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    if (null == mainExtract)
                                    {
                                        jobId = manifest.JobId;
                                        manifestId = manifest.ManifestId;
                                        facilityId = manifest.FacilityId;
                                    }
                                    else
                                    {
                                        jobId = mainExtract.JobId;
                                        manifestId = mainExtract.ManifestId;
                                        facilityId = mainExtract.FacilityId;
                                    }
                                }
                                else
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    jobId = mainExtract.JobId;
                                    manifestId = mainExtract.ManifestId;
                                    facilityId = mainExtract.FacilityId;
                                }

                                for (int page = 1; page <= packageInfo.PageCount; page++)
                                {
                                    count++;
                                    var extracts = _packager.GenerateSmartBatchExtracts<PatientStatusExtractView>(page, packageInfo.PageSize).ToList();
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    count++;
                                    try
                                    {
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync("http://localhost:21751/api/v3/PatientStatus", message);
                                            if (response.IsSuccessStatusCode)
                                            {
                                                allowSend = false;
                                                var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                                responses.Add(res);

                                                var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, res.JobId);
                                                _transportLogRepository.CreateLatest(tlog);
                                            }
                                            else
                                            {
                                                retryCount++;
                                                if (retryCount == 4)
                                                {
                                                    var sentIds = messageBag.SendIds;
                                                    var error = await response.Content.ReadAsStringAsync();
                                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                                        error));
                                                    throw new Exception(error);
                                                }
                                            }
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error");
                                        throw;
                                    }

                                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName, messageBag.GetProgress(count, total), recordCount)));

                                }

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) { }


                        }

                    }
                    else if (archive.Entries[i].Name == "PatientVisitExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                VisitMessageSourceBag messageBag = JsonConvert.DeserializeObject<VisitMessageSourceBag>(Extract);
                                var batchSize = 2000;

                                var packageInfo = _packager.GetPackageInfo<PatientVisitExtractView>(batchSize);
                                int sendCound = 0;
                                int count = 0;
                                int total = packageInfo.PageCount;
                                int overall = 0;
                                long recordCount = 0;
                                int retryCount = 0;
                                bool allowSend = true;

                                string jobId = string.Empty; Guid manifestId; Guid facilityId;
                                var manifest = _transportLogRepository.GetManifest();

                                if (messageBag.ExtractName == nameof(PatientExtract))
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    if (null == mainExtract)
                                    {
                                        jobId = manifest.JobId;
                                        manifestId = manifest.ManifestId;
                                        facilityId = manifest.FacilityId;
                                    }
                                    else
                                    {
                                        jobId = mainExtract.JobId;
                                        manifestId = mainExtract.ManifestId;
                                        facilityId = mainExtract.FacilityId;
                                    }
                                }
                                else
                                {
                                    var mainExtract = _transportLogRepository.GetMainExtract();
                                    jobId = mainExtract.JobId;
                                    manifestId = mainExtract.ManifestId;
                                    facilityId = mainExtract.FacilityId;
                                }

                                for (int page = 1; page <= packageInfo.PageCount; page++)
                                {
                                    count++;
                                    var extracts = _packager.GenerateSmartBatchExtracts<PatientVisitExtractView>(page, packageInfo.PageSize).ToList();
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    count++;
                                    try
                                    {
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync("http://localhost:21751/api/v3/PatientVisits", message);
                                            if (response.IsSuccessStatusCode)
                                            {
                                                allowSend = false;
                                                var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                                responses.Add(res);

                                                var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, res.JobId);
                                                _transportLogRepository.CreateLatest(tlog);
                                            }
                                            else
                                            {
                                                retryCount++;
                                                if (retryCount == 4)
                                                {
                                                    var sentIds = messageBag.SendIds;
                                                    var error = await response.Content.ReadAsStringAsync();
                                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                                        error));
                                                    throw new Exception(error);
                                                }
                                            }
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error");
                                        throw;
                                    }

                                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName, messageBag.GetProgress(count, total), recordCount)));

                                }

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) { }


                        }
                    }
                }

            }
            return responses;
        
            
            }
           
            





    }
}
