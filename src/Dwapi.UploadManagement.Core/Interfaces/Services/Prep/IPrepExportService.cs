using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Exchange.Prep;

namespace Dwapi.UploadManagement.Core.Interfaces.Services.Prep
{
    public interface IPrepExportService
    {
        HttpClient Client { get; set; }

        Task<List<SendManifestResponse>> ExportManifestAsync(SendManifestPackageDTO sendTo, string version);
        Task<List<SendManifestResponse>> ExportManifestAsync(SendManifestPackageDTO sendTo, ManifestMessageBag messageBag, string version);

        Task<List<SendMpiResponse>> ExportPatientPrepsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportPatientPrepsAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag);

        Task<List<SendMpiResponse>> ExportPrepAdverseEventsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportPrepAdverseEventsAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag);

        Task<List<SendMpiResponse>> ExportPrepBehaviourRisksAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportPrepBehaviourRisksAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag);

        Task<List<SendMpiResponse>> ExportPrepCareTerminationsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportPrepCareTerminationsAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag);
        Task<List<SendMpiResponse>> ExportPrepLabsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportPrepLabsAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag);

        Task<List<SendMpiResponse>> ExportPrepPharmacysAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportPrepPharmacysAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag);

        Task<List<SendMpiResponse>> ExportPrepVisitsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportPrepVisitsAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag);

        Task NotifyPostSending(SendManifestPackageDTO sendTo, string version);
    }
}
