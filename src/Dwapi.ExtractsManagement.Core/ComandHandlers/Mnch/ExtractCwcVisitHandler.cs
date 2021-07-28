using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Source.Mnch;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Mnch
{
    public class ExtractCwcVisitHandler :IRequestHandler<ExtractCwcVisit,bool>
    {
        private readonly ICwcVisitSourceExtractor _cwcVisitSourceExtractor;
        private readonly IMnchExtractValidator _extractValidator;
        private readonly ICwcVisitLoader _cwcVisitLoader;
        private readonly IExtractHistoryRepository _extractHistoryRepository;

        public ExtractCwcVisitHandler(ICwcVisitSourceExtractor cwcVisitSourceExtractor, IMnchExtractValidator extractValidator, ICwcVisitLoader cwcVisitLoader, IExtractHistoryRepository extractHistoryRepository)
        {
            _cwcVisitSourceExtractor = cwcVisitSourceExtractor;
            _extractValidator = extractValidator;
            _cwcVisitLoader = cwcVisitLoader;
            _extractHistoryRepository = extractHistoryRepository;
        }

        public async Task<bool> Handle(ExtractCwcVisit request, CancellationToken cancellationToken)
        {
            //Extract
            int found = await _cwcVisitSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(CwcVisitExtract), $"{nameof(TempCwcVisitExtract)}s");

            //Load
            int loaded = await _cwcVisitLoader.Load(request.Extract.Id, found, request.DatabaseProtocol.SupportsDifferential);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


            _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected, request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new MnchExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(CwcVisitExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
