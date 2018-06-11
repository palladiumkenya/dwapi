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
    public class ExtractPatientArtHandler :IRequestHandler<ExtractPatientArt,bool>
    {
        private readonly IPatientArtSourceExtractor _patientArtSourceExtractor;
        private readonly IExtractValidator _extractValidator;
        private readonly IPatientArtLoader _patientArtLoader;
        private readonly IClearDwhExtracts _clearDwhExtracts;

        public ExtractPatientArtHandler(IPatientArtSourceExtractor patientArtSourceExtractor, IExtractValidator extractValidator, IPatientArtLoader patientArtLoader, IClearDwhExtracts clearDwhExtracts)
        {
            _patientArtSourceExtractor = patientArtSourceExtractor;
            _extractValidator = extractValidator;
            _patientArtLoader = patientArtLoader;
            _clearDwhExtracts = clearDwhExtracts;
        }

        public async Task<bool> Handle(ExtractPatientArt request, CancellationToken cancellationToken)
        {
            //Extract
            int found = await _patientArtSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //Validate
            await _extractValidator.Validate(found, nameof(PatientArtExtract), $"{nameof(TempPatientArtExtract)}s");

            //Load
            int loaded = await _patientArtLoader.Load(found);

            int rejected = found - loaded;

            //notify loaded
            DomainEvents.Dispatch(
                new ExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(PatientArtExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}