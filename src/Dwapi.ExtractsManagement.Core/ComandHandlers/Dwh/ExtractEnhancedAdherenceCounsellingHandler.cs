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
    public class ExtractEnhancedAdherenceCounsellingHandler :IRequestHandler<ExtractEnhancedAdherenceCounselling,bool>
    {
        private readonly IEnhancedAdherenceCounsellingSourceExtractor _EnhancedAdherenceCounsellingSourceExtractor;
        private readonly IExtractValidator _extractValidator;
        private readonly IEnhancedAdherenceCounsellingLoader _EnhancedAdherenceCounsellingLoader;
        private readonly IClearDwhExtracts _clearDwhExtracts;
        private readonly IExtractHistoryRepository _extractHistoryRepository;

        public ExtractEnhancedAdherenceCounsellingHandler(IEnhancedAdherenceCounsellingSourceExtractor EnhancedAdherenceCounsellingSourceExtractor, IExtractValidator extractValidator, IEnhancedAdherenceCounsellingLoader EnhancedAdherenceCounsellingLoader, IClearDwhExtracts clearDwhExtracts, IExtractHistoryRepository extractHistoryRepository)
        {
            _EnhancedAdherenceCounsellingSourceExtractor = EnhancedAdherenceCounsellingSourceExtractor;
            _extractValidator = extractValidator;
            _EnhancedAdherenceCounsellingLoader = EnhancedAdherenceCounsellingLoader;
            _clearDwhExtracts = clearDwhExtracts;
            _extractHistoryRepository = extractHistoryRepository;
        }

        public async Task<bool> Handle(ExtractEnhancedAdherenceCounselling request, CancellationToken cancellationToken)
        {
            //Extract
            int found = await _EnhancedAdherenceCounsellingSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(EnhancedAdherenceCounsellingExtract), $"{nameof(TempEnhancedAdherenceCounsellingExtract)}s");

            //Load
            int loaded = await _EnhancedAdherenceCounsellingLoader.Load(request.Extract.Id, found, request.DatabaseProtocol.SupportsDifferential);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


            _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected, request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new ExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(EnhancedAdherenceCounsellingExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
