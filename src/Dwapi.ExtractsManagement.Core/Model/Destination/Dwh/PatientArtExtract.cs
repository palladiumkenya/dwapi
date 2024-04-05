using System;
using Dwapi.Contracts.Ct;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Dwh
{

    public class PatientArtExtract : ClientExtract,IArt
    {
        public string FacilityName { get; set; }
        public DateTime? DOB { get; set; }
        public decimal? AgeEnrollment { get; set; }
        public decimal? AgeARTStart { get; set; }
        public decimal? AgeLastVisit { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string PatientSource { get; set; }
        public string Gender { get; set; }
        public DateTime? StartARTDate { get; set; }
        public DateTime? PreviousARTStartDate { get; set; }
        public string PreviousARTRegimen { get; set; }
        public DateTime? StartARTAtThisFacility { get; set; }
        public string StartRegimen { get; set; }
        public string StartRegimenLine { get; set; }
        public DateTime? LastARTDate { get; set; }
        public string LastRegimen { get; set; }
        public string LastRegimenLine { get; set; }
        public decimal? Duration { get; set; }
        public DateTime? ExpectedReturn { get; set; }
        public string Provider { get; set; }
        public DateTime? LastVisit { get; set; }
        public string ExitReason { get; set; }
        public DateTime? ExitDate { get; set; }


        public string PreviousARTUse { get; set; }
        public string  PreviousARTPurpose { get; set; }
        public string RecordUUID { get; set; }
            public bool? Voided { get; set; }
        public DateTime?  DateLastUsed { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}
