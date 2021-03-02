using System;

namespace Dwapi.Contracts.Ct
{
    public interface IOtz
    {
        string FacilityName { get; set; }
        int? VisitID { get; set; }
        DateTime? VisitDate { get; set; }
        DateTime? OTZEnrollmentDate { get; set; }
        string TransferInStatus { get; set; }
        string ModulesPreviouslyCovered { get; set; }
        string ModulesCompletedToday { get; set; }
        string SupportGroupInvolvement { get; set; }
        string Remarks { get; set; }
        string TransitionAttritionReason { get; set; }
        DateTime? OutcomeDate { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}
