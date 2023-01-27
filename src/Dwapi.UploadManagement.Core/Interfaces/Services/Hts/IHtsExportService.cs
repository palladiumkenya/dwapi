using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Exchange.Hts;
using Microsoft.AspNetCore.Http;

namespace Dwapi.UploadManagement.Core.Interfaces.Services.Hts
{
    public interface IHtsExportService
    {
        HttpClient Client { get; set; }

        Task<List<SendManifestResponse>> ExportManifestAsync(SendManifestPackageDTO sendTo, string version);
        Task<List<SendManifestResponse>> ExportManifestAsync(SendManifestPackageDTO sendTo, ManifestMessageBag messageBag, string version);

        Task<List<SendMpiResponse>> ExportClientsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportClientsAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag);
        Task<List<SendMpiResponse>> ExportClientTestsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportClientTestsAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag);
        Task<List<SendMpiResponse>> ExportTestKitsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportTestKitsAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag);
        Task<List<SendMpiResponse>> ExportClientTracingAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportClientTracingAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag);
        Task<List<SendMpiResponse>> ExportPartnerTracingAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportPartnerTracingAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag);
        Task<List<SendMpiResponse>> ExportPartnerNotificationServicesAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportPartnerNotificationServicesAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag);

        Task<List<SendMpiResponse>> ExportClientsLinkagesAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportClientsLinkagesAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag);

        Task<List<SendMpiResponse>> ExportHtsEligibilityExtractsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportHtsEligibilityExtractsAsync(SendManifestPackageDTO sendTo, HtsMessageBag messageBag);


        Task<List<SendMpiResponse>> SendHtsFiles(IFormFile file);


        Task NotifyPostSending(SendManifestPackageDTO sendTo, string version);

        Task ZipExtractsAsync(SendManifestPackageDTO sendTo, string version);
        Task ZipExtractsAsync(SendManifestPackageDTO sendTo, ManifestMessageBag messageBag, string version);
    }
}
