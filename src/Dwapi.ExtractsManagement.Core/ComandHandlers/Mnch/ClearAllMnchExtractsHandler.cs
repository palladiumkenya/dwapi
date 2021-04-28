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
        private readonly IClearMnchExtracts _clearHtsExtracts;

        public ClearAllMnchExtractsHandler(IClearMnchExtracts clearHtsExtracts)
        {
            _clearHtsExtracts = clearHtsExtracts;
        }

        public async Task<bool> Handle(ClearAllMnchExtracts request, CancellationToken cancellationToken)
        {
            await _clearHtsExtracts.Clear(request.ExtractIds);
            return true;
        }
    }
}
