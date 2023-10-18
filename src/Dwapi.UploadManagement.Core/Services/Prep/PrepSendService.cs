using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
using Newtonsoft.Json;
using Serilog;

namespace Dwapi.UploadManagement.Core.Services.Prep
{
    public class PrepSendService : IPrepSendService
    {
        private readonly string _endPoint;
        private readonly IPrepPackager _packager;
        private readonly IMediator _mediator;
        private readonly IEmrMetricReader _reader;

        public HttpClient Client { get; set; }

        public PrepSendService(IPrepPackager packager, IMediator mediator, IEmrMetricReader reader)
        {
            _packager = packager;
            _mediator = mediator;
            _reader = reader;
            _endPoint = "api/prep/";
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
            await _mediator.Publish(new HandshakeStart("PREPSendStart", version, manifestMessage.Session));
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

        public Task<List<SendMpiResponse>> SendPatientPrepsAsync(SendManifestPackageDTO sendTo)
        {
            return SendPatientPrepsAsync(sendTo, PrepMessageBag.Create(_packager.GeneratePatientPreps().ToList()));
        }
        public async Task<List<SendMpiResponse>> SendPatientPrepsAsync(SendManifestPackageDTO sendTo,PrepMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));
            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}PatientPrep"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);
                        var sentIds = message.PatientPrepExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new PrepExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new PrepExtractSentEvent(message.PatientPrepExtracts.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
                DomainEvents.Dispatch(new PrepSendNotification(new SendProgress(nameof(PatientPrepExtract), Common.GetProgress(count,total),sendCound)));
            }
            DomainEvents.Dispatch(new PrepSendNotification(new SendProgress(nameof(PatientPrepExtract), Common.GetProgress(count,total),sendCound,true)));
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> SendPrepAdverseEventsAsync(SendManifestPackageDTO sendTo)
        {
            return SendPrepAdverseEventsAsync(sendTo, PrepMessageBag.Create(_packager.GeneratePrepAdverseEvents().ToList()));
        }
        public async Task<List<SendMpiResponse>> SendPrepAdverseEventsAsync(SendManifestPackageDTO sendTo,PrepMessageBag messageBag)
        {
              var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));
            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}PrepAdverseEvent"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);
                        var sentIds = message.PrepAdverseEventExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new PrepExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new PrepExtractSentEvent(message.PrepAdverseEventExtracts.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
                DomainEvents.Dispatch(new PrepSendNotification(new SendProgress(nameof(PrepAdverseEventExtract), Common.GetProgress(count,total),sendCound)));
            }
            DomainEvents.Dispatch(new PrepSendNotification(new SendProgress(nameof(PrepAdverseEventExtract), Common.GetProgress(count,total),sendCound,true)));
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> SendPrepBehaviourRisksAsync(SendManifestPackageDTO sendTo)
        {
            return SendPrepBehaviourRisksAsync(sendTo, PrepMessageBag.Create(_packager.GeneratePrepBehaviourRisks().ToList()));
        }
        public async Task<List<SendMpiResponse>> SendPrepBehaviourRisksAsync(SendManifestPackageDTO sendTo,PrepMessageBag messageBag)
        {
              var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));
            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}PrepBehaviourRisk"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);
                        var sentIds = message.PrepBehaviourRiskExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new PrepExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new PrepExtractSentEvent(message.PrepBehaviourRiskExtracts.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
                DomainEvents.Dispatch(new PrepSendNotification(new SendProgress(nameof(PrepBehaviourRiskExtract), Common.GetProgress(count,total),sendCound)));
            }
            DomainEvents.Dispatch(new PrepSendNotification(new SendProgress(nameof(PrepBehaviourRiskExtract), Common.GetProgress(count,total),sendCound,true)));
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> SendPrepCareTerminationsAsync(SendManifestPackageDTO sendTo)
        {
            return SendPrepCareTerminationsAsync(sendTo, PrepMessageBag.Create(_packager.GeneratePrepCareTerminations().ToList()));
        }
        public async Task<List<SendMpiResponse>> SendPrepCareTerminationsAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag)
        {
              var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));
            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}PrepCareTermination"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);
                        var sentIds = message.PrepCareTerminationExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new PrepExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new PrepExtractSentEvent(message.PrepCareTerminationExtracts.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
                DomainEvents.Dispatch(new PrepSendNotification(new SendProgress(nameof(PrepCareTerminationExtract), Common.GetProgress(count,total),sendCound)));
            }
            DomainEvents.Dispatch(new PrepSendNotification(new SendProgress(nameof(PrepCareTerminationExtract), Common.GetProgress(count,total),sendCound,true)));
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> SendPrepLabsAsync(SendManifestPackageDTO sendTo)
        {
            return SendPrepLabsAsync(sendTo, PrepMessageBag.Create(_packager.GeneratePrepLabs().ToList()));
        }
        public async Task<List<SendMpiResponse>> SendPrepLabsAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag)
        {
             var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));
            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}PrepLab"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);
                        var sentIds = message.PrepLabExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new PrepExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new PrepExtractSentEvent(message.PrepLabExtracts.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
                DomainEvents.Dispatch(new PrepSendNotification(new SendProgress(nameof(PrepLabExtract), Common.GetProgress(count,total),sendCound)));
            }
            DomainEvents.Dispatch(new PrepSendNotification(new SendProgress(nameof(PrepLabExtract), Common.GetProgress(count,total),sendCound,true)));
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> SendPrepPharmacysAsync(SendManifestPackageDTO sendTo)
        {
            return SendPrepPharmacysAsync(sendTo, PrepMessageBag.Create(_packager.GeneratePrepPharmacys().ToList()));
        }
        public async Task<List<SendMpiResponse>> SendPrepPharmacysAsync(SendManifestPackageDTO sendTo,PrepMessageBag messageBag)
        {
              var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));
            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}PrepPharmacy"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);
                        var sentIds = message.PrepPharmacyExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new PrepExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new PrepExtractSentEvent(message.PrepPharmacyExtracts.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
                DomainEvents.Dispatch(new PrepSendNotification(new SendProgress(nameof(PrepPharmacyExtract), Common.GetProgress(count,total),sendCound)));
            }
            DomainEvents.Dispatch(new PrepSendNotification(new SendProgress(nameof(PrepPharmacyExtract), Common.GetProgress(count,total),sendCound,true)));
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> SendPrepVisitsAsync(SendManifestPackageDTO sendTo)
        {
            return SendPrepVisitsAsync(sendTo, PrepMessageBag.Create(_packager.GeneratePrepVisits().ToList()));
        }
        public async Task<List<SendMpiResponse>> SendPrepVisitsAsync(SendManifestPackageDTO sendTo,PrepMessageBag messageBag)
        {
              var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));
            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}PrepVisit"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);
                        var sentIds = message.PrepVisitExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new PrepExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new PrepExtractSentEvent(message.PrepVisitExtracts.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
                DomainEvents.Dispatch(new PrepSendNotification(new SendProgress(nameof(PrepVisitExtract), Common.GetProgress(count,total),sendCound)));
            }
            DomainEvents.Dispatch(new PrepSendNotification(new SendProgress(nameof(PrepVisitExtract), Common.GetProgress(count,total),sendCound,true)));
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));
            return responses;
        }

        public Task<List<SendMpiResponse>> SendPrepMonthlyRefillAsync(SendManifestPackageDTO sendTo)
        {
            return SendPrepMonthlyRefillAsync(sendTo, PrepMessageBag.Create(_packager.GeneratePrepMonthlyRefill().ToList()));
        }
        public async Task<List<SendMpiResponse>> SendPrepMonthlyRefillAsync(SendManifestPackageDTO sendTo,PrepMessageBag messageBag)
        {
              var responses = new List<SendMpiResponse>();
            var client = Client ?? new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));
            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}PrepMonthlyRefill"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);
                        var sentIds = message.PrepMonthlyRefillExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new PrepExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new PrepExtractSentEvent(message.PrepMonthlyRefillExtracts.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
                DomainEvents.Dispatch(new PrepSendNotification(new SendProgress(nameof(PrepMonthlyRefillExtract), Common.GetProgress(count,total),sendCound)));
            }
            DomainEvents.Dispatch(new PrepSendNotification(new SendProgress(nameof(PrepMonthlyRefillExtract), Common.GetProgress(count,total),sendCound,true)));
            DomainEvents.Dispatch(new PrepStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));
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
