using System.Threading.Tasks;
using Dwapi.SettingsManagement.Core.Model;

namespace Dwapi.SettingsManagement.Core.Interfaces.Services
{
    public interface IRegistryManagerService
    {
        Task<VerificationResponse> Verify(CentralRegistry centralRegistry,string endpoint = "api/test");
        CentralRegistry GetDefault();
        void SaveDefault(CentralRegistry centralRegistry);
    }
}