using Dwapi.ExtractsManagement.Core.DTOs;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Event.Dwh;
using Dwapi.UploadManagement.Core.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Services.Dwh;
using Dwapi.UploadManagement.Core.Notifications.Dwh;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Dwapi.SettingsManagement.Core.Model;
using Hangfire;

namespace Dwapi.UploadManagement.Core.Services.Dwh
{
    public class DwhSendService : IDwhSendService
    {
        private readonly IDwhExtractReader _reader;
        private readonly IDwhPackager _packager;
        private readonly IExtractStatusService _extractStatusService;
        private readonly IEmrSystemRepository _emrSystemRepository;
        private readonly string _endPoint;
        private const int MaxRetries = 3;
        private int _count;
        private int _artCount;
        private int _baselineCount;
        private int _labCount;
        private int _pharmacyCount;
        private int _statusCount;
        private int _visitCount;
        private int _adverseEventCount;
        private int _total;
        private const int Batch = 50;
        private readonly Extract _patientExtract;
        private readonly Extract _patientArtExtract;
        private readonly Extract _patientBaselineExtract;
        private readonly Extract _patientLabExtract;
        private readonly Extract _patientPharmacyExtract;
        private readonly Extract _patientStatusExtract;
        private readonly Extract _patientVisitExtract;
        private readonly Extract _patientAdverseEventExtract;
        private readonly ExtractEventDTO _patientExtractStatus;
        private readonly ExtractEventDTO _patientArtExtractStatus;
        private readonly ExtractEventDTO _patientBaselineExtractStatus;
        private readonly ExtractEventDTO _patientLabExtractStatus;
        private readonly ExtractEventDTO _patientPharmacyExtractStatus;
        private readonly ExtractEventDTO _patientStatusExtractStatus;
        private readonly ExtractEventDTO _patientVisitExtractStatus;
        private readonly ExtractEventDTO _patientAdverseEventExtractStatus;

        public HttpClient Client { get; set; }

        public DwhSendService(IDwhPackager packager, IDwhExtractReader reader, IExtractStatusService extractStatusService, IEmrSystemRepository emrSystemRepository)
        {
            _packager = packager;
            _reader = reader;
            _extractStatusService = extractStatusService;
            _emrSystemRepository = emrSystemRepository;
            _endPoint = "api/";
            var defaultEmr = _emrSystemRepository.GetDefault();
            var extracts = defaultEmr.Extracts;
            _patientExtract = extracts.FirstOrDefault(x => x.Name.Equals(nameof(PatientExtract)));
            if (_patientExtract != null) _patientExtractStatus = _extractStatusService.GetStatus(_patientExtract.Id);
            _patientArtExtract = extracts.FirstOrDefault(x => x.Name.Equals(nameof(PatientArtExtract)));
            if (_patientArtExtract != null)
                _patientArtExtractStatus = _extractStatusService.GetStatus(_patientArtExtract.Id);
            _patientBaselineExtract = extracts.FirstOrDefault(x => x.Name.Equals("PatientBaselineExtract"));
            if (_patientBaselineExtract != null)
                _patientBaselineExtractStatus = _extractStatusService.GetStatus(_patientBaselineExtract.Id);
            _patientLabExtract = extracts.FirstOrDefault(x => x.Name.Equals("PatientLabExtract"));
            if (_patientLabExtract != null)
                _patientLabExtractStatus = _extractStatusService.GetStatus(_patientLabExtract.Id);
            _patientPharmacyExtract = extracts.FirstOrDefault(x => x.Name.Equals(nameof(PatientPharmacyExtract)));
            if (_patientPharmacyExtract != null)
                _patientPharmacyExtractStatus = _extractStatusService.GetStatus(_patientPharmacyExtract.Id);
            _patientStatusExtract = extracts.FirstOrDefault(x => x.Name.Equals(nameof(PatientStatusExtract)));
            if (_patientStatusExtract != null)
                _patientStatusExtractStatus = _extractStatusService.GetStatus(_patientStatusExtract.Id);
            _patientVisitExtract = extracts.FirstOrDefault(x => x.Name.Equals(nameof(PatientVisitExtract)));
            if (_patientVisitExtract != null)
                _patientVisitExtractStatus = _extractStatusService.GetStatus(_patientVisitExtract.Id);
            _patientAdverseEventExtract = extracts.FirstOrDefault(x => x.Name.Equals(nameof(PatientAdverseEventExtract)));
            _patientAdverseEventExtractStatus = _extractStatusService.GetStatus(_patientAdverseEventExtract.Id);

        }
        public Task<List<SendDhwManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo)
        {
            return SendManifestAsync(sendTo, DwhManifestMessageBag.Create(_packager.GenerateWithMetrics(sendTo.GetEmrDto()).ToList()));
        }

