using System;
using System.Collections.Generic;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.Commands.Mnch
{
    public class ClearAllMnchExtracts:IRequest<bool>
    {
        public List<Guid> ExtractIds { get;  }

        public ClearAllMnchExtracts(List<Guid> extractIds)
        {
            ExtractIds = extractIds;
        }
    }
}
