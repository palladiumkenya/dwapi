using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Dwh;
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
    public class ExtractPatientStatusHandler :IRequestHandler<ExtractPatientStatus,bool>
    {
        private readonly IPatientStatusSourceExtractor _patientStatusSourceExtractor;
        private readonly IExtractValidator _extractValidator;
        private readonly IPatientStatusLoader _patientStatusLoader;
        private readonly IClearDwhExtracts _clearDwhExtracts;

        public ExtractPatientStatusHandler(IPatientStatusSourceExtractor patientStatusSourceExtractor, IExtractValidator extractValidator, IPatientStatusLoader patientStatusLoader, IClearDwhExtracts clearDwhExtracts)
        {
            _patientStatusSourceExtractor = patientStatusSourceExtractor;
            _extractValidator = extractValidator;
            _patientStatusLoader = patientStatusLoader;
            _clearDwhExtracts = clearDwhExtracts;
        }

        public async Task<bool> Handle(ExtractPatientStatus request, CancellationToken cancellationToken)
        {
            //Extract
            int found = await _patientStatusSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(PatientStatusExtract), $"{nameof(TempPatientStatusExtract)}s");

            //Load
            int loaded = await _patientStatusLoader.Load(request.Extract.Id, found);

            int rejected = found - loaded;

            //notify loaded
            DomainEvents.Dispatch(
                new ExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(PatientStatusExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}