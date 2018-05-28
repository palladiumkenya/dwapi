using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Core.Interfaces.Services
{
    public interface IPsmartSendService
    {
        Task<SendResponse> SendAsync(SendPackageDTO sendTo, SmartMessageBag smartMessageBag);
    }
}