using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Exchange.Hts;

namespace Dwapi.UploadManagement.Core.Interfaces.Services.Hts
{
    public interface IHtsSendService
    {
        Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo);
        Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo, ManifestMessageBag messageBag);

        Task<List<SendMpiResponse>> SendClientsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendClientsAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag);

        Task<List<SendMpiResponse>> SendClientLinkagesAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendClientLinkagesAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag);

        Task<List<SendMpiResponse>> SendClientPartnersAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendClientPartnersAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag);
    }
}
