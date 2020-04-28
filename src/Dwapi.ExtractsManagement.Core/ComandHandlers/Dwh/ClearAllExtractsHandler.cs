using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Dwh
{
    public class ClearAllExtractsHandler : IRequestHandler<ClearAllExtracts,bool>
    {
        private readonly IClearDwhExtracts _clearDwhExtracts;

        public ClearAllExtractsHandler(IClearDwhExtracts clearDwhExtracts)
        {
            _clearDwhExtracts = clearDwhExtracts;
        }

        public async Task<bool> Handle(ClearAllExtracts request, CancellationToken cancellationToken)
        {
            await _clearDwhExtracts.Clear(request.ExtractIds);
            return true;
        }
    }
}