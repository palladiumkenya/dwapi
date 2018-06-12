﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Services.Dwh;
using Newtonsoft.Json;
using Serilog;

namespace Dwapi.UploadManagement.Core.Services.Dwh
{
    public class DwhSendService : IDwhSendService
    {
        private IDwhExtractReader _reader;
        private IDwhPackager _packager;

        private readonly string _endPoint;

        public DwhSendService(IDwhPackager packager, IDwhExtractReader reader)
        {
            _packager = packager;
            _reader = reader;
            _endPoint = "api/";
        }

        public Task<List<SendDhwManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo)
        {
            return SendManifestAsync(sendTo, DwhManifestMessageBag.Create(_packager.Generate().ToList()));
        }

        public async Task<List<SendDhwManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo, DwhManifestMessageBag messageBag)
        {
            var responses = new List<SendDhwManifestResponse>();

            var client = new HttpClient();

            foreach (var message in messageBag.Messages)
            {
                try
                {
                    var msg = JsonConvert.SerializeObject(message);
                    var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}spot"), message.Manifest);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        responses.Add(new SendDhwManifestResponse(content));
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

        public async Task<List<string>> SendExtractsAsync(SendManifestPackageDTO sendTo)
        {
            var client = new HttpClient();
            var responses = new List<string>();

            var ids = _reader.ReadAllIds();
            foreach (var id in ids)
            {
                var extractView = _packager.GenerateExtracts(id);
                var messageBag = ArtMessageBag.Create(extractView, extractView.PatientArtExtracts.ToList());


                foreach (var message in messageBag.Messages)
                {
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{messageBag.EndPoint}"), message);
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
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
                        Log.Error(e, $"Send Error");
                        throw;
                    }
                }

            }

            return responses;
        }
    }
}