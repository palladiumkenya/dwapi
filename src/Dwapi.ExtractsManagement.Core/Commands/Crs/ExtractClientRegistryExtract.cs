using Dwapi.SharedKernel.Model;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.Commands.Crs
{
    public class ExtractClientRegistryExtract : IRequest<bool>
    {
        public DbExtract Extract { get; set; }
        public DbProtocol DatabaseProtocol { get; set; }
    }
}