        public Task<List<SendDhwManifestResponse>> SendDiffManifestAsync(SendManifestPackageDTO sendTo)
        {
            return SendManifestAsync(sendTo, DwhManifestMessageBag.Create(_packager.GenerateDiffWithMetrics(sendTo.GetEmrDto()).ToList()));
        }

        public async Task<List<SendDhwManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo, DwhManifestMessageBag messageBag)
        {
            var responses = new List<SendDhwManifestResponse>();
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
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}spot"), message.Manifest);
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

      public async Task<List<string>> SendExtractsAsync(SendManifestPackageDTO sendTo)
        {
            //hack to prevent hang fire from requeueing the job every 30 minutes
            if (_patientExtractStatus.LastStatus != nameof(ExtractStatus.Sending))
            {
                var responses = new List<Task<string>>();
                var output = new List<string>();
                HttpClientHandler handler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                };
                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                    client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(0,1,0);
                    var ids = _reader.ReadAllIds().ToList();
                    _total = ids.Count;
                    var sentPatients = new List<Guid>();
                    var sentPatientArts = new List<Guid>();
                    var sentPatientBaselines = new List<Guid>();
                    var sentPatientLabs = new List<Guid>();
                    var sentPatientPharmacies = new List<Guid>();
                    var sentPatientStatuses = new List<Guid>();
                    var sentPatientVisits = new List<Guid>();
                    var sentPatientAdverseEvents = new List<Guid>();

                    //update status to sending
                    UpdateUiNumbers(ExtractStatus.Sending);
                    //Show error message in UI
                    DomainEvents.Dispatch(new DwhMessageNotification(false, $"Sending started..."));

                    var httpResponseMessages = new List<Task<HttpResponseMessage>>();

                    foreach (var id in ids)
                    {
                        _count++;
                        var patient = _packager.GenerateExtracts(id);
                        var artMessageBag = ArtMessageBag.Create(patient);
                        var artMessage = artMessageBag.Message;
                        var baselineMessageBag = BaselineMessageBag.Create(patient);
                        var baselineMessage = baselineMessageBag.Message;
                        var labMessageBag = LabMessageBag.Create(patient);
                        var labMessage = labMessageBag.Message;
                        var pharmacyMessageBag = PharmacyMessageBag.Create(patient);
                        var pharmacyMessage = pharmacyMessageBag.Message;
                        var statusMessageBag = StatusMessageBag.Create(patient);
                        var statusMessage = statusMessageBag.Message;
                        var visitsMessageBag = VisitsMessageBag.Create(patient);
                        var visitsMessage = visitsMessageBag.Message;
                        var adverseEventsMessageBag = AdverseEventsMessageBag.Create(patient);
                        var adverseEventsMessage = adverseEventsMessageBag.Message;

                        if (artMessage.HasContents)
                        {
                            try
                            {
                                var response = client.PostAsCompressedAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{artMessageBag.EndPoint}"), artMessage);
                                httpResponseMessages.Add(response);
                                _artCount += artMessage.Extracts.Count;
                                sentPatientArts.AddRange(artMessage.Extracts.Select(x => x.Id).ToList());

                            }
                            catch (Exception e)
                            {
                                //Show error message in UI
                                DomainEvents.Dispatch(new DwhMessageNotification(true, $"Error sending {ExtractType.PatientArt.ToString()}s for patient id {id}"));
                                DomainEvents.Dispatch(new DwhExtractSentEvent(ExtractType.PatientArt, artMessage.Extracts.Select(x => x.Id).ToList(), SendStatus.Failed, e.Message));
                                Log.Error(e, $"Send Error");
                                PrintMessage(artMessage);
                                throw;
                            }
                        }

                        if(baselineMessage.HasContents)
                        {
                            try
                            {
                                var response = client.PostAsCompressedAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{baselineMessageBag.EndPoint}"), baselineMessage);
                                httpResponseMessages.Add(response);
                                _baselineCount += baselineMessage.Extracts.Count;
                                sentPatientBaselines.AddRange(baselineMessage.Extracts.Select(x => x.Id).ToList());

                            }
                            catch (Exception e)
                            {
                                //Show error message in UI
                                DomainEvents.Dispatch(new DwhMessageNotification(true, $"Error sending {ExtractType.PatientBaseline.ToString()}s for patient id {id}"));
                                DomainEvents.Dispatch(new DwhExtractSentEvent(ExtractType.PatientBaseline, baselineMessage.Extracts.Select(x => x.Id).ToList(), SendStatus.Failed, e.Message));
                                Log.Error(e, $"Send Error");
                                PrintMessage(baselineMessage);
                                throw;
                            }
                        }

                        if (labMessage.HasContents)
                        {
                            try
                            {
                                var response = client.PostAsCompressedAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{labMessageBag.EndPoint}"), labMessage);
                                httpResponseMessages.Add(response);
                                _labCount += labMessage.Extracts.Count;
                                sentPatientLabs.AddRange(labMessage.Extracts.Select(x => x.Id).ToList());
                            }
                            catch (Exception e)
                            {
                                //Show error message in UI
                                DomainEvents.Dispatch(new DwhMessageNotification(true, $"Error sending {ExtractType.PatientLab.ToString()}s for patient id {id}"));
                                DomainEvents.Dispatch(new DwhExtractSentEvent(ExtractType.PatientLab, labMessage.Extracts.Select(x => x.Id).ToList(), SendStatus.Failed, e.Message));
                                Log.Error(e, $"Send Error");
                                PrintMessage(labMessage);
                                throw;
                            }
                        }

                        if (pharmacyMessage.HasContents)
                        {
                            try
                            {
                                var response = client.PostAsCompressedAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{pharmacyMessageBag.EndPoint}"), pharmacyMessage);
                                httpResponseMessages.Add(response);
                                _pharmacyCount += pharmacyMessage.Extracts.Count;
                                sentPatientPharmacies.AddRange(pharmacyMessage.Extracts.Select(x => x.Id).ToList());
                            }
                            catch (Exception e)
                            {
                                //Show error message in UI
                                DomainEvents.Dispatch(new DwhMessageNotification(true, $"Error sending {ExtractType.PatientPharmacy.ToString()}s for patient id {id}"));
                                DomainEvents.Dispatch(new DwhExtractSentEvent(ExtractType.PatientPharmacy, pharmacyMessage.Extracts.Select(x => x.Id).ToList(), SendStatus.Failed, e.Message));
                                Log.Error(e, $"Send Error");
                                PrintMessage(pharmacyMessage);
                                throw;
                            }
                        }

                        if (statusMessage.HasContents)
                        {
                            try
                            {
                                var response = client.PostAsCompressedAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{statusMessageBag.EndPoint}"), statusMessage);
                                httpResponseMessages.Add(response);
                                _statusCount += statusMessage.Extracts.Count;
                                sentPatientStatuses.AddRange(statusMessage.Extracts.Select(x => x.Id).ToList());

                            }
                            catch (Exception e)
                            {
                                //Show error message in UI
                                DomainEvents.Dispatch(new DwhMessageNotification(true, $"Error sending {ExtractType.PatientStatus.ToString()}es for patient id {id}"));
                                Log.Error(e, $"Send Error");
                                DomainEvents.Dispatch(new DwhExtractSentEvent(ExtractType.PatientStatus, statusMessage.Extracts.Select(x => x.Id).ToList(), SendStatus.Failed, e.Message));
                                PrintMessage(statusMessage);
                                throw;
                            }
                        }

                        if (visitsMessage.HasContents)
                        {
                            try
                            {
                                var response = client.PostAsCompressedAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{visitsMessageBag.EndPoint}"), visitsMessage);
                                httpResponseMessages.Add(response);
                                _visitCount += visitsMessage.Extracts.Count;
                                sentPatientVisits.AddRange(visitsMessage.Extracts.Select(x => x.Id).ToList());

                            }
                            catch (Exception e)
                            {
                                //Show error message in UI
                                DomainEvents.Dispatch(new DwhMessageNotification(true, $"Error sending {ExtractType.PatientVisit.ToString()}s for patient id {id}"));
                                Log.Error(e, $"Send Error");
                                DomainEvents.Dispatch(new DwhExtractSentEvent(ExtractType.PatientVisit, visitsMessage.Extracts.Select(x => x.Id).ToList(), SendStatus.Sent, e.Message));
                                PrintMessage(visitsMessage);
                                throw;
                            }
                        }

                        if (adverseEventsMessage.HasContents)
                        {
                            try
                            {
                                var response = client.PostAsCompressedAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{adverseEventsMessageBag.EndPoint}"), adverseEventsMessage);
                                httpResponseMessages.Add(response);
                                _adverseEventCount += adverseEventsMessage.Extracts.Count;
                                sentPatientAdverseEvents.AddRange(adverseEventsMessage.Extracts.Select(x => x.Id).ToList());
                            }
                            catch (Exception e)
                            {
                                //Show error message in UI
                                DomainEvents.Dispatch(new DwhMessageNotification(true, $"Error sending {ExtractType.PatientAdverseEvent.ToString()}s for patient id {id}"));
                                Log.Error(e, $"Send Error");
                                DomainEvents.Dispatch(new DwhExtractSentEvent(ExtractType.PatientAdverseEvent, adverseEventsMessage.Extracts.Select(x => x.Id).ToList(), SendStatus.Sent, e.Message));
                                PrintMessage(adverseEventsMessage);
                                throw;
                            }
                        }

                        //update UI in set number of batches
                        if (_count % Batch == 0)
                        {
                            foreach (var httpResponseMessage in httpResponseMessages)
                            {
                                var response = await httpResponseMessage;
                                if (response.IsSuccessStatusCode)
                                {
                                    var content = response.Content.ReadAsStringAsync();
                                    responses.Add(content);
                                    sentPatients.Add(id);
                                }
                                //Retry failed requests for set number of retries
                                else
                                {
                                    int retry = 0;
                                    for (int i = 0; i < MaxRetries; i++)
                                    {
                                        retry = i;
                                        var r = await client.PostAsCompressedAsync(sendTo.GetUrl($"{response.RequestMessage.RequestUri}"), response.RequestMessage.Content);
                                        if (r.IsSuccessStatusCode)
                                        {
                                            var content = response.Content.ReadAsStringAsync();
                                            responses.Add(content);
                                            break;
                                        }
                                    }
                                    //if all retries fail throw error
                                    if (retry == 3)
                                    {
                                        //Show error message in UI
                                        DomainEvents.Dispatch(new DwhMessageNotification(true, $"Error sending Extracts for patient id {id}"));
                                        var error = await response.Content.ReadAsStringAsync();
                                        Log.Error(error, $"Host Response Error");
                                        throw new Exception(error);
                                    }
                                }
                            }
                            httpResponseMessages.Clear();
                            UpdateUiNumbers(ExtractStatus.Sending);
                        }

                    }
                    //update extract sent field
                    BackgroundJob.Enqueue(() => UpdateExtractSent(ExtractType.Patient, sentPatients));
                    BackgroundJob.Enqueue(() => UpdateExtractSent(ExtractType.PatientArt, sentPatientArts));
                    BackgroundJob.Enqueue(() => UpdateExtractSent(ExtractType.PatientBaseline, sentPatientBaselines));
                    BackgroundJob.Enqueue(() => UpdateExtractSent(ExtractType.PatientLab, sentPatientLabs));
                    BackgroundJob.Enqueue(() => UpdateExtractSent(ExtractType.PatientPharmacy, sentPatientPharmacies));
                    BackgroundJob.Enqueue(() => UpdateExtractSent(ExtractType.PatientStatus, sentPatientStatuses));
                    BackgroundJob.Enqueue(() => UpdateExtractSent(ExtractType.PatientVisit, sentPatientVisits));
                    BackgroundJob.Enqueue(() => UpdateExtractSent(ExtractType.PatientAdverseEvent, sentPatientAdverseEvents));
                    UpdateUiNumbers(ExtractStatus.Sent);
                    //Show error message in UI
                    DomainEvents.Dispatch(new DwhMessageNotification(false, $"Extracts sent successfully!"));
                }

                foreach (var r in responses)
                {
                    var response = await r;
                    output.Add(response);
                }
                return output;
            }
            //when hang fire tries to re-queue the job after thirty minutes or when sending was interrupted midway
            return null;
        }

        public void UpdateExtractSent(ExtractType extractType, List<Guid> sentIds)
        {
            DomainEvents.Dispatch(new DwhExtractSentEvent(extractType, sentIds, SendStatus.Sent));
        }

        public void UpdateUiNumbers(ExtractStatus status)
        {
            //update progress bar
            DomainEvents.Dispatch(new DwhSendNotification(new SendProgress(nameof(PatientExtract), Common.GetProgress(_count, _total),0)));
            //update Patients
            if (_patientExtractStatus.Found != null)
                if (_patientExtractStatus.Loaded != null)
                    if (_patientExtractStatus.Rejected != null)
                        DomainEvents.Dispatch(new ExtractActivityNotification(_patientExtract.Id,
                            new DwhProgress(nameof(PatientExtract), status.ToString(),
                                (int)_patientExtractStatus.Found,
                                (int)_patientExtractStatus.Loaded, (int)_patientExtractStatus.Rejected,
                                (int)_patientExtractStatus.Loaded,
                                _count)));
            //update Patient ARTs
            if (_patientArtExtractStatus.Found != null)
                if (_patientArtExtractStatus.Loaded != null)
                    if (_patientArtExtractStatus.Rejected != null)
                        DomainEvents.Dispatch(new ExtractActivityNotification(_patientArtExtract.Id,
                            new DwhProgress(nameof(PatientExtract), status.ToString(),
                                (int)_patientArtExtractStatus.Found,
                                (int)_patientArtExtractStatus.Loaded, (int)_patientArtExtractStatus.Rejected,
                                (int)_patientArtExtractStatus.Loaded,
                                _artCount)));
            //update Patient Baselines
            if (_patientBaselineExtractStatus.Found != null)
                if (_patientBaselineExtractStatus.Loaded != null)
                    if (_patientBaselineExtractStatus.Rejected != null)
                        DomainEvents.Dispatch(new ExtractActivityNotification(_patientBaselineExtract.Id,
                            new DwhProgress(nameof(PatientExtract), status.ToString(),
                                (int)_patientBaselineExtractStatus.Found,
                                (int)_patientBaselineExtractStatus.Loaded, (int)_patientBaselineExtractStatus.Rejected,
                                (int)_patientBaselineExtractStatus.Loaded,
                                _baselineCount)));
            //update Patient Labs
            if (_patientLabExtractStatus.Found != null)
                if (_patientLabExtractStatus.Loaded != null)
                    if (_patientLabExtractStatus.Rejected != null)
                        DomainEvents.Dispatch(new ExtractActivityNotification(_patientLabExtract.Id,
                            new DwhProgress(nameof(PatientExtract), status.ToString(),
                                (int)_patientLabExtractStatus.Found,
                                (int)_patientLabExtractStatus.Loaded, (int)_patientLabExtractStatus.Rejected,
                                (int)_patientLabExtractStatus.Loaded,
                                _labCount)));
            //update Patient Pharmacies
            if (_patientPharmacyExtractStatus.Found != null)
                if (_patientPharmacyExtractStatus.Loaded != null)
                    if (_patientPharmacyExtractStatus.Rejected != null)
                        DomainEvents.Dispatch(new ExtractActivityNotification(_patientPharmacyExtract.Id,
                            new DwhProgress(nameof(PatientExtract), status.ToString(),
                                (int)_patientPharmacyExtractStatus.Found,
                                (int)_patientPharmacyExtractStatus.Loaded, (int)_patientPharmacyExtractStatus.Rejected,
                                (int)_patientPharmacyExtractStatus.Loaded,
                                _pharmacyCount)));
            //update Patient Statuses
            if (_patientStatusExtractStatus.Found != null)
                if (_patientStatusExtractStatus.Loaded != null)
                    if (_patientStatusExtractStatus.Rejected != null)
                        DomainEvents.Dispatch(new ExtractActivityNotification(_patientStatusExtract.Id,
                            new DwhProgress(nameof(PatientExtract), status.ToString(),
                                (int)_patientStatusExtractStatus.Found,
                                (int)_patientStatusExtractStatus.Loaded, (int)_patientStatusExtractStatus.Rejected,
                                (int)_patientStatusExtractStatus.Loaded,
                                _statusCount)));
            //update Patient Visits
            if (_patientVisitExtractStatus.Found != null)
                if (_patientVisitExtractStatus.Loaded != null)
                    if (_patientVisitExtractStatus.Rejected != null)
                        DomainEvents.Dispatch(new ExtractActivityNotification(_patientVisitExtract.Id,
                            new DwhProgress(nameof(PatientExtract), status.ToString(),
                                (int)_patientVisitExtractStatus.Found,
                                (int)_patientVisitExtractStatus.Loaded, (int)_patientVisitExtractStatus.Rejected,
                                (int)_patientVisitExtractStatus.Loaded,
                                _visitCount)));
            //update Patient Adverse Events
            if (_patientAdverseEventExtractStatus.Found != null)
                if (_patientAdverseEventExtractStatus.Loaded != null)
                    if (_patientAdverseEventExtractStatus.Rejected != null)
                        DomainEvents.Dispatch(new ExtractActivityNotification(_patientAdverseEventExtract.Id,
                            new DwhProgress(nameof(PatientExtract), status.ToString(),
                                (int)_patientAdverseEventExtractStatus.Found,
                                (int)_patientAdverseEventExtractStatus.Loaded, (int)_patientAdverseEventExtractStatus.Rejected,
                                (int)_patientAdverseEventExtractStatus.Loaded,
                                _adverseEventCount)));
        }

        private void PrintMessage(object message)
        {
            try
            {
                Log.Debug(new string('+', 40));
                Log.Debug(JsonConvert.SerializeObject(message, Formatting.Indented));
                Log.Debug(new string('+', 40));
            }
            catch
            {
                // ignored
            }
        }
    }
}
