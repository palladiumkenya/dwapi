using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Exchange.Mnch;

namespace Dwapi.UploadManagement.Core.Interfaces.Services.Mnch
{
    public interface IMnchSendService
    {
        HttpClient Client { get; set; }

        Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo,string version);
        Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo, ManifestMessageBag messageBag,string version);

        Task<List<SendMpiResponse>> SendPatientMnchsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendPatientMnchsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);
        Task<List<SendMpiResponse>> SendMnchEnrolmentsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendMnchEnrolmentsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);
        Task<List<SendMpiResponse>> SendMnchArtsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendMnchArtsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);
        Task<List<SendMpiResponse>> SendAncVisitsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendAncVisitsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);
        Task<List<SendMpiResponse>> SendMatVisitsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendMatVisitsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);
        Task<List<SendMpiResponse>> SendPncVisitsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendPncVisitsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);
        Task<List<SendMpiResponse>> SendMotherBabyPairsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendMotherBabyPairsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);
        Task<List<SendMpiResponse>> SendCwcEnrolmentsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendCwcEnrolmentsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);
        Task<List<SendMpiResponse>> SendCwcVisitsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendCwcVisitsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);
        Task<List<SendMpiResponse>> SendHeisAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendHeisAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);
        Task<List<SendMpiResponse>> SendMnchLabsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendMnchLabsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);
        Task<List<SendMpiResponse>> SendMnchImmunizationsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendMnchImmunizationsAsync(SendManifestPackageDTO sendTo, MnchMessageBag messageBag);

        Task NotifyPostSending(SendManifestPackageDTO sendTo, string version);
    }
}
