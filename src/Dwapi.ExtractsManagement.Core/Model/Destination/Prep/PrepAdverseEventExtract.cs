using System;
using Dwapi.Contracts.Prep;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Prep
{
    public class PrepAdverseEventExtract : PrepClientExtract,IPrepAdverseEvent
    {
        public string FacilityName { get; set; }
        public string PrepNumber { get; set; }
        public string AdverseEvent { get; set; }
        public DateTime? AdverseEventStartDate { get; set; }
        public DateTime? AdverseEventEndDate { get; set; }
        public string Severity { get; set; }
        public DateTime? VisitDate { get; set; }
        public string AdverseEventActionTaken { get; set; }
        public string AdverseEventClinicalOutcome { get; set; }
        public string AdverseEventIsPregnant { get; set; }
        public string AdverseEventCause { get; set; }
        public string AdverseEventRegimen { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }
    }
}
