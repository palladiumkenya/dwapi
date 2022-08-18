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
    public class ExtractPrepAdverseEventHandler :IRequestHandler<ExtractPrepAdverseEvent,bool>
    {
        private readonly IPrepAdverseEventSourceExtractor _PrepAdverseEventSourceExtractor;
        private readonly IPrepExtractValidator _extractValidator;
        private readonly IPrepAdverseEventLoader _PrepAdverseEventLoader;
        private readonly IExtractHistoryRepository _extractHistoryRepository;

        public ExtractPrepAdverseEventHandler(IPrepAdverseEventSourceExtractor PrepAdverseEventSourceExtractor, IPrepExtractValidator extractValidator, IPrepAdverseEventLoader PrepAdverseEventLoader, IExtractHistoryRepository extractHistoryRepository)
        {
            _PrepAdverseEventSourceExtractor = PrepAdverseEventSourceExtractor;
            _extractValidator = extractValidator;
            _PrepAdverseEventLoader = PrepAdverseEventLoader;
            _extractHistoryRepository = extractHistoryRepository;
        }

        public async Task<bool> Handle(ExtractPrepAdverseEvent request, CancellationToken cancellationToken)
        {
            //Extract
            int found = await _PrepAdverseEventSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(PrepAdverseEventExtract), $"{nameof(TempPrepAdverseEventExtract)}s");

            //Load
            int loaded = await _PrepAdverseEventLoader.Load(request.Extract.Id, found, request.DatabaseProtocol.SupportsDifferential);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


            _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected, request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new PrepExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(PrepAdverseEventExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
