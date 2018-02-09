using System.Threading.Tasks;
using Dwapi.SettingsManagement.Core.Model;

namespace Dwapi.SettingsManagement.Core.Interfaces.Services
{
    public interface IRegistryManagerService
    {
        Task<bool> Verify(CentralRegistry centralRegistry);
        CentralRegistry GetDefault();
        void SaveDefault(CentralRegistry centralRegistry);
    }
}