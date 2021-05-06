using System;
using System.Collections.Generic;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.Commands.Mts
{
    public class ClearAllMtsExtracts:IRequest<bool>
    {
        public List<Guid> ExtractIds { get;  }

        public ClearAllMtsExtracts(List<Guid> extractIds)
        {
            ExtractIds = extractIds;
        }
    }
}
