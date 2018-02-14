using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.UploadManagement.Core.Model;

namespace Dwapi.UploadManagement.Core.Interfaces.Services.Psmart
{
    public interface IPsmartSendService
    {
        Task<SendResponse> SendAsync(SendPackageDTO sendTo, PsmartBag psmartBag);
        Task<SendResponse> SendAsync(SendPackageDTO sendTo, IEnumerable<PsmartBag> psmartBag);

        Task<SendResponse> SendAsync(SendPackageDTO sendTo, PsmartMessage psmartMessage);
        Task<SendResponse> SendAsync(SendPackageDTO sendTo, IEnumerable<PsmartStageDTO> message);
    }
}