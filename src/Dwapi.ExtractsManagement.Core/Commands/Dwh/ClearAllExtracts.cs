using System;
using System.Collections.Generic;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.Commands.Dwh
{
    public class ClearAllExtracts:IRequest<bool>
    {
        public List<Guid> ExtractIds { get;  }

        public ClearAllExtracts(List<Guid> extractIds)
        {
            ExtractIds = extractIds;
        }
    }
}