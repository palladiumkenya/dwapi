using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
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
    public class ExtractGbvScreeningHandler :IRequestHandler<ExtractGbvScreening,bool>
    {
        private readonly IGbvScreeningSourceExtractor _GbvScreeningSourceExtractor;
        private readonly IExtractValidator _extractValidator;
        private readonly IGbvScreeningLoader _GbvScreeningLoader;
        private readonly IClearDwhExtracts _clearDwhExtracts;
        private readonly IExtractHistoryRepository _extractHistoryRepository;

        public ExtractGbvScreeningHandler(IGbvScreeningSourceExtractor GbvScreeningSourceExtractor, IExtractValidator extractValidator, IGbvScreeningLoader GbvScreeningLoader, IClearDwhExtracts clearDwhExtracts, IExtractHistoryRepository extractHistoryRepository)
        {
            _GbvScreeningSourceExtractor = GbvScreeningSourceExtractor;
            _extractValidator = extractValidator;
            _GbvScreeningLoader = GbvScreeningLoader;
            _clearDwhExtracts = clearDwhExtracts;
            _extractHistoryRepository = extractHistoryRepository;
        }

        public async Task<bool> Handle(ExtractGbvScreening request, CancellationToken cancellationToken)
        {
            //Extract
            int found = await _GbvScreeningSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(GbvScreeningExtract), $"{nameof(TempGbvScreeningExtract)}s");

            //Load
            int loaded = await _GbvScreeningLoader.Load(request.Extract.Id, found);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


            _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected, request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new ExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(GbvScreeningExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
