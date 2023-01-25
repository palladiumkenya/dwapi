using Dwapi.ExtractsManagement.Core.Model.Destination.Prep;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SettingsManagement.Core.Application.Metrics.Events;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Event.Prep;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Exchange.Prep;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Prep;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Services.Prep;
using Dwapi.UploadManagement.Core.Notifications.Prep;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dwapi.UploadManagement.Core.Services.Prep
{
    public class PrepExportService : IPrepExportService
    {
        private readonly string _endPoint;
        private readonly IPrepPackager _packager;
        private readonly IMediator _mediator;
        private readonly IEmrMetricReader _reader;
        private IHostingEnvironment _hostingEnvironment;

        public HttpClient Client { get; set; }

        public PrepExportService(IPrepPackager packager, IMediator mediator, IEmrMetricReader reader, IHostingEnvironment hostingEnvironment)
        {
            _packager = packager;
            _mediator = mediator;
            _reader = reader;
            _endPoint = "api/prep/";
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
                    string folderName = Path.Combine(projectPath, Convert.ToString(message.Manifest.SiteCode) + "-Prep").HasToEndsWith(@"\");
                    Directory.CreateDirectory(folderName);    // Write that JSON to txt file,
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
                        string folderName = Path.Combine(projectPath, message.PatientPrepExtracts[0].SiteCode + "-Prep" + "\\extracts").HasToEndsWith(@"\");

                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName  + "PatientPrepExtracts.dump" + ".json";

                        await File.WriteAllTextAsync(fileName.ToOsStyle(), Base64Extract);

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
                        string folderName = Path.Combine(projectPath, message.PrepAdverseEventExtracts[0].SiteCode + "-Prep" + "\\extracts").HasToEndsWith(@"\");
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "PrepAdverseEventExtracts.dump" + ".json";

                        File.WriteAllText(fileName.ToOsStyle(), Base64Extract);
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
                        string folderName = Path.Combine(projectPath, message.PrepBehaviourRiskExtracts[0].SiteCode + "-Prep" + "\\extracts").HasToEndsWith(@"\");
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName  + "PrepBehaviourRiskExtracts.dump" + ".json";

                        File.WriteAllText(fileName.ToOsStyle(), Base64Extract);
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
                        string folderName = Path.Combine(projectPath, message.PrepCareTerminationExtracts[0].SiteCode + "-Prep" + "\\extracts").HasToEndsWith(@"\");
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName  + "PrepCareTerminationExtracts.dump" + ".json";

                        File.WriteAllText(fileName.ToOsStyle(), Base64Extract);
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
                        string folderName = Path.Combine(projectPath, message.PrepLabExtracts[0].SiteCode + "-Prep" + "\\extracts").HasToEndsWith(@"\");
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "PrepLabExtracts.dump" + ".json";

                        File.WriteAllText(fileName.ToOsStyle(), Base64Extract);
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
                        string folderName = Path.Combine(projectPath, message.PrepPharmacyExtracts[0].SiteCode + "-Prep" + "\\extracts").HasToEndsWith(@"\");
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName  + "PrepPharmacyExtracts.dump" + ".json";

                        File.WriteAllText(fileName.ToOsStyle(), Base64Extract);
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
                        string folderName = Path.Combine(projectPath, message.PrepVisitExtracts[0].SiteCode + "-Prep" + "\\extracts").HasToEndsWith(@"\");
                        startPath = Path.Combine(projectPath, message.PrepVisitExtracts[0].SiteCode + "-Prep");
                        zipPath = Path.Combine(projectPath, message.PrepVisitExtracts[0].SiteCode + "-Prep" + ".zip");
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "PrepVisitExtracts.dump" + ".json";

                        File.WriteAllText(fileName.ToOsStyle(), Base64Extract);
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


         
            if (File.Exists(zipPath))
                File.Delete(zipPath);
            ZipFile.CreateFromDirectory(startPath, zipPath, CompressionLevel.Fastest, true);


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
    }
}
