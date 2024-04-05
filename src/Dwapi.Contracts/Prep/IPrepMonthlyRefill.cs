using System;

namespace Dwapi.Contracts.Prep
{
    public interface IPrepMonthlyRefill
    {
        string FacilityName { get; set; }
        string PrepNumber { get; set; }

        string VisitDate { get; set; }
        string BehaviorRiskAssessment { get; set; }
        string SexPartnerHIVStatus { get; set; }
        string SymptomsAcuteHIV { get; set; }
        string AdherenceCounsellingDone { get; set; }
        string ContraIndicationForPrEP { get; set; }
        string PrescribedPrepToday { get; set; }
        string RegimenPrescribed { get; set; }
        string NumberOfMonths { get; set; }
        string CondomsIssued { get; set; }
        int NumberOfCondomsIssued { get; set; }
        string ClientGivenNextAppointment { get; set; }
        DateTime? AppointmentDate { get; set; }
        string ReasonForFailureToGiveAppointment { get; set; }
        DateTime? DateOfLastPrepDose { get; set; }
        
        string RecordUUID { get; set; }
        bool? Voided { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}
