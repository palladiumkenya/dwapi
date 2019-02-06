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

namespace Dwapi.UploadManagement.Core.Services.Dwh
{
    public class DwhSendService : IDwhSendService
    {
        private readonly IDwhExtractReader _reader;
        private readonly IDwhPackager _packager;
        private readonly IExtractStatusService _extractStatusService;
        private readonly IEmrSystemRepository _emrSystemRepository;
        private readonly string _endPoint;

        public DwhSendService(IDwhPackager packager, IDwhExtractReader reader, IExtractStatusService extractStatusService, IEmrSystemRepository emrSystemRepository)
        {
            _packager = packager;
            _reader = reader;
            _extractStatusService = extractStatusService;
            _emrSystemRepository = emrSystemRepository;
            _endPoint = "api/";
        }

        public Task<List<SendDhwManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo)
        {
            return SendManifestAsync(sendTo, DwhManifestMessageBag.Create(_packager.Generate().ToList()));
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
            var responses = new List<string>();
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
                var defaultEmr = _emrSystemRepository.GetDefault();
                var extracts = defaultEmr.Extracts;
                var patientExtract = extracts.FirstOrDefault(x => x.Name.Equals(nameof(PatientExtract)));
                var patientExtractStatus = _extractStatusService.GetStatus(patientExtract.Id);
                var patientArtExtract = extracts.FirstOrDefault(x => x.Name.Equals(nameof(PatientArtExtract)));
                var patientArtExtractStatus = _extractStatusService.GetStatus(patientArtExtract.Id);
                var patientBaselineExtract = extracts.FirstOrDefault(x => x.Name.Equals("PatientBaselineExtract"));
                var patientBaselineExtractStatus = _extractStatusService.GetStatus(patientBaselineExtract.Id);
                var patientLabExtract = extracts.FirstOrDefault(x => x.Name.Equals("PatientLabExtract"));
                var patientLabExtractStatus = _extractStatusService.GetStatus(patientLabExtract.Id);
                var patientPharmacyExtract = extracts.FirstOrDefault(x => x.Name.Equals(nameof(PatientPharmacyExtract)));
                var patientPharmacyExtractStatus = _extractStatusService.GetStatus(patientPharmacyExtract.Id);
                var patientStatusExtract = extracts.FirstOrDefault(x => x.Name.Equals(nameof(PatientStatusExtract)));
                var patientStatusExtractStatus = _extractStatusService.GetStatus(patientStatusExtract.Id);
                var patientVisitExtract = extracts.FirstOrDefault(x => x.Name.Equals(nameof(PatientVisitExtract)));
                var patientVisitExtractStatus = _extractStatusService.GetStatus(patientVisitExtract.Id);
                var patientAdverseEventExtract = extracts.FirstOrDefault(x => x.Name.Equals(nameof(PatientAdverseEventExtract)));
                var patientAdverseEventExtractStatus = _extractStatusService.GetStatus(patientAdverseEventExtract.Id);
                var ids = _reader.ReadAllIds().ToList();
                var count = 0;
                var artCount = 0;
                var baselineCount = 0;
                var labCount = 0;
                var pharmacyCount = 0;
                var statusCount = 0;
                var visitCount = 0;
                var adverseEventCount = 0;
                var total = ids.Count;
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
                    new ExtractSentEventDto(patientExtract.Id, patientExtractStatus, ExtractType.Patient, count),
                    new ExtractSentEventDto(patientArtExtract.Id, patientArtExtractStatus, ExtractType.PatientArt,
                        artCount),
                    new ExtractSentEventDto(patientBaselineExtract.Id, patientBaselineExtractStatus,
                        ExtractType.PatientBaseline, baselineCount),
                    new ExtractSentEventDto(patientLabExtract.Id, patientLabExtractStatus, ExtractType.PatientLab,
                        labCount),
                    new ExtractSentEventDto(patientPharmacyExtract.Id, patientPharmacyExtractStatus,
                        ExtractType.PatientPharmacy, pharmacyCount),
                    new ExtractSentEventDto(patientStatusExtract.Id, patientStatusExtractStatus, ExtractType.PatientStatus,
                        statusCount),
                    new ExtractSentEventDto(patientVisitExtract.Id, patientVisitExtractStatus, ExtractType.PatientVisit,
                        visitCount),
                    new ExtractSentEventDto(patientAdverseEventExtract.Id, patientAdverseEventExtractStatus, ExtractType.PatientAdverseEvent,
                        visitCount)
                };
                UpdateStatusNotification(extractSendingStatuses, ExtractStatus.Sending);

