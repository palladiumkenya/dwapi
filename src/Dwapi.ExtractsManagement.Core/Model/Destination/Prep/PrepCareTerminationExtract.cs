using System;
using Dwapi.Contracts.Prep;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Prep
{
    public class PrepCareTerminationExtract : PrepClientExtract,IPrepCareTermination
    {
        public string FacilityName { get; set; }
        public string PrepNumber { get; set; }
        public string HtsNumber { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }
        public DateTime? DateOfLastPrepDose { get; set; }
    }
}
