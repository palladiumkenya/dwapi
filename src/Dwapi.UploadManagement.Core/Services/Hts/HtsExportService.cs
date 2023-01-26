using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
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
using Dwapi.UploadManagement.Core.Interfaces.Packager.Hts;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Services.Hts;
using Dwapi.UploadManagement.Core.Notifications.Dwh;
using Dwapi.UploadManagement.Core.Notifications.Hts;
using MediatR;
using Microsoft.AspNetCore.Hosting;
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

        public HttpClient Client { get; set; }
        public HtsExportService(IHtsPackager packager, IMediator mediator, IEmrMetricReader reader, IHostingEnvironment hostingEnvironment)
        {
            _packager = packager;
            _mediator = mediator;
            _reader = reader;
            _endPoint = "api/hts/";
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
                    string folderName = Path.Combine(projectPath, Convert.ToString(message.Manifest.SiteCode) + "-Hts").HasToEndsWith(@"\");
                    Directory.CreateDirectory(folderName);                    // Write that JSON to txt file,
                    string fileName = folderName + "manifest.dump" + ".json";
                    File.WriteAllText(fileName.ToOsStyle(), Base64Manifest);
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
                        string folderName = Path.Combine(projectPath, message.Clients[0].SiteCode + "-Hts" + "\\extracts").HasToEndsWith(@"\");

                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName  + "Clients.dump" + ".json";                
                        await File.WriteAllTextAsync(fileName.ToOsStyle(), Base64Extract);
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
                        string folderName = Path.Combine(projectPath, message.ClientLinkage[0].SiteCode + "-Hts" + "\\extracts").HasToEndsWith(@"\");

                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "ClientLinkage.dump" + ".json";

                        await File.WriteAllTextAsync(fileName.ToOsStyle(), Base64Extract);
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
                        string folderName = Path.Combine(projectPath, message.ClientTests[0].SiteCode + "-Hts" + "\\extracts").HasToEndsWith(@"\"); ;


                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "ClientTests.dump" + ".json";

                        await File.WriteAllTextAsync(fileName.ToOsStyle(), Base64Extract);
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
                        string folderName = Path.Combine(projectPath, message.TestKits[0].SiteCode + "-Hts" + "\\extracts").HasToEndsWith(@"\"); ;
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName  + "TestKits.dump" + ".json";

                        await File.WriteAllTextAsync(fileName.ToOsStyle(), Base64Extract);
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
                        string folderName = Path.Combine(projectPath, message.ClientTracing[0].SiteCode + "-Hts" + "\\extracts").HasToEndsWith(@"\"); ;
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName  + "ClientTracing.dump" + ".json";

                        await File.WriteAllTextAsync(fileName.ToOsStyle(), Base64Extract);
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
                        string folderName = Path.Combine(projectPath, message.PartnerTracing[0].SiteCode + "-Hts" + "\\extracts").HasToEndsWith(@"\"); ;
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "PartnerTracing.dump" + ".json";

                        await File.WriteAllTextAsync(fileName.ToOsStyle(), Base64Extract);
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
                        string folderName = Path.Combine(projectPath, message.PartnerNotificationServices[0].SiteCode + "-Hts" + "\\extracts").HasToEndsWith(@"\"); ;
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "PartnerNotificationServices.dump" + ".json";

                        await File.WriteAllTextAsync(fileName.ToOsStyle(), Base64Extract);
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
                        string folderName = Path.Combine(projectPath, message.HTSEligibility[0].SiteCode + "-Hts" + "\\extracts").HasToEndsWith(@"\"); ;
                        startPath = Path.Combine(projectPath, message.HTSEligibility[0].SiteCode + "-Hts");
                        zipPath = Path.Combine(projectPath, message.HTSEligibility[0].SiteCode + "-Hts" + ".zip");


                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "HTSEligibility.dump" + ".json";

                        await File.WriteAllTextAsync(fileName.ToOsStyle(), Base64Extract);
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

           
            if (File.Exists(zipPath))
                File.Delete(zipPath);
            ZipFile.CreateFromDirectory(startPath, zipPath, CompressionLevel.Fastest, true);

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
    }



}
