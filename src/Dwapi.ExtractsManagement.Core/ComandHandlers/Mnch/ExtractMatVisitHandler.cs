using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Source.Mnch;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Mnch
{
    public class ExtractMatVisitHandler :IRequestHandler<ExtractMatVisit,bool>
    {
        private readonly IMatVisitSourceExtractor _matVisitSourceExtractor;
        private readonly IExtractValidator _extractValidator;
        private readonly IMatVisitLoader _matVisitLoader;
        private readonly IExtractHistoryRepository _extractHistoryRepository;

        public ExtractMatVisitHandler(IMatVisitSourceExtractor matVisitSourceExtractor, IExtractValidator extractValidator, IMatVisitLoader matVisitLoader, IExtractHistoryRepository extractHistoryRepository)
        {
            _matVisitSourceExtractor = matVisitSourceExtractor;
            _extractValidator = extractValidator;
            _matVisitLoader = matVisitLoader;
            _extractHistoryRepository = extractHistoryRepository;
        }

        public async Task<bool> Handle(ExtractMatVisit request, CancellationToken cancellationToken)
        {
            //Extract
            int found = await _matVisitSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(MatVisitExtract), $"{nameof(TempMatVisitExtract)}s");

            //Load
            int loaded = await _matVisitLoader.Load(request.Extract.Id, found, request.DatabaseProtocol.SupportsDifferential);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


            _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected, request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new ExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(MatVisitExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
