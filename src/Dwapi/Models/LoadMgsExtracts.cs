using Dwapi.ExtractsManagement.Core.Commands;

namespace Dwapi.Models
{
    public class LoadMgsExtracts
    {
        public LoadMgsFromEmrCommand LoadMgsFromEmrCommand { get; set; }
    }
    public class LoadMtsExtracts
    {
        public LoadMtsFromEmrCommand LoadMtsFromEmrCommand { get; set; }
    }
}
