using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Cbs
{
    public class ExtractMasterPatientIndexHandler : IRequestHandler<ExtractMasterPatientIndex,bool>
    {
        private readonly IMasterPatientIndexSourceExtractor _patientSourceExtractor;
        private readonly IMasterPatientIndexValidator _patientValidator;
        private readonly IMasterPatientIndexLoader _patientLoader;
        private readonly IClearCbsExtracts _clearExtracts;

        public ExtractMasterPatientIndexHandler(IMasterPatientIndexSourceExtractor patientSourceExtractor, IMasterPatientIndexValidator patientValidator, IMasterPatientIndexLoader patientLoader, IClearCbsExtracts clearExtracts)
        {
            _patientSourceExtractor = patientSourceExtractor;
            _patientValidator = patientValidator;
            _patientLoader = patientLoader;
            _clearExtracts = clearExtracts;
        }

        public async Task<bool> Handle(ExtractMasterPatientIndex request, CancellationToken cancellationToken)
        {
            //  clear
            await _clearExtracts.Clean(request.Extract.Id);
            
            //  extract
            int found = await _patientSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            DomainEvents.Dispatch(new CbsNotification( new ExtractProgress(nameof(MasterPatientIndex), "validating...")));
            
            //  await _patientValidator.Validate();

            DomainEvents.Dispatch(new CbsNotification(new ExtractProgress(nameof(MasterPatientIndex), "loading...", found, 0, 0, 0, 0)));
            int loaded = await _patientLoader.Load(request.Extract.Id, found, false);
            DomainEvents.Dispatch(new CbsNotification(new ExtractProgress(nameof(MasterPatientIndex), "loaded", found, loaded, found-loaded, loaded, 0)));
            //notify loaded
            return true;
        }
    }
}
