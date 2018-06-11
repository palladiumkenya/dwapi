using System.Threading.Tasks;
using Dwapi.SettingsManagement.Core.Model;

namespace Dwapi.SettingsManagement.Core.Interfaces.Services
{
    public interface IRegistryManagerService
    {
        Task<VerificationResponse> Verify(CentralRegistry centralRegistry,string endpoint = "api/v1/test");
        CentralRegistry GetDefault();
        CentralRegistry GetByDocket(string docket);
        void SaveDefault(CentralRegistry centralRegistry);
    }
}