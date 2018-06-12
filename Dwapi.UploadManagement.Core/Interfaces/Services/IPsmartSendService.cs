using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.UploadManagement.Core.Exchange.Psmart;
using SendResponse = Dwapi.UploadManagement.Core.Exchange.Psmart.SendResponse;

namespace Dwapi.UploadManagement.Core.Interfaces.Services
{
    public interface IPsmartSendService
    {
        Task<SendResponse> SendAsync(SendPackageDTO sendTo, SmartMessageBag smartMessageBag);
    }
}