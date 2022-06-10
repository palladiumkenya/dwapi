using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.DTOs;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.Commands
{
    public class LoadPrepFromEmrCommand : IRequest<bool>
    {
        public IList<ExtractProfile> Extracts { get; set; }
    }
}