using Dwapi.ExtractsManagement.Core.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Dwapi.ExtractsManagement.Core.Commands
{
    public class LoadFromEmrCommand : IRequest<bool>
    {
        public bool LoadChangesOnly { get; set; }
        public IList<ExtractProfile> Extracts { get; set; }
    }
}
