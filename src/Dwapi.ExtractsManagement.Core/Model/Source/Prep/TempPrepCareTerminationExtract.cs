using System;
using Dwapi.Contracts.Prep;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Prep
{
    public class TempPrepCareTerminationExtract:TempExtract,IPrepCareTermination
    {
        public string FacilityName { get; set; }
        public string PrepNumber { get; set; }
        public string HtsNumber { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }
        public DateTime? DateOfLastPrepDose { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}
