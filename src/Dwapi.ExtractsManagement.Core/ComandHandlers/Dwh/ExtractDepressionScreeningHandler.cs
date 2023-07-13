using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Diff;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mts;
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
    public class ExtractDepressionScreeningHandler :IRequestHandler<ExtractDepressionScreening,bool>
    {
        private readonly IDepressionScreeningSourceExtractor _DepressionScreeningSourceExtractor;
        private readonly IExtractValidator _extractValidator;
        private readonly IDepressionScreeningLoader _DepressionScreeningLoader;
        private readonly IClearDwhExtracts _clearDwhExtracts;
        private readonly IExtractHistoryRepository _extractHistoryRepository;
        private readonly IDiffLogRepository _diffLogRepository;
        private readonly IIndicatorExtractRepository _indicatorExtractRepository;


        public ExtractDepressionScreeningHandler(IDepressionScreeningSourceExtractor DepressionScreeningSourceExtractor, IExtractValidator extractValidator, IDepressionScreeningLoader DepressionScreeningLoader, IClearDwhExtracts clearDwhExtracts, IExtractHistoryRepository extractHistoryRepository, IDiffLogRepository diffLogRepository,IIndicatorExtractRepository indicatorExtractRepository)
        {
            _DepressionScreeningSourceExtractor = DepressionScreeningSourceExtractor;
            _extractValidator = extractValidator;
            _DepressionScreeningLoader = DepressionScreeningLoader;
            _clearDwhExtracts = clearDwhExtracts;
            _extractHistoryRepository = extractHistoryRepository;
            _diffLogRepository = diffLogRepository;
            _indicatorExtractRepository = indicatorExtractRepository;

        }

        public async Task<bool> Handle(ExtractDepressionScreening request, CancellationToken cancellationToken)
        {
            // Get current site and docket dates,
            int found;
            var mflcode =   _indicatorExtractRepository.GetMflCode();

            var loadChangesOnly = request.LoadChangesOnly;
            var difflog = _diffLogRepository.GetLog("NDWH", "DepressionScreeningExtract", mflcode);
            var changesLoadedStatus= false;
            
            if(null==difflog)
                found  = await _DepressionScreeningSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);
            else
                if (true == loadChangesOnly)
                {
                    changesLoadedStatus = true;
                    found = await _DepressionScreeningSourceExtractor.Extract(request.Extract,
                        request.DatabaseProtocol, difflog.MaxCreated, difflog.MaxModified, difflog.SiteCode);
                }
                else
                {
                    found  = await _DepressionScreeningSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

                }
            //update status
            _diffLogRepository.UpdateExtractsSentStatus("NDWH", "DepressionScreeningExtract", changesLoadedStatus);


            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(DepressionScreeningExtract), $"{nameof(TempDepressionScreeningExtract)}s");

            //Load
            int loaded = await _DepressionScreeningLoader.Load(request.Extract.Id, found, request.DatabaseProtocol.SupportsDifferential);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


            _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected, request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new ExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(DepressionScreeningExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
