using System;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Interfaces.Services;
using Dwapi.UploadManagement.Core.Model;
using Serilog;

namespace Dwapi.UploadManagement.Core.Services
{
    public class PsmartSendService: IPsmartSendService
    {
        private readonly string _endPoint;

        public PsmartSendService()
        {
            _endPoint = "api/v1/inbound";
        }

        public async Task<SendResponse> SendAsync(SendPackageDTO sendTo, SmartMessageBag smartMessageBag)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("SubscriberId", $"{sendTo.Destination.SubscriberId}");
            if (sendTo.Destination.RequiresAuthentication())
            {
                client.DefaultRequestHeaders.Add("Token", $"{sendTo.Destination.AuthToken}");
            }

            var response = await client.PostAsJsonAsync(sendTo.GetUrl(_endPoint), smartMessageBag);
            SendResponse content = null;
            if (null != response && response.IsSuccessStatusCode)
            {
                try
                {
                    content = await response.Content.ReadAsJsonAsync<SendResponse>();
                }
                catch (Exception e)
                {
                    // send error
                    Log.Debug($"{e}");
                }
            }
            return content;
        }
    }
}