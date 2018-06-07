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
    public class ExtractPatientVisitHandler :IRequestHandler<ExtractPatientVisit,bool>
    {
        private readonly IPatientVisitSourceExtractor _patientVisitSourceExtractor;
        private readonly IExtractValidator _extractValidator;
        private readonly IPatientVisitLoader _patientVisitLoader;
        private readonly IClearDwhExtracts _clearDwhExtracts;

        public ExtractPatientVisitHandler(IPatientVisitSourceExtractor patientVisitSourceExtractor, IExtractValidator extractValidator, IPatientVisitLoader patientVisitLoader, IClearDwhExtracts clearDwhExtracts)
        {
            _patientVisitSourceExtractor = patientVisitSourceExtractor;
            _extractValidator = extractValidator;
            _patientVisitLoader = patientVisitLoader;
            _clearDwhExtracts = clearDwhExtracts;
        }

        public async Task<bool> Handle(ExtractPatientVisit request, CancellationToken cancellationToken)
        {
            //clear
            int count = await _clearDwhExtracts.Clear();

            //Extract
            int found = await _patientVisitSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //Validate
            await _extractValidator.Validate(found, nameof(PatientVisitExtract), $"{nameof(TempPatientVisitExtract)}s");

            //Load
            int loaded = await _patientVisitLoader.Load(found);

            int rejected = found - loaded;

            //notify loaded
            DomainEvents.Dispatch(
                new ExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(PatientVisitExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}