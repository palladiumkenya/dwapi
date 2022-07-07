using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Exchange.Hts;

namespace Dwapi.UploadManagement.Core.Interfaces.Services.Hts
{
    public interface IHtsSendService
    {
        HttpClient Client { get; set; }

        Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo,string version);
        Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo, ManifestMessageBag messageBag,string version);

        Task<List<SendMpiResponse>> SendClientsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendClientsAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag);
        Task<List<SendMpiResponse>> SendClientTestsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendClientTestsAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag);
        Task<List<SendMpiResponse>> SendTestKitsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendTestKitsAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag);
        Task<List<SendMpiResponse>> SendClientTracingAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendClientTracingAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag);
        Task<List<SendMpiResponse>> SendPartnerTracingAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendPartnerTracingAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag);
        Task<List<SendMpiResponse>> SendPartnerNotificationServicesAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendPartnerNotificationServicesAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag);

        Task<List<SendMpiResponse>> SendClientsLinkagesAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendClientsLinkagesAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag);
        
        Task<List<SendMpiResponse>> SendHtsEligibilityExtractsAsync(SendManifestPackageDTO sendTo);


        //Task<List<SendMpiResponse>> SendClientPartnersAsync(SendManifestPackageDTO sendTo);
        //Task<List<SendMpiResponse>> SendClientPartnersAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag);

        Task NotifyPostSending(SendManifestPackageDTO sendTo,string version);
    }
}
