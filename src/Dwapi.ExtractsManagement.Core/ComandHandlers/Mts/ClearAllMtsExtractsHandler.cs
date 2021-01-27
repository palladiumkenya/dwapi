using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Mgs;
using Dwapi.ExtractsManagement.Core.Commands.Mts;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Mts;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Mts
{
    public class ClearAllMtsExtractsHandler : IRequestHandler<ClearAllMtsExtracts,bool>
    {
        private readonly IClearMtsExtracts _cleanMgsExtracts;

        public ClearAllMtsExtractsHandler(IClearMtsExtracts cleanMgsExtracts)
        {
            _cleanMgsExtracts = cleanMgsExtracts;
        }

        public async Task<bool> Handle(ClearAllMtsExtracts request, CancellationToken cancellationToken)
        {
            await _cleanMgsExtracts.Clear(request.ExtractIds);
            return true;
        }
    }
}
