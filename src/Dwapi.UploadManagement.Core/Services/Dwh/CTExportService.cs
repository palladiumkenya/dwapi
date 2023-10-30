using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
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
using Dwapi.UploadManagement.Core.Hubs.BoardRoomUpload;
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
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext<ProgressHub> _hubContext;
        //private readonly IHubContext<ProgressHub> _hubContext;
        private int _totalRecords;
        private int _recordsSaved;

        public HttpClient Client { get; set; }

        public CTExportService(IDwhPackager packager, IMediator mediator, IEmrMetricReader reader, ITransportLogRepository transportLogRepository, IHostingEnvironment hostingEnvironment, IHubContext<ProgressHub> hubContext)
        {
            _packager = packager;
            _mediator = mediator;
            _reader = reader;
            _transportLogRepository = transportLogRepository;
            _endPoint = "api/";
            _hubContext = hubContext;
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
                    message.Manifest.UploadMode = UploadMode.Boardroom;
                    var msg = JsonConvert.SerializeObject(message.Manifest);                   
                    var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                    var Base64Manifest = Convert.ToBase64String(plainTextBytes);
                    string projectPath = "exports";
                    string foldername = Path.Combine(projectPath, Convert.ToString(message.Manifest.SiteCode) + "-CT").HasToEndsWith(@"\").ToOsStyle();

                    Directory.CreateDirectory(foldername);
                    string fileName = foldername + "manifest.dump.json";

                    await File.WriteAllTextAsync(fileName, Base64Manifest);

                    //endpointUrl
                    var extractsDetails = JsonConvert.SerializeObject(sendTo);
                    var plainTextBytesdet = Encoding.UTF8.GetBytes(extractsDetails);
                    var Base64Manifestdet = Convert.ToBase64String(plainTextBytesdet);
                    string fName = foldername + "package.dump.json";
                    await File.WriteAllTextAsync(fName, Base64Manifestdet);


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
        public void NotifyPreSendingBoardRoom()
        {
            DomainEvents.Dispatch(new DwhMessageNotification(false, $"Sending started..."));

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
                        bool allowExport = true;
                        while (allowExport)
                        {
                            //if (message.ExtractName == "DefaulterTracingExtract")
                            //{

                            //    var msg = JsonConvert.SerializeObject(message);
                            //    var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                            //    var Base64Extract = Convert.ToBase64String(plainTextBytes);
                            //    string projectPath = ("exports");
                            //    string folderName = Path.Combine(projectPath, message.Extracts[0].SiteCode + "-CT" + "\\extracts").HasToEndsWith(@"\");
                            //    string fileName = folderName + messageBag.ExtractName + ".dump" + ".json";

                            //    await File.WriteAllTextAsync(fileName.ToOsStyle(), Base64Extract);
                            //    allowExport = false;


                            //    var sentIds = messageBag.SendIds;
                            //    sendCound += sentIds.Count;

                            //    DomainEvents.Dispatch(new CTExtractSentEvent(sentIds, SendStatus.Exported,
                            //       messageBag.ExtractType));


                            //    startPath = Path.Combine(projectPath, message.Extracts[0].SiteCode + "-CT");
                            //    string zipPath = Path.Combine(projectPath, message.Extracts[0].SiteCode + "-CT" + ".zip");


                            //    if (File.Exists(zipPath))
                            //        File.Delete(zipPath);
                            //    ZipFile.CreateFromDirectory(startPath, zipPath, CompressionLevel.Fastest, true);



                            //}
//                            //else
                            //{
                                var msg = JsonConvert.SerializeObject(message);
                                var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                                var Base64Extract = Convert.ToBase64String(plainTextBytes);
                                string projectPath = "exports";
                                string folderName = Path.Combine(projectPath, message.Extracts[0].SiteCode + "-CT" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                                string fileName = folderName + messageBag.ExtractName + ".dump" + ".json";
                                if (!Directory.Exists(folderName))
                                    Directory.CreateDirectory(folderName);

                                await File.WriteAllTextAsync(fileName, Base64Extract);
                                allowExport = false;

                                var sentIds = messageBag.SendIds;
                                sendCound += sentIds.Count;

                                DomainEvents.Dispatch(new CTExtractSentEvent(sentIds, SendStatus.Exported,
                                   messageBag.ExtractType));

                           // }
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





        public async Task ZipExtractsAsync<T>(SendManifestPackageDTO sendTo, IMessageSourceBag<T> messageBag) where T : ClientExtract
        {

            try
            {
                string jobId = string.Empty; Guid manifestId = new Guid(); Guid facilityId = new Guid();


                var packageInfo = _packager.GetPackageInfo<T>(100);
                if (packageInfo.PageCount > 0)
                {

                    int page = 1;
                    
                    var extracts = _packager.GenerateSmartBatchExtracts<T>(page, Convert.ToInt32(packageInfo.TotalRecords)).ToList();
                  
                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                    var message = messageBag;

                    string projectPath = ("exports");
                    string startPath = Path.Combine(projectPath, message.Extracts[0].SiteCode + "-CT");
                    string zipPath = Path.Combine(projectPath, message.Extracts[0].SiteCode + "-CT" + ".zip");


                    if (File.Exists(zipPath))
                        File.Delete(zipPath);
                    ZipFile.CreateFromDirectory(startPath, zipPath, CompressionLevel.Fastest, true);


                }
            }
            catch(Exception ex) {
                Log.Error(ex, $"Zip Extracts {ex} Error");
                throw;
            }
           
            }


        public async Task NotifyPostExport(SendManifestPackageDTO sendTo, string version)
        {
            DomainEvents.Dispatch(new DwExporthMessageNotification(false, $"Exporting completed"));


            Thread.Sleep(3000);


            
        }

        //BoardRoom Uploads

       
       

        public async Task<List<SendCTResponse>> SendFileManifest(IFormFile file, string apiVersion = "")
        {
            var responses = new List<SendCTResponse>();
            SendManifestPackageDTO sendTo=null;
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
            if (!Directory.Exists(tempFullPath))
                Directory.CreateDirectory(tempFullPath);
            using (ZipArchive archive = ZipFile.OpenRead(fullPath))
                {
                _totalRecords = archive.Entries.Count;
                _recordsSaved = 0;
                for (int i = 0; i < archive.Entries.Count; i++)
                {
                    //// var response = await client.PostAsJsonAsync(
                    // sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
                    if (archive.Entries[i].Name == "package.dump.json")
                    {
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));



                        archive.Entries[i].ExtractToFile(destinationPath, true);

                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);

                                sendTo = JsonConvert.DeserializeObject<SendManifestPackageDTO>(Extract);



                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;

                            }

                            _recordsSaved++;
                            await UpdateProgress();
                        }
                        break;

                    }
                    
                }
                    for (int i = 0; i < archive.Entries.Count; i++)
                    {
                    if (archive.Entries[i].Name == "manifest.dump.json")
                        {

                        


                            string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));

                     

                            archive.Entries[i].ExtractToFile(destinationPath, true);

                            var filestream = File.OpenRead(destinationPath);
                            using (StreamReader sr = new StreamReader(filestream))
                            {
                                try
                                {
                                    text = await sr.ReadToEndAsync(); // OK                         

                                    byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                    var Extract = Encoding.UTF8.GetString(base64EncodedBytes);

                                    DwhManifest manifest = JsonConvert.DeserializeObject<DwhManifest>(Extract);
                                    manifest.GenerateID();

                                    var start = DateTime.Now;
                                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}spot", apiVersion), manifest);

                                if (response.IsSuccessStatusCode)
                                    {

                                    

                                    var content = await response.Content.ReadAsJsonAsync<ManifestResponse>();
                                        responses.Add(new SendCTResponse());

                                        var tlog = TransportLog.GenerateManifest("NDWH", content.JobId,
                                            new Guid(content.ManifestId), content.Code, start, content.FacilityId);
                                        _transportLogRepository.CreateLatest(tlog);
                                        Log.Debug(new string('+', 40));
                                        Log.Debug($"CONNECTED:  {sendTo.Destination.Url}");
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
                                catch(Exception ex)
                                {
                                    Log.Error(ex, $"Send Extracts {archive.Entries[i].Name} Error");
                                    throw;

                                 }

                                    
                                }
                        _recordsSaved++;
                        await UpdateProgress();

                    }
                    

                    break;
                            
                 }
                for (int i = 1; i < archive.Entries.Count; i++)
                {
                    if (archive.Entries[i].Name == "PatientExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                       
                        int count = 0;
                        long recordCount = 0;
                      

                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try { 
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                PatientMessageSourceBag messageBag = JsonConvert.DeserializeObject<PatientMessageSourceBag>(Extract);
                                var batchSize = 2000;                               
                                var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                var list = new List<PatientMessageSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<PatientExtractView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new PatientMessageSourceBag(newList));
                                }
                               
                                int sendCound = 0;
                                foreach (var item in list)
                                {
                                messageBag.Extracts = item._PatientExtractView;




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

                          
                                   
                                    int overall = 0;
                                    count++;
                                    var extracts = messageBag.Extracts;
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;                                                                 
                                    try
                                     {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync(
                                                  sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
                                          
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

                                   

                                }
                                _recordsSaved++;
                                await UpdateProgress();

                            await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));
                            }
                            catch (Exception e)
                            {
                                Log.Error(e, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }

                           

                        }

                        break;
                    }

                }
                for (int i = 1; i < archive.Entries.Count; i++)
                {

                    if (archive.Entries[i].Name == "AllergiesChronicIllnessExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        int total = 0;
                        int count = 0;
                        long recordCount = 0;
                        
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                AllergiesChronicIllnessMessageSourceBag messageBag = JsonConvert.DeserializeObject<AllergiesChronicIllnessMessageSourceBag>(Extract);
                                var batchSize =2000;
                                var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                var list = new List<AllergiesChronicIllnessMessageSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<AllergiesChronicIllnessExtractView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new AllergiesChronicIllnessMessageSourceBag(newList));
                                }

                                int sendCound = 0;
                                foreach (var item in list)
                                {
                                messageBag.Extracts = item._AllergiesChronicIllnessExtractView;

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

                              
                                    count++;
                                    var extracts = messageBag.Extracts;
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    
                                    try
                                    {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync(
                                                  sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
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

                                   

                                }                              

                                _recordsSaved++;
                                await UpdateProgress();

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));
                            }
                            catch (Exception e)
                            {
                                Log.Error(e, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }



                        }
                    }
                    else if (archive.Entries[i].Name == "ContactListingExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        int total = 0;
                        int count = 0;
                        long recordCount = 0;
                        
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
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
                                var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                var list = new List<ContactListingMessageSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<ContactListingExtractView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new ContactListingMessageSourceBag(newList));
                                }

                                int sendCound = 0;
                                foreach (var item in list)
                                {
                                    messageBag.Extracts = item._ContactListingExtractView;

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

                             
                                    count++;
                                    var extracts = messageBag.Extracts;
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    
                                    try
                                    {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync(
                                                   sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
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
                                    }                                                                                                                             }


                                _recordsSaved++;
                                await UpdateProgress();

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));
                            }
                            catch (Exception e)
                            {
                                Log.Error(e, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }

                        }
                    }
                    else if (archive.Entries[i].Name == "CovidExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        int total = 0;
                        int count = 0;
                        long recordCount = 0;
                      
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
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
                                var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                var list = new List<CovidMessageSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<CovidExtractView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new CovidMessageSourceBag(newList));
                                }

                                int sendCound = 0;
                                foreach (var item in list)
                                {
                                    messageBag.Extracts = item._CovidExtractView;


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

                                count++;
                                    var extracts = messageBag.Extracts;
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                   
                                    try
                                    {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync(
                                                  sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
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

                                  

                                }
                                _recordsSaved++;
                                await UpdateProgress();

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));
                            }
                            catch (Exception e)
                            {
                                Log.Error(e, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }


                        }
                    }
                    else if (archive.Entries[i].Name == "DefaulterTracingExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        int total = 0;
                        int count = 0;
                        long recordCount = 0;
                      
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
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
                                var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                var list = new List<DefaulterTracingMessageSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<DefaulterTracingExtractView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new DefaulterTracingMessageSourceBag(newList));
                                }

                                int sendCound = 0;
                                foreach (var item in list)
                                {
                                messageBag.Extracts = item._DefaulterTracingExtractView;



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

                              
                                    count++;
                                    var extracts = messageBag.Extracts;
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                   
                                    try
                                    {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync(
                                                  sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
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

                                    

                                }

                                _recordsSaved++;
                                await UpdateProgress();

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));
                            }
                            catch (Exception e)
                            {
                                Log.Error(e, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }

                        }
                    }
                    else if (archive.Entries[i].Name == "DepressionScreeningExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        int total = 0;
                        int count = 0;
                        long recordCount = 0;
                        
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
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
                                var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                var list = new List<DepressionScreeningMessageSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<DepressionScreeningExtractView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new DepressionScreeningMessageSourceBag(newList));
                                }

                                int sendCound = 0;
                                foreach (var item in list)
                                {
                                messageBag.Extracts = item._DepressionScreeningExtractView;


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

                                    count++;
                                    var extracts = messageBag.Extracts;
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    
                                    try
                                    {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync(
                                                  sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
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

                                }

                                _recordsSaved++;
                                await UpdateProgress();

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));
                            }
                            catch (Exception e)
                            {
                                Log.Error(e, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }

                        }

                    }
                    else if (archive.Entries[i].Name == "DrugAlcoholScreeningExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        int total = 0;
                        int count = 0;
                        long recordCount = 0;
                        
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
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
                                var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                var list = new List<DrugAlcoholScreeningMessageSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<DrugAlcoholScreeningExtractView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new DrugAlcoholScreeningMessageSourceBag(newList));
                                }

                                int sendCound = 0;
                                foreach (var item in list)
                                {
                                messageBag.Extracts = item._DrugAlcoholScreeningExtractView
                                        ;


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

                              
                                    count++;
                                    var extracts =messageBag.Extracts;  
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    
                                    try
                                    {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync(
                                                  sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
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

                                    

                                }

                                _recordsSaved++;
                                await UpdateProgress();

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));
                            }
                            catch (Exception e)
                            {
                                Log.Error(e, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }

                        }
                    }
                    else if (archive.Entries[i].Name == "EnhancedAdherenceCounsellingExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        int total = 0;
                        int count = 0;
                        long recordCount = 0;
                      
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
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
                                var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                var list = new List<EnhancedAdherenceCounsellingMessageSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<EnhancedAdherenceCounsellingExtractView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new EnhancedAdherenceCounsellingMessageSourceBag(newList));
                                }

                                int sendCound = 0;
                                foreach (var item in list)
                                {
                                 messageBag.Extracts = item._EnhancedAdherenceCounsellingExtractView;



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

                                    count++;
                                    var extracts = messageBag.Extracts;
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                   
                                    try
                                    {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync(
                                                   sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
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

                                  

                                }
                                _recordsSaved++;
                                await UpdateProgress();
                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) {
                                Log.Error(ex, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }
                          

                        }
                    }
                    else if (archive.Entries[i].Name == "GbvScreeningExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        int total = 0;
                        int count = 0;
                        long recordCount = 0;
                       
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
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
                                var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                var list = new List<GbvScreeningMessageSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<GbvScreeningExtractView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new GbvScreeningMessageSourceBag(newList));
                                }

                                int sendCound = 0;
                                foreach (var item in list)
                                {
                                 messageBag.Extracts = item._GbvScreeningExtractView;


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

                                    count++;
                                    var extracts = messageBag.Extracts;
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    count++;
                                    try
                                    {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync(
                                                  sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
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

                                   

                                }
                                _recordsSaved++;
                                await UpdateProgress();
                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) {
                                Log.Error(ex, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }
                           


                        }
                    }
                    else if (archive.Entries[i].Name == "IptExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        int total = 0;
                        int count = 0;
                        long recordCount = 0;
                       
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
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
                                var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                var list = new List<IptMessageSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<IptExtractView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new IptMessageSourceBag(newList));
                                }

                                int sendCound = 0;
                                foreach (var item in list)
                                {
                                messageBag.Extracts = item._IptExtractView;

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

                              
                                    count++;
                                    var extracts = messageBag.Extracts;
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    
                                    try
                                    {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync(
                                                  sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
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

                                   

                                }
                                _recordsSaved++;
                                await UpdateProgress();
                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex) {
                                Log.Error(ex, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }
                            

                        }

                    }
                    else if (archive.Entries[i].Name == "OtzExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        int total = 0;
                        int count = 0;
                        long recordCount = 0;
                      
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
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
                                var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                var list = new List<OtzMessageSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<OtzExtractView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new OtzMessageSourceBag(newList));
                                }

                                int sendCound = 0;
                                foreach (var item in list)
                                {
                                    messageBag.Extracts = item._OtzExtractView;


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

                               
                                    count++;
                                    var extracts = messageBag.Extracts;
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    
                                    try
                                    {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync(
                                                  sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
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

                                  

                                }
                                _recordsSaved++;
                                await UpdateProgress();
                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }
                          


                        }
                    }
                    else if (archive.Entries[i].Name == "OvcExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        int total = 0;
                        int count = 0;
                        long recordCount = 0;
                      
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
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
                                var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                var list = new List<OvcMessageSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<OvcExtractView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new OvcMessageSourceBag(newList));
                                }

                                int sendCound = 0;
                                foreach (var item in list)
                                {
                                messageBag.Extracts = item._OvcExtractView;


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

                                    count++;
                                    var extracts = messageBag.Extracts;
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    
                                    try
                                    {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {

                                            var response = await client.PostAsJsonAsync(
                                                   sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
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

                                    

                                }
                                _recordsSaved++;
                                await UpdateProgress();
                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }
                            


                        }

                    }
                    else if (archive.Entries[i].Name == "PatientAdverseEventExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        int total = 0;
                        int count = 0;
                        long recordCount = 0;
                       
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
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
                                var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                var list = new List<AdverseEventMessageSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<PatientAdverseEventView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new AdverseEventMessageSourceBag(newList));
                                }

                                int sendCound = 0;
                                foreach (var item in list)
                                {
                                messageBag.Extracts = item._PatientAdverseEventView;


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

                             
                                    count++;
                                    var extracts = messageBag.Extracts;
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    
                                    try
                                    {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync(
                                                   sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
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

                                   

                                }
                                _recordsSaved++;
                                await UpdateProgress();
                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }
                           

                        }

                    }
                    else if (archive.Entries[i].Name == "PatientArtExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        int total = 0;
                        int count = 0;
                        long recordCount = 0;
                       
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
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
                                var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                var list = new List<ArtMessageSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<PatientArtExtractView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new ArtMessageSourceBag(newList));
                                }

                                int sendCound = 0;
                                foreach (var item in list)
                                {
                                 messageBag.Extracts = item._PatientArtExtractView;

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

                                    count++;
                                    var extracts = messageBag.Extracts;
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                   
                                    try
                                    {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync(
                                                  sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
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

                                   

                                }
                                _recordsSaved++;
                                await UpdateProgress();
                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }
                           

                        }

                    }
                    else if (archive.Entries[i].Name == "PatientBaselineExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        int total = 0;
                        int count = 0;
                        long recordCount = 0;
                        
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
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
                                var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                var list = new List<BaselineMessageSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<PatientBaselinesExtractView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new BaselineMessageSourceBag(newList));
                                }

                                int sendCound = 0;
                                foreach (var item in list)
                                {
                                messageBag.Extracts = item._PatientBaselinesExtractView;
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
                                    count++;
                                    var extracts = messageBag.Extracts;
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                   
                                    try
                                    {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync(
                                                  sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
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

                                   

                                }
                                _recordsSaved++;
                                await UpdateProgress();
                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }
                           


                        }
                    }
                    else if (archive.Entries[i].Name == "PatientLabExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        int total = 0;
                        int count = 0;
                        long recordCount = 0;
                       
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
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
                                var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                var list = new List<LaboratoryMessageSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<PatientLaboratoryExtractView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new LaboratoryMessageSourceBag(newList));
                                }

                                int sendCound = 0;
                                foreach (var item in list)
                                {
                                messageBag.Extracts = item._PatientLaboratoryExtractView;
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

                                    count++;
                                    var extracts = messageBag.Extracts;
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    try
                                    {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync(
                                                  sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
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

                                    

                                }
                                _recordsSaved++;
                                await UpdateProgress();
                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }
                            


                        }
                    }

                    else if (archive.Entries[i].Name == "PatientPharmacyExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        int total = 0;
                        int count = 0;
                        long recordCount = 0;
                       
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
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
                                var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                var list = new List<PharmacyMessageSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<PatientPharmacyExtractView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new PharmacyMessageSourceBag(newList));
                                }

                                int sendCound = 0;
                                foreach (var item in list)
                                {
                                messageBag.Extracts = item._PatientPharmacyExtractView;
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

                                    count++;
                                    var extracts = messageBag.Extracts;
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    
                                    try
                                    {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync(
                                                  sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
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

                                   

                                }
                                _recordsSaved++;
                                await UpdateProgress();
                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }
                            


                        }
                    }
                    else if (archive.Entries[i].Name == "PatientStatusExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        int total = 0;
                        int count = 0;
                        long recordCount = 0;
                       
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
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
                                    var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                    var list = new List<StatusMessageSourceBag>();
                                    for (int x = 0; x < numberOfBatches; x++)
                                    {
                                        List<PatientStatusExtractView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                        list.Add(new StatusMessageSourceBag(newList));
                                    }

                                    int sendCound = 0;
                                    foreach (var item in list)
                                    {
                                    messageBag.Extracts = item._PatientStatusExtractView;
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

                                    count++;
                                    var extracts = messageBag.Extracts;
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                   
                                    try
                                    {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync(
                                                  sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
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

                                    

                                }
                                _recordsSaved++;
                                await UpdateProgress();
                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }
                          


                        }

                    }
                    else if (archive.Entries[i].Name == "PatientVisitExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        int total = 0;
                        int count = 0;
                        long recordCount = 0;
                       
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
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
                                var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                var list = new List<VisitMessageSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<PatientVisitExtractView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new VisitMessageSourceBag(newList));
                                }

                                int sendCound = 0;
                                foreach (var item in list)
                                {
                                messageBag.Extracts = item._PatientVisitExtractView;
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

                                    count++;
                                    var extracts = messageBag.Extracts;
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                   
                                    try
                                   {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
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

                                    _recordsSaved++;
                                    await UpdateProgress();

                                }

                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));



                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }
                           


                        }
                    }
                    
                    else if (archive.Entries[i].Name == "CancerScreeningExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        int total = 0;
                        int count = 0;
                        long recordCount = 0;
                       
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                CancerScreeningMessageSourceBag messageBag = JsonConvert.DeserializeObject<CancerScreeningMessageSourceBag>(Extract);
                                var batchSize = 2000;
                                var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                var list = new List<CancerScreeningMessageSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<CancerScreeningExtractView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new CancerScreeningMessageSourceBag(newList));
                                }

                                int sendCound = 0;
                                foreach (var item in list)
                                {
                                messageBag.Extracts = item._CancerScreeningExtractView;
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

                                    count++;
                                    var extracts = messageBag.Extracts;
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    
                                    try
                                    {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync(
                                                  sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
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

                                   

                                }
                                _recordsSaved++;
                                await UpdateProgress();
                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }
                        }
                    }
                    
                    else if (archive.Entries[i].Name == "IITRiskScoresExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        int total = 0;
                        int count = 0;
                        long recordCount = 0;
                       
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                IITRiskScoresMessageSourceBag messageBag = JsonConvert.DeserializeObject<IITRiskScoresMessageSourceBag>(Extract);
                                var batchSize = 2000;
                                var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                var list = new List<IITRiskScoresMessageSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<IITRiskScoresExtractView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new IITRiskScoresMessageSourceBag(newList));
                                }

                                int sendCound = 0;
                                foreach (var item in list)
                                {
                                messageBag.Extracts = item._IITRiskScoresExtractView;
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

                                    count++;
                                    var extracts = messageBag.Extracts;
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    
                                    try
                                    {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync(
                                                  sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
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

                                   

                                }
                                _recordsSaved++;
                                await UpdateProgress();
                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }
                        }
                    }
                    else if (archive.Entries[i].Name == "ArtFastTrackExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        int total = 0;
                        int count = 0;
                        long recordCount = 0;
                       
                        string destinationPath = Path.GetFullPath(Path.Combine(tempFullPath, archive.Entries[i].Name));
                        archive.Entries[i].ExtractToFile(destinationPath, true);
                        var filestream = File.OpenRead(destinationPath);
                        using (StreamReader sr = new StreamReader(filestream))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);
                                ArtFastTrackMessageSourceBag messageBag = JsonConvert.DeserializeObject<ArtFastTrackMessageSourceBag>(Extract);
                                var batchSize = 2000;
                                var numberOfBatches = (int)Math.Ceiling((double)messageBag.Extracts.Count() / batchSize);
                                var list = new List<ArtFastTrackMessageSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<ArtFastTrackExtractView> newList = messageBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new ArtFastTrackMessageSourceBag(newList));
                                }

                                int sendCound = 0;
                                foreach (var item in list)
                                {
                                messageBag.Extracts = item._ArtFastTrackExtractView;
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

                                    count++;
                                    var extracts = messageBag.Extracts;
                                    recordCount = messageBag.Extracts.Count;
                                    messageBag.Generate(extracts, manifestId, facilityId, jobId);
                                    var message = messageBag;

                                    
                                    try
                                    {
                                        int retryCount = 0;
                                        bool allowSend = true;
                                        while (allowSend)
                                        {
                                            var response = await client.PostAsJsonAsync(
                                                  sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
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

                                   

                                }
                                _recordsSaved++;
                                await UpdateProgress();
                                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));

                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }
                        }
                    }
                    
                }

            }

            string version = GetType().Assembly.GetName().Version.ToString();
            await NotifyPostSending(sendTo, version);
            return responses;
        
            
            }



        public async Task NotifyPostSending(SendManifestPackageDTO sendTo, string version)
        {


            int maxRetries = 4;
            int retries = 0;
            var notificationend = new HandshakeEnd("CTSendEnd", version);
            DomainEvents.Dispatch(new DwhMessageNotification(false, $"Sending completed"));
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

        private async Task UpdateProgress()
        {
            var progress = ((double)_recordsSaved / _totalRecords)*100;            
            await _hubContext.Clients.All.SendAsync("ReceiveProgress", progress);
        }







    }
}
