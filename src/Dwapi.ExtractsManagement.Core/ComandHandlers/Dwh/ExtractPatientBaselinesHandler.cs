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
    public class ExtractPatientBaselinesHandler :IRequestHandler<ExtractPatientBaselines,bool>
    {
        private readonly IPatientBaselinesSourceExtractor _patientBaselinesSourceExtractor;
        private readonly IExtractValidator _extractValidator;
        private readonly IPatientBaselinesLoader _patientBaselinesLoader;
        private readonly IClearDwhExtracts _clearDwhExtracts;
        private readonly IExtractHistoryRepository _extractHistoryRepository;

        public ExtractPatientBaselinesHandler(IPatientBaselinesSourceExtractor patientBaselinesSourceExtractor, IExtractValidator extractValidator, IPatientBaselinesLoader patientBaselinesLoader, IClearDwhExtracts clearDwhExtracts, IExtractHistoryRepository extractHistoryRepository)
        {
            _patientBaselinesSourceExtractor = patientBaselinesSourceExtractor;
            _extractValidator = extractValidator;
            _patientBaselinesLoader = patientBaselinesLoader;
            _clearDwhExtracts = clearDwhExtracts;
            _extractHistoryRepository = extractHistoryRepository;
        }

        public async Task<bool> Handle(ExtractPatientBaselines request, CancellationToken cancellationToken)
        {
            //Extract
            int found = await _patientBaselinesSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(PatientBaselinesExtract), $"{nameof(TempPatientBaselinesExtract)}s");

            //Load
            int loaded = await _patientBaselinesLoader.Load(request.Extract.Id, found, false);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


            _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected, request.Extract);
            //notify loaded
            DomainEvents.Dispatch(
                new ExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(PatientBaselinesExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
