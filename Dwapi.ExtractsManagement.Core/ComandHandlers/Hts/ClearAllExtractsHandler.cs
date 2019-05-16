using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Hts;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Hts
{
    public class ClearAllHTSExtractsHandler : IRequestHandler<ClearAllHTSExtracts,bool>
    {
        private readonly ICleanHtsExtracts _clearDwhExtracts;

        public ClearAllHTSExtractsHandler(ICleanHtsExtracts clearDwhExtracts)
        {
            _clearDwhExtracts = clearDwhExtracts;
        }

        public async Task<bool> Handle(ClearAllHTSExtracts request, CancellationToken cancellationToken)
        {
            await _clearDwhExtracts.Clean(request.ExtractIds);
            return true;
        }
    }
}
