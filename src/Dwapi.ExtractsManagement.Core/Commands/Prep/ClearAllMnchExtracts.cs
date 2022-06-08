using System;
using System.Collections.Generic;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.Commands.Prep
{
    public class ClearAllPrepExtracts:IRequest<bool>
    {
        public List<Guid> ExtractIds { get;  }

        public ClearAllPrepExtracts(List<Guid> extractIds)
        {
            ExtractIds = extractIds;
        }
    }
}
