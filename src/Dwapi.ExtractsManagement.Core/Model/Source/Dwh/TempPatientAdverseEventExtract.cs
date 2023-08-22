using System;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{

    public class TempPatientAdverseEventExtract : TempExtract
    {
        public string AdverseEvent { get; set; }
        public DateTime? AdverseEventStartDate { get; set; }
        public DateTime? AdverseEventEndDate { get; set; }
        public string Severity { get; set; }
        public string AdverseEventRegimen { get; set; }
        public string AdverseEventCause { get; set; }
        public string AdverseEventClinicalOutcome { get; set; }
        public string AdverseEventActionTaken { get; set; }
        public bool? AdverseEventIsPregnant { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }


        public bool HasData()
        {
            return !string.IsNullOrWhiteSpace(PatientID) &&
                   PatientPK.HasValue &&
                   SiteCode.HasValue;
        }
    }
}
