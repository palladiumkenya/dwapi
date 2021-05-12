using System.Linq;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Commands.Cbs;

namespace Dwapi.Models
{
    public class LoadMnchExtracts
    {
        public LoadMnchFromEmrCommand LoadMnchFromEmrCommand { get; set; }
        public ExtractMasterPatientIndex ExtractMpi { get; set; }
        public bool LoadMpi { get; set; }

        public bool IsValid()
        {
            return null != LoadMnchFromEmrCommand && LoadMnchFromEmrCommand.Extracts.Any();
        }
    }
}