                foreach (var id in ids)
                {
                    count++;
                    var patient = _packager.GenerateExtracts(id);
                    var artMessageBag = ArtMessageBag.Create(patient);
                    var baselineMessageBag = BaselineMessageBag.Create(patient);
                    var labMessageBag = LabMessageBag.Create(patient);
                    var pharmacyMessageBag = PharmacyMessageBag.Create(patient);
                    var statusMessageBag = StatusMessageBag.Create(patient);
                    var visitsMessageBag = VisitsMessageBag.Create(patient);
                    var adverseEventsMessageBag = AdverseEventsMessageBag.Create(patient);

                    foreach (var message in artMessageBag.Messages.Where(x => x.HasContents))
                    {
                        try
                        {
                            var msg = JsonConvert.SerializeObject(message);
                            var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{artMessageBag.EndPoint}"), message);
                            if (response.IsSuccessStatusCode)
                            {
                                artCount += message.ArtExtracts.Count;
                                var content = await response.Content.ReadAsStringAsync();
                                responses.Add(content);
                                sentPatientArts.AddRange(message.ArtExtracts.Select(x => x.Id).ToList());
                                /*if (patientArtExtractStatus.Found != null)
                                if (patientArtExtractStatus.Loaded != null)
                                    if (patientArtExtractStatus.Rejected != null)
                                        DomainEvents.Dispatch(new ExtractActivityNotification(patientArtExtract.Id,
                                            new DwhProgress(nameof(PatientArtExtract), nameof(ExtractStatus.Sending),
                                                (int)patientArtExtractStatus.Found,
                                                (int)patientArtExtractStatus.Loaded, (int)patientArtExtractStatus.Rejected,
                                                (int)patientArtExtractStatus.Loaded,
                                                artCount)));*/
                            }
                            else
                            {
                                var error = await response.Content.ReadAsStringAsync();
                                Log.Error(error, $"Host Response Error");
                                throw new Exception(error);
                            }
                        }
                        catch (Exception e)
                        {
                            DomainEvents.Dispatch(new DwhExtractSentEvent(ExtractType.PatientArt, message.ArtExtracts.Select(x => x.Id).ToList(), SendStatus.Failed, e.Message));
                            Log.Error(e, $"Send Error");
                            PrintMessage(message);
                            throw;
                        }
                    }

                    foreach (var message in baselineMessageBag.Messages.Where(x => x.HasContents))
                    {
                        try
                        {
                            var msg = JsonConvert.SerializeObject(message);
                            var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{baselineMessageBag.EndPoint}"), message);
                            if (response.IsSuccessStatusCode)
                            {
                                baselineCount += message.BaselinesExtracts.Count;
                                var content = await response.Content.ReadAsStringAsync();
                                responses.Add(content);
                                sentPatientBaselines.AddRange(message.BaselinesExtracts.Select(x => x.Id).ToList());
                            }
                            else
                            {
                                var error = await response.Content.ReadAsStringAsync();
                                Log.Error(error, $"Host Response Error");
                                throw new Exception(error);
                            }
                        }
                        catch (Exception e)
                        {
                            DomainEvents.Dispatch(new DwhExtractSentEvent(ExtractType.PatientBaseline, message.BaselinesExtracts.Select(x => x.Id).ToList(), SendStatus.Failed, e.Message));
                            Log.Error(e, $"Send Error");
                            PrintMessage(message);
                            throw;
                        }
                    }

                    foreach (var message in labMessageBag.Messages.Where(x => x.HasContents))
                    {
                        try
                        {
                            var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{labMessageBag.EndPoint}"), message);
                            if (response.IsSuccessStatusCode)
                            {
                                labCount += message.LaboratoryExtracts.Count;
                                var content = await response.Content.ReadAsStringAsync();
                                responses.Add(content);
                                sentPatientLabs.AddRange(message.LaboratoryExtracts.Select(x => x.Id).ToList());
                            }
                            else
                            {
                                var error = await response.Content.ReadAsStringAsync();
                                Log.Error(error, $"Host Response Error");
                                throw new Exception(error);
                            }
                        }
                        catch (Exception e)
                        {
                            DomainEvents.Dispatch(new DwhExtractSentEvent(ExtractType.PatientLab, message.LaboratoryExtracts.Select(x => x.Id).ToList(), SendStatus.Failed, e.Message));
                            Log.Error(e, $"Send Error");
                            PrintMessage(message);
                            throw;
                        }
                    }

                    foreach (var message in pharmacyMessageBag.Messages.Where(x => x.HasContents))
                    {
                        try
                        {
                            var msg = JsonConvert.SerializeObject(message);
                            var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{pharmacyMessageBag.EndPoint}"), message);
                            if (response.IsSuccessStatusCode)
                            {
                                pharmacyCount += message.PharmacyExtracts.Count;
                                var content = await response.Content.ReadAsStringAsync();
                                responses.Add(content);
                                sentPatientPharmacies.AddRange(message.PharmacyExtracts.Select(x => x.Id).ToList());
                            }
                            else
                            {
                                var error = await response.Content.ReadAsStringAsync();
                                Log.Error(error, $"Host Response Error");
                                throw new Exception(error);
                            }
                        }
                        catch (Exception e)
                        {
                            DomainEvents.Dispatch(new DwhExtractSentEvent(ExtractType.PatientPharmacy, message.PharmacyExtracts.Select(x => x.Id).ToList(), SendStatus.Failed, e.Message));
                            Log.Error(e, $"Send Error");
                            PrintMessage(message);
                            throw;
                        }
                    }

                    foreach (var message in statusMessageBag.Messages.Where(x => x.HasContents))
                    {
                        try
                        {
                            var msg = JsonConvert.SerializeObject(message);
                            var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{statusMessageBag.EndPoint}"), message);
                            if (response.IsSuccessStatusCode)
                            {
                                statusCount += message.StatusExtracts.Count;
                                var content = await response.Content.ReadAsStringAsync();
                                responses.Add(content);
                                sentPatientStatuses.AddRange(message.StatusExtracts.Select(x => x.Id).ToList());
                            }
                            else
                            {
                                var error = await response.Content.ReadAsStringAsync();
                                Log.Error(error, $"Host Response Error");
                                throw new Exception(error);
                            }
                        }
                        catch (Exception e)
                        {
                            Log.Error(e, $"Send Error");
                            DomainEvents.Dispatch(new DwhExtractSentEvent(ExtractType.PatientStatus, message.StatusExtracts.Select(x => x.Id).ToList(), SendStatus.Failed, e.Message));
                            PrintMessage(message);
                            throw;
                        }
                    }

                    foreach (var message in visitsMessageBag.Messages.Where(x => x.HasContents))
                    {
                        try
                        {
                            var msg = JsonConvert.SerializeObject(message);
                            var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{visitsMessageBag.EndPoint}"), message);
                            if (response.IsSuccessStatusCode)
                            {
                                visitCount += message.VisitExtracts.Count;
                                var content = await response.Content.ReadAsStringAsync();
                                responses.Add(content);
                                sentPatientVisits.AddRange(message.VisitExtracts.Select(x => x.Id).ToList());
                            }
                            else
                            {
                                var error = await response.Content.ReadAsStringAsync();
                                Log.Error(error, $"Host Response Error");
                                throw new Exception(error);
                            }
                        }
                        catch (Exception e)
                        {
                            Log.Error(e, $"Send Error");
                            DomainEvents.Dispatch(new DwhExtractSentEvent(ExtractType.PatientVisit, message.VisitExtracts.Select(x => x.Id).ToList(), SendStatus.Sent, e.Message));
                            PrintMessage(message);
                            throw;
                        }
                    }

                    foreach (var message in adverseEventsMessageBag.Messages.Where(x => x.HasContents))
                    {
                        try
                        {
                            var msg = JsonConvert.SerializeObject(message);
                            var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{adverseEventsMessageBag.EndPoint}"), message);
                            if (response.IsSuccessStatusCode)
                            {
                                visitCount += message.AdverseEventExtracts.Count;
                                var content = await response.Content.ReadAsStringAsync();
                                responses.Add(content);
                                sentPatientVisits.AddRange(message.AdverseEventExtracts.Select(x => x.Id).ToList());
                            }
                            else
                            {
                                var error = await response.Content.ReadAsStringAsync();
                                Log.Error(error, $"Host Response Error");
                                throw new Exception(error);
                            }
                        }
                        catch (Exception e)
                        {
                            Log.Error(e, $"Send Error");
                            DomainEvents.Dispatch(new DwhExtractSentEvent(ExtractType.PatientAdverseEvent, message.AdverseEventExtracts.Select(x => x.Id).ToList(), SendStatus.Sent, e.Message));
                            PrintMessage(message);
                            throw;
                        }
                    }

                    DomainEvents.Dispatch(new DwhSendNotification(new SendProgress(nameof(PatientExtract), Common.GetProgress(count, total))));
                    /*if (patientExtractStatus.Found != null)
                        if (patientExtractStatus.Loaded != null)
                            if (patientExtractStatus.Rejected != null)
                                DomainEvents.Dispatch(new ExtractActivityNotification(patientExtract.Id,
                                    new DwhProgress(nameof(PatientExtract), nameof(ExtractStatus.Sending),
                                        (int)patientExtractStatus.Found,
                                        (int)patientExtractStatus.Loaded, (int)patientExtractStatus.Rejected,
                                        (int)patientExtractStatus.Loaded,
                                        count)));*/
                    sentPatients.AddRange(ids);
                }

