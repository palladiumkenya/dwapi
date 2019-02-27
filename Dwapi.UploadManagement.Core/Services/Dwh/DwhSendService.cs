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
            _patientExtractStatus = _extractStatusService.GetStatus(_patientExtract.Id);
            _patientArtExtract = extracts.FirstOrDefault(x => x.Name.Equals(nameof(PatientArtExtract)));
            _patientArtExtractStatus = _extractStatusService.GetStatus(_patientArtExtract.Id);
            _patientBaselineExtract = extracts.FirstOrDefault(x => x.Name.Equals("PatientBaselineExtract"));
            _patientBaselineExtractStatus = _extractStatusService.GetStatus(_patientBaselineExtract.Id);
            _patientLabExtract = extracts.FirstOrDefault(x => x.Name.Equals("PatientLabExtract"));
            _patientLabExtractStatus = _extractStatusService.GetStatus(_patientLabExtract.Id);
            _patientPharmacyExtract = extracts.FirstOrDefault(x => x.Name.Equals(nameof(PatientPharmacyExtract)));
            _patientPharmacyExtractStatus = _extractStatusService.GetStatus(_patientPharmacyExtract.Id);
            _patientStatusExtract = extracts.FirstOrDefault(x => x.Name.Equals(nameof(PatientStatusExtract)));
            _patientStatusExtractStatus = _extractStatusService.GetStatus(_patientStatusExtract.Id);
            _patientVisitExtract = extracts.FirstOrDefault(x => x.Name.Equals(nameof(PatientVisitExtract)));
            _patientVisitExtractStatus = _extractStatusService.GetStatus(_patientVisitExtract.Id);
            _patientAdverseEventExtract = extracts.FirstOrDefault(x => x.Name.Equals(nameof(PatientAdverseEventExtract)));
            _patientAdverseEventExtractStatus = _extractStatusService.GetStatus(_patientAdverseEventExtract.Id);

        }

        public Task<List<SendDhwManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo)
        {
            return SendManifestAsync(sendTo, DwhManifestMessageBag.Create(_packager.GenerateWithMetrics().ToList()));
        }

        public async Task<List<SendDhwManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo, DwhManifestMessageBag messageBag)
        {
            var responses = new List<SendDhwManifestResponse>();
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            foreach (var message in messageBag.Messages)
            {
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
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
                var extractSendingStatuses = new List<ExtractSentEventDto>
                {
                    new ExtractSentEventDto(_patientExtract.Id, _patientExtractStatus, ExtractType.Patient, _count),
                    new ExtractSentEventDto(_patientArtExtract.Id, _patientArtExtractStatus, ExtractType.PatientArt,
                        _artCount),
                    new ExtractSentEventDto(_patientBaselineExtract.Id, _patientBaselineExtractStatus,
                        ExtractType.PatientBaseline, _baselineCount),
                    new ExtractSentEventDto(_patientLabExtract.Id, _patientLabExtractStatus, ExtractType.PatientLab,
                        _labCount),
                    new ExtractSentEventDto(_patientPharmacyExtract.Id, _patientPharmacyExtractStatus,
                        ExtractType.PatientPharmacy, _pharmacyCount),
                    new ExtractSentEventDto(_patientStatusExtract.Id, _patientStatusExtractStatus, ExtractType.PatientStatus,
                        _statusCount),
                    new ExtractSentEventDto(_patientVisitExtract.Id, _patientVisitExtractStatus, ExtractType.PatientVisit,
                        _visitCount),
                    new ExtractSentEventDto(_patientAdverseEventExtract.Id, _patientAdverseEventExtractStatus, ExtractType.PatientAdverseEvent,
                        _visitCount)
                };
                UpdateStatusNotification(extractSendingStatuses, ExtractStatus.Sending);
                

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
                            var response = client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{artMessageBag.EndPoint}"), artMessage);
                            httpResponseMessages.Add(response);
                            _artCount += artMessage.ArtExtracts.Count;
                            sentPatientArts.AddRange(artMessage.ArtExtracts.Select(x => x.Id).ToList());

                        }
                        catch (Exception e)
                        {
                            DomainEvents.Dispatch(new DwhExtractSentEvent(ExtractType.PatientArt, artMessage.ArtExtracts.Select(x => x.Id).ToList(), SendStatus.Failed, e.Message));
                            Log.Error(e, $"Send Error");
                            PrintMessage(artMessage);
                            throw;
                        }
                    }

                    if(baselineMessage.HasContents)
                    {
                        try
                        {
                            var response = client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{baselineMessageBag.EndPoint}"), baselineMessage);
                            httpResponseMessages.Add(response);
                            _baselineCount += baselineMessage.BaselinesExtracts.Count;
                            sentPatientBaselines.AddRange(baselineMessage.BaselinesExtracts.Select(x => x.Id).ToList());
                            
                        }
                        catch (Exception e)
                        {
                            DomainEvents.Dispatch(new DwhExtractSentEvent(ExtractType.PatientBaseline, baselineMessage.BaselinesExtracts.Select(x => x.Id).ToList(), SendStatus.Failed, e.Message));
                            Log.Error(e, $"Send Error");
                            PrintMessage(baselineMessage);
                            throw;
                        }
                    }

                    if (labMessage.HasContents)
                    {
                        try
                        {
                            var response = client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{labMessageBag.EndPoint}"), labMessage);
                            httpResponseMessages.Add(response);
                            _labCount += labMessage.LaboratoryExtracts.Count;
                            sentPatientLabs.AddRange(labMessage.LaboratoryExtracts.Select(x => x.Id).ToList());
                        }
                        catch (Exception e)
                        {
                            DomainEvents.Dispatch(new DwhExtractSentEvent(ExtractType.PatientLab, labMessage.LaboratoryExtracts.Select(x => x.Id).ToList(), SendStatus.Failed, e.Message));
                            Log.Error(e, $"Send Error");
                            PrintMessage(labMessage);
                            throw;
                        }
                    }

                    if (pharmacyMessage.HasContents)
                    {
                        try
                        {
                            var response = client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{pharmacyMessageBag.EndPoint}"), pharmacyMessage);
                            httpResponseMessages.Add(response);
                            _pharmacyCount += pharmacyMessage.PharmacyExtracts.Count;
                            sentPatientPharmacies.AddRange(pharmacyMessage.PharmacyExtracts.Select(x => x.Id).ToList());
                        }
                        catch (Exception e)
                        {
                            DomainEvents.Dispatch(new DwhExtractSentEvent(ExtractType.PatientPharmacy, pharmacyMessage.PharmacyExtracts.Select(x => x.Id).ToList(), SendStatus.Failed, e.Message));
                            Log.Error(e, $"Send Error");
                            PrintMessage(pharmacyMessage);
                            throw;
                        }
                    }

                    if (statusMessage.HasContents)
                    {
                        try
                        {
                            var response = client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{statusMessageBag.EndPoint}"), statusMessage);
                            httpResponseMessages.Add(response);
                            _statusCount += statusMessage.StatusExtracts.Count;
                            sentPatientStatuses.AddRange(statusMessage.StatusExtracts.Select(x => x.Id).ToList());

                        }
                        catch (Exception e)
                        {
                            Log.Error(e, $"Send Error");
                            DomainEvents.Dispatch(new DwhExtractSentEvent(ExtractType.PatientStatus, statusMessage.StatusExtracts.Select(x => x.Id).ToList(), SendStatus.Failed, e.Message));
                            PrintMessage(statusMessage);
                            throw;
                        }
                    }

                    if (visitsMessage.HasContents)
                    {
                        try
                        {
                            var response = client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{visitsMessageBag.EndPoint}"), visitsMessage);
                            httpResponseMessages.Add(response);
                            _visitCount += visitsMessage.VisitExtracts.Count;
                            sentPatientVisits.AddRange(visitsMessage.VisitExtracts.Select(x => x.Id).ToList());

                        }
                        catch (Exception e)
                        {
                            Log.Error(e, $"Send Error");
                            DomainEvents.Dispatch(new DwhExtractSentEvent(ExtractType.PatientVisit, visitsMessage.VisitExtracts.Select(x => x.Id).ToList(), SendStatus.Sent, e.Message));
                            PrintMessage(visitsMessage);
                            throw;
                        }
                    }

                    if (adverseEventsMessage.HasContents)
                    {
                        try
                        {
                            var response = client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{adverseEventsMessageBag.EndPoint}"), adverseEventsMessage);
                            httpResponseMessages.Add(response);
                            _adverseEventCount += adverseEventsMessage.AdverseEventExtracts.Count;
                            sentPatientAdverseEvents.AddRange(adverseEventsMessage.AdverseEventExtracts.Select(x => x.Id).ToList());
                        }
                        catch (Exception e)
                        {
                            Log.Error(e, $"Send Error");
                            DomainEvents.Dispatch(new DwhExtractSentEvent(ExtractType.PatientAdverseEvent, adverseEventsMessage.AdverseEventExtracts.Select(x => x.Id).ToList(), SendStatus.Sent, e.Message));
                            PrintMessage(adverseEventsMessage);
                            throw;
                        }
                    }
                    
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
                                var r = await client.PostAsJsonAsync(sendTo.GetUrl($"{response.RequestMessage.RequestUri}"), response.RequestMessage.Content);
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
                                var error = await response.Content.ReadAsStringAsync();
                                Log.Error(error, $"Host Response Error");
                                throw new Exception(error);
                            }
                        }
                    }

                    //start of the update UI process 
                    if (_count == 1)
                    {
                        UpdateUiNumbers();
                    }
                    //update UI in set number of batches
                    if (_count % Batch == 0)
                    {
                        UpdateUiNumbers();
                    }

                    httpResponseMessages.Clear();
                }
                //update UI for the remainder of the batch
                UpdateUiNumbers();

                //update extract sent field
                var b = BackgroundJob.Enqueue(() => UpdateExtractSent(ExtractType.Patient, sentPatients));
                var b1 = BackgroundJob.ContinueWith(b, () => UpdateExtractSent(ExtractType.PatientArt, sentPatientArts));
                var b2 = BackgroundJob.ContinueWith(b1, () => UpdateExtractSent(ExtractType.PatientBaseline, sentPatientBaselines));
                var b3 = BackgroundJob.ContinueWith(b2, () => UpdateExtractSent(ExtractType.PatientLab, sentPatientLabs));
                var b4 = BackgroundJob.ContinueWith(b3, () => UpdateExtractSent(ExtractType.PatientPharmacy, sentPatientPharmacies));
                var b5 = BackgroundJob.ContinueWith(b4, () => UpdateExtractSent(ExtractType.PatientStatus, sentPatientStatuses));
                var b6 = BackgroundJob.ContinueWith(b5, () => UpdateExtractSent(ExtractType.PatientVisit, sentPatientVisits));
                BackgroundJob.ContinueWith(b6, () => UpdateExtractSent(ExtractType.PatientAdverseEvent, sentPatientAdverseEvents));

                // update sent status notification
                var extractsStatuses = new List<ExtractSentEventDto>();
                var patientStatus = new ExtractSentEventDto(_patientExtract.Id, _patientExtractStatus, ExtractType.Patient, _count);
                extractsStatuses.Add(patientStatus);
                var patientArtStatus = new ExtractSentEventDto(_patientArtExtract.Id, _patientArtExtractStatus, ExtractType.PatientArt, _artCount);
                extractsStatuses.Add(patientArtStatus);
                var patientBaselineStatus = new ExtractSentEventDto(_patientBaselineExtract.Id, _patientBaselineExtractStatus, ExtractType.PatientBaseline, _baselineCount);
                extractsStatuses.Add(patientBaselineStatus);
                var patientLabStatus = new ExtractSentEventDto(_patientLabExtract.Id, _patientLabExtractStatus, ExtractType.PatientLab, _labCount);
                extractsStatuses.Add(patientLabStatus);
                var patientPharmacyStatus = new ExtractSentEventDto(_patientPharmacyExtract.Id, _patientPharmacyExtractStatus, ExtractType.PatientPharmacy, _pharmacyCount);
                extractsStatuses.Add(patientPharmacyStatus);
                var patientStatusStatus = new ExtractSentEventDto(_patientStatusExtract.Id, _patientStatusExtractStatus, ExtractType.PatientStatus, _statusCount);
                extractsStatuses.Add(patientStatusStatus);
                var patientVisitStatus = new ExtractSentEventDto(_patientVisitExtract.Id, _patientVisitExtractStatus, ExtractType.PatientVisit, _visitCount);
                extractsStatuses.Add(patientVisitStatus);
                var patientAdverseEventStatus = new ExtractSentEventDto(_patientAdverseEventExtract.Id, _patientAdverseEventExtractStatus, ExtractType.PatientAdverseEvent, _adverseEventCount);
                extractsStatuses.Add(patientAdverseEventStatus);
                UpdateStatusNotification(extractsStatuses, ExtractStatus.Sent);
            }

            
            foreach (var r in responses)
            {
                var response = await r;
                output.Add(response);
            }
            
            return output;
        }

        private void UpdateStatusNotification(List<ExtractSentEventDto> extractStatuses, ExtractStatus status)
        {
            string statusString = "";
            if (status == ExtractStatus.Sent)
            {
                statusString = nameof(ExtractStatus.Sent);
            }
            else if (status == ExtractStatus.Sending)
            {
                statusString = nameof(ExtractStatus.Sending);
            }

            foreach (var extractStatus in extractStatuses)
            {
                switch (extractStatus.ExtractType)
                {
                    case ExtractType.Patient:
                        if (extractStatus.ExtractEvent.Found != null)
                            if (extractStatus.ExtractEvent.Loaded != null)
                                if (extractStatus.ExtractEvent.Rejected != null)
                                    DomainEvents.Dispatch(new ExtractActivityNotification(extractStatus.ExtractId,
                                        new DwhProgress(nameof(PatientExtract), statusString,
                                            (int)extractStatus.ExtractEvent.Found,
                                            (int)extractStatus.ExtractEvent.Loaded,
                                            (int)extractStatus.ExtractEvent.Rejected,
                                            (int)extractStatus.ExtractEvent.Loaded,
                                            extractStatus.Sent)));
                        break;

                    case ExtractType.PatientArt:
                        if (extractStatus.ExtractEvent.Found != null)
                            if (extractStatus.ExtractEvent.Loaded != null)
                                if (extractStatus.ExtractEvent.Rejected != null)
                                    DomainEvents.Dispatch(new ExtractActivityNotification(extractStatus.ExtractId,
                                        new DwhProgress(nameof(PatientArtExtract), statusString,
                                            (int)extractStatus.ExtractEvent.Found,
                                            (int)extractStatus.ExtractEvent.Loaded,
                                            (int)extractStatus.ExtractEvent.Rejected,
                                            (int)extractStatus.ExtractEvent.Loaded,
                                            extractStatus.Sent)));
                        break;

                    case ExtractType.PatientBaseline:
                        if (extractStatus.ExtractEvent.Found != null)
                            if (extractStatus.ExtractEvent.Loaded != null)
                                if (extractStatus.ExtractEvent.Rejected != null)
                                    DomainEvents.Dispatch(new ExtractActivityNotification(extractStatus.ExtractId,
                                        new DwhProgress("PatientBaselineExtract", statusString,
                                            (int)extractStatus.ExtractEvent.Found,
                                            (int)extractStatus.ExtractEvent.Loaded,
                                            (int)extractStatus.ExtractEvent.Rejected,
                                            (int)extractStatus.ExtractEvent.Loaded,
                                            extractStatus.Sent)));
                        break;

                    case ExtractType.PatientLab:
                        if (extractStatus.ExtractEvent.Found != null)
                            if (extractStatus.ExtractEvent.Loaded != null)
                                if (extractStatus.ExtractEvent.Rejected != null)
                                    DomainEvents.Dispatch(new ExtractActivityNotification(extractStatus.ExtractId,
                                        new DwhProgress("PatientLabExtract", statusString,
                                            (int)extractStatus.ExtractEvent.Found,
                                            (int)extractStatus.ExtractEvent.Loaded,
                                            (int)extractStatus.ExtractEvent.Rejected,
                                            (int)extractStatus.ExtractEvent.Loaded,
                                            extractStatus.Sent)));
                        break;

                    case ExtractType.PatientPharmacy:
                        if (extractStatus.ExtractEvent.Found != null)
                            if (extractStatus.ExtractEvent.Loaded != null)
                                if (extractStatus.ExtractEvent.Rejected != null)
                                    DomainEvents.Dispatch(new ExtractActivityNotification(extractStatus.ExtractId,
                                        new DwhProgress(nameof(PatientPharmacyExtract), statusString,
                                            (int)extractStatus.ExtractEvent.Found,
                                            (int)extractStatus.ExtractEvent.Loaded,
                                            (int)extractStatus.ExtractEvent.Rejected,
                                            (int)extractStatus.ExtractEvent.Loaded,
                                            extractStatus.Sent)));
                        break;

                    case ExtractType.PatientStatus:
                        if (extractStatus.ExtractEvent.Found != null)
                            if (extractStatus.ExtractEvent.Loaded != null)
                                if (extractStatus.ExtractEvent.Rejected != null)
                                    DomainEvents.Dispatch(new ExtractActivityNotification(extractStatus.ExtractId,
                                        new DwhProgress(nameof(PatientStatusExtract), statusString,
                                            (int)extractStatus.ExtractEvent.Found,
                                            (int)extractStatus.ExtractEvent.Loaded,
                                            (int)extractStatus.ExtractEvent.Rejected,
                                            (int)extractStatus.ExtractEvent.Loaded,
                                            extractStatus.Sent)));
                        break;

                    case ExtractType.PatientVisit:
                        if (extractStatus.ExtractEvent.Found != null)
                            if (extractStatus.ExtractEvent.Loaded != null)
                                if (extractStatus.ExtractEvent.Rejected != null)
                                    DomainEvents.Dispatch(new ExtractActivityNotification(extractStatus.ExtractId,
                                        new DwhProgress(nameof(PatientVisitExtract), statusString,
                                            (int)extractStatus.ExtractEvent.Found,
                                            (int)extractStatus.ExtractEvent.Loaded,
                                            (int)extractStatus.ExtractEvent.Rejected,
                                            (int)extractStatus.ExtractEvent.Loaded,
                                            extractStatus.Sent)));
                        break;
                    case ExtractType.PatientAdverseEvent:
                        if (extractStatus.ExtractEvent.Found != null)
                            if (extractStatus.ExtractEvent.Loaded != null)
                                if (extractStatus.ExtractEvent.Rejected != null)
                                    DomainEvents.Dispatch(new ExtractActivityNotification(extractStatus.ExtractId,
                                        new DwhProgress(nameof(PatientAdverseEventExtract), statusString,
                                            (int)extractStatus.ExtractEvent.Found,
                                            (int)extractStatus.ExtractEvent.Loaded,
                                            (int)extractStatus.ExtractEvent.Rejected,
                                            (int)extractStatus.ExtractEvent.Loaded,
                                            extractStatus.Sent)));
                        break;
                }
            }
        }

        public void UpdateExtractSent(ExtractType extractType, List<Guid> sentIds)
        {
            DomainEvents.Dispatch(new DwhExtractSentEvent(extractType, sentIds, SendStatus.Sent));
        }

        public void UpdateUiNumbers()
        {
            //update progress bar
            DomainEvents.Dispatch(new DwhSendNotification(new SendProgress(nameof(PatientExtract), Common.GetProgress(_count, _total))));
            //update Patients
            if (_patientExtractStatus.Found != null)
                if (_patientExtractStatus.Loaded != null)
                    if (_patientExtractStatus.Rejected != null)
                        DomainEvents.Dispatch(new ExtractActivityNotification(_patientExtract.Id,
                            new DwhProgress(nameof(PatientExtract), nameof(ExtractStatus.Sending),
                                (int)_patientExtractStatus.Found,
                                (int)_patientExtractStatus.Loaded, (int)_patientExtractStatus.Rejected,
                                (int)_patientExtractStatus.Loaded,
                                _count)));
            //update Patient ARTs
            if (_patientArtExtractStatus.Found != null)
                if (_patientArtExtractStatus.Loaded != null)
                    if (_patientArtExtractStatus.Rejected != null)
                        DomainEvents.Dispatch(new ExtractActivityNotification(_patientArtExtract.Id,
                            new DwhProgress(nameof(PatientExtract), nameof(ExtractStatus.Sending),
                                (int)_patientArtExtractStatus.Found,
                                (int)_patientArtExtractStatus.Loaded, (int)_patientArtExtractStatus.Rejected,
                                (int)_patientArtExtractStatus.Loaded,
                                _artCount)));
            //update Patient Baselines
            if (_patientBaselineExtractStatus.Found != null)
                if (_patientBaselineExtractStatus.Loaded != null)
                    if (_patientBaselineExtractStatus.Rejected != null)
                        DomainEvents.Dispatch(new ExtractActivityNotification(_patientBaselineExtract.Id,
                            new DwhProgress(nameof(PatientExtract), nameof(ExtractStatus.Sending),
                                (int)_patientBaselineExtractStatus.Found,
                                (int)_patientBaselineExtractStatus.Loaded, (int)_patientBaselineExtractStatus.Rejected,
                                (int)_patientBaselineExtractStatus.Loaded,
                                _baselineCount)));
            //update Patient Labs
            if (_patientLabExtractStatus.Found != null)
                if (_patientLabExtractStatus.Loaded != null)
                    if (_patientLabExtractStatus.Rejected != null)
                        DomainEvents.Dispatch(new ExtractActivityNotification(_patientLabExtract.Id,
                            new DwhProgress(nameof(PatientExtract), nameof(ExtractStatus.Sending),
                                (int)_patientLabExtractStatus.Found,
                                (int)_patientLabExtractStatus.Loaded, (int)_patientLabExtractStatus.Rejected,
                                (int)_patientLabExtractStatus.Loaded,
                                _labCount)));
            //update Patient Pharmacies
            if (_patientPharmacyExtractStatus.Found != null)
                if (_patientPharmacyExtractStatus.Loaded != null)
                    if (_patientPharmacyExtractStatus.Rejected != null)
                        DomainEvents.Dispatch(new ExtractActivityNotification(_patientPharmacyExtract.Id,
                            new DwhProgress(nameof(PatientExtract), nameof(ExtractStatus.Sending),
                                (int)_patientPharmacyExtractStatus.Found,
                                (int)_patientPharmacyExtractStatus.Loaded, (int)_patientPharmacyExtractStatus.Rejected,
                                (int)_patientPharmacyExtractStatus.Loaded,
                                _pharmacyCount)));
            //update Patient Statuses
            if (_patientStatusExtractStatus.Found != null)
                if (_patientStatusExtractStatus.Loaded != null)
                    if (_patientStatusExtractStatus.Rejected != null)
                        DomainEvents.Dispatch(new ExtractActivityNotification(_patientStatusExtract.Id,
                            new DwhProgress(nameof(PatientExtract), nameof(ExtractStatus.Sending),
                                (int)_patientStatusExtractStatus.Found,
                                (int)_patientStatusExtractStatus.Loaded, (int)_patientStatusExtractStatus.Rejected,
                                (int)_patientStatusExtractStatus.Loaded,
                                _statusCount)));
            //update Patient Visits
            if (_patientVisitExtractStatus.Found != null)
                if (_patientVisitExtractStatus.Loaded != null)
                    if (_patientVisitExtractStatus.Rejected != null)
                        DomainEvents.Dispatch(new ExtractActivityNotification(_patientVisitExtract.Id,
                            new DwhProgress(nameof(PatientExtract), nameof(ExtractStatus.Sending),
                                (int)_patientVisitExtractStatus.Found,
                                (int)_patientVisitExtractStatus.Loaded, (int)_patientVisitExtractStatus.Rejected,
                                (int)_patientVisitExtractStatus.Loaded,
                                _visitCount)));
            //update Patient Adverse Events
            if (_patientAdverseEventExtractStatus.Found != null)
                if (_patientAdverseEventExtractStatus.Loaded != null)
                    if (_patientAdverseEventExtractStatus.Rejected != null)
                        DomainEvents.Dispatch(new ExtractActivityNotification(_patientAdverseEventExtract.Id,
                            new DwhProgress(nameof(PatientExtract), nameof(ExtractStatus.Sending),
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
            }
        }
    }

    public class ExtractSentEventDto
    {
        public ExtractSentEventDto(Guid extractId, ExtractEventDTO extractEvent, ExtractType extractType, int sent)
        {
            ExtractId = extractId;
            ExtractEvent = extractEvent;
            ExtractType = extractType;
            Sent = sent;
        }

        public Guid ExtractId { get; set; }
        public ExtractEventDTO ExtractEvent { get; set; }
        public ExtractType ExtractType { get; set; }
        public int Sent { get; set; }
    }
}
