using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Exchange.Mnch;

namespace Dwapi.UploadManagement.Core.Interfaces.Services.Mnch
{
    public interface IMnchExportService
    {
        HttpClient Client { get; set; }

        Task<List<SendManifestResponse>> ExportManifestAsync(SendManifestPackageDTO sendTo, string version);
        Task<List<SendManifestResponse>> ExportManifestAsync(SendManifestPackageDTO sendTo, ManifestMessageBag messageBag, string version);

        Task<List<SendMpiResponse>> ExportPatientMnchsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportPatientMnchsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);
        Task<List<SendMpiResponse>> ExportMnchEnrolmentsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportMnchEnrolmentsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);
        Task<List<SendMpiResponse>> ExportMnchArtsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportMnchArtsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);
        Task<List<SendMpiResponse>> ExportAncVisitsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportAncVisitsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);
        Task<List<SendMpiResponse>> ExportMatVisitsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportMatVisitsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);
        Task<List<SendMpiResponse>> ExportPncVisitsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportPncVisitsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);
        Task<List<SendMpiResponse>> ExportMotherBabyPairsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportMotherBabyPairsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);
        Task<List<SendMpiResponse>> ExportCwcEnrolmentsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportCwcEnrolmentsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);
        Task<List<SendMpiResponse>> ExportCwcVisitsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportCwcVisitsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);
        Task<List<SendMpiResponse>> ExportHeisAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportHeisAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);
        Task<List<SendMpiResponse>> ExportMnchLabsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> ExportMnchLabsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);

        Task NotifyPostSending(SendManifestPackageDTO sendTo, string version);
    }
}
