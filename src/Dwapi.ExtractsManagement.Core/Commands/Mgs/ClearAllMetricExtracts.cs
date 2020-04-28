using System;
using System.Collections.Generic;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.Commands.Mgs
{
    public class ClearAllMetricExtracts:IRequest<bool>
    {
        public List<Guid> ExtractIds { get;  }

        public ClearAllMetricExtracts(List<Guid> extractIds)
        {
            ExtractIds = extractIds;
        }
    }
}
