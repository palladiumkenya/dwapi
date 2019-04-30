using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Hts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Hts
{
    public class ExtractHTSClientPartnerHandler :IRequestHandler<ExtractHTSClientPartner,bool>
    {
        private readonly IHTSClientPartnerSourceExtractor _patientLaboratorySourceExtractor;
        private readonly IHtsExtractValidator _extractValidator;
        private readonly IHTSClientPartnerLoader _patientLaboratoryLoader;


        public ExtractHTSClientPartnerHandler(IHTSClientPartnerSourceExtractor patientLaboratorySourceExtractor, IHtsExtractValidator extractValidator, IHTSClientPartnerLoader patientLaboratoryLoader)
        {
            _patientLaboratorySourceExtractor = patientLaboratorySourceExtractor;
            _extractValidator = extractValidator;
            _patientLaboratoryLoader = patientLaboratoryLoader;
        }

        public async Task<bool> Handle(ExtractHTSClientPartner request, CancellationToken cancellationToken)
        {
            //Extract
            int found = await _patientLaboratorySourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //Validate

            await _extractValidator.Validate(request.Extract.Id, found, nameof(HTSClientPartnerExtract), $"{nameof(TempHTSClientPartnerExtract)}s");

            //Load
            int loaded = await _patientLaboratoryLoader.Load(request.Extract.Id, found);

            int rejected = found - loaded;

            //notify loaded
            DomainEvents.Dispatch(
                new ExtractActivityNotification(request.Extract.Id, new ExtractProgress(
                    nameof(HTSClientPartnerExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
