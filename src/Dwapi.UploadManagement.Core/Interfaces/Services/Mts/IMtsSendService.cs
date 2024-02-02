using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts.Dto;
using Dwapi.SettingsManagement.Core.DTOs;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Exchange.Mgs;

namespace Dwapi.UploadManagement.Core.Interfaces.Services.Mts
{
    public interface IMtsSendService
    {
        HttpClient Client { get; set; }
        Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo);
        Task<List<SendManifestResponse>> SendManifestAsync(SendManifestPackageDTO sendTo, ManifestMessageBag messageBag);

        Task<List<SendMpiResponse>> SendMigrationsAsync(SendManifestPackageDTO sendTo);
        Task<List<SendMpiResponse>> SendMigrationsAsync(SendManifestPackageDTO sendTo, MgsMessageBag messageBag);
        Task SendIndicators(List<IndicatorExtractDto> indicators);
    }
}
