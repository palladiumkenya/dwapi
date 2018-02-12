using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.TransmissionManagement.Core.Model;

namespace Dwapi.TransmissionManagement.Core.Services.Psmart
{
    public interface PsmartSendService
    {
        Task<IEnumerable<SendResponse>> SendAsync(IEnumerable<PsmartStageDTO> message);
    }
}