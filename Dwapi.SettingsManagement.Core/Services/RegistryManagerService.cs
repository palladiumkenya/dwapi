using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SettingsManagement.Core.Services
{
    public class RegistryManagerService:IRegistryManagerService
    {
        private readonly ICentralRegistryRepository _centralRegistryRepository;

        public RegistryManagerService(ICentralRegistryRepository centralRegistryRepository)
        {
            _centralRegistryRepository = centralRegistryRepository;
        }

        public async Task<VerificationResponse> Verify(CentralRegistry centralRegistry, string endpoint = "api/v1/test")
        {
            var verificationResponse = new VerificationResponse(string.Empty, false);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("SubscriberId", $"{centralRegistry.SubscriberId}");
            if (centralRegistry.RequiresAuthentication())
            {
                client.DefaultRequestHeaders.Add("Token", $"{centralRegistry.AuthToken}");
            }

            var response = await client.GetAsync($"{centralRegistry.Url.HasToEndsWith("/")}{endpoint}");

            if (response.IsSuccessStatusCode)
            {
                verificationResponse.Verified = true;
                verificationResponse.RegistryName = await response.Content.ReadAsStringAsync();
            }

            return verificationResponse;

        }
        public async Task<VerificationResponse> VerifyDocket(CentralRegistry centralRegistry)
        {
            var verificationResponse = new VerificationResponse(string.Empty, false);

            var client = new HttpClient();
         
            string endpoint = $"api/{centralRegistry.DocketId}/Verify";
            
         
            var response = await client.PostAsJsonAsync($"{centralRegistry.Url.HasToEndsWith("/")}{endpoint}",centralRegistry);

            if (response.IsSuccessStatusCode)
            {
                verificationResponse.Verified = true;
                verificationResponse.RegistryName = await response.Content.ReadAsStringAsync();
            }

            return verificationResponse;

        }

        public CentralRegistry GetDefault()
        {
            return _centralRegistryRepository.GetDefault();
        }

        public CentralRegistry GetByDocket(string docket)
        {
            return _centralRegistryRepository.Get(x=>x.DocketId.IsSameAs(docket));
        }

        public void SaveDefault(CentralRegistry centralRegistry)
        {
            _centralRegistryRepository.SaveDefault(centralRegistry);
            _centralRegistryRepository.SaveChanges();
        }
    }
}