using System;
using System.Collections.Generic;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.Commands.Hts
{
    public class ClearAllHTSExtracts:IRequest<bool>
    {
        public List<Guid> ExtractIds { get;  }

        public ClearAllHTSExtracts(List<Guid> extractIds)
        {
            ExtractIds = extractIds;
        }
    }
}
