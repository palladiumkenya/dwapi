using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Exchange.Prep;

namespace Dwapi.UploadManagement.Core.Interfaces.Services.Prep
{
    public interface IPrepSendService
    {
        HttpClient Client { get; set; }

        Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo,string version);
        Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo, ManifestMessageBag messageBag,string version);

        Task<List<SendMpiResponse>> SendPatientPrepsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendPatientPrepsAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag);

        Task<List<SendMpiResponse>> SendPrepAdverseEventAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendPrepAdverseEventAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag);

        Task<List<SendMpiResponse>> SendPrepBehaviourRisksAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendPrepBehaviourRisksAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag);

        Task<List<SendMpiResponse>> SendPrepCareTerminationsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendPrepCareTerminationsAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag);
        Task<List<SendMpiResponse>> SendPrepLabsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendPrepLabsAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag);

        Task<List<SendMpiResponse>> SendPrepPharmacysAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendPrepPharmacysAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag);

        Task<List<SendMpiResponse>> SendPrepVisitsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendPrepVisitsAsync(SendManifestPackageDTO sendTo, PrepMessageBag messageBag);

        Task NotifyPostSending(SendManifestPackageDTO sendTo, string version);
    }
}
