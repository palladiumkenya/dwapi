using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts;
using Dwapi.ExtractsManagement.Core.Notifications;
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
using Dwapi.UploadManagement.Core.Interfaces.Services.Hts;
using Dwapi.UploadManagement.Core.Notifications.Hts;
using Newtonsoft.Json;
using Serilog;

namespace Dwapi.UploadManagement.Core.Services.Hts
{
    public class HtsSendService : IHtsSendService
    {
        private readonly string _endPoint;
        private readonly IHtsPackager _packager;

        public HtsSendService(IHtsPackager packager)
        {
            _packager = packager;
            _endPoint = "api/hts/";
        }

        public Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo)
        {
            return SendManifestAsync(sendTo, ManifestMessageBag.Create(_packager.GenerateWithMetrics().ToList()));
        }

        public async Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo, ManifestMessageBag manifestMessage)
        {
            var responses=new List<SendManifestResponse>();

            var client = new HttpClient();

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

        public Task<List<SendMpiResponse>> SendClientsAsync(SendManifestPackageDTO sendTo)
        {
            return SendClientsAsync(sendTo, HtsMessageBag.Create(_packager.GenerateClients().ToList()));
        }

        public async Task<List<SendMpiResponse>> SendClientsAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();

            var client = new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));

            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}Clients"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);

                        var sentIds = message.Clients.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new HtsExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new HtsExtractSentEvent(message.Clients.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }

                DomainEvents.Dispatch(new HtsSendNotification(new SendProgress(nameof(HTSClientExtract), Common.GetProgress(count,total))));
            }

            DomainEvents.Dispatch(new HtsSendNotification(new SendProgress(nameof(HTSClientExtract), Common.GetProgress(count,total),true)));

            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));

            return responses;
        }

        public Task<List<SendMpiResponse>> SendClientLinkagesAsync(SendManifestPackageDTO sendTo)
        {
            return SendClientLinkagesAsync(sendTo, HtsMessageBag.Create(_packager.GenerateLinkages().ToList()));
        }

        public async Task<List<SendMpiResponse>> SendClientLinkagesAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();

            var client = new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));

            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}Linkages"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);

                        var sentIds = message.ClientLinkages.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new HtsExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new HtsExtractSentEvent(message.ClientLinkages.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }

                DomainEvents.Dispatch(new HtsSendNotification(new SendProgress(nameof(HTSClientLinkageExtract), Common.GetProgress(count,total))));
            }

            DomainEvents.Dispatch(new HtsSendNotification(new SendProgress(nameof(HTSClientLinkageExtract), Common.GetProgress(count,total),true)));

            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));

            return responses;
        }

        public Task<List<SendMpiResponse>> SendClientPartnersAsync(SendManifestPackageDTO sendTo)
        {
            return SendClientPartnersAsync(sendTo, HtsMessageBag.Create(_packager.GeneratePartners().ToList()));
        }

        public async Task<List<SendMpiResponse>> SendClientPartnersAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();

            var client = new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));

            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}Partners"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);

                        var sentIds = message.ClientPartners.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new HtsExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new HtsExtractSentEvent(message.ClientPartners.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }

                DomainEvents.Dispatch(new HtsSendNotification(new SendProgress(nameof(HTSClientPartnerExtract), Common.GetProgress(count,total))));
            }

            DomainEvents.Dispatch(new HtsSendNotification(new SendProgress(nameof(HTSClientPartnerExtract), Common.GetProgress(count,total),true)));

            DomainEvents.Dispatch(new HtsStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));

            return responses;
        }
    }
}
