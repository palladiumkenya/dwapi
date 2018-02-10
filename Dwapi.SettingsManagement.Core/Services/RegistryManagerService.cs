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
        private readonly ICentralRegistryRepository _centralRegistryRepository;

        public RegistryManagerService(ICentralRegistryRepository centralRegistryRepository)
        {
            _centralRegistryRepository = centralRegistryRepository;
        }

        public async Task<bool> Verify(CentralRegistry centralRegistry)
        {
            var client=new HttpClient();
            client.DefaultRequestHeaders.Add("SubscriberId", $"{centralRegistry.SubscriberId}");
            if (centralRegistry.RequiresAuthentication())
            {
                client.DefaultRequestHeaders.Add("Token", $"{centralRegistry.AuthToken}");
            }
            var result = await client.GetAsync(centralRegistry.Url);
            return result.IsSuccessStatusCode;
        }

        public CentralRegistry GetDefault()
        {
            return _centralRegistryRepository.GetDefault();
        }

        public void SaveDefault(CentralRegistry centralRegistry)
        {
            _centralRegistryRepository.SaveDefault(centralRegistry);
            _centralRegistryRepository.SaveChanges();
        }
    }
}