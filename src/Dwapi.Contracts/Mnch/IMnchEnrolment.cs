using System;

namespace Dwapi.Contracts.Mnch
{
    public interface IMnchEnrolment
    {
        string PatientMnchID { get; set; }
        string FacilityName { get; set; }
        string ServiceType { get; set; }
        DateTime? EnrollmentDateAtMnch { get; set; }
        DateTime? MnchNumber { get; set; }
        DateTime? FirstVisitAnc { get; set; }
        string Parity { get; set; }
        int Gravidae { get; set; }
        DateTime? LMP { get; set; }
        DateTime? EDDFromLMP { get; set; }
        string HIVStatusBeforeANC { get; set; }
        DateTime? HIVTestDate { get; set; }
        string PartnerHIVStatus { get; set; }
        DateTime? PartnerHIVTestDate { get; set; }
        string BloodGroup { get; set; }
        string StatusAtMnch { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}
