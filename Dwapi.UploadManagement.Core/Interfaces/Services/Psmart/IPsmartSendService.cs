using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Model;

namespace Dwapi.UploadManagement.Core.Interfaces.Services.Psmart
{
    public interface IPsmartSendService
    {
        Task<SendResponse> SendAsync(SendPackageDTO sendTo, SmartMessageBag smartMessageBag);
    }
}