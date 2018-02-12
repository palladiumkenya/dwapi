using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Utility;
using Dwapi.TransmissionManagement.Core.Interfaces.Services.Psmart;
using Dwapi.TransmissionManagement.Core.Model;
using Serilog;

namespace Dwapi.TransmissionManagement.Core.Services.Psmart
{
    public class PsmartSendService: IPsmartSendService
    {
        private HttpClient _client;

        public PsmartSendService()
        {
            _client=new HttpClient();
        }

        public async Task<IEnumerable<SendResponse>> SendAsync(string endpoint, IEnumerable<PsmartStageDTO> message)
        {
            var response = await _client.PostAsJsonAsync(endpoint, message);
            IEnumerable<SendResponse> content = null;
            if (null != response)
            {
                try
                {
                    content = await response.Content.ReadAsJsonAsync<IEnumerable<SendResponse>>();
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