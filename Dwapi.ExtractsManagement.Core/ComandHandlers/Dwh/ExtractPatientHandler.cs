using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors;
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
    public class ExtractPatientHandler :IRequestHandler<ExtractPatient,bool>
    {
        private readonly IPatientSourceExtractor _patientSourceExtractor;
        private readonly IExtractValidator _extractValidator;
        private readonly IPatientLoader _patientLoader;
        private readonly IClearDwhExtracts _clearDwhExtracts;

        public ExtractPatientHandler(IPatientSourceExtractor patientSourceExtractor, IExtractValidator extractValidator, IPatientLoader patientLoader, IClearDwhExtracts clearDwhExtracts)
        {
            _patientSourceExtractor = patientSourceExtractor;
            _extractValidator = extractValidator;
            _patientLoader = patientLoader;
            _clearDwhExtracts = clearDwhExtracts;
        }

        public async Task<bool> Handle(ExtractPatient request, CancellationToken cancellationToken)
        {
            //clear
            int count = await _clearDwhExtracts.Clear(request.Extract.Id);

            //Extract
            int found = await _patientSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(PatientExtract), $"{nameof(TempPatientExtract)}s");

            //Load
            int loaded = await _patientLoader.Load(request.Extract.Id, found);

            int rejected = found - loaded;

            //notify loaded
            DomainEvents.Dispatch(
                new ExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(PatientExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}