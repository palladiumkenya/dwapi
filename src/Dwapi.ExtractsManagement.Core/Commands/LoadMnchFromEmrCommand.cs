using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.DTOs;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.Commands
{
    public class LoadMnchFromEmrCommand : IRequest<bool>
    {
        public bool LoadChangesOnly { get; set; }
        public IList<ExtractProfile> Extracts { get; set; }

    }
}
