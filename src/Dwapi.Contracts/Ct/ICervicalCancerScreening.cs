using System;

namespace Dwapi.Contracts.Ct
{
    public interface ICervicalCancerScreening
    {
       
        string FacilityName { get; set; }
        int? VisitID  { get; set; }
        DateTime? VisitDate  { get; set; }
        string VisitType { get; set; }
        string ScreeningMethod { get; set; }
        string TreatmentToday { get; set; }
        string ReferredOut { get; set; }
        DateTime? NextAppointmentDate { get; set; }
        string ScreeningType { get; set; }
        string ScreeningResult { get; set; }
        string PostTreatmentComplicationCause  { get; set; }
        string OtherPostTreatmentComplication  { get; set; }
        string ReferralReason  { get; set; }
        string RecordUUID { get; set; }
         bool? Voided { get; set; }

        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}