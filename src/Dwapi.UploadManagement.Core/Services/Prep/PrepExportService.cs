using Dwapi.ExtractsManagement.Core.Application.Events;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Prep;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SettingsManagement.Core.Application.Metrics.Events;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Event.Dwh;
using Dwapi.UploadManagement.Core.Event.Prep;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Exchange.Dwh.Smart;
using Dwapi.UploadManagement.Core.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Exchange.Prep;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Prep;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Services.Prep;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Dwapi.UploadManagement.Core.Notifications.Prep;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Dwapi.UploadManagement.Core.Hubs.BoardRoomUpload;
using System.Net.Http.Headers;
using Dwapi.UploadManagement.Core.Notifications.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;

namespace Dwapi.UploadManagement.Core.Services.Prep
{
    public class PrepExportService : IPrepExportService
    {
        private readonly string _endPoint;
        private readonly IPrepPackager _packager;
        private readonly IMediator _mediator;
        private readonly IEmrMetricReader _reader;
        private IHostingEnvironment _hostingEnvironment;
        private readonly IHubContext<ProgressHub> _hubContext;        
        private int _totalRecords;
        private int _recordsSaved;

        public HttpClient Client { get; set; }

        public PrepExportService(IPrepPackager packager, IMediator mediator, IEmrMetricReader reader, IHostingEnvironment hostingEnvironment, IHubContext<ProgressHub> hubContext)
        {
            _packager = packager;
            _mediator = mediator;
            _reader = reader;
            _endPoint = "api/prep/";
            _hubContext = hubContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public Task<List<SendManifestResponse>> ExportManifestAsync(SendManifestPackageDTO sendTo, string version)
        {
            return ExportManifestAsync(sendTo,
                ManifestMessageBag.Create(_packager.GenerateWithMetrics(sendTo.GetEmrDto()).ToList()), version);
        }

        public async Task<List<SendManifestResponse>> ExportManifestAsync(SendManifestPackageDTO sendTo,
            ManifestMessageBag manifestMessage, string version)
        {
            var responses = new List<SendManifestResponse>();
            await _mediator.Publish(new HandshakeStart("PREPSendStart", version, manifestMessage.Session));
            var client = Client ?? new HttpClient();

            foreach (var message in manifestMessage.Messages)
            {
                try
                {
                    var msg = JsonConvert.SerializeObject(message);

                    var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                    var Base64Manifest = Convert.ToBase64String(plainTextBytes);
                    string projectPath = "exports";
                    string folderName = Path.Combine(projectPath, Convert.ToString(message.Manifest.SiteCode) + "-Prep").HasToEndsWith(@"\").ToOsStyle();
                    Directory.CreateDirectory(folderName);    // Write that JSON to txt file,
                    string fileName = folderName + "manifest.dump" + ".json";
                    File.WriteAllText(fileName, Base64Manifest);

                    //endpointUrl
                    var extractsDetails = JsonConvert.SerializeObject(sendTo);
                    var plainTextBytesdet = Encoding.UTF8.GetBytes(extractsDetails);
                    var Base64Manifestdet = Convert.ToBase64String(plainTextBytesdet);
                    string fName = folderName + "package.dump.json";
                    await File.WriteAllTextAsync(fName, Base64Manifestdet);
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Export Manifest Error");
                    throw;
                }
            }

            return responses;
        }


        public Task<List<SendMpiResponse>> ExportPatientPrepsAsync(SendManifestPackageDTO sendTo)
        {
            return ExportPatientPrepsAsync(sendTo, PrepMessageBag.CreateEx(_packager.GeneratePatientPreps().ToList()));
        }
        public async Task<List<SendMpiResponse>> ExportPatientPrepsAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));
            foreach (var message in messageBag.Messages)
            {
                if (message.PatientPrepExtracts.Count > 0)
                {
                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.PatientPrepExtracts[0].SiteCode + "-Prep" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();

                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName  + "PatientPrepExtracts.dump" + ".json";

                        await File.WriteAllTextAsync(fileName, Base64Extract);

                        var sentIds = message.PatientPrepExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new PrepExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));
                        


                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Export Patient Extract Error");
                        throw;
                    }
                }
                DomainEvents.Dispatch(new PrepExportNotification(new SendProgress(nameof(PatientPrepExtract), Common.GetProgress(count, total), sendCound)));
            }
            DomainEvents.Dispatch(new PrepExportNotification(new SendProgress(nameof(PatientPrepExtract), Common.GetProgress(count, total), sendCound, true)));
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> ExportPrepAdverseEventsAsync(SendManifestPackageDTO sendTo)
        {
            return ExportPrepAdverseEventsAsync(sendTo, PrepMessageBag.CreateEx(_packager.GeneratePrepAdverseEvents().ToList()));
        }
        public async Task<List<SendMpiResponse>> ExportPrepAdverseEventsAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));
            foreach (var message in messageBag.Messages)
            {
                if (message.PrepAdverseEventExtracts.Count > 0)
                {
                    count++;
                    try
                    {

                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.PrepAdverseEventExtracts[0].SiteCode + "-Prep" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "PrepAdverseEventExtracts.dump" + ".json";

                        File.WriteAllText(fileName, Base64Extract);
                        var sentIds = message.PrepAdverseEventExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new PrepExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));
                       
                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Patient adverse events extract export Error");
                        throw;
                    }
                }
                DomainEvents.Dispatch(new PrepExportNotification(new SendProgress(nameof(PrepAdverseEventExtract), Common.GetProgress(count, total), sendCound)));
            }
            DomainEvents.Dispatch(new PrepExportNotification(new SendProgress(nameof(PrepAdverseEventExtract), Common.GetProgress(count, total), sendCound, true)));
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> ExportPrepBehaviourRisksAsync(SendManifestPackageDTO sendTo)
        {
            return ExportPrepBehaviourRisksAsync(sendTo, PrepMessageBag.CreateEx(_packager.GeneratePrepBehaviourRisks().ToList()));
        }
        public async Task<List<SendMpiResponse>> ExportPrepBehaviourRisksAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));
            foreach (var message in messageBag.Messages)
            {

                if (message.PrepBehaviourRiskExtracts.Count > 0)
                {
                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.PrepBehaviourRiskExtracts[0].SiteCode + "-Prep" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName  + "PrepBehaviourRiskExtracts.dump" + ".json";

                        File.WriteAllText(fileName, Base64Extract);
                        var sentIds = message.PrepBehaviourRiskExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new PrepExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));
                        
                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Export PrepBehaviourRiskExtracts Error");
                        throw;
                    }
                }
                DomainEvents.Dispatch(new PrepExportNotification(new SendProgress(nameof(PrepBehaviourRiskExtract), Common.GetProgress(count, total), sendCound)));
            }
            DomainEvents.Dispatch(new PrepExportNotification(new SendProgress(nameof(PrepBehaviourRiskExtract), Common.GetProgress(count, total), sendCound, true)));
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> ExportPrepCareTerminationsAsync(SendManifestPackageDTO sendTo)
        {
            return ExportPrepCareTerminationsAsync(sendTo, PrepMessageBag.CreateEx(_packager.GeneratePrepCareTerminations().ToList()));
        }
        public async Task<List<SendMpiResponse>> ExportPrepCareTerminationsAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));
            foreach (var message in messageBag.Messages)
            {

                if (message.PrepCareTerminationExtracts.Count > 0)
                {
                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath ="exports";
                        string folderName = Path.Combine(projectPath, message.PrepCareTerminationExtracts[0].SiteCode + "-Prep" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName  + "PrepCareTerminationExtracts.dump" + ".json";

                        File.WriteAllText(fileName, Base64Extract);
                        var sentIds = message.PrepCareTerminationExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new PrepExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));
                       
                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Export PrepCareTerminationExtracts Error");
                        throw;
                    }
                }
                DomainEvents.Dispatch(new PrepExportNotification(new SendProgress(nameof(PrepCareTerminationExtract), Common.GetProgress(count, total), sendCound)));
            }
            DomainEvents.Dispatch(new PrepExportNotification(new SendProgress(nameof(PrepCareTerminationExtract), Common.GetProgress(count, total), sendCound, true)));
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> ExportPrepLabsAsync(SendManifestPackageDTO sendTo)
        {
            return ExportPrepLabsAsync(sendTo, PrepMessageBag.CreateEx(_packager.GeneratePrepLabs().ToList()));
        }
        public async Task<List<SendMpiResponse>> ExportPrepLabsAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));
            foreach (var message in messageBag.Messages)
            {
                if (message.PrepLabExtracts.Count > 0)
                {
                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath ="exports";
                        string folderName = Path.Combine(projectPath, message.PrepLabExtracts[0].SiteCode + "-Prep" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "PrepLabExtracts.dump" + ".json";

                        File.WriteAllText(fileName, Base64Extract);
                        var sentIds = message.PrepLabExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new PrepExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));
                       
                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Export PrepLabExtracts Error");
                        throw;
                    }
                }
                DomainEvents.Dispatch(new PrepExportNotification(new SendProgress(nameof(PrepLabExtract), Common.GetProgress(count, total), sendCound)));
            }
            DomainEvents.Dispatch(new PrepExportNotification(new SendProgress(nameof(PrepLabExtract), Common.GetProgress(count, total), sendCound, true)));
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> ExportPrepPharmacysAsync(SendManifestPackageDTO sendTo)
        {
            return ExportPrepPharmacysAsync(sendTo, PrepMessageBag.CreateEx(_packager.GeneratePrepPharmacys().ToList()));
        }
        public async Task<List<SendMpiResponse>> ExportPrepPharmacysAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));
            foreach (var message in messageBag.Messages)
            {

                if (message.PrepPharmacyExtracts.Count > 0)
                {
                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath =  "exports";
                        string folderName = Path.Combine(projectPath, message.PrepPharmacyExtracts[0].SiteCode + "-Prep" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName  + "PrepPharmacyExtracts.dump" + ".json";

                        File.WriteAllText(fileName, Base64Extract);
                        var sentIds = message.PrepPharmacyExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new PrepExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));
                       
                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Export PrepPharmacyExtracts Error");
                        throw;
                    }
                }
                DomainEvents.Dispatch(new PrepExportNotification(new SendProgress(nameof(PrepPharmacyExtract), Common.GetProgress(count, total), sendCound)));
            }
            DomainEvents.Dispatch(new PrepExportNotification(new SendProgress(nameof(PrepPharmacyExtract), Common.GetProgress(count, total), sendCound, true)));
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> ExportPrepVisitsAsync(SendManifestPackageDTO sendTo)
        {
            return ExportPrepVisitsAsync(sendTo, PrepMessageBag.CreateEx(_packager.GeneratePrepVisits().ToList()));
        }
        public async Task<List<SendMpiResponse>> ExportPrepVisitsAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();
            string startPath = "";
            string zipPath = "";
            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));
            foreach (var message in messageBag.Messages)
            {

                if (message.PrepVisitExtracts.Count > 0)
                {
                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.PrepVisitExtracts[0].SiteCode + "-Prep" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                        startPath = Path.Combine(projectPath, message.PrepVisitExtracts[0].SiteCode + "-Prep");
                        zipPath = Path.Combine(projectPath, message.PrepVisitExtracts[0].SiteCode + "-Prep" + ".zip");
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "PrepVisitExtracts.dump" + ".json";

                        File.WriteAllText(fileName, Base64Extract);
                        var sentIds = message.PrepVisitExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new PrepExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));
                        



                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Export PrepVisitExtracts Error");
                        throw;
                    }
                }
                DomainEvents.Dispatch(new PrepExportNotification(new SendProgress(nameof(PrepVisitExtract), Common.GetProgress(count, total), sendCound)));



            }       


            DomainEvents.Dispatch(new PrepExportNotification(new SendProgress(nameof(PrepVisitExtract), Common.GetProgress(count, total), sendCound, true)));
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));
            return responses;
        }

        public async Task NotifyPostSending(SendManifestPackageDTO sendTo, string version)
        {
            var notificationend = new HandshakeEnd("PREPSendEnd", version);
            await _mediator.Publish(notificationend);
            var client = Client ?? new HttpClient();
            try
            {
                var session = _reader.GetSession(notificationend.EndName);
                var response =
                    await client.PostAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}Handshake?session={session}"),
                        null);
            }
            catch (Exception e)
            {
                Log.Error(e, $"Send Handshake Error");
            }
        }

        public async Task<List<SendMpiResponse>> SendPrepFiles(IFormFile file)
        {
            var responses = new List<SendMpiResponse>();
            SendManifestPackageDTO sendTo = null;
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


                        }
                        _recordsSaved++;
                        await UpdateProgress();

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

                                ManifestMessage manifest = JsonConvert.DeserializeObject<ManifestMessage>(Extract);
                                manifest.GenerateId();
                                
                               
                                
  try
                                    {
                                        var msg = JsonConvert.SerializeObject(manifest);
                                        var response =
                                            await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}manifest"), manifest);
                                        if (response.IsSuccessStatusCode)
                                        {
                                            var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                                            responses.Add(content);
                                        }
                                        else
                                        {
                                            var error = await response.Content.ReadAsStringAsync();
                                            throw new Exception(error);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send Manifest Error");
                                        throw;
                                    }
                                

                            }
                            catch (Exception ex)
                            {
                                Log.Error(ex, $"Send Manifest Error");
                                throw;

                            }
                            _recordsSaved++;
                            await UpdateProgress();


                        }

                    }

                    break;

                }               
                for (int i = 1; i < archive.Entries.Count; i++)
                {

                    if (archive.Entries[i].Name == "PatientPrepExtracts.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                int count = 0;
                                PrepMessage message = JsonConvert.DeserializeObject<PrepMessage>(Extract);
                               
                                    count++;
                                    try
                                    {
                                        var msg = JsonConvert.SerializeObject(message);
                                        var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}PatientPrep"), message);
                                        if (response.IsSuccessStatusCode)
                                        {
                                            var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                                            responses.Add(content);
                                           
                                            
                                        }
                                        else
                                        {
                                            var error = await response.Content.ReadAsStringAsync();                                            
                                            throw new Exception(error);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send PatientPrep Extracts Error");
                                        throw;
                                    }
                                   
                                
                            }
                            catch (Exception e)
                            {
                                Log.Error(e, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }

                            _recordsSaved++;
                            await UpdateProgress();
                            
                        }
                    }
                    else if (archive.Entries[i].Name == "PrepBehaviourRiskExtracts.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                int count = 0;
                                PrepMessage message = JsonConvert.DeserializeObject<PrepMessage>(Extract);
                              
                                    count++;
                                    try
                                    {
                                        var msg = JsonConvert.SerializeObject(message);
                                        var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}PrepBehaviourRisk"), message);
                                        if (response.IsSuccessStatusCode)
                                        {
                                            var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                                            responses.Add(content);
                                            

                                        }
                                        else
                                        {
                                            var error = await response.Content.ReadAsStringAsync();
                                            throw new Exception(error);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send PatientPrep Extracts Error");
                                        throw;
                                    }
                                
                              
                            }
                            catch (Exception e)
                            {
                                Log.Error(e, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }
                            _recordsSaved++;
                            await UpdateProgress();
                        }
                    }
                    else if (archive.Entries[i].Name == "PrepVisitExtracts.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                int count = 0;
                                PrepMessage message = JsonConvert.DeserializeObject<PrepMessage>(Extract);
                               
                                    count++;
                                    try
                                    {
                                        var msg = JsonConvert.SerializeObject(message);
                                        var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}PrepVisit"), message);
                                        if (response.IsSuccessStatusCode)
                                        {
                                            var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                                            responses.Add(content);

                                        }
                                        else
                                        {
                                            var error = await response.Content.ReadAsStringAsync();
                                            throw new Exception(error);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send PatientPrep Extracts Error");
                                        throw;
                                    }

                                
                        
                            }
                            catch (Exception e)
                            {
                                Log.Error(e, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }
                            _recordsSaved++;
                            await UpdateProgress();
                        }
                    }
                    else if (archive.Entries[i].Name == "PrepLabExtracts.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                int count = 0;
                                PrepMessage message = JsonConvert.DeserializeObject<PrepMessage>(Extract);

                                    count++;
                                    try
                                    {
                                        var msg = JsonConvert.SerializeObject(message);
                                        var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}PrepLab"), message);
                                        if (response.IsSuccessStatusCode)
                                        {
                                            var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                                            responses.Add(content);
                                            

                                        }
                                        else
                                        {
                                            var error = await response.Content.ReadAsStringAsync();
                                            throw new Exception(error);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send PatientPrep Extracts Error");
                                        throw;
                                    }

                                
                                
                            }
                            catch (Exception e)
                            {
                                Log.Error(e, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }
                            _recordsSaved++;
                            await UpdateProgress();
                        }
                    }
                    else if (archive.Entries[i].Name == "PrepPharmacyExtracts.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                int count = 0;
                                PrepMessage message = JsonConvert.DeserializeObject<PrepMessage>(Extract);
                              
                                    count++;
                                    try
                                    {
                                        var msg = JsonConvert.SerializeObject(message);
                                        var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}PrepPharmacy"), message);
                                        if (response.IsSuccessStatusCode)
                                        {
                                            var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                                            responses.Add(content);
                                            

                                        }
                                        else
                                        {
                                            var error = await response.Content.ReadAsStringAsync();
                                            throw new Exception(error);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send PrepPharmacy Extracts Error");
                                        throw;
                                    }

                                
                          
                            }
                            catch (Exception e)
                            {
                                Log.Error(e, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }
                            _recordsSaved++;
                            await UpdateProgress();
                            
                        }
                    }
                    else if (archive.Entries[i].Name == "PrepAdverseEventExtracts.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                int count = 0;
                                PrepMessage message = JsonConvert.DeserializeObject<PrepMessage>(Extract);
                                
                                    try
                                    {
                                        var msg = JsonConvert.SerializeObject(message);
                                        var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}PrepAdverseEvent"), message);
                                        if (response.IsSuccessStatusCode)
                                        {
                                            var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                                            responses.Add(content);

                                        }
                                        else
                                        {
                                            var error = await response.Content.ReadAsStringAsync();
                                            throw new Exception(error);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send PrepAdverseEvent Extracts Error");
                                        throw;
                                    }

                                
                            }
                            catch (Exception e)
                            {
                                Log.Error(e, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }

                            _recordsSaved++;
                            await UpdateProgress();
                            
                        }
                    }
                    else if (archive.Entries[i].Name == "PrepCareTerminationExtracts.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                int count = 0;
                                PrepMessage message = JsonConvert.DeserializeObject<PrepMessage>(Extract);
                               
                             
                                    try
                                    {
                                        var msg = JsonConvert.SerializeObject(message);
                                        var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}PrepCareTermination"), message);
                                        if (response.IsSuccessStatusCode)
                                        {
                                            var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                                            responses.Add(content);

                                        }
                                        else
                                        {
                                            var error = await response.Content.ReadAsStringAsync();
                                            throw new Exception(error);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e, $"Send PrepCareTerminationExtracts Extracts Error");
                                        throw;
                                    }

                                
                                
                            }
                            catch (Exception e)
                            {
                                Log.Error(e, $"Send Extracts {archive.Entries[i].Name} Error");
                                throw;
                            }
                            _recordsSaved++;
                            await UpdateProgress();
                           
                        }
                    }

                }


                return responses;

            }
            


        }

        public Task ZipExtractsAsync(SendManifestPackageDTO sendTo, string version)
        {
            return ZipExtractsAsync(sendTo,
               ManifestMessageBag.Create(_packager.GenerateWithMetrics(sendTo.GetEmrDto()).ToList()), version);
        }

        public async Task ZipExtractsAsync(SendManifestPackageDTO sendTo,ManifestMessageBag manifestMessage, string version)
        {
           
            foreach (var message in manifestMessage.Messages)
            {


                string projectPath = ("exports");
                string startPath = Path.Combine(projectPath, message.Manifest.SiteCode + "-Prep");
                string zipPath = Path.Combine(projectPath, message.Manifest.SiteCode + "-Prep" + ".zip");


                if (File.Exists(zipPath))
                    File.Delete(zipPath);
                ZipFile.CreateFromDirectory(startPath, zipPath, CompressionLevel.Fastest, true);


            }

        }


        private async Task UpdateProgress()
        {
            var progress = ((double)_recordsSaved / _totalRecords) * 100;
            await _hubContext.Clients.All.SendAsync("ReceiveProgressPrep", progress);
        }
    }
}
