using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Services.Dwh;
using Dwapi.UploadManagement.Core.Notifications;
using Dwapi.UploadManagement.Core.Notifications.Dwh;
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

            var ids = _reader.ReadAllIds().ToList();
            int count = 0;
            int total = ids.Count;

            foreach (var id in ids)
            {
                count++;

                var patient = _packager.GenerateExtracts(id);
                var artMessageBag = ArtMessageBag.Create(patient);
                var baselineMessageBag = BaselineMessageBag.Create(patient);
                var labMessageBag = LabMessageBag.Create(patient);
                var pharmacyMessageBag = PharmacyMessageBag.Create(patient);
                var statusMessageBag = StatusMessageBag.Create(patient);
                var visitsMessageBag = VisitsMessageBag.Create(patient);


                foreach (var message in artMessageBag.Messages.Where(x => x.HasContents))
                {
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{artMessageBag.EndPoint}"), message);
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
                        PrintMessage(message);
                        throw;
                    }
                }

                foreach (var message in baselineMessageBag.Messages.Where(x => x.HasContents))
                {
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{baselineMessageBag.EndPoint}"), message);
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
                        PrintMessage(message);
                        throw;
                    }
                }

                foreach (var message in labMessageBag.Messages.Where(x => x.HasContents))
                {
                    try
                    {
                     
                        var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{labMessageBag.EndPoint}"), message);
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
                        PrintMessage(message);
                        throw;
                    }
                }

                foreach (var message in pharmacyMessageBag.Messages.Where(x => x.HasContents))
                {
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{pharmacyMessageBag.EndPoint}"), message);
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
                        PrintMessage(message);
                        throw;
                    }
                }

                foreach (var message in statusMessageBag.Messages.Where(x => x.HasContents))
                {
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{statusMessageBag.EndPoint}"), message);
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
                        PrintMessage(message);
                        throw;
                    }
                }
                
                foreach (var message in visitsMessageBag.Messages.Where(x => x.HasContents))
                {
                    try
                    {
                        var msg = JsonConvert.SerializeObject(message);
                        var response = await client.PostAsJsonAsync(sendTo.GetUrl($"{_endPoint.HasToEndsWith("/")}{visitsMessageBag.EndPoint}"), message);
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
                        PrintMessage(message);
                        throw;
                    }
                }

               DomainEvents.Dispatch(new DwhSendNotification(new SendProgress(nameof(PatientExtract), Common.GetProgress(count, total))));

            }

            return responses;
        }


        private void PrintMessage(object message)
        {
            try
            {
                Log.Debug(new string('+', 40));
                Log.Debug(JsonConvert.SerializeObject(message, Formatting.Indented));
                Log.Debug(new string('+', 40));
            }
            catch
            {
            }
        }
    }
}