using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Prep;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Prep;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Prep;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Prep;
using Dwapi.ExtractsManagement.Core.Model.Destination.Prep;
using Dwapi.ExtractsManagement.Core.Model.Source.Prep;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Prep
{
    public class ExtractPrepVisitHandler :IRequestHandler<ExtractPrepVisit,bool>
    {
        private readonly IPrepVisitSourceExtractor _prepVisitSourceExtractor;
        private readonly IPrepExtractValidator _extractValidator;
        private readonly IPrepVisitLoader _prepVisitLoader;
        private readonly IExtractHistoryRepository _extractHistoryRepository;

        public ExtractPrepVisitHandler(IPrepVisitSourceExtractor prepVisitSourceExtractor, IPrepExtractValidator extractValidator, IPrepVisitLoader prepVisitLoader, IExtractHistoryRepository extractHistoryRepository)
        {
            _prepVisitSourceExtractor = prepVisitSourceExtractor;
            _extractValidator = extractValidator;
            _prepVisitLoader = prepVisitLoader;
            _extractHistoryRepository = extractHistoryRepository;
        }

        public async Task<bool> Handle(ExtractPrepVisit request, CancellationToken cancellationToken)
        {
            //Extract
            int found = await _prepVisitSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(PrepVisitExtract), $"{nameof(TempPrepVisitExtract)}s");

            //Load
            int loaded = await _prepVisitLoader.Load(request.Extract.Id, found, request.DatabaseProtocol.SupportsDifferential);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


            _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected, request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new PrepExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(PrepVisitExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
