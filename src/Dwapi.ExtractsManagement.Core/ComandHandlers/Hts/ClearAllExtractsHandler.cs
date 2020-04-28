using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Hts;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Hts
{
    public class ClearAllHTSExtractsHandler : IRequestHandler<ClearAllHTSExtracts,bool>
    {
        private readonly IClearHtsExtracts _clearHtsExtracts;

        public ClearAllHTSExtractsHandler(IClearHtsExtracts clearHtsExtracts)
        {
            _clearHtsExtracts = clearHtsExtracts;
        }

        public async Task<bool> Handle(ClearAllHTSExtracts request, CancellationToken cancellationToken)
        {
            await _clearHtsExtracts.Clear(request.ExtractIds);
            return true;
        }
    }
}