                //update extract sent field

                UpdateExtractSent(ExtractType.Patient, sentPatients);
                UpdateExtractSent(ExtractType.PatientArt, sentPatientArts);
                UpdateExtractSent(ExtractType.PatientBaseline, sentPatientBaselines);
                UpdateExtractSent(ExtractType.PatientLab, sentPatientLabs);
                UpdateExtractSent(ExtractType.PatientPharmacy, sentPatientPharmacies);
                UpdateExtractSent(ExtractType.PatientStatus, sentPatientStatuses);
                UpdateExtractSent(ExtractType.PatientVisit, sentPatientVisits);
                UpdateExtractSent(ExtractType.PatientAdverseEvent, sentPatientAdverseEvents);

                // update sent status notification
                var extractsStatuses = new List<ExtractSentEventDto>();
                var patientStatus = new ExtractSentEventDto(patientExtract.Id, patientExtractStatus, ExtractType.Patient, count);
                extractsStatuses.Add(patientStatus);
                var patientArtStatus = new ExtractSentEventDto(patientArtExtract.Id, patientArtExtractStatus, ExtractType.PatientArt, artCount);
                extractsStatuses.Add(patientArtStatus);
                var patientBaselineStatus = new ExtractSentEventDto(patientBaselineExtract.Id, patientBaselineExtractStatus, ExtractType.PatientBaseline, baselineCount);
                extractsStatuses.Add(patientBaselineStatus);
                var patientLabStatus = new ExtractSentEventDto(patientLabExtract.Id, patientLabExtractStatus, ExtractType.PatientLab, labCount);
                extractsStatuses.Add(patientLabStatus);
                var patientPharmacyStatus = new ExtractSentEventDto(patientPharmacyExtract.Id, patientPharmacyExtractStatus, ExtractType.PatientPharmacy, pharmacyCount);
                extractsStatuses.Add(patientPharmacyStatus);
                var patientStatusStatus = new ExtractSentEventDto(patientStatusExtract.Id, patientStatusExtractStatus, ExtractType.PatientStatus, statusCount);
                extractsStatuses.Add(patientStatusStatus);
                var patientVisitStatus = new ExtractSentEventDto(patientVisitExtract.Id, patientVisitExtractStatus, ExtractType.PatientVisit, visitCount);
                extractsStatuses.Add(patientVisitStatus);
                var patientAdverseEventStatus = new ExtractSentEventDto(patientAdverseEventExtract.Id, patientAdverseEventExtractStatus, ExtractType.PatientAdverseEvent, adverseEventCount);
                extractsStatuses.Add(patientAdverseEventStatus);
                UpdateStatusNotification(extractsStatuses, ExtractStatus.Sent);
            }

            return responses;
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

        private void UpdateExtractSent(ExtractType extractType, List<Guid> sentIds)
        {
            DomainEvents.Dispatch(new DwhExtractSentEvent(extractType, sentIds, SendStatus.Sent));
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