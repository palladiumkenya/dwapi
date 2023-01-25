using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
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
using Dwapi.UploadManagement.Core.Interfaces.Packager.Mnch;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Services.Mnch;
using Dwapi.UploadManagement.Core.Notifications.Mnch;
using MediatR;
using Microsoft.AspNetCore.Hosting;
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

        public HttpClient Client { get; set; }

        public MnchExportService(IMnchPackager packager, IMediator mediator, IEmrMetricReader reader, IHostingEnvironment hostingEnvironment)
        {
            _packager = packager;
            _mediator = mediator;
            _reader = reader;
            _endPoint = "api/mnch/";
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
                    string folderName = Path.Combine(projectPath, Convert.ToString(message.Manifest.SiteCode) + "-Mnch").HasToEndsWith(@"\");
                    Directory.CreateDirectory(folderName);
                    string fileName = folderName + "manifest.dump" + ".json";
                    File.WriteAllText(fileName.ToOsStyle(), Base64Manifest);
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
                        string folderName = Path.Combine(projectPath, message.PatientMnchExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\");

                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                            string fileName = folderName  + "PatientMnchExtracts.dump" + ".json";

                   
                            await File.WriteAllTextAsync(fileName.ToOsStyle(), msg);
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
                        string folderName = Path.Combine(projectPath, message.MnchEnrolmentExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\");
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                             string fileName = folderName  + "MnchEnrolmentExtracts.dump" + ".json";

                    
                            await File.WriteAllTextAsync(fileName.ToOsStyle(), msg);
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
                        string folderName = Path.Combine(projectPath, message.MnchArtExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\");
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                            string fileName = folderName  + "MnchArtExtracts.dump" + ".json";                    
                            await File.WriteAllTextAsync(fileName.ToOsStyle(), msg);
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
                        string folderName = Path.Combine(projectPath, message.AncVisitExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\");
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName  + "AncVisitExtracts.dump" + ".json";

                        await File.WriteAllTextAsync(fileName.ToOsStyle(), msg);
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
                        string folderName = Path.Combine(projectPath, message.MatVisitExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\");
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                            string fileName = folderName  + "MatVisitExtracts.dump" + ".json";

                            await File.WriteAllTextAsync(fileName.ToOsStyle(), msg);
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
                        string folderName = Path.Combine(projectPath, message.PncVisitExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\");
                        startPath = Path.Combine(projectPath, message.PncVisitExtracts[0].SiteCode + "-Mnch");
                        zipPath = Path.Combine(projectPath, message.PncVisitExtracts[0].SiteCode + "-Mnch" + ".zip");

                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string  fileName= folderName + "PncVisitExtracts.dump" + ".json";

                        await File.WriteAllTextAsync(fileName.ToOsStyle(), msg);
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
                        string folderName = Path.Combine(projectPath, message.MotherBabyPairExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\");
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                            string fileName = folderName + "MotherBabyPairExtracts.dump" + ".json";

                            await File.WriteAllTextAsync(fileName.ToOsStyle(), msg);
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
                        string folderName = Path.Combine(projectPath, message.CwcEnrolmentExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\");
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "CwcEnrolmentExtracts.dump" + ".json";

                        await File.WriteAllTextAsync(fileName.ToOsStyle(), msg);
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
                        string folderName = Path.Combine(projectPath, message.CwcVisitExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\");
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "CwcVisitExtracts.dump" + ".json";

                        await File.WriteAllTextAsync(fileName.ToOsStyle(), msg);
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
                        string folderName = Path.Combine(projectPath, message.HeiExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\");
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName  + "HeiExtracts.dump" + ".json";

                        await File.WriteAllTextAsync(fileName.ToOsStyle(), msg);
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
                        string folderName = Path.Combine(projectPath, message.MnchLabExtracts[0].SiteCode + "-Mnch" + "\\extracts").HasToEndsWith(@"\");
                        if (!Directory.Exists(folderName))
                            Directory.CreateDirectory(folderName);

                        string fileName = folderName + "MnchLabExtracts.dump" + ".json";

                        await File.WriteAllTextAsync(fileName.ToOsStyle(), msg);
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
