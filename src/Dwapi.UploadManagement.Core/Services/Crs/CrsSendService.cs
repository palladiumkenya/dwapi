using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Model.Destination.Crs;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SettingsManagement.Core.Application.Metrics.Events;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Event.Crs;
using Dwapi.UploadManagement.Core.Exchange.Crs;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Crs;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Services.Crs;
using Dwapi.UploadManagement.Core.Notifications.Crs;
using Dwapi.UploadManagement.Core.Notifications.Dwh;
using MediatR;
using Newtonsoft.Json;
using Serilog;

namespace Dwapi.UploadManagement.Core.Services.Crs
{
    public class CrsSendService : ICrsSendService
    {
        private readonly string _endPoint;
        private readonly ICrsPackager _packager;
        private readonly IMediator _mediator;
        private IEmrMetricReader _reader;

        public HttpClient Client { get; set; }

        public CrsSendService(ICrsPackager packager, IMediator mediator, IEmrMetricReader reader)
        {
            _packager = packager;
            _mediator = mediator;
            _reader = reader;
            _endPoint = "api/Crs/";
        }

        public Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo,string version)
        {
            return SendManifestAsync(sendTo, ManifestMessageBag.Create(_packager.GenerateWithMetrics(sendTo.GetEmrDto()).ToList()),version);
        }

        public Task<List<SendCrsResponse>> SendCrsAsync(SendManifestPackageDTO sendTo)
        {
            return SendCrsAsync(sendTo, CrsMessageBag.Create(_packager.GenerateCrs().ToList()));
        }

        public async Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo, ManifestMessageBag manifestMessage,string version)
        {
            var responses=new List<SendManifestResponse>();
            await _mediator.Publish(new HandshakeStart("CRSSendStart", version, manifestMessage.Session));
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

        public async Task<List<SendCrsResponse>> SendCrsAsync(SendManifestPackageDTO sendTo, CrsMessageBag messageBag)
        {
            var responses = new List<SendCrsResponse>();

            var client = Client ?? new HttpClient();

            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new CrsStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));

            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}ClientRegistry"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendCrsResponse>();
                        responses.Add(content);

                        var sentIds = message.ClientRegistryExtracts.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new CrsExtractSentEvent(sentIds, SendStatus.Sent));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new CrsExtractSentEvent(message.ClientRegistryExtracts.Select(x => x.Id).ToList(), SendStatus.Failed,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Extracts Error");
                    throw;
                }

                DomainEvents.Dispatch(new CrsSendNotification(new SendProgress(nameof(ClientRegistryExtract), Common.GetProgress(count,total),sendCound)));
            }

            DomainEvents.Dispatch(new CrsStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));

            return responses;
        }

        public async Task NotifyPostSending(SendManifestPackageDTO sendTo,string version)
        {
            var notificationend = new HandshakeEnd("CRSSendEnd", version);
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
