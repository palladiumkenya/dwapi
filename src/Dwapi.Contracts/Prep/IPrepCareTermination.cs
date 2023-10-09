using System;

namespace Dwapi.Contracts.Prep
{
    public interface IPrepCareTermination
    {
        string FacilityName { get; set; }
        string PrepNumber { get; set; }
        string HtsNumber { get; set; }
        DateTime? ExitDate { get; set; }
        string ExitReason { get; set; }
        DateTime? DateOfLastPrepDose { get; set; }
        string RecordUUID { get; set; }
        bool? Voided { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}
