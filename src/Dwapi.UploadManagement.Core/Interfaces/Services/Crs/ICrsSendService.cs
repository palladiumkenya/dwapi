using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Exchange.Crs;

namespace Dwapi.UploadManagement.Core.Interfaces.Services.Crs
{
    public interface ICrsSendService
    {
        HttpClient Client { get; set; }
        Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo,string version);

        Task<List<SendCrsResponse>> SendCrsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo, ManifestMessageBag messageBag,string version);

        Task<List<SendCrsResponse>> SendCrsAsync(SendManifestPackageDTO sendTo, CrsMessageBag messageBag);
        Task NotifyPostSending(SendManifestPackageDTO sendTo,string version);
    }
}
