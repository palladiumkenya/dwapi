using System.Linq;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Commands.Cbs;
using Dwapi.SharedKernel.Enum;

namespace Dwapi.Models
{
    public class LoadExtracts
    {
        public LoadFromEmrCommand LoadFromEmrCommand { get; set; }
        public ExtractMasterPatientIndex ExtractMpi { get; set; }
        public bool LoadMpi { get; set; }
        public EmrSetup EmrSetup { get; set; }

        public bool IsValid()
        {
            return null != LoadFromEmrCommand && LoadFromEmrCommand.Extracts.Any();
        }
    }
}
