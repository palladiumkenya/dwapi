using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.DTOs;
using Dwapi.SettingsManagement.Core.Model;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.Commands
{
    public class LoadMtsFromEmrCommand : IRequest<bool>
    {
        public List<Extract> Extracts { get; set; }
        public DatabaseProtocol DatabaseProtocol { get; set; }
    }
}
