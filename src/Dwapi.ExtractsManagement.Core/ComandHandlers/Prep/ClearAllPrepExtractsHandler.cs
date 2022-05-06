using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Hts;
using Dwapi.ExtractsManagement.Core.Commands.Prep;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Prep;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Prep
{
    public class ClearAllPrepExtractsHandler : IRequestHandler<ClearAllPrepExtracts,bool>
    {
        private readonly IClearPrepExtracts _clearPrepExtracts;

        public ClearAllPrepExtractsHandler(IClearPrepExtracts clearPrepExtracts)
        {
            _clearPrepExtracts = clearPrepExtracts;
        }

        public async Task<bool> Handle(ClearAllPrepExtracts request, CancellationToken cancellationToken)
        {
            await _clearPrepExtracts.Clear(request.ExtractIds);
            return true;
        }
    }
}
