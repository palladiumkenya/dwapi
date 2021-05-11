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
    public class ExtractMnchEnrolmentHandler :IRequestHandler<ExtractMnchEnrolment, bool>
    {
        private readonly IMnchEnrolmentSourceExtractor _mnchEnrolmentSourceExtractor;
        private readonly IExtractValidator _extractValidator;
        private readonly IMnchEnrolmentLoader _mnchEnrolmentLoader;
        private readonly IExtractHistoryRepository _extractHistoryRepository;

        public ExtractMnchEnrolmentHandler(IMnchEnrolmentSourceExtractor mnchEnrolmentSourceExtractor, IExtractValidator extractValidator, IMnchEnrolmentLoader mnchEnrolmentLoader, IExtractHistoryRepository extractHistoryRepository)
        {
            _mnchEnrolmentSourceExtractor = mnchEnrolmentSourceExtractor;
            _extractValidator = extractValidator;
            _mnchEnrolmentLoader = mnchEnrolmentLoader;
            _extractHistoryRepository = extractHistoryRepository;
        }

        public async Task<bool> Handle(ExtractMnchEnrolment request, CancellationToken cancellationToken)
        {
            //Extract
            int found = await _mnchEnrolmentSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(MnchEnrolmentExtract), $"{nameof(TempMnchEnrolmentExtract)}s");

            //Load
            int loaded = await _mnchEnrolmentLoader.Load(request.Extract.Id, found, request.DatabaseProtocol.SupportsDifferential);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


            _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected, request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new ExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(MnchEnrolmentExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
