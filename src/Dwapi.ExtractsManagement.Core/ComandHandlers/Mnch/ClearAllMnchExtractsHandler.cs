using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Hts;
using Dwapi.ExtractsManagement.Core.Commands.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Mnch;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Mnch
{
    public class ClearAllMnchExtractsHandler : IRequestHandler<ClearAllMnchExtracts,bool>
    {
        private readonly IClearMnchExtracts _clearMnchExtracts;

        public ClearAllMnchExtractsHandler(IClearMnchExtracts clearMnchExtracts)
        {
            _clearMnchExtracts = clearMnchExtracts;
        }

        public async Task<bool> Handle(ClearAllMnchExtracts request, CancellationToken cancellationToken)
        {
            await _clearMnchExtracts.Clear(request.ExtractIds);
            return true;
        }
    }
}
