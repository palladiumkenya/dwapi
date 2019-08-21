using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Hts;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Hts
{
    public class ClearAllHTSExtractsHandler : IRequestHandler<ClearAllHTSExtracts,bool>
    {
        private readonly IClearHtsExtracts _cleanHtsExtracts;

        public ClearAllHTSExtractsHandler(IClearHtsExtracts cleanHtsExtracts)
        {
            _cleanHtsExtracts = cleanHtsExtracts;
        }

        public async Task<bool> Handle(ClearAllHTSExtracts request, CancellationToken cancellationToken)
        {
            await _cleanHtsExtracts.Clear(request.ExtractIds);
            return true;
        }
    }
}
