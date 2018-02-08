using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Model;

namespace Dwapi.SettingsManagement.Core.Services
{
    public class RegistryService:IRegistryService
    {
        private HttpClient _client;

        public RegistryService()
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