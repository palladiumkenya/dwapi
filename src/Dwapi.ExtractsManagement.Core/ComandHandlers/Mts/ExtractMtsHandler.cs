using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Mgs;
using Dwapi.ExtractsManagement.Core.Commands.Mts;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Mts;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Mts;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mts;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Mts
{
    public class ExtractMtsHandler :IRequestHandler<ExtractMts,bool>
    {
        private readonly IIndicatorSourceExtractor _indicatorSourceExtractor;
        private readonly IMetricExtractValidator _extractValidator;
        private readonly IMtsMigrationLoader _migrationLoader;
        private readonly ITempIndicatorExtractRepository _tempIndicatorExtractRepository;
        private readonly IExtractHistoryRepository _extractHistoryRepository;

        public ExtractMtsHandler(IIndicatorSourceExtractor indicatorSourceExtractor, IMetricExtractValidator extractValidator, IMtsMigrationLoader migrationLoader, ITempIndicatorExtractRepository tempIndicatorExtractRepository, IExtractHistoryRepository extractHistoryRepository)
        {
            _indicatorSourceExtractor = indicatorSourceExtractor;
            _extractValidator = extractValidator;
            _migrationLoader = migrationLoader;
            _tempIndicatorExtractRepository = tempIndicatorExtractRepository;
            _extractHistoryRepository = extractHistoryRepository;
        }

        public async Task<bool> Handle(ExtractMts request, CancellationToken cancellationToken)
        {

            //Extract
            int found = await _indicatorSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);


            //Validate
          //  await _extractValidator.Validate(request.Extract.Id, found, "Migration", "TempMetricMigrationExtracts");

            //Load
            int loaded = await _migrationLoader.Load(request.Extract.Id, found);

            //int rejected =
               // TODO: CHECK MGS Rejection
              // _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract,false);


            //_extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected,request.Extract,false);

            //notify loaded
           /* DomainEvents.Dispatch(
                new MgsExtractActivityNotification(request.Extract.Id, new ExtractProgress(
                    nameof(IndicatorExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));
            */
            return true;
        }
    }
}
