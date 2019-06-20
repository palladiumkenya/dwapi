using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Dwh
{
    public class ProcessPatientArtExcludedHandler : IRequestHandler<ProcessExcludedArt, bool>
    {
        public Task<bool> Handle(ProcessExcludedArt request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
