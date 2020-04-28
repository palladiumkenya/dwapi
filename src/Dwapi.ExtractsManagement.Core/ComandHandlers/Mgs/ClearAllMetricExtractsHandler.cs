using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Mgs;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Mgs
{
    public class ClearAllMetricExtractsHandler : IRequestHandler<ClearAllMetricExtracts,bool>
    {
        private readonly IClearMgsExtracts _cleanMgsExtracts;

        public ClearAllMetricExtractsHandler(IClearMgsExtracts cleanMgsExtracts)
        {
            _cleanMgsExtracts = cleanMgsExtracts;
        }

        public async Task<bool> Handle(ClearAllMetricExtracts request, CancellationToken cancellationToken)
        {
            await _cleanMgsExtracts.Clear(request.ExtractIds);
            return true;
        }
    }
}
