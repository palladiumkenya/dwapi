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
    public class ExtractPrepLabHandler :IRequestHandler<ExtractPrepLab,bool>
    {
        private readonly IPrepLabSourceExtractor _PrepLabSourceExtractor;
        private readonly IPrepExtractValidator _extractValidator;
        private readonly IPrepLabLoader _PrepLabLoader;
        private readonly IExtractHistoryRepository _extractHistoryRepository;

        public ExtractPrepLabHandler(IPrepLabSourceExtractor PrepLabSourceExtractor, IPrepExtractValidator extractValidator, IPrepLabLoader PrepLabLoader, IExtractHistoryRepository extractHistoryRepository)
        {
            _PrepLabSourceExtractor = PrepLabSourceExtractor;
            _extractValidator = extractValidator;
            _PrepLabLoader = PrepLabLoader;
            _extractHistoryRepository = extractHistoryRepository;
        }

        public async Task<bool> Handle(ExtractPrepLab request, CancellationToken cancellationToken)
        {
            //Extract
            int found = await _PrepLabSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(PrepLabExtract), $"{nameof(TempPrepLabExtract)}s");

            //Load
            int loaded = await _PrepLabLoader.Load(request.Extract.Id, found, request.DatabaseProtocol.SupportsDifferential);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


            _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected, request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new PrepExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(PrepLabExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
