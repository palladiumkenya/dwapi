using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SettingsManagement.Core.Application.Metrics.Events;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Event.Hts;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Exchange.Hts;
using Dwapi.UploadManagement.Core.Exchange.Prep;
using Dwapi.UploadManagement.Core.Hubs.BoardRoomUpload;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Hts;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Services.Hts;
using Dwapi.UploadManagement.Core.Notifications.Dwh;
using Dwapi.UploadManagement.Core.Notifications.Hts;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Serilog;
namespace Dwapi.UploadManagement.Core.Services.Hts
{
    public class HtsExportService : IHtsExportService
    {
        private readonly string _endPoint;
        private readonly IHtsPackager _packager;
        private readonly IMediator _mediator;
        private readonly IEmrMetricReader _reader;
        private IHostingEnvironment _hostingEnvironment;
        private readonly IHubContext<ProgressHub> _hubContext;
        private int _totalRecords;
        private int _recordsSaved;

        public HttpClient Client { get; set; }
        public HtsExportService(IHtsPackager packager, IMediator mediator, IEmrMetricReader reader, IHostingEnvironment hostingEnvironment, IHubContext<ProgressHub> hubContext)
        {
            _packager = packager;
            _mediator = mediator;
            _reader = reader;
            _endPoint = "api/hts/";
            _hubContext= hubContext;
            _hostingEnvironment = hostingEnvironment;

        }

        public Task<List<SendManifestResponse>> ExportManifestAsync(SendManifestPackageDTO sendTo, string version)
        {
            return ExportManifestAsync(sendTo, ManifestMessageBag.Create(_packager.GenerateWithMetrics(sendTo.GetEmrDto()).ToList()), version);
        }

        public async Task<List<SendManifestResponse>> ExportManifestAsync(SendManifestPackageDTO sendTo, ManifestMessageBag manifestMessage, string version)
        {
            var responses = new List<SendManifestResponse>();
            await _mediator.Publish(new HandshakeStart("HTSSendStart", version, manifestMessage.Session));
            var client = Client ?? new HttpClient();

            foreach (var message in manifestMessage.Messages)
            {
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                    var Base64Manifest = Convert.ToBase64String(plainTextBytes);
                    string projectPath ="exports";
                    string folderName = Path.Combine(projectPath, Convert.ToString(message.Manifest.SiteCode) + "-Hts").HasToEndsWith(@"\").ToOsStyle();
                    Directory.CreateDirectory(folderName);                    // Write that JSON to txt file,
                    string fileName = folderName + "manifest.dump" + ".json";
                    await File.WriteAllTextAsync(fileName, Base64Manifest);

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

        public Task<List<SendMpiResponse>> ExportClientsAsync(SendManifestPackageDTO sendTo)
        {
            return ExportClientsAsync(sendTo, HtsMessageBag.CreateEx(_packager.GenerateClients().ToList()));
        }

        public async Task<List<SendMpiResponse>> ExportClientsAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();

            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));

            foreach (var message in messageBag.Messages)
            {
                if (message.Clients.Count > 0)
                {
                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.Clients[0].SiteCode + "-Hts" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();

                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName  + "Clients.dump" + ".json";                
                        await File.WriteAllTextAsync(fileName, Base64Extract);
                        var sentIds = message.ClientLinkage.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new HtsExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));

                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Export Clients Error");
                        throw;
                    }
                }

