using Dwapi.SharedKernel.Model;
using MediatR;
using System.Collections.Generic;

namespace Dwapi.ExtractsManagement.Core.Commands
{
    public class LoadFromEmrCommand : IRequest<bool>
    {
        public IList<DbExtract> Extracts { get; set; }
        public DbProtocol DatabaseProtocol { get; set; }
    }
}
