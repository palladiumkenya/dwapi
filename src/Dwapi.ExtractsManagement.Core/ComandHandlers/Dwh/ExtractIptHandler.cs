using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Diff;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Dwh
{
    public class ExtractIptHandler :IRequestHandler<ExtractIpt,bool>
    {
        private readonly IIptSourceExtractor _IptSourceExtractor;
        private readonly IExtractValidator _extractValidator;
        private readonly IIptLoader _IptLoader;
        private readonly IClearDwhExtracts _clearDwhExtracts;
        private readonly IExtractHistoryRepository _extractHistoryRepository;
        private readonly IDiffLogRepository _diffLogRepository;

        public ExtractIptHandler(IIptSourceExtractor IptSourceExtractor, IExtractValidator extractValidator, IIptLoader IptLoader, IClearDwhExtracts clearDwhExtracts, IExtractHistoryRepository extractHistoryRepository, IDiffLogRepository diffLogRepository)
        {
            _IptSourceExtractor = IptSourceExtractor;
            _extractValidator = extractValidator;
            _IptLoader = IptLoader;
            _clearDwhExtracts = clearDwhExtracts;
            _extractHistoryRepository = extractHistoryRepository;
            _diffLogRepository = diffLogRepository;
        }

        public async Task<bool> Handle(ExtractIpt request, CancellationToken cancellationToken)
        {
            // Differential loading
            // Get current site and docket dates,
            int found;
            var loadChangesOnly = request.LoadChangesOnly;
            var difflog = _diffLogRepository.GetLog("NDWH", "IptExtract");
            var changesLoadedStatus = false;
            if (request.DatabaseProtocol.SupportsDifferential)
            {
                if(null==difflog)
                    found  = await _IptSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);
                else
                    if (true == loadChangesOnly)
                    {
                        changesLoadedStatus = true;
                        found = await _IptSourceExtractor.Extract(request.Extract,
                            request.DatabaseProtocol, difflog.MaxCreated, difflog.MaxModified, difflog.SiteCode);
                    }
                    else
                    {
                        found  = await _IptSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

                    }
            }
            else
            {
                found  = await _IptSourceExtractor.Extract(request.Extract, request.DatabaseProtocol,difflog.MaxCreated,difflog.MaxModified,difflog.SiteCode);
            }
            //update changes
            _diffLogRepository.UpdateExtractsSentStatus("NDWH", "IptExtract", changesLoadedStatus);


            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(IptExtract), $"{nameof(TempIptExtract)}s");

            //Load
            int loaded = await _IptLoader.Load(request.Extract.Id, found, request.DatabaseProtocol.SupportsDifferential);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


            _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected, request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new ExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(IptExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
