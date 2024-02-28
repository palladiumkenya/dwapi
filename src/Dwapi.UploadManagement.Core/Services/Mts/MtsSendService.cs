using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts.Dto;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SettingsManagement.Core.DTOs;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Event.Mgs;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Exchange.Mgs;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Mgs;
using Dwapi.UploadManagement.Core.Interfaces.Services.Mts;
using Dwapi.UploadManagement.Core.Notifications.Mgs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;

namespace Dwapi.UploadManagement.Core.Services.Mts
{
    public class MtsSendService : IMtsSendService
    {
        private readonly string _endPoint;
        private readonly IMgsPackager _packager;
        // private readonly HttpClient _httpClient;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly IPatientExtractRepository _repository;


        public HttpClient Client { get; set; }

        public MtsSendService(IMgsPackager packager,IPatientExtractRepository repository)
        {
            _packager = packager;
            _endPoint = "api/mgs/";
            _repository = repository;
        }

        public Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo)
        {
            return SendManifestAsync(sendTo, ManifestMessageBag.Create(_packager.GenerateWithMetrics(sendTo.GetEmrDto()).ToList()));
        }

        public async Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo, ManifestMessageBag manifestMessage)
        {
            var responses=new List<SendManifestResponse>();

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

        public Task<List<SendMpiResponse>> SendMigrationsAsync(SendManifestPackageDTO sendTo)
        {
            return SendMigrationsAsync(sendTo, MgsMessageBag.Create(_packager.GenerateMigrations().ToList()));
        }

        public async Task<List<SendMpiResponse>> SendMigrationsAsync(SendManifestPackageDTO sendTo, MgsMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();

            var client =Client ?? new HttpClient();
            int sendCound=0;
            int count = 0;
            int total = messageBag.Messages.Count;
            DomainEvents.Dispatch(new MgsStatusNotification(sendTo.ExtractId, ExtractStatus.Sending));

            foreach (var message in messageBag.Messages)
            {
                count++;
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}migrations"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);

                        var sentIds = message.Migrations.Select(x => x.Id).ToList();
                        sendCound += sentIds.Count;
                        DomainEvents.Dispatch(new MgsExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        DomainEvents.Dispatch(new MgsExtractSentEvent(message.Migrations.Select(x => x.Id).ToList(), SendStatus.Failed,sendTo.ExtractName,error));
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }

                DomainEvents.Dispatch(new MgsSendNotification(new SendProgress("Migration", Common.GetProgress(count,total),sendCound)));
            }

            DomainEvents.Dispatch(new MgsSendNotification(new SendProgress("Migration", Common.GetProgress(count,total),sendCound,true)));

            DomainEvents.Dispatch(new MgsStatusNotification(sendTo.ExtractId, ExtractStatus.Sent, sendCound));

            return responses;
        }
        
        
        // public async Task<Result> Handle(SendToSpot request, CancellationToken cancellationToken)
        public async Task SendIndicators(List<IndicatorExtractDto> indicators)
        {
            var client = Client ?? new HttpClient();

            try
            {
                List<JObject> ModifiedIndicators = new List<JObject>();
                
                int sitecode = _repository.GetSiteCode();
                string facilityname = _repository.GetFacilityName();

                foreach (var indicator in indicators)
                {
                    // Create a sample JObject
                    JObject ind = new JObject();
                    ind["facilityManifestId"] = null;
                    ind["facilityCode"] = sitecode;
                    ind["facilityName"] = facilityname;
                    ind["name"] = indicator.Indicator;
                    ind["statusInfo"] = null;
                    ind["value"] = indicator.IndicatorValue;
                    ind["indicatorDate"] = indicator.IndicatorDate;
                    ind["stage"] = "DWH";
                    ind["status"] = "SENT";
                    ind["statusDate"] = DateTime.Now;     
                    
                    ModifiedIndicators.Add(ind);
                }
                
                string requestEndpoint = "metric";
               
                Log.Debug("posting metrics to Livesync...");
                // var content = JsonConvert.SerializeObject(indicators, _serializerSettings);
                
                string content = JsonConvert.SerializeObject(ModifiedIndicators);
                StringContent toSend = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://ndwh.kenyahmis.org/metrics/api/LivesyncIndicators/SyncLivesyncIndicators", toSend
                );
                // var response = await client.PostAsync("https://localhost:7157/api/LivesyncIndicators/SyncLivesyncIndicators", toSend
                // );
                response.EnsureSuccessStatusCode();
                
                // DomainEvents.Dispatch(new MgsExtractSentEvent(sentIds, SendStatus.Sent,sendTo.ExtractName));
            }
            catch (Exception e)
            {
                Log.Error("Send to Livesync Error", e);
                Log.Error(e, $"Send Manifest Error");
                throw;
            }
        }
        
        
        public async Task<dynamic> VerifyMtsApi()
        {
            var client = Client ?? new HttpClient();

            try
            {
                var getresponse =
                    await client.GetAsync(
                        "https://ndwh.kenyahmis.org/metrics/api/LivesyncIndicators/api/verify");
                
                getresponse.EnsureSuccessStatusCode(); 
                return getresponse;

            }
            catch (Exception e)
            {
                Log.Error("MetricsAPI Error", e);
                Log.Error(e, $"Send Manifest Error");
                throw new Exception($"======> Metrics API is not reachable. Error Message: {e.Message}");
                // return e.Message;
            }
        }

    }
}
