using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json;
using Serilog;

namespace Dwapi.UploadManagement.Core.Services.Mnch
{
    public class MnchSendService : IMnchSendService
    {
        private readonly string _endPoint;
        private readonly IMnchPackager _packager;
        private readonly IMediator _mediator;
        private readonly IEmrMetricReader _reader;

        public HttpClient Client { get; set; }

        public MnchSendService(IMnchPackager packager, IMediator mediator, IEmrMetricReader reader)
        {
            _packager = packager;
            _mediator = mediator;
            _reader = reader;
            _endPoint = "api/mnch/";
        }

        public Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo, string version)
        {
            return SendManifestAsync(sendTo,
                ManifestMessageBag.Create(_packager.GenerateWithMetrics(sendTo.GetEmrDto()).ToList()), version);
        }

        public async Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo,
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
                    var response =
                        await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}manifest"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendManifestResponse>();
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

            return responses;
        }

        public Task<List<SendMpiResponse>> SendPatientMnchsAsync(SendManifestPackageDTO sendTo)
        {
            return SendPatientMnchsAsync(sendTo, MnchMessageBag.Create(_packager.GeneratePatientMnchs().ToList()));
        }

        public async Task<List<SendMpiResponse>> SendPatientMnchsAsync(SendManifestPackageDTO sendTo,
            MnchMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));
            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}PatientMnch"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);
                        var sentIds = message.PatientMnchExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MnchExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new MnchExtractSentEvent(message.PatientMnchExtracts.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
                DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(PatientMnchExtract), Common.GetProgress(count,total),sendCound)));
            }
            DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(PatientMnchExtract), Common.GetProgress(count,total),sendCound,true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> SendMnchEnrolmentsAsync(SendManifestPackageDTO sendTo)
        {
            return SendMnchEnrolmentsAsync(sendTo, MnchMessageBag.Create(_packager.GenerateMnchEnrolments().ToList()));
        }

        public async Task<List<SendMpiResponse>> SendMnchEnrolmentsAsync(SendManifestPackageDTO sendTo,
            MnchMessageBag messageBag)
        {
              var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));
            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}MnchEnrolment"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);
                        var sentIds = message.MnchEnrolmentExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MnchExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new MnchExtractSentEvent(message.MnchEnrolmentExtracts.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
                DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(MnchEnrolmentExtract), Common.GetProgress(count,total),sendCound)));
            }
            DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(MnchEnrolmentExtract), Common.GetProgress(count,total),sendCound,true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> SendMnchArtsAsync(SendManifestPackageDTO sendTo)
        {
            return SendMnchArtsAsync(sendTo, MnchMessageBag.Create(_packager.GenerateMnchArts().ToList()));
        }

        public async Task<List<SendMpiResponse>> SendMnchArtsAsync(SendManifestPackageDTO sendTo,
            MnchMessageBag messageBag)
        {
              var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));
            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}MnchArt"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);
                        var sentIds = message.MnchArtExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MnchExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new MnchExtractSentEvent(message.MnchArtExtracts.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
                DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(MnchArtExtract), Common.GetProgress(count,total),sendCound)));
            }
            DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(MnchArtExtract), Common.GetProgress(count,total),sendCound,true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> SendAncVisitsAsync(SendManifestPackageDTO sendTo)
        {
            return SendAncVisitsAsync(sendTo, MnchMessageBag.Create(_packager.GenerateAncVisits().ToList()));
        }

        public async Task<List<SendMpiResponse>> SendAncVisitsAsync(SendManifestPackageDTO sendTo,
            MnchMessageBag messageBag)
        {
              var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));
            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}AncVisit"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);
                        var sentIds = message.AncVisitExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MnchExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new MnchExtractSentEvent(message.AncVisitExtracts.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
                DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(AncVisitExtract), Common.GetProgress(count,total),sendCound)));
            }
            DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(AncVisitExtract), Common.GetProgress(count,total),sendCound,true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> SendMatVisitsAsync(SendManifestPackageDTO sendTo)
        {
            return SendMatVisitsAsync(sendTo, MnchMessageBag.Create(_packager.GenerateMatVisits().ToList()));
        }

        public async Task<List<SendMpiResponse>> SendMatVisitsAsync(SendManifestPackageDTO sendTo,
            MnchMessageBag messageBag)
        {
              var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));
            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}MatVisit"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);
                        var sentIds = message.MatVisitExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MnchExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new MnchExtractSentEvent(message.MatVisitExtracts.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
                DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(MatVisitExtract), Common.GetProgress(count,total),sendCound)));
            }
            DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(MatVisitExtract), Common.GetProgress(count,total),sendCound,true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> SendPncVisitsAsync(SendManifestPackageDTO sendTo)
        {
            return SendPncVisitsAsync(sendTo, MnchMessageBag.Create(_packager.GeneratePncVisits().ToList()));
        }

        public async Task<List<SendMpiResponse>> SendPncVisitsAsync(SendManifestPackageDTO sendTo,
            MnchMessageBag messageBag)
        {
              var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));
            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}PncVisit"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);
                        var sentIds = message.PncVisitExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MnchExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new MnchExtractSentEvent(message.PncVisitExtracts.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
                DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(PncVisitExtract), Common.GetProgress(count,total),sendCound)));
            }
            DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(PncVisitExtract), Common.GetProgress(count,total),sendCound,true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> SendMotherBabyPairsAsync(SendManifestPackageDTO sendTo)
        {
            return SendMotherBabyPairsAsync(sendTo,
                MnchMessageBag.Create(_packager.GenerateMotherBabyPairs().ToList()));
        }

        public async Task<List<SendMpiResponse>> SendMotherBabyPairsAsync(SendManifestPackageDTO sendTo,
            MnchMessageBag messageBag)
        {
              var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));
            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}MotherBabyPair"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);
                        var sentIds = message.MotherBabyPairExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MnchExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new MnchExtractSentEvent(message.MotherBabyPairExtracts.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
                DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(MotherBabyPairExtract), Common.GetProgress(count,total),sendCound)));
            }
            DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(MotherBabyPairExtract), Common.GetProgress(count,total),sendCound,true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> SendCwcEnrolmentsAsync(SendManifestPackageDTO sendTo)
        {
            return SendCwcEnrolmentsAsync(sendTo, MnchMessageBag.Create(_packager.GenerateCwcEnrolments().ToList()));
        }

        public async Task<List<SendMpiResponse>> SendCwcEnrolmentsAsync(SendManifestPackageDTO sendTo,
            MnchMessageBag messageBag)
        {
              var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));
            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}CwcEnrolment"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);
                        var sentIds = message.CwcEnrolmentExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MnchExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new MnchExtractSentEvent(message.CwcEnrolmentExtracts.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
                DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(CwcEnrolmentExtract), Common.GetProgress(count,total),sendCound)));
            }
            DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(CwcEnrolmentExtract), Common.GetProgress(count,total),sendCound,true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> SendCwcVisitsAsync(SendManifestPackageDTO sendTo)
        {
            return SendCwcVisitsAsync(sendTo, MnchMessageBag.Create(_packager.GenerateCwcVisits().ToList()));
        }

        public async Task<List<SendMpiResponse>> SendCwcVisitsAsync(SendManifestPackageDTO sendTo,
            MnchMessageBag messageBag)
        {
              var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));
            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}CwcVisit"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);
                        var sentIds = message.CwcVisitExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MnchExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new MnchExtractSentEvent(message.CwcVisitExtracts.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
                DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(CwcVisitExtract), Common.GetProgress(count,total),sendCound)));
            }
            DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(CwcVisitExtract), Common.GetProgress(count,total),sendCound,true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> SendHeisAsync(SendManifestPackageDTO sendTo)
        {
            return SendHeisAsync(sendTo, MnchMessageBag.Create(_packager.GenerateHeis().ToList()));
        }

        public async Task<List<SendMpiResponse>> SendHeisAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag)
        {
              var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));
            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}Hei"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);
                        var sentIds = message.HeiExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MnchExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new MnchExtractSentEvent(message.HeiExtracts.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
                DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(HeiExtract), Common.GetProgress(count,total),sendCound)));
            }
            DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(HeiExtract), Common.GetProgress(count,total),sendCound,true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> SendMnchLabsAsync(SendManifestPackageDTO sendTo)
        {
            return SendMnchLabsAsync(sendTo, MnchMessageBag.Create(_packager.GenerateMnchLabs().ToList()));
        }

        public async Task<List<SendMpiResponse>> SendMnchLabsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag)
        {
             var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));
            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}MnchLab"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);
                        var sentIds = message.MnchLabExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MnchExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new MnchExtractSentEvent(message.MnchLabExtracts.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
                DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(MnchLabExtract), Common.GetProgress(count,total),sendCound)));
            }
            DomainEvents.Dispatch(new MnchSendNotification(new SendProgress(nameof(MnchLabExtract), Common.GetProgress(count,total),sendCound,true)));
            DomainEvents.Dispatch(new MnchStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));
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
