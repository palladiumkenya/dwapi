using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Exchange.Cbs;

namespace Dwapi.UploadManagement.Core.Interfaces.Services.Cbs
{
    public interface ICbsSendService
    {
        Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo);

        Task<List<SendMpiResponse>> SendMpiAsync(SendManifestPackageDTO sendTo);
        Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo, ManifestMessageBag messageBag);

        Task<List<SendMpiResponse>> SendMpiAsync(SendManifestPackageDTO sendTo, MpiMessageBag messageBag);
    }
}