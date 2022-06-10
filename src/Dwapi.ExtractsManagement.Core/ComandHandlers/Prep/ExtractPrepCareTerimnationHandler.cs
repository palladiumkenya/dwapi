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
    public class ExtractPrepCareTerminationHandler :IRequestHandler<ExtractPrepCareTermination,bool>
    {
        private readonly IPrepCareTerminationSourceExtractor _PrepCareTerminationSourceExtractor;
        private readonly IPrepExtractValidator _extractValidator;
        private readonly IPrepCareTerminationLoader _PrepCareTerminationLoader;
        private readonly IExtractHistoryRepository _extractHistoryRepository;

        public ExtractPrepCareTerminationHandler(IPrepCareTerminationSourceExtractor PrepCareTerminationSourceExtractor, IPrepExtractValidator extractValidator, IPrepCareTerminationLoader PrepCareTerminationLoader, IExtractHistoryRepository extractHistoryRepository)
        {
            _PrepCareTerminationSourceExtractor = PrepCareTerminationSourceExtractor;
            _extractValidator = extractValidator;
            _PrepCareTerminationLoader = PrepCareTerminationLoader;
            _extractHistoryRepository = extractHistoryRepository;
        }

        public async Task<bool> Handle(ExtractPrepCareTermination request, CancellationToken cancellationToken)
        {
            //Extract
            int found = await _PrepCareTerminationSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(PrepCareTerminationExtract), $"{nameof(TempPrepCareTerminationExtract)}s");

            //Load
            int loaded = await _PrepCareTerminationLoader.Load(request.Extract.Id, found, request.DatabaseProtocol.SupportsDifferential);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


            _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected, request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new PrepExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(PrepCareTerminationExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
