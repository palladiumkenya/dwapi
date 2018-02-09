using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Model;

namespace Dwapi.SettingsManagement.Core.Services
{
    public class RegistryManagerService:IRegistryManagerService
    {
        public async Task<bool> Verify(CentralRegistry centralRegistry)
        {
            var client=new HttpClient();
            var result = await client.GetAsync(centralRegistry.Url);
            return result.IsSuccessStatusCode;
        }
    }
}