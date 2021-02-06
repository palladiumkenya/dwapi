using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SettingsManagement.Core.Application.Metrics.Events;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Event.Cbs;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Services.Cbs;
using Dwapi.UploadManagement.Core.Notifications.Cbs;
using Dwapi.UploadManagement.Core.Notifications.Dwh;
using MediatR;
using Newtonsoft.Json;
using Serilog;

namespace Dwapi.UploadManagement.Core.Services.Cbs
{
    public class CbsSendService : ICbsSendService
    {
        private readonly string _endPoint;
        private readonly ICbsPackager _packager;
        private readonly IMediator _mediator;
        private IEmrMetricReader _reader;

        public HttpClient Client { get; set; }

        public CbsSendService(ICbsPackager packager, IMediator mediator, IEmrMetricReader reader)
        {
            _packager = packager;
            _mediator = mediator;
            _reader = reader;
            _endPoint = "api/cbs/";
        }

        public Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo,string version)
        {
            return SendManifestAsync(sendTo, ManifestMessageBag.Create(_packager.GenerateWithMetrics(sendTo.GetEmrDto()).ToList()),version);
        }

        public Task<List<SendMpiResponse>> SendMpiAsync(SendManifestPackageDTO sendTo)
        {
            return SendMpiAsync(sendTo, MpiMessageBag.Create(_packager.GenerateDtoMpi().ToList()));
        }

        public async Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo, ManifestMessageBag manifestMessage,string version)
        {
            var responses=new List<SendManifestResponse>();
            await _mediator.Publish(new HandshakeStart("MPISendStart", version, manifestMessage.Session));
            var client = Client ?? new HttpClient();

            foreach (var message in manifestMessage.Messages)
            {
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}manifest"), message);
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

        public async Task<List<SendMpiResponse>> SendMpiAsync(SendManifestPackageDTO sendTo, MpiMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();

            var client = Client ?? new HttpClient();

            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new CbsStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));

            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}mpi"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);

                        var sentIds = message.MasterPatientIndices.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new CbsExtractSentEvent(sentIds, SendStatus.Sent));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new CbsExtractSentEvent(message.MasterPatientIndices.Select(x => x.Id).ToList(), SendStatus.Failed,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Extracts Error");
                    throw;
                }

                DomainEvents.Dispatch(new CbsSendNotification(new SendProgress(nameof(MasterPatientIndex), Common.GetProgress(count,total),sendCound)));
            }

            DomainEvents.Dispatch(new CbsStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));

            return responses;
        }

        public async Task NotifyPostSending(SendManifestPackageDTO sendTo,string version)
        {
            var notificationend = new HandshakeEnd("MPISendEnd", version);
            await _mediator.Publish(notificationend);
            var client = Client ?? new HttpClient();
            try
            {
                var session = _reader.GetSession(notificationend.EndName);
                var response = await client.PostAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}Handshake?session={session}"),null);
            }
            catch (Exception e)
            {
                Log.Error(e, $"Send Handshake Error");
            }
        }
    }
}
