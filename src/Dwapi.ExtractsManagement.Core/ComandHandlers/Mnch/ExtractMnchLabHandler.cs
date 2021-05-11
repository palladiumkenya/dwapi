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
    public class ExtractMnchLabHandler :IRequestHandler<ExtractMnchLab,bool>
    {
        private readonly IMnchLabSourceExtractor _mnchLabSourceExtractor;
        private readonly IExtractValidator _extractValidator;
        private readonly IMnchLabLoader _mnchLabLoader;
        private readonly IExtractHistoryRepository _extractHistoryRepository;

        public ExtractMnchLabHandler(IMnchLabSourceExtractor mnchLabSourceExtractor, IExtractValidator extractValidator, IMnchLabLoader mnchLabLoader, IExtractHistoryRepository extractHistoryRepository)
        {
            _mnchLabSourceExtractor = mnchLabSourceExtractor;
            _extractValidator = extractValidator;
            _mnchLabLoader = mnchLabLoader;
            _extractHistoryRepository = extractHistoryRepository;
        }

        public async Task<bool> Handle(ExtractMnchLab request, CancellationToken cancellationToken)
        {
            //Extract
            int found = await _mnchLabSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(MnchLabExtract), $"{nameof(TempMnchLabExtract)}s");

            //Load
            int loaded = await _mnchLabLoader.Load(request.Extract.Id, found, request.DatabaseProtocol.SupportsDifferential);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


            _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected, request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new ExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(MnchLabExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
