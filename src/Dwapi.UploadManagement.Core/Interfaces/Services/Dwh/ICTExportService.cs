using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
namespace Dwapi.UploadManagement.Core.Interfaces.Services.Dwh
{
    public interface ICTExportService
    {
        HttpClient Client { get; set; }


        Task<List<SendDhwManifestResponse>> ExportManifestAsync(SendManifestPackageDTO sendTo,
         DwhManifestMessageBag messageBag, string version, string apiVersion = "");

        Task<List<SendDhwManifestResponse>> ExportManifestAsync(SendManifestPackageDTO sendTo, string version, string apiVersion = "");
       
        Task<List<SendDhwManifestResponse>> ExportSmartManifestAsync(SendManifestPackageDTO sendTo, string version, string apiVersion = "");
        Task<List<SendDhwManifestResponse>> ExportSmartManifestAsync(SendManifestPackageDTO sendTo,
          DwhManifestMessageBag messageBag, string version, string apiVersion = "");


        void NotifyPreSending();

        
        Task<List<SendCTResponse>> ExportBatchExtractsAsync<T>(
            SendManifestPackageDTO sendTo,
            int batchSize,
            IMessageBag<T> messageBag)
            where T : ClientExtract;

        Task<List<SendCTResponse>> ExportSmartBatchExtractsAsync<T>(
             SendManifestPackageDTO sendTo,
             int batchSize,
             IMessageSourceBag<T> messageBag)
             where T : ClientExtract;


        Task NotifyPostSending(SendManifestPackageDTO sendTo, string version);
    }
}
