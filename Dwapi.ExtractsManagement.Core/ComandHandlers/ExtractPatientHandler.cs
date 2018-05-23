using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers
{
    public class ExtractPatientHandler :IRequestHandler<ExtractPatient>
    {
        public Task Handle(ExtractPatient request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}