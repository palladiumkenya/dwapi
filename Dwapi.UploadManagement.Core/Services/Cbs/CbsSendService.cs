using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Services.Cbs;
using Newtonsoft.Json;
using Serilog;

namespace Dwapi.UploadManagement.Core.Services.Cbs
{
    public class CbsSendService : ICbsSendService
    {
        private readonly string _endPoint;
        private readonly ICbsPackager _packager;

        public CbsSendService(ICbsPackager packager)
        {
            _packager = packager;
            _endPoint = "api/cbs/";
        }

        public Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo)
        {
            return SendManifestAsync(sendTo, ManifestMessageBag.Create(_packager.Generate().ToList()));
        }

        public Task<List<SendMpiResponse>> SendMpiAsync(SendManifestPackageDTO sendTo)
        {
            return SendMpiAsync(sendTo, MpiMessageBag.Create(_packager.GenerateMpi().ToList()));
        }

        public async Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo, ManifestMessageBag manifestMessage)
        {
            var responses=new List<SendManifestResponse>();

            var client = new HttpClient();

            foreach (var message in manifestMessage.Messages)
            {
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}manifest"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendManifestResponse>();
                        responses.Add(content);
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        throw new Exception(error);
                    }
               }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
            }
            
            return responses;
        }

        public async Task<List<SendMpiResponse>> SendMpiAsync(SendManifestPackageDTO sendTo, MpiMessageBag messageBag)
        {
            var responses = new List<SendMpiResponse>();

            var client = new HttpClient();

            foreach (var message in messageBag.Messages)
            {
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}mpi"), message);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsJsonAsync<SendMpiResponse>();
                        responses.Add(content);
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        throw new Exception(error);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Send Manifest Error");
                    throw;
                }
            }

            return responses;
        }
    }
}