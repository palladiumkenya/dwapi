using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.DTOs
{
    public class ExtractProfile
    {
        public DbExtract Extract { get; set; }
        public DbProtocol DatabaseProtocol { get; set; }
    }
}