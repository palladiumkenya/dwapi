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
    public class ExtractPncVisitHandler :IRequestHandler<ExtractPncVisit,bool>
    {
        private readonly IPncVisitSourceExtractor _pncVisitSourceExtractor;
        private readonly IExtractValidator _extractValidator;
        private readonly IPncVisitLoader _pncVisitLoader;
        private readonly IExtractHistoryRepository _extractHistoryRepository;

        public ExtractPncVisitHandler(IPncVisitSourceExtractor pncVisitSourceExtractor, IExtractValidator extractValidator, IPncVisitLoader pncVisitLoader, IExtractHistoryRepository extractHistoryRepository)
        {
            _pncVisitSourceExtractor = pncVisitSourceExtractor;
            _extractValidator = extractValidator;
            _pncVisitLoader = pncVisitLoader;
            _extractHistoryRepository = extractHistoryRepository;
        }

        public async Task<bool> Handle(ExtractPncVisit request, CancellationToken cancellationToken)
        {
            //Extract
            int found = await _pncVisitSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(PncVisitExtract), $"{nameof(TempPncVisitExtract)}s");

            //Load
            int loaded = await _pncVisitLoader.Load(request.Extract.Id, found, request.DatabaseProtocol.SupportsDifferential);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


            _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected, request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new ExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(PncVisitExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
