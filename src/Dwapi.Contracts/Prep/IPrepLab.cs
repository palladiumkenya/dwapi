using System;

namespace Dwapi.Contracts.Prep
{
    public interface IPrepLab
    {
        string FacilityName { get; set; }
        string PrepNumber { get; set; }
        string HtsNumber { get; set; }
        int? VisitID { get; set; }
        string TestName { get; set; }
        string TestResult { get; set; }
        DateTime? SampleDate { get; set; }
        DateTime? TestResultDate { get; set; }
        string Reason { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}
