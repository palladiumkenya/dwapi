using System;

namespace Dwapi.Contracts.Mnch
{
    public interface IPncVisit
    {
        string PatientMnchID { get; set; }
        int? VisitID { get; set; }
        DateTime? VisitDate { get; set; }
        string PNCRegisterNumber { get; set; }
        int? PNCVisitNo { get; set; }
        DateTime? DeliveryDate { get; set; }
        string ModeOfDelivery { get; set; }
        string PlaceOfDelivery { get; set; }
        decimal? Height { get; set; }
        decimal? Weight { get; set; }
        decimal? Temp { get; set; }
        int? PulseRate { get; set; }
        int? RespiratoryRate { get; set; }
        decimal? OxygenSaturation { get; set; }
        int? MUAC { get; set; }
        int? BP { get; set; }
        string BreastExam { get; set; }
        string GeneralCondition { get; set; }
        string HasPallor { get; set; }
        string Pallor { get; set; }
        string Breast { get; set; }
        string PPH { get; set; }
        string CSScar { get; set; }
        string UterusInvolution { get; set; }
        string Episiotomy { get; set; }
        string Lochia { get; set; }
        string Fistula { get; set; }
        string MaternalComplications { get; set; }
        string TBScreening { get; set; }
        string ClientScreenedCACx { get; set; }
        string CACxScreenMethod { get; set; }
        string CACxScreenResults { get; set; }
        string PriorHIVStatus { get; set; }
        string HIVTestingDone { get; set; }
        string HIVTest1 { get; set; }
        string HIVTest1Result { get; set; }
        string HIVTest2 { get; set; }
        string HIVTest2Result { get; set; }
        string HIVTestFinalResult { get; set; }
        string InfantProphylaxisGiven { get; set; }
        string MotherProphylaxisGiven { get; set; }
        string CoupleCounselled { get; set; }
        string PartnerHIVTestingPNC { get; set; }
        string PartnerHIVResultPNC { get; set; }
        string CounselledOnFP { get; set; }
        string ReceivedFP { get; set; }
        string HaematinicsGiven { get; set; }
        string DeliveryOutcome { get; set; }
        string BabyConditon { get; set; }
        string BabyFeeding { get; set; }
        string UmbilicalCord { get; set; }
        string Immunization { get; set; }
        string InfantFeeding { get; set; }
        string PreventiveServices { get; set; }
        string ReferredFrom { get; set; }
        string ReferredTo { get; set; }
        DateTime? NextAppointmentPNC { get; set; }
        string ClinicalNotes { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}
