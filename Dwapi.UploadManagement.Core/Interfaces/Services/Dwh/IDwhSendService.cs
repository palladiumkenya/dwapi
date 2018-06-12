using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Exchange.Cbs;

namespace Dwapi.UploadManagement.Core.Interfaces.Services.Cbs
{
    public interface IDwhSendService
    {
        Task<List<SendDhwManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo, DwhManifestMessageBag messageBag);

    }
}