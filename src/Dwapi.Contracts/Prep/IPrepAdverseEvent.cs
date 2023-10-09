using System;

namespace Dwapi.Contracts.Prep
{
    public interface IPrepAdverseEvent
    {
        string FacilityName { get; set; }
        string PrepNumber { get; set; }
        string AdverseEvent { get; set; }
        DateTime? AdverseEventStartDate { get; set; }
        DateTime? AdverseEventEndDate { get; set; }
        string Severity { get; set; }
        DateTime? VisitDate { get; set; }
        string AdverseEventActionTaken { get; set; }
        string AdverseEventClinicalOutcome { get; set; }
        string AdverseEventIsPregnant { get; set; }
        string AdverseEventCause { get; set; }
        string AdverseEventRegimen { get; set; }
        string RecordUUID { get; set; }
        bool? Voided { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}
