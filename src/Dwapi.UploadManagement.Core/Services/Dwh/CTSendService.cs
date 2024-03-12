using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Dwapi.ExtractsManagement.Core.Application.Events;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Diff;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SettingsManagement.Core.Application.Metrics.Events;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Event.Dwh;
using Dwapi.UploadManagement.Core.Exceptions;
using Dwapi.UploadManagement.Core.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Exchange.Dwh.Smart;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Services.Dwh;
using Dwapi.UploadManagement.Core.Notifications.Dwh;
using Humanizer;
using MediatR;
using Serilog;

namespace Dwapi.UploadManagement.Core.Services.Dwh
{
    public class CTSendService : ICTSendService
    {
        private readonly string _endPoint;
        private readonly IDwhPackager _packager;
        private readonly IMediator _mediator;
        private IEmrMetricReader _reader;
        private readonly ITransportLogRepository _transportLogRepository;
        private readonly IDiffLogRepository _diffLogRepository;
        private readonly IIndicatorExtractRepository _indicatorExtractRepository;
        private readonly IDwhExtractReader _dwhExtractReader;


        public HttpClient Client { get; set; }

        public CTSendService(IDwhPackager packager, IMediator mediator, IEmrMetricReader reader, ITransportLogRepository transportLogRepository, IDiffLogRepository diffLogRepository,IIndicatorExtractRepository indicatorExtractRepository, IDwhExtractReader dwhExtractReader)
        {
            _packager = packager;
            _mediator = mediator;
            _reader = reader;
            _transportLogRepository = transportLogRepository;
            _endPoint = "api/";
            _diffLogRepository = diffLogRepository;
            _indicatorExtractRepository = indicatorExtractRepository;
            _dwhExtractReader = dwhExtractReader;
        }

        public Task<List<SendDhwManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo)
        {
            return SendManifestAsync(sendTo,
                DwhManifestMessageBag.Create(_packager.GenerateWithMetrics(sendTo.GetEmrDto()).ToList()));
        }

        public Task<List<SendDhwManifestResponse>> SendSmartManifestAsync(SendManifestPackageDTO sendTo, string version,
            string apiVersion = "")
        {
            
            var response = SendSmartManifestAsync(sendTo,
                DwhManifestMessageBag.Create(_packager.GenerateWithMetrics(sendTo.GetEmrDto()).ToList()), version,
                apiVersion);
            return response;
            
        }

