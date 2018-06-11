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
    public class ExtractPatientPharmacyHandler :IRequestHandler<ExtractPatientPharmacy,bool>
    {
        private readonly IPatientPharmacySourceExtractor _patientPharmacySourceExtractor;
        private readonly IExtractValidator _extractValidator;
        private readonly IPatientPharmacyLoader _patientPharmacyLoader;
        private readonly IClearDwhExtracts _clearDwhExtracts;

        public ExtractPatientPharmacyHandler(IPatientPharmacySourceExtractor patientPharmacySourceExtractor, IExtractValidator extractValidator, IPatientPharmacyLoader patientPharmacyLoader, IClearDwhExtracts clearDwhExtracts)
        {
            _patientPharmacySourceExtractor = patientPharmacySourceExtractor;
            _extractValidator = extractValidator;
            _patientPharmacyLoader = patientPharmacyLoader;
            _clearDwhExtracts = clearDwhExtracts;
        }

        public async Task<bool> Handle(ExtractPatientPharmacy request, CancellationToken cancellationToken)
        {
            //Extract
            int found = await _patientPharmacySourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //Validate
            await _extractValidator.Validate(found, nameof(PatientPharmacyExtract), $"{nameof(TempPatientPharmacyExtract)}s");

            //Load
            int loaded = await _patientPharmacyLoader.Load(found);

            int rejected = found - loaded;

            //notify loaded
            DomainEvents.Dispatch(
                new ExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(PatientPharmacyExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}