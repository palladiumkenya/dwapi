using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.UploadManagement.Core.Model;

namespace Dwapi.UploadManagement.Core.Interfaces.Services.Psmart
{
    public interface IPsmartSendService
    {
        Task<SendResponse> SendAsync(string endpoint,IEnumerable<PsmartStageDTO> message);
    }
}