        public async Task<List<SendDhwManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo,
            DwhManifestMessageBag messageBag)
        {
            var responses = new List<SendDhwManifestResponse>();
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            var client = Client ?? new HttpClient(handler);
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            foreach (var message in messageBag.Messages)
            {
                try
                {
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}spot"),
                        message.Manifest);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        responses.Add(new SendDhwManifestResponse(content));
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

        public async Task<List<SendDhwManifestResponse>> SendSmartManifestAsync(SendManifestPackageDTO sendTo, DwhManifestMessageBag messageBag, string version,
            string apiVersion = "")
        {
             var responses = new List<SendDhwManifestResponse>();

            await _mediator.Publish(new HandshakeStart("CTSendStart", version, messageBag.Session));

            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            var client =Client ?? new HttpClient(handler);
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            foreach (var message in messageBag.Messages)
            {
                try
                {
                    var start = DateTime.Now;
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}spot",apiVersion), message.Manifest);
                    if (response.IsSuccessStatusCode)
                    {
                        if (string.IsNullOrWhiteSpace(apiVersion))
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            responses.Add(new SendDhwManifestResponse(content));
                        }
                        else
                        {
                            var content = await response.Content.ReadAsJsonAsync<ManifestResponse>();
                            responses.Add(new SendDhwManifestResponse(content));

                            var tlog = TransportLog.GenerateManifest("NDWH", content.JobId,
                                new Guid(content.ManifestId), content.Code,start,content.FacilityId);
                            _transportLogRepository.CreateLatest(tlog);
                            Log.Debug(new string('+',40));
                            Log.Debug($"CONNECTED: {sendTo.Destination.Url}");
                            Log.Debug(new string('_',40));
                            Log.Debug($"RECIEVED: {content}");
                            Log.Debug(new string('+',40));
                        }
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

        public void NotifyPreSending()
        {
            DomainEvents.Dispatch(new DwhMessageNotification(false, $"Sending started..."));

        }

        public async Task<List<SendCTResponse>> SendBatchExtractsAsync<T>(
            SendManifestPackageDTO sendTo,
            int batchSize,
            IMessageBag<T> messageBag)
            where T : ClientExtract
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            var client = Client ?? new HttpClient(handler);

            var responses = new List<SendCTResponse>();
            var packageInfo = _packager.GetPackageInfo<T>(batchSize);
            int sendCound = 0;
            int count = 0;
            int total = packageInfo.PageCount;
            int overall = 0;

            DomainEvents.Dispatch(new CTStatusNotification(sendTo.ExtractId, sendTo.GetExtractId(messageBag.ExtractName), ExtractStatus.Sending));
            long recordCount = 0;

            try
            {
                for (int page = 1; page <= packageInfo.PageCount; page++)
                {
                    count++;
                    var extracts = _packager.GenerateBatchExtracts<T>(page, packageInfo.PageSize).ToList();
                    recordCount = recordCount + extracts.Count;
                    Log.Debug(
                        $">>>> Sending {messageBag.ExtractName} {recordCount}/{packageInfo.TotalRecords} Page:{page} of {packageInfo.PageCount}");
                    messageBag = messageBag.Generate(extracts);
                    var message = messageBag.Messages;
                    try
                    {
                        int retryCount = 0;
                        bool allowSend = true;
                        while (allowSend)
                        {
                            var mflcode =   _indicatorExtractRepository.GetMflCode();
                            if (0 == mflcode)
                            {
                                // throw error
                                throw new Exception("First Time loading? Please load all first.");
                            }

                            var response = new HttpResponseMessage();
                            
                            var changesLoadedDifflog = _diffLogRepository.GetIfChangesHasBeenLoadedAlreadyLog("NDWH", "PatientExtract",mflcode);

                            if (null != changesLoadedDifflog)
                            {
                                response = await client.PostAsJsonAsync(
                                    sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);

                            }
                            else
                            {
                                response = await client.PostAsJsonAsync(
                                    sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v2/{messageBag.EndPoint}"), message);

                            }
                            // var response = await client.PostAsJsonAsync(
                            //     sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v2/{messageBag.EndPoint}"), message);
                            if (response.IsSuccessStatusCode)
                            {
                                allowSend = false;
                                responses.Add(new SendCTResponse());

                                var sentIds = messageBag.SendIds;
                                sendCound += sentIds.Count;
                                DomainEvents.Dispatch(new CTExtractSentEvent(sentIds, SendStatus.Sent,
                                    messageBag.ExtractType));
                            }
                            else
                            {
                                retryCount++;
                                if (retryCount == 4)
                                {
                                    var sentIds = messageBag.SendIds;
                                    var error = await response.Content.ReadAsStringAsync();
                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                        error));
                                    throw new Exception(error);
                                }
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Send Extracts {messageBag.ExtractName} Error ");
                        throw;
                    }

                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName,messageBag.GetProgress(count, total),recordCount)));

                }

                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));
            }
            catch (Exception e)
            {
                Log.Error(e, $"Send Extracts {messageBag.ExtractName} Error");
                throw;
            }

            DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName,
                messageBag.GetProgress(count, total), recordCount,true)));

            DomainEvents.Dispatch(new CTStatusNotification(sendTo.ExtractId,sendTo.GetExtractId(messageBag.ExtractName), ExtractStatus.Sent, sendCound)
                {UpdatePatient = (messageBag is ArtMessageBag || messageBag is BaselineMessageBag || messageBag is StatusMessageBag)}
            );

            return responses;
        }

        public async Task<List<SendCTResponse>> SendSmartBatchExtractsAsync<T>(SendManifestPackageDTO sendTo, int batchSize, IMessageSourceBag<T> messageBag) where T : ClientExtract
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            var client = Client ?? new HttpClient(handler);

            var responses = new List<SendCTResponse>();
            var packageInfo = _packager.GetPackageInfo<T>(batchSize);
            int sendCound = 0;
            int count = 0;
            int total = packageInfo.PageCount;
            int overall = 0;

            DomainEvents.Dispatch(new CTStatusNotification(sendTo.ExtractId, sendTo.GetExtractId(messageBag.ExtractName), ExtractStatus.Sending));
            long recordCount = 0;

            if (messageBag.ExtractName == "PatientArtExtract")
            {
                string x = "ss";
            }


            try
            {
                string jobId=string.Empty;Guid manifestId;Guid facilityId;
                var manifest = _transportLogRepository.GetManifest();

                if (messageBag.ExtractName == nameof(PatientExtract))
                {
                    var mainExtract = _transportLogRepository.GetMainExtract();
                    if (null == mainExtract)
                    {
                        jobId = manifest.JobId;
                        manifestId = manifest.ManifestId;
                        facilityId = manifest.FacilityId;
                    }
                    else
                    {
                        jobId = mainExtract.JobId;
                        manifestId = mainExtract.ManifestId;
                        facilityId = mainExtract.FacilityId;
                    }
                }
                else
                {
                    var mainExtract = _transportLogRepository.GetMainExtract();
                    jobId = mainExtract.JobId;
                    manifestId = mainExtract.ManifestId;
                    facilityId = mainExtract.FacilityId;
                }


                for (int page = 1; page <= packageInfo.PageCount; page++)
                {
                    count++;
                    var extracts = _packager.GenerateSmartBatchExtracts<T>(page, packageInfo.PageSize).ToList();
                    recordCount = recordCount + extracts.Count;
                    messageBag.Generate(extracts, manifestId, facilityId,jobId);
                    //messageBag = messageBag.Generate(extracts);
                    var message = messageBag;

                   Log.Debug(
                        $">>>> Sending {messageBag.ExtractName} {recordCount}/{packageInfo.TotalRecords}  Pks:[{messageBag.MinPk}-{messageBag.MaxPk}] Page:{page} of {packageInfo.PageCount}");

                    try
                    {
                        int retryCount = 0;
                        bool allowSend = true;
                        while (allowSend)
                        {
                            var response = await client.PostAsJsonAsync(
                                sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
                            if (response.IsSuccessStatusCode)
                            {
                                allowSend = false;
                                var res= await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                responses.Add(res);

                                var sentIds = messageBag.SendIds;
                                sendCound += sentIds.Count;
                                DomainEvents.Dispatch(new CTExtractSentEvent(sentIds, SendStatus.Sent,
                                    messageBag.ExtractType));

                                var tlog = TransportLog.GenerateExtract("NDWH",messageBag.ExtractName, res.JobId);
                                _transportLogRepository.CreateLatest(tlog);
                            }
                            else
                            {
                                retryCount++;
                                if (retryCount == 4)
                                {
                                    var sentIds = messageBag.SendIds;
                                    var error = await response.Content.ReadAsStringAsync();
                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                        error));
                                    throw new Exception(error);
                                }
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error SendSmartBatchExtractsAsync");
                        throw;
                    }

                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName,messageBag.GetProgress(count, total),recordCount)));

                }

                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));
            }
            catch (Exception e)
            {
                Log.Error(e, $"Send Extracts {messageBag.ExtractName} Error ");
                throw;
            }

            DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName,messageBag.GetProgress(count, total), recordCount,true)));

            DomainEvents.Dispatch(new CTStatusNotification(sendTo.ExtractId,sendTo.GetExtractId(messageBag.ExtractName), ExtractStatus.Sent, sendCound)
                {UpdatePatient = (messageBag is ArtMessageSourceBag || messageBag is BaselineMessageSourceBag || messageBag is StatusMessageSourceBag)}
            );
            stopWatch.Stop();
            Log.Debug(new string('*',40));
            Log.Debug($"Sent {recordCount} | {messageBag.ExtractName} in [{stopWatch.ElapsedMilliseconds/1000}] s");
            Log.Debug(new string('*',40));
            return responses;
        }

        public async Task<List<SendCTResponse>> SendSmartBatchExtractsFromReaderAsync<T>(SendManifestPackageDTO sendTo, int batchSize,
            IMessageSourceBag<T> messageBag) where T : ClientExtract
        {
             Stopwatch stopWatch = Stopwatch.StartNew();

            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            var client = Client ?? new HttpClient(handler);

            var responses = new List<SendCTResponse>();
            var packageInfo = _packager.GetPackageInfo<T>(batchSize);
            int sendCound = 0;
            int count = 0;
            int page = 0;
            int total = packageInfo.PageCount;
            int overall = 0;

            DomainEvents.Dispatch(new CTStatusNotification(sendTo.ExtractId, sendTo.GetExtractId(messageBag.ExtractName), ExtractStatus.Sending));
            long recordCount = 0; 
            
            try
            {
                string jobId=string.Empty;Guid manifestId;Guid facilityId;
                var manifest = _transportLogRepository.GetManifest();

                if (messageBag.ExtractName == nameof(PatientExtract))
                {
                    var mainExtract = _transportLogRepository.GetMainExtract();
                    if (null == mainExtract)
                    {
                        jobId = manifest.JobId;
                        manifestId = manifest.ManifestId;
                        facilityId = manifest.FacilityId;
                    }
                    else
                    {
                        jobId = mainExtract.JobId;
                        manifestId = mainExtract.ManifestId;
                        facilityId = mainExtract.FacilityId;
                    }
                }
                else
                {
                    var mainExtract = _transportLogRepository.GetMainExtract();
                    jobId = mainExtract.JobId;
                    manifestId = mainExtract.ManifestId;
                    facilityId = mainExtract.FacilityId;
                }

                var smartReader = _dwhExtractReader.GetSmartReader(messageBag.GetTableName());
                
                var extracts = new List<T>();
                
                while (smartReader.Read())
                {
                    
                    // int page = 1; page <= packageInfo.PageCount; page++)
                    
                    // map row to Extract

                    if (count == batchSize)
                    {
                        page++;
                        // send.
                        
                        #region SEND EXTRACTS
                        
                        recordCount += count;
                        messageBag.Generate(extracts, manifestId, facilityId, jobId);
                        //messageBag = messageBag.Generate(extracts);
                        var message = messageBag;

                        Log.Debug(
                            $">>>> Sending {messageBag.ExtractName} {recordCount}/{packageInfo.TotalRecords}  Pks:[{messageBag.MinPk}-{messageBag.MaxPk}] Page:{page} of {packageInfo.PageCount}");

                         try
                        {
                            int retryCount = 0;
                            bool allowSend = true;
                            while (allowSend)
                            {
                                var response = await client.PostAsJsonAsync(
                                    sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
                                if (response.IsSuccessStatusCode)
                                {
                                    allowSend = false;
                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);

                                    var sentIds = messageBag.SendIds;
                                    sendCound += sentIds.Count;
                                    DomainEvents.Dispatch(new CTExtractSentEvent(sentIds, SendStatus.Sent,
                                        messageBag.ExtractType));

                                    var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, res.JobId);
                                    _transportLogRepository.CreateLatest(tlog);
                                }
                                else
                                {
                                    retryCount++;
                                    if (retryCount == 4)
                                    {
                                        var sentIds = messageBag.SendIds;
                                        var error = await response.Content.ReadAsStringAsync();
                                        DomainEvents.Dispatch(new CTExtractSentEvent(
                                            sentIds, SendStatus.Failed, messageBag.ExtractType,
                                            error));
                                        throw new Exception(error);
                                    }
                                }
                            }

                        }
                        catch (Exception e)
                        {
                            Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error SendSmartBatchExtractsAsync");
                            throw;
                        }

                      
                        
                        DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName,
                            messageBag.GetProgress(count, total), recordCount)));
                        #endregion
                        
                        count = 0;
                        extracts = new List<T>(); 
                    }
                    
                    var parser = smartReader.GetRowParser<T>(typeof(T));
                    var extract = parser(smartReader);
                    extracts.Add(extract);
                    count++;
                }

                if (count > 0)
                {
                    page++;
                    #region SEND REMAINING EXTRACTS
                        
                        recordCount += count;
                        messageBag.Generate(extracts, manifestId, facilityId, jobId);
                        //messageBag = messageBag.Generate(extracts);
                        var message = messageBag;

                        Log.Debug(
                            $">>>> Sending {messageBag.ExtractName} {recordCount}/{packageInfo.TotalRecords}  Pks:[{messageBag.MinPk}-{messageBag.MaxPk}] Page:{page} of {packageInfo.PageCount}");

                         try
                        {
                            int retryCount = 0;
                            bool allowSend = true;
                            while (allowSend)
                            {
                                var response = await client.PostAsJsonAsync(
                                    sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v3/{messageBag.EndPoint}"), message);
                                if (response.IsSuccessStatusCode)
                                {
                                    allowSend = false;
                                    var res = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                    responses.Add(res);

                                    var sentIds = messageBag.SendIds;
                                    sendCound += sentIds.Count;
                                    DomainEvents.Dispatch(new CTExtractSentEvent(sentIds, SendStatus.Sent,
                                        messageBag.ExtractType));

                                    var tlog = TransportLog.GenerateExtract("NDWH", messageBag.ExtractName, res.JobId);
                                    _transportLogRepository.CreateLatest(tlog);
                                }
                                else
                                {
                                    retryCount++;
                                    if (retryCount == 4)
                                    {
                                        var sentIds = messageBag.SendIds;
                                        var error = await response.Content.ReadAsStringAsync();
                                        DomainEvents.Dispatch(new CTExtractSentEvent(
                                            sentIds, SendStatus.Failed, messageBag.ExtractType,
                                            error));
                                        throw new Exception(error);
                                    }
                                }
                            }

                        }
                        catch (Exception e)
                        {
                            Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error SendSmartBatchExtractsAsync");
                            throw;
                        }

                      
                        
                        DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName,
                            messageBag.GetProgress(count, total), recordCount)));
                        #endregion
                }
                
                
                
                 await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));
            }
            catch (Exception e)
            {
                Log.Error(e, $"Send Extracts {messageBag.ExtractName} Error ");
                throw;
            }

            DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName,messageBag.GetProgress(count, total), recordCount,true)));

            DomainEvents.Dispatch(new CTStatusNotification(sendTo.ExtractId,sendTo.GetExtractId(messageBag.ExtractName), ExtractStatus.Sent, sendCound)
                {UpdatePatient = (messageBag is ArtMessageSourceBag || messageBag is BaselineMessageSourceBag || messageBag is StatusMessageSourceBag)}
            );
            stopWatch.Stop();
            Log.Debug(new string('*',40));
            Log.Debug($"Sent {recordCount} | {messageBag.ExtractName} in [{stopWatch.ElapsedMilliseconds/1000}] s");
            Log.Debug(new string('*',40));
            return responses;
        }

        public async Task<List<SendCTResponse>> SendDiffBatchExtractsAsync<T>(SendManifestPackageDTO sendTo, int batchSize, IMessageBag<T> messageBag) where T : ClientExtract
        {
          HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            var client = Client ?? new HttpClient(handler);

            var responses = new List<SendCTResponse>();
            var packageInfo = _packager.GetPackageInfo<T>(batchSize);
            int sendCound = 0;
            int count = 0;
            int total = packageInfo.PageCount;
            int overall = 0;

            DomainEvents.Dispatch(new CTStatusNotification(sendTo.ExtractId, sendTo.GetExtractId(messageBag.ExtractName), ExtractStatus.Sending));
            long recordCount = 0;

            try
            {
                for (int page = 1; page <= packageInfo.PageCount; page++)
                {
                    count++;
                    var extracts = _packager.GenerateDiffBatchExtracts<T>(page, packageInfo.PageSize, messageBag.Docket,
                        messageBag.DocketExtract).ToList();
                    recordCount = recordCount + extracts.Count;

                    if (!extracts.Any())
                    {
                        count = total;
                        recordCount = packageInfo.TotalRecords;
                        DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName,messageBag.GetProgress(count, total),recordCount)));
                        break;
                    }

                    Log.Debug(
                        $">>>> Sending {messageBag.ExtractName} {recordCount}/{packageInfo.TotalRecords} Page:{page} of {packageInfo.PageCount}");
                    messageBag = messageBag.Generate(extracts);
                    var message = messageBag.Messages;
                    try
                    {
                        int retryCount = 0;
                        bool allowSend = true;
                        while (allowSend)
                        {
                            var response = await client.PostAsJsonAsync(
                                sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}v2/{messageBag.EndPoint}"), message);
                            if (response.IsSuccessStatusCode)
                            {
                                allowSend = false;
                                // var content = await response.Content.ReadAsJsonAsync<SendCTResponse>();
                                responses.Add(new SendCTResponse());

                                var sentIds = messageBag.SendIds;
                                sendCound += sentIds.Count;
                                DomainEvents.Dispatch(new CTExtractSentEvent(sentIds, SendStatus.Sent,
                                    messageBag.ExtractType));
                            }
                            else
                            {
                                retryCount++;
                                if (retryCount == 4)
                                {
                                    var sentIds = messageBag.SendIds;
                                    var error = await response.Content.ReadAsStringAsync();
                                    DomainEvents.Dispatch(new CTExtractSentEvent(
                                        sentIds, SendStatus.Failed, messageBag.ExtractType,
                                        error));
                                    throw new Exception(error);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error(e, $"Send Extracts{messageBag.ExtractName} Error");
                        throw;
                    }

                    DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName,messageBag.GetProgress(count, total),recordCount)));

                }

                await _mediator.Publish(new DocketExtractSent(messageBag.Docket, messageBag.DocketExtract));
            }
            catch (Exception e)
            {
                Log.Error(e, $"Send Extracts {messageBag.ExtractName} Error ");
                throw;
            }

            DomainEvents.Dispatch(new CTSendNotification(new SendProgress(messageBag.ExtractName,
                messageBag.GetProgress(count, total), recordCount,true)));

            DomainEvents.Dispatch(new CTStatusNotification(sendTo.ExtractId,sendTo.GetExtractId(messageBag.ExtractName), ExtractStatus.Sent, sendCound)
                {UpdatePatient = (messageBag is ArtMessageBag || messageBag is BaselineMessageBag || messageBag is StatusMessageBag)}
            );

            return responses;
        }

        public async Task NotifyPostSending(SendManifestPackageDTO sendTo,string version)
        {
            int maxRetries = 4;
            int retries = 0;
            var notificationend = new HandshakeEnd("CTSendEnd", version);
            DomainEvents.Dispatch(new DwhMessageNotification(false, $"Sending completed"));
            await _mediator.Publish(new HandshakeEnd("CTSendEnd", version));

            Thread.Sleep(3000);

            var client = Client ?? new HttpClient();

            while (retries < maxRetries)
            {
                try
                {
                    var session = _reader.GetSession(notificationend.EndName);
                    var response =
                        await client.PostAsync(
                            sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}Handshake?session={session}"), null);

                    if (!session.IsNullOrEmpty())
                    {
                        Log.Debug(new string('*',50));
                        Log.Debug("SUCCESS Sent Handshake");
                        Log.Debug(new string('*',50));
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Handshake Error");
                }
                retries++;
            }
        }
    }
}
