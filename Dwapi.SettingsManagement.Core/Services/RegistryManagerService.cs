using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Model;

namespace Dwapi.SettingsManagement.Core.Services
{
    public class RegistryManagerService:IRegistryManagerService
    {
        private HttpClient _client;

        public RegistryManagerService()
        {
            _client=new HttpClient();
        }

        public async Task<bool> Verify(CentralRegistry centralRegistry)
        {
            var result = await _client.GetAsync(centralRegistry.Url);
            return result.IsSuccessStatusCode;
        }
    }
}