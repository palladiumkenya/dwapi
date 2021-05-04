using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;

namespace Dwapi.UploadManagement.Core.Interfaces.Services.Dwh
{
    public interface IDwhSendService
    {
        HttpClient Client { get; set; }
        Task<List<SendDhwManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo,string version);

        Task<List<SendDhwManifestResponse>> SendDiffManifestAsync(SendManifestPackageDTO sendTo,string version);


        Task<List<SendDhwManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo,
            DwhManifestMessageBag messageBag,string  version);

        Task<List<string>> SendExtractsAsync(SendManifestPackageDTO sendTo);
    }
}
