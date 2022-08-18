using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Crs;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Crs;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Crs;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Crs;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Crs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Crs;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Crs
{
    public class ExtractClientRegistryExtractHandler : IRequestHandler<ExtractClientRegistryExtract,bool>
    {
        private readonly IClientRegistryExtractSourceExtractor _clientRegistrySourceExtractor;
        private readonly IClientRegistryExtractValidator _clientRegistryValidator;
        private readonly IClientRegistryExtractLoader _clientRegistryLoader;
        private readonly IClearCrsExtracts _clearExtracts;

        public ExtractClientRegistryExtractHandler(IClientRegistryExtractSourceExtractor clientRegistrySourceExtractor, IClientRegistryExtractValidator clientRegistryValidator, IClientRegistryExtractLoader clientRegistryLoader, IClearCrsExtracts clearExtracts)
        {
            _clientRegistrySourceExtractor = clientRegistrySourceExtractor;
            _clientRegistryValidator = clientRegistryValidator;
            _clientRegistryLoader = clientRegistryLoader;
            _clearExtracts = clearExtracts;
        }

        public async Task<bool> Handle(ExtractClientRegistryExtract request, CancellationToken cancellationToken)
        {
            //  clear
            await _clearExtracts.Clean(request.Extract.Id);
            
            //  extract
            int found = await _clientRegistrySourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            DomainEvents.Dispatch(new CrsNotification( new ExtractProgress(nameof(ClientRegistryExtract), "validating...")));
            
            //  await _clientRegistryValidator.Validate();

            DomainEvents.Dispatch(new CrsNotification(new ExtractProgress(nameof(ClientRegistryExtract), "loading...", found, 0, 0, 0, 0)));
            int loaded = await _clientRegistryLoader.Load(request.Extract.Id, found, false);
            DomainEvents.Dispatch(new CrsNotification(new ExtractProgress(nameof(ClientRegistryExtract), "loaded", found, loaded, found-loaded, loaded, 0)));
            //notify loaded
            return true;
        }
    }
}
