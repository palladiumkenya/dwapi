using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Exchange.Mgs;

namespace Dwapi.UploadManagement.Core.Interfaces.Services.Mgs
{
    public interface IMgsSendService
    {
        HttpClient Client { get; set; }
        Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo);
        Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo, ManifestMessageBag messageBag);

        Task<List<SendMpiResponse>> SendMigrationsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendMigrationsAsync(SendManifestPackageDTO sendTo, MgsMessageBag messageBag);
    }
}
