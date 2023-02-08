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
using Dwapi.ExtractsManagement.Core.Model.Destination.Mnch;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SettingsManagement.Core.Application.Metrics.Events;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Event.Mnch;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Exchange.Mnch;
using Dwapi.UploadManagement.Core.Exchange.Prep;
using Dwapi.UploadManagement.Core.Hubs.BoardRoomUpload;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Mnch;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Services.Mnch;
using Dwapi.UploadManagement.Core.Notifications.Mnch;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Serilog;


namespace Dwapi.UploadManagement.Core.Services.Mnch
{
    public class MnchExportService : IMnchExportService
    {
        private readonly string _endPoint;
        private readonly IMnchPackager _packager;
        private readonly IMediator _mediator;
        private readonly IEmrMetricReader _reader;
        private IHostingEnvironment _hostingEnvironment;
        private readonly IHubContext<ProgressHub> _hubContext;
        private int _totalRecords;
        private int _recordsSaved;

        public HttpClient Client { get; set; }

        public MnchExportService(IMnchPackager packager, IMediator mediator, IEmrMetricReader reader, IHostingEnvironment hostingEnvironment, IHubContext<ProgressHub> hubContext)
        {
            _packager = packager;
            _mediator = mediator;
            _reader = reader;
            _endPoint = "api/mnch/";
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
            await _mediator.Publish(new HandshakeStart("MNCHSendStart", version, manifestMessage.Session));
            var client = Client ?? new HttpClient();

            foreach (var message in manifestMessage.Messages)
            {
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                    var Base64Manifest = Convert.ToBase64String(plainTextBytes);
                    string projectPath = ("exports");
                    string folderName = Path.Combine(projectPath, Convert.ToString(message.Manifest.SiteCode) + "-Mnch").HasToEndsWith(@"\").ToOsStyle();
                    Directory.CreateDirectory(folderName);
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
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
            }

            return responses;
        }

        public Task<List<SendMpiResponse>> ExportPatientMnchsAsync(SendManifestPackageDTO sendTo)
        {
            return ExportPatientMnchsAsync(sendTo, MnchMessageBag.CreateEx(_packager.GeneratePatientMnchs().ToList()));
        }

        public async Task<List<SendMpiResponse>> ExportPatientMnchsAsync(SendManifestPackageDTO sendTo,
            MnchMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));
            foreach (var message in messageBag.Messages)
            {
                if (message.PatientMnchExtracts.Count > 0)
                {
                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.PatientMnchExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();

                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "PatientMnchExtracts.dump" + ".json";


                        await File.WriteAllTextAsync(fileName, Base64Extract);
                        var sentIds = message.MnchEnrolmentExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MnchExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));



                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Error exporting client");
                        throw;
                    }
                }

            }
            DomainEvents.Dispatch(new MnchExportNotification(new SendProgress(nameof(PatientMnchExtract), Common.GetProgress(count, total), sendCound, true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> ExportMnchEnrolmentsAsync(SendManifestPackageDTO sendTo)
        {
            return ExportMnchEnrolmentsAsync(sendTo, MnchMessageBag.CreateEx(_packager.GenerateMnchEnrolments().ToList()));
        }

        public async Task<List<SendMpiResponse>> ExportMnchEnrolmentsAsync(SendManifestPackageDTO sendTo,
            MnchMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));
            foreach (var message in messageBag.Messages)
            {
                if (message.MnchEnrolmentExtracts.Count > 0)
                {

                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.MnchEnrolmentExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "MnchEnrolmentExtracts.dump" + ".json";


                        await File.WriteAllTextAsync(fileName, Base64Extract);
                        var sentIds = message.MnchEnrolmentExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MnchExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));

                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Error exporting MnchEnrolmentExtracts");
                        throw;
                    }
                }

            }
            DomainEvents.Dispatch(new MnchExportNotification(new SendProgress(nameof(MnchEnrolmentExtract), Common.GetProgress(count, total), sendCound, true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> ExportMnchArtsAsync(SendManifestPackageDTO sendTo)
        {
            return ExportMnchArtsAsync(sendTo, MnchMessageBag.CreateEx(_packager.GenerateMnchArts().ToList()));
        }

        public async Task<List<SendMpiResponse>> ExportMnchArtsAsync(SendManifestPackageDTO sendTo,
            MnchMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));
            foreach (var message in messageBag.Messages)
            {
                if (message.MnchArtExtracts.Count > 0)
                {
                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.MnchArtExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "MnchArtExtracts.dump" + ".json";
                        await File.WriteAllTextAsync(fileName, Base64Extract);
                        var sentIds = message.MnchArtExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MnchExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));

                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Error exporting MnchArtExtracts");
                        throw;

                    }
                }

            }
            DomainEvents.Dispatch(new MnchExportNotification(new SendProgress(nameof(MnchArtExtract), Common.GetProgress(count, total), sendCound, true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> ExportAncVisitsAsync(SendManifestPackageDTO sendTo)
        {
            return ExportAncVisitsAsync(sendTo, MnchMessageBag.CreateEx(_packager.GenerateAncVisits().ToList()));
        }

        public async Task<List<SendMpiResponse>> ExportAncVisitsAsync(SendManifestPackageDTO sendTo,
            MnchMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));
            foreach (var message in messageBag.Messages)
            {
                if (message.AncVisitExtracts.Count > 0)
                {
                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.AncVisitExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "AncVisitExtracts.dump" + ".json";

                        await File.WriteAllTextAsync(fileName, Base64Extract);
                        var sentIds = message.AncVisitExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MnchExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));

                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Error exporting Anc Visits");
                        throw;
                    }
                }
            }
            DomainEvents.Dispatch(new MnchExportNotification(new SendProgress(nameof(AncVisitExtract), Common.GetProgress(count, total), sendCound, true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> ExportMatVisitsAsync(SendManifestPackageDTO sendTo)
        {
            return ExportMatVisitsAsync(sendTo, MnchMessageBag.CreateEx(_packager.GenerateMatVisits().ToList()));
        }

        public async Task<List<SendMpiResponse>> ExportMatVisitsAsync(SendManifestPackageDTO sendTo,
            MnchMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));
            foreach (var message in messageBag.Messages)
            {
                if (message.MatVisitExtracts.Count > 0)
                {
                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.MatVisitExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "MatVisitExtracts.dump" + ".json";

                        await File.WriteAllTextAsync(fileName, Base64Extract);
                        var sentIds = message.MatVisitExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MnchExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));

                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Error exporting MatVisitExtract");
                        throw;
                    }
                }
            }
            DomainEvents.Dispatch(new MnchExportNotification(new SendProgress(nameof(MatVisitExtract), Common.GetProgress(count, total), sendCound, true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> ExportPncVisitsAsync(SendManifestPackageDTO sendTo)
        {
            return ExportPncVisitsAsync(sendTo, MnchMessageBag.CreateEx(_packager.GeneratePncVisits().ToList()));
        }

        public async Task<List<SendMpiResponse>> ExportPncVisitsAsync(SendManifestPackageDTO sendTo,
            MnchMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();
            string startPath = "";
            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            string zipPath = "";
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));
            foreach (var message in messageBag.Messages)
            {

                if (message.PncVisitExtracts.Count > 0)
                {
                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.PncVisitExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                        startPath = Path.Combine(projectPath, message.PncVisitExtracts[0].SiteCode + "-Mnch");
                        zipPath = Path.Combine(projectPath, message.PncVisitExtracts[0].SiteCode + "-Mnch" + ".zip");

                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "PncVisitExtracts.dump" + ".json";

                        await File.WriteAllTextAsync(fileName, Base64Extract);
                        var sentIds = message.PncVisitExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MnchExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));



                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Send Manifest Error");
                        throw;
                    }
                }

            }


            

            DomainEvents.Dispatch(new MnchExportNotification(new SendProgress(nameof(PncVisitExtract), Common.GetProgress(count, total), sendCound, true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> ExportMotherBabyPairsAsync(SendManifestPackageDTO sendTo)
        {
            return ExportMotherBabyPairsAsync(sendTo,
                MnchMessageBag.CreateEx(_packager.GenerateMotherBabyPairs().ToList()));
        }

        public async Task<List<SendMpiResponse>> ExportMotherBabyPairsAsync(SendManifestPackageDTO sendTo,
            MnchMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));
            foreach (var message in messageBag.Messages)
            {

                if (message.MotherBabyPairExtracts.Count > 0)
                {
                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.MotherBabyPairExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "MotherBabyPairExtracts.dump" + ".json";

                        await File.WriteAllTextAsync(fileName, Base64Extract);
                        var sentIds = message.MotherBabyPairExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MnchExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));

                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Error exporting MotherBabyPairExtracts");
                        throw;
                    }
                }
            }
            DomainEvents.Dispatch(new MnchExportNotification(new SendProgress(nameof(MotherBabyPairExtract), Common.GetProgress(count, total), sendCound, true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> ExportCwcEnrolmentsAsync(SendManifestPackageDTO sendTo)
        {
            return ExportCwcEnrolmentsAsync(sendTo, MnchMessageBag.CreateEx(_packager.GenerateCwcEnrolments().ToList()));
        }

        public async Task<List<SendMpiResponse>> ExportCwcEnrolmentsAsync(SendManifestPackageDTO sendTo,
            MnchMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));
            foreach (var message in messageBag.Messages)
            {

                if (message.CwcEnrolmentExtracts.Count > 0)
                {
                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.CwcEnrolmentExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "CwcEnrolmentExtracts.dump" + ".json";

                        await File.WriteAllTextAsync(fileName, Base64Extract);
                        var sentIds = message.CwcEnrolmentExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MnchExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));

                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Send Manifest Error");
                        throw;
                    }
                }

            }
            DomainEvents.Dispatch(new MnchExportNotification(new SendProgress(nameof(CwcEnrolmentExtract), Common.GetProgress(count, total), sendCound, true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> ExportCwcVisitsAsync(SendManifestPackageDTO sendTo)
        {
            return ExportCwcVisitsAsync(sendTo, MnchMessageBag.CreateEx(_packager.GenerateCwcVisits().ToList()));
        }

        public async Task<List<SendMpiResponse>> ExportCwcVisitsAsync(SendManifestPackageDTO sendTo,
            MnchMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));
            foreach (var message in messageBag.Messages)
            {
                if (message.CwcVisitExtracts.Count > 0)
                {

                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.CwcVisitExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "CwcVisitExtracts.dump" + ".json";

                        await File.WriteAllTextAsync(fileName, Base64Extract);
                        var sentIds = message.CwcVisitExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MnchExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));

                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Error exporting CwcVisitsExtract");
                        throw;
                    }
                }
            }
            DomainEvents.Dispatch(new MnchExportNotification(new SendProgress(nameof(CwcVisitExtract), Common.GetProgress(count, total), sendCound, true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> ExportHeisAsync(SendManifestPackageDTO sendTo)
        {
            return ExportHeisAsync(sendTo, MnchMessageBag.CreateEx(_packager.GenerateHeis().ToList()));
        }

        public async Task<List<SendMpiResponse>> ExportHeisAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));
            foreach (var message in messageBag.Messages)
            {
                if (message.HeiExtracts.Count > 0)
                {

                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.HeiExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "HeiExtracts.dump" + ".json";

                        await File.WriteAllTextAsync(fileName, Base64Extract);
                        var sentIds = message.HeiExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MnchExtractSentEvent(sentIds, SendStatus.Exported, sendTo.ExtractName));

                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Send Manifest Error");
                        throw;
                    }
                }

            }
            DomainEvents.Dispatch(new MnchExportNotification(new SendProgress(nameof(HeiExtract), Common.GetProgress(count, total), sendCound, true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> ExportMnchLabsAsync(SendManifestPackageDTO sendTo)
        {
            return ExportMnchLabsAsync(sendTo, MnchMessageBag.CreateEx(_packager.GenerateMnchLabs().ToList()));
        }

        public async Task<List<SendMpiResponse>> ExportMnchLabsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound = 0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Exporting));
            foreach (var message in messageBag.Messages)
            {

                if (message.MnchLabExtracts.Count > 0)
                {
                    count++;
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var plainTextBytes = Encoding.UTF8.GetBytes(msg);
                        var Base64Extract = Convert.ToBase64String(plainTextBytes);
                        string projectPath = "exports";
                        string folderName = Path.Combine(projectPath, message.MnchLabExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\").ToOsStyle();
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "MnchLabExtracts.dump" + ".json";

                        await File.WriteAllTextAsync(fileName, Base64Extract);
                        var sentIds = message.MnchLabExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;


                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Error exporting MnchLabExtracts");
                        throw;
                    }
                }

            }
            DomainEvents.Dispatch(new MnchExportNotification(new SendProgress(nameof(MnchLabExtract), Common.GetProgress(count, total), sendCound, true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.exported, sendCound));
            return responses;
        }

        public async Task<List<SendMpiResponse>> SendMnchFiles(IFormFile file)
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

                    if (archive.Entries[i].Name == "PatientMnchExtracts.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                MnchMessage message = JsonConvert.DeserializeObject<MnchMessage>(Extract);

                                count++;
                                var msg = JsonConvert.SerializeObject(message);
                                var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}PatientMnch"), message);
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
                                Log.Error(e, $"Send PatientMnchExtracts Extracts Error");
                                throw;
                            }


                        }


                        _recordsSaved++;
                        await UpdateProgress();

                    }
                    else if (archive.Entries[i].Name == "MnchEnrolmentExtracts.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                MnchMessage message = JsonConvert.DeserializeObject<MnchMessage>(Extract);

                                count++;
                                var msg = JsonConvert.SerializeObject(message);
                                var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}MnchEnrolment"), message);
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
                                Log.Error(e, $"Send MnchEnrolment Extracts Error");
                                throw;
                            }


                        }


                        _recordsSaved++;
                        await UpdateProgress();

                    }
                    else if (archive.Entries[i].Name == "MnchArtExtracts.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                MnchMessage message = JsonConvert.DeserializeObject<MnchMessage>(Extract);

                                count++;
                                var msg = JsonConvert.SerializeObject(message);
                                var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}MnchArt"), message);
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
                                Log.Error(e, $"Send MnchArtExtracts Extracts Error");
                                throw;
                            }


                        }


                        _recordsSaved++;
                        await UpdateProgress();

                    }
                    else if (archive.Entries[i].Name == "AncVisitExtracts.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                MnchMessage message = JsonConvert.DeserializeObject<MnchMessage>(Extract);

                                count++;
                                var msg = JsonConvert.SerializeObject(message);
                                var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}AncVisit"), message);
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
                                Log.Error(e, $"Send AncVisitExtracts Extracts Error");
                                throw;
                            }


                        }


                        _recordsSaved++;
                        await UpdateProgress();

                    }
                    else if (archive.Entries[i].Name == "MatVisitExtracts.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                MnchMessage message = JsonConvert.DeserializeObject<MnchMessage>(Extract);

                                count++;
                                var msg = JsonConvert.SerializeObject(message);
                                var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}MatVisit"), message);
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
                                Log.Error(e, $"Send MatVisitExtracts Extracts Error");
                                throw;
                            }


                        }


                        _recordsSaved++;
                        await UpdateProgress();

                    }
                    else if (archive.Entries[i].Name == "PncVisitExtracts.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                MnchMessage message = JsonConvert.DeserializeObject<MnchMessage>(Extract);

                                count++;
                                var msg = JsonConvert.SerializeObject(message);
                                var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}PncVisit"), message);
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
                                Log.Error(e, $"Send PncVisitExtracts Extracts Error");
                                throw;
                            }


                        }


                        _recordsSaved++;
                        await UpdateProgress();

                    }
                    else if (archive.Entries[i].Name == "MotherBabyPairExtracts.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                MnchMessage message = JsonConvert.DeserializeObject<MnchMessage>(Extract);

                                count++;
                                var msg = JsonConvert.SerializeObject(message);
                                var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}MotherBabyPair"), message);
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
                                Log.Error(e, $"Send PncVisitExtracts Extracts Error");
                                throw;
                            }


                        }


                        _recordsSaved++;
                        await UpdateProgress();

                    }
                    else if (archive.Entries[i].Name == "CwcEnrolmentExtracts.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                MnchMessage message = JsonConvert.DeserializeObject<MnchMessage>(Extract);

                                count++;
                                var msg = JsonConvert.SerializeObject(message);
                                var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}CwcEnrolment"), message);
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
                                Log.Error(e, $"Send CwcEnrolmentExtracts Extracts Error");
                                throw;
                            }


                        }


                        _recordsSaved++;
                        await UpdateProgress();

                    }
                    else if (archive.Entries[i].Name == "CwcVisitExtracts.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                MnchMessage message = JsonConvert.DeserializeObject<MnchMessage>(Extract);

                                count++;
                                var msg = JsonConvert.SerializeObject(message);
                                var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}CwcVisit"), message);
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
                                Log.Error(e, $"Send CwcVisit Extracts Error");
                                throw;
                            }


                        }


                        _recordsSaved++;
                        await UpdateProgress();

                    }
                    else if (archive.Entries[i].Name == "HeiExtracts.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                MnchMessage message = JsonConvert.DeserializeObject<MnchMessage>(Extract);

                                count++;
                                var msg = JsonConvert.SerializeObject(message);
                                var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}Hei"), message);
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
                                Log.Error(e, $"Send HeiExtracts Extracts Error");
                                throw;
                            }


                        }


                        _recordsSaved++;
                        await UpdateProgress();

                    }
                    else if (archive.Entries[i].Name == "MnchLabExtracts.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
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
                                MnchMessage message = JsonConvert.DeserializeObject<MnchMessage>(Extract);

                                count++;
                                var msg = JsonConvert.SerializeObject(message);
                                var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}MnchLab"), message);
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
                                Log.Error(e, $"Send HeiExtracts Extracts Error");
                                throw;
                            }


                        }


                        _recordsSaved++;
                        await UpdateProgress();

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
                string startPath = Path.Combine(projectPath, message.Manifest.SiteCode + "-Mnch");
                string zipPath = Path.Combine(projectPath, message.Manifest.SiteCode + "-Mnch" + ".zip");


                if (File.Exists(zipPath))
                    File.Delete(zipPath);
                ZipFile.CreateFromDirectory(startPath, zipPath, CompressionLevel.Fastest, true);


            }
        }


        private async Task UpdateProgress()
        {
            var progress = ((double)_recordsSaved / _totalRecords) * 100;
            await _hubContext.Clients.All.SendAsync("ReceiveProgressMnch", progress);
        }


        public async Task NotifyPostSending(SendManifestPackageDTO sendTo, string version)
        {
            var notificationend = new HandshakeEnd("MNCHSendEnd", version);
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
