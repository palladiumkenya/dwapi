using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.TransmissionManagement.Core.Model;

namespace Dwapi.TransmissionManagement.Core.Interfaces.Services.Psmart
{
    public interface IPsmartSendService
    {
        Task<IEnumerable<SendResponse>> SendAsync(string endpoint,IEnumerable<PsmartStageDTO> message);
    }
}