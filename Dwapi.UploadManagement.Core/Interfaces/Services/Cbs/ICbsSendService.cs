using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Core.Interfaces.Services.Cbs
{
    public interface ICbsSendService
    {
        Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo, ManifestMessage message);
    }
}