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
    public class ExtractPrepMonthlyRefillHandler :IRequestHandler<ExtractPrepMonthlyRefill,bool>
    {
        private readonly IPrepMonthlyRefillSourceExtractor _PrepMonthlyRefillSourceExtractor;
        private readonly IPrepExtractValidator _extractValidator;
        private readonly IPrepMonthlyRefillLoader _PrepMonthlyRefillLoader;
        private readonly IExtractHistoryRepository _extractHistoryRepository;

        public ExtractPrepMonthlyRefillHandler(IPrepMonthlyRefillSourceExtractor PrepMonthlyRefillSourceExtractor, IPrepExtractValidator extractValidator, IPrepMonthlyRefillLoader PrepMonthlyRefillLoader, IExtractHistoryRepository extractHistoryRepository)
        {
            _PrepMonthlyRefillSourceExtractor = PrepMonthlyRefillSourceExtractor;
            _extractValidator = extractValidator;
            _PrepMonthlyRefillLoader = PrepMonthlyRefillLoader;
            _extractHistoryRepository = extractHistoryRepository;
        }

        public async Task<bool> Handle(ExtractPrepMonthlyRefill request, CancellationToken cancellationToken)
        {
            //Extract
            int found = await _PrepMonthlyRefillSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(PrepMonthlyRefillExtract), $"{nameof(TempPrepMonthlyRefillExtract)}s");

            //Load
            int loaded = await _PrepMonthlyRefillLoader.Load(request.Extract.Id, found, request.DatabaseProtocol.SupportsDifferential);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


            _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected, request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new PrepExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(PrepMonthlyRefillExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
