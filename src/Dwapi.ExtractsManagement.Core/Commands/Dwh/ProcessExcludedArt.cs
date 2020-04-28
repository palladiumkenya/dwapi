using Dwapi.SharedKernel.Model;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Dwh
{
   public class ProcessExcludedArt : IRequest<bool>
    {
        public DbExtract Extract { get; set; }
        public DbProtocol DatabaseProtocol { get; set; }
    }
}
