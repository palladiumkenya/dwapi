using System;

namespace Dwapi.Contracts.Mnch
{
    public interface ICwcVisit
    {
        string FacilityName { get; set; }
        string PatientMnchID { get; set; }
        DateTime? VisitDate { get; set; }
        int? VisitID { get; set; }
        decimal? Height { get; set; }
        decimal? Weight { get; set; }
        decimal? Temp { get; set; }
        int? PulseRate { get; set; }
        int? RespiratoryRate { get; set; }
        decimal? OxygenSaturation { get; set; }
        int? MUAC { get; set; }
        string WeightCategory { get; set; }
        string Stunted { get; set; }
        string InfantFeeding { get; set; }
        string MedicationGiven { get; set; }
        string TBAssessment { get; set; }
        string MNPsSupplementation { get; set; }
        string Immunization { get; set; }
        string DangerSigns { get; set; }
        string Milestones { get; set; }
        string VitaminA { get; set; }
        string Disability { get; set; }
        string ReceivedMosquitoNet { get; set; }
        string Dewormed { get; set; }
        string ReferredFrom { get; set; }
        string ReferredTo { get; set; }
        string ReferralReasons { get; set; }
        string FollowUP { get; set; }
        DateTime? NextAppointment { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}