                DomainEvents.Dispatch(new HtsExportNotification(new SendProgress(nameof(HtsClients), Common.GetProgress(count, total), sendCound)));
            }

            DomainEvents.Dispatch(new HtsExportNotification(new SendProgress(nameof(HtsClients), Common.GetProgress(count, total), sendCound, true)));

            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));

            return responses;
        }

        public Task<List<SendMpiResponse>> ExportClientsLinkagesAsync(SendManifestPackageDTO sendTo)
        {
            return ExportClientsLinkagesAsync(sendTo, HtsMessageBag.CreateEx(_packager.GenerateClientLinkage().ToList()));
        }

        public async Task<List<SendMpiResponse>> ExportClientsLinkagesAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();

            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));

            foreach (var message in messageBag.Messages)
            {
                if (message.ClientLinkage.Count > 0)
                {

                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.ClientLinkage[0].SiteCode + "-Hts" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();

                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "ClientLinkage.dump" + ".json";

                        await File.WriteAllTextAsync(fileName, Base64Extract);
                        var sentIds = message.ClientLinkage.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new HtsExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));
                       


                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Error exporting Client Linkage");
                        throw;
                    }
                }

                DomainEvents.Dispatch(new HtsExportNotification(new SendProgress(nameof(HtsClientLinkage), Common.GetProgress(count, total), sendCound)));
            }

            DomainEvents.Dispatch(new HtsExportNotification(new SendProgress(nameof(HtsClientLinkage), Common.GetProgress(count, total), sendCound, true)));

            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));

            return responses;
        }

        public Task<List<SendMpiResponse>> ExportClientTestsAsync(SendManifestPackageDTO sendTo)
        {
            return ExportClientTestsAsync(sendTo, HtsMessageBag.CreateEx(_packager.GenerateClientTests().ToList()));
        }

        public async Task<List<SendMpiResponse>> ExportClientTestsAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();

            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));

            foreach (var message in messageBag.Messages)
            {
                if (message.ClientTests.Count > 0)
                {

                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.ClientTests[0].SiteCode + "-Hts" + "\\extracts").HasToEndsWith(@"\").ToOsStyle(); ;


                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "ClientTests.dump" + ".json";

                        await File.WriteAllTextAsync(fileName, Base64Extract);
                        var sentIds = message.ClientTests.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new HtsExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));
                        //}


                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Export Client Test Error");
                        throw;
                    }
                }

                DomainEvents.Dispatch(new HtsExportNotification(new SendProgress(nameof(HtsClientTests), Common.GetProgress(count, total), sendCound)));
            }

            DomainEvents.Dispatch(new HtsExportNotification(new SendProgress(nameof(HtsClientTests), Common.GetProgress(count, total), sendCound, true)));

            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));

            return responses;
        }

        public Task<List<SendMpiResponse>> ExportTestKitsAsync(SendManifestPackageDTO sendTo)
        {
            return ExportTestKitsAsync(sendTo, HtsMessageBag.CreateEx(_packager.GenerateTestKits().ToList()));
        }

        public async Task<List<SendMpiResponse>> ExportTestKitsAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();

            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));

            foreach (var message in messageBag.Messages)
            {
                if (message.TestKits.Count > 0)
                {

                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.TestKits[0].SiteCode + "-Hts" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName  + "TestKits.dump" + ".json";

                        await File.WriteAllTextAsync(fileName, Base64Extract);
                        var sentIds = message.TestKits.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new HtsExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));
                        
                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Export Test Kits Error");
                        throw;
                    }
                }

                DomainEvents.Dispatch(new HtsExportNotification(new SendProgress(nameof(HtsTestKits), Common.GetProgress(count, total), sendCound)));
            }

            DomainEvents.Dispatch(new HtsExportNotification(new SendProgress(nameof(HtsTestKits), Common.GetProgress(count, total), sendCound, true)));

            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));

            return responses;
        }

        public Task<List<SendMpiResponse>> ExportClientTracingAsync(SendManifestPackageDTO sendTo)
        {
            return ExportClientTracingAsync(sendTo, HtsMessageBag.CreateEx(_packager.GenerateClientTracing().ToList()));
        }

        public async Task<List<SendMpiResponse>> ExportClientTracingAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();

            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));

            foreach (var message in messageBag.Messages)
            {
                if (message.ClientTracing.Count > 0)
                {
                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.ClientTracing[0].SiteCode + "-Hts" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName  + "ClientTracing.dump" + ".json";

                        await File.WriteAllTextAsync(fileName, Base64Extract);
                        var sentIds = message.ClientTracing.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new HtsExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));
                        

                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Export ClientTracing Error");
                        throw;
                    }
                }

                DomainEvents.Dispatch(new HtsExportNotification(new SendProgress(nameof(HtsClientTracing), Common.GetProgress(count, total), sendCound)));
            }

            DomainEvents.Dispatch(new HtsExportNotification(new SendProgress(nameof(HtsClientTracing), Common.GetProgress(count, total), sendCound, true)));

            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));

            return responses;
        }

        public Task<List<SendMpiResponse>> ExportPartnerTracingAsync(SendManifestPackageDTO sendTo)
        {
            return ExportPartnerTracingAsync(sendTo, HtsMessageBag.CreateEx(_packager.GeneratePartnerTracing().ToList()));
        }

        public async Task<List<SendMpiResponse>> ExportPartnerTracingAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();

            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));

            foreach (var message in messageBag.Messages)
            {
                if (message.PartnerTracing.Count > 0)
                {

                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath ="exports";
                        string folderName = Path.Combine(projectPath, message.PartnerTracing[0].SiteCode + "-Hts" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "PartnerTracing.dump" + ".json";

                        await File.WriteAllTextAsync(fileName, Base64Extract);
                        var sentIds = message.PartnerTracing.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new HtsExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));
                       
                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Export Partner Tracing Error");
                        throw;
                    }
                }

                DomainEvents.Dispatch(new HtsExportNotification(new SendProgress(nameof(HtsPartnerTracing), Common.GetProgress(count, total), sendCound)));
            }

            DomainEvents.Dispatch(new HtsExportNotification(new SendProgress(nameof(HtsPartnerTracing), Common.GetProgress(count, total), sendCound, true)));

            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));

            return responses;
        }

        public Task<List<SendMpiResponse>> ExportPartnerNotificationServicesAsync(SendManifestPackageDTO sendTo)
        {
            return ExportPartnerNotificationServicesAsync(sendTo, HtsMessageBag.CreateEx(_packager.GeneratePartnerNotificationServices().ToList()));
        }

        public async Task<List<SendMpiResponse>> ExportPartnerNotificationServicesAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();

            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));

            foreach (var message in messageBag.Messages)
            {
                if (message.PartnerNotificationServices.Count > 0)
                {
                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.PartnerNotificationServices[0].SiteCode + "-Hts" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "PartnerNotificationServices.dump" + ".json";

                        await File.WriteAllTextAsync(fileName, Base64Extract);
                        var sentIds = message.PartnerNotificationServices.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new HtsExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));
                    }

                    catch (Exception e)
                    {
                        Log.Error(e, $"Export  PartnerNotificationServices Error");
                        throw;
                    }
                }

                DomainEvents.Dispatch(new HtsExportNotification(new SendProgress(nameof(HtsPartnerNotificationServices), Common.GetProgress(count, total), sendCound)));
            }

            DomainEvents.Dispatch(new HtsExportNotification(new SendProgress(nameof(HtsPartnerNotificationServices), Common.GetProgress(count, total), sendCound, true)));

            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));

            return responses;
        }

        public Task<List<SendMpiResponse>> ExportHtsEligibilityExtractsAsync(SendManifestPackageDTO sendTo)
        {
            return ExportHtsEligibilityExtractsAsync(sendTo, HtsMessageBag.CreateEx(_packager.GenerateHtsEligibilityExtracts().ToList()));
        }

        public async Task<List<SendMpiResponse>> ExportHtsEligibilityExtractsAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();
            string startPath = "";
            string zipPath = "";
            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));

            foreach (var message in messageBag.Messages)
            {

                if (message.HTSEligibility.Count > 0)
                {
                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.HTSEligibility[0].SiteCode + "-Hts" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                        startPath = Path.Combine(projectPath, message.HTSEligibility[0].SiteCode + "-Hts");
                        zipPath = Path.Combine(projectPath, message.HTSEligibility[0].SiteCode + "-Hts" + ".zip");


                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "HTSEligibility.dump" + ".json";

                        await File.WriteAllTextAsync(fileName, Base64Extract);
                        var sentIds = message.HTSEligibility.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new HtsExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));
                       


                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Error sending hts eligibility screening");
                        throw;
                    }
                }

                DomainEvents.Dispatch(new HtsExportNotification(new SendProgress(nameof(HtsEligibilityExtract), Common.GetProgress(count, total), sendCound)));
            }

           
           

            DomainEvents.Dispatch(new HtsExportNotification(new SendProgress(nameof(HtsEligibilityExtract), Common.GetProgress(count, total), sendCound, true)));

            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));

            return responses;
        }


        public async Task NotifyPostSending(SendManifestPackageDTO sendTo, string version)
        {
            var notificationend = new HandshakeEnd("HTSSendEnd", version);
            await _mediator.Publish(notificationend);
            var client = Client ?? new HttpClient();
            try
            {
                var session = _reader.GetSession(notificationend.EndName);
                var response = await client.PostAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}Handshake?session={session}"), null);
            }
            catch (Exception e)
            {
                Log.Error(e, $"Send Handshake Error");
            }
        }

        //Upload

        public async Task<List<SendMpiResponse>> SendHtsFiles(IFormFile file)
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
                                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}manifest"), manifest);
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

                    if (archive.Entries[i].Name == "Clients.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                HtsMessage message = JsonConvert.DeserializeObject<HtsMessage>(Extract);

                                count++;
                                try
                                {
                                    var msg = JsonConvert.SerializeObject(message);
                                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}Clients"), message);
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
                    else if (archive.Entries[i].Name == "ClientLinkage.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                HtsMessage message = JsonConvert.DeserializeObject<HtsMessage>(Extract);

                                count++;
                                try
                                {
                                    var msg = JsonConvert.SerializeObject(message);
                                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}Linkages"), message);
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
                    else if (archive.Entries[i].Name == "ClientTests.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                HtsMessage message = JsonConvert.DeserializeObject<HtsMessage>(Extract);

                                count++;
                                try
                                {
                                    var msg = JsonConvert.SerializeObject(message);
                                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}HtsClientTests"), message);
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
                    else if (archive.Entries[i].Name == "TestKits.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                HtsMessage message = JsonConvert.DeserializeObject<HtsMessage>(Extract);

                                count++;
                                try
                                {
                                    var msg = JsonConvert.SerializeObject(message);
                                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}HtsTestKits"), message);
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
                    else if (archive.Entries[i].Name == "ClientTracing.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                HtsMessage message = JsonConvert.DeserializeObject<HtsMessage>(Extract);

                                count++;
                                try
                                {
                                    var msg = JsonConvert.SerializeObject(message);
                                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}HtsClientTracings"), message);
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
                    else if (archive.Entries[i].Name == "ClientTracing.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                HtsMessage message = JsonConvert.DeserializeObject<HtsMessage>(Extract);

                                count++;
                                try
                                {
                                    var msg = JsonConvert.SerializeObject(message);
                                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}HtsClientTracings"), message);
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
                    else if (archive.Entries[i].Name == "PartnerTracing.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                HtsMessage message = JsonConvert.DeserializeObject<HtsMessage>(Extract);

                                count++;
                                try
                                {
                                    var msg = JsonConvert.SerializeObject(message);
                                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}HtsPartnerTracings"), message);
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
                    else if (archive.Entries[i].Name == "PartnerNotificationServices.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                HtsMessage message = JsonConvert.DeserializeObject<HtsMessage>(Extract);

                                count++;
                                try
                                {
                                    var msg = JsonConvert.SerializeObject(message);
                                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}Pns"), message);
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
                    else if (archive.Entries[i].Name == "HTSEligibility.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                HtsMessage message = JsonConvert.DeserializeObject<HtsMessage>(Extract);

                                count++;
                                try
                                {
                                    var msg = JsonConvert.SerializeObject(message);
                                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}HtsEligibilityScreening"), message);
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
                   

                }
            }
            string version = GetType().Assembly.GetName().Version.ToString();
            await NotifyPostSending(sendTo, version);

            return responses;



        }
        public Task ZipExtractsAsync(SendManifestPackageDTO sendTo, string version)
        {
            return ZipExtractsAsync(sendTo,
               ManifestMessageBag.Create(_packager.GenerateWithMetrics(sendTo.GetEmrDto()).ToList()), version);
        }

        public async Task ZipExtractsAsync(SendManifestPackageDTO sendTo, ManifestMessageBag manifestMessage, string version)
        {

            foreach (var message in manifestMessage.Messages)
            {


                string projectPath = ("exports");
                string startPath = Path.Combine(projectPath, message.Manifest.SiteCode + "-Hts");
                string zipPath = Path.Combine(projectPath, message.Manifest.SiteCode + "-Hts" + ".zip");


                if (File.Exists(zipPath))
                    File.Delete(zipPath);
                ZipFile.CreateFromDirectory(startPath, zipPath, CompressionLevel.Fastest, true);


            }

        }
        private async Task UpdateProgress()
        {
            var progress = ((double)_recordsSaved / _totalRecords) * 100;
            await _hubContext.Clients.All.SendAsync("ReceiveProgressHts", progress);
        }
    }



}
