using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Application.Events;
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
using Dwapi.UploadManagement.Core.Notifications.Dwh;
using Humanizer;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Serilog;

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
            int total = packageInfo.PageCount;
            int overall = 0;

           DomainEvents.Dispatch(new CTStatusNotification(sendTo.ExtractId, sendTo.GetExtractId(messageBag.ExtractName), ExtractStatus.Exporting));
            long recordCount = 0;

            if (messageBag.ExtractName == "PatientArtExtract")
            {
                string x = "ss";
            }


            try
            {
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
                            
                            //for(int i = 0; i < extracts.Count; i++)
                            //{
                            //  IMessageSourceBag mg=  Convert.ToInt64(message.Extracts[i].PatientID);
                            //}
                            //foreach (var extract in message.Extracts)
                            //    Convert.ToInt64(message.Extracts[i].PatientID);


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
                                DomainEvents.Dispatch(new CTExtractSentEvent(sentIds, SendStatus.Sent,
                                   messageBag.ExtractType));
                                var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, jobId);
                                _transportLogRepository.CreateLatest(tlog);

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

                                var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, jobId);
                                _transportLogRepository.CreateLatest(tlog);
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

       
    }
}
