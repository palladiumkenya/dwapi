using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Interfaces.Services.Cbs;
using Serilog;

namespace Dwapi.UploadManagement.Core.Services.Cbs
{
    public class CbsSendService : ICbsSendService
    {
        private readonly string _endPoint;

        public CbsSendService()
        {
            _endPoint = "api/cbs/manifest";
        }

        public async Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo, ManifestMessage manifestMessage)
        {
            var responses=new List<SendManifestResponse>();

            var client = new HttpClient();

            foreach (var message in manifestMessage.Manifests)
            {
                var response = await client.PostAsJsonAsync(sendTo.GetUrl(_endPoint), message);
                if (null != response && response.IsSuccessStatusCode)
                {
                    try
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendManifestResponse>();
                        responses.Add(content);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e,$"Send Manifest Error");
                        throw;
                    }
                }
            }
            
            return responses;
        }

    }
}