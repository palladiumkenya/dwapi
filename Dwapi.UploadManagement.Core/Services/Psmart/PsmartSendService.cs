using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Interfaces.Services.Psmart;
using Dwapi.UploadManagement.Core.Model;
using Serilog;

namespace Dwapi.UploadManagement.Core.Services.Psmart
{
    public class PsmartSendService: IPsmartSendService
    {
        private HttpClient _client;

        public PsmartSendService()
        {
            _client=new HttpClient();
        }

        public async Task<SendResponse> SendAsync(string endpoint, IEnumerable<PsmartStageDTO> message)
        {
            var response = await _client.PostAsJsonAsync(endpoint, message);
            SendResponse content = null;
            if (null != response)
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