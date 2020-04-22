using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Exchange.Dwh;

namespace Dwapi.UploadManagement.Core.Interfaces.Services.Dwh
{
    public interface IDwhSendService
    {
        HttpClient Client { get; set; }
        Task<List<SendDhwManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo);

        Task<List<SendDhwManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo,
            DwhManifestMessageBag messageBag);

        Task<List<string>> SendExtractsAsync(SendManifestPackageDTO sendTo);
    }
}
