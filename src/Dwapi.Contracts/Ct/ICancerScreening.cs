using System;

namespace Dwapi.Contracts.Ct
{
    public interface ICancerScreening
    {
       
        string FacilityName { get; set; }
        string VisitType { get; set; }
        int? VisitID { get; set; }
        DateTime? VisitDate { get; set; }
        string SmokesCigarette { get; set; }
        int? NumberYearsSmoked { get; set; }
        int? NumberCigarettesPerDay { get; set; }
        string OtherFormTobacco { get; set; }
        string TakesAlcohol { get; set; }
        string HIVStatus { get; set; }
        string FamilyHistoryOfCa { get; set; }
        string PreviousCaTreatment { get; set; }
        string SymptomsCa { get; set; }
        string CancerType { get; set; }
        string FecalOccultBloodTest { get; set; }
        string TreatmentOccultBlood { get; set; }
        string Colonoscopy { get; set; }
        string TreatmentColonoscopy { get; set; }
        string EUA { get; set; }
        string TreatmentRetinoblastoma     { get; set; }
        string RetinoblastomaGene  { get; set; }
        string TreatmentEUA { get; set; }
        string DRE { get; set; }
        string TreatmentDRE { get; set; }
        string PSA { get; set; }
        string TreatmentPSA { get; set; }
        string VisualExamination { get; set; }
        string TreatmentVE { get; set; }
        string Cytology { get; set; }
        string TreatmentCytology { get; set; }
        string Imaging { get; set; }
        string TreatmentImaging { get; set; }
        string Biopsy { get; set; }
        string TreatmentBiopsy { get; set; }
        string PostTreatmentComplicationCause { get; set; }
        string OtherPostTreatmentComplication { get; set; }
        string ReferralReason { get; set; }
        string ScreeningMethod { get; set; }
        string TreatmentToday { get; set; }
        string ReferredOut { get; set; }
        DateTime? NextAppointmentDate { get; set; }
        string ScreeningType { get; set; }
        string HPVScreeningResult { get; set; }
        string TreatmentHPV { get; set; }
        string VIAScreeningResult { get; set; }
        string VIAVILIScreeningResult { get; set; }
        string VIATreatmentOptions { get; set; }
        string PAPSmearScreeningResult { get; set; }
        string TreatmentPapSmear { get; set; }
        string ReferalOrdered { get; set; }
        string Colposcopy { get; set; }
        string TreatmentColposcopy { get; set; }
        string BiopsyCINIIandAbove { get; set; }
        string BiopsyCINIIandBelow { get; set; }
        string BiopsyNotAvailable { get; set; }
        string CBE { get; set; }
        string TreatmentCBE { get; set; }
        string Ultrasound { get; set; }
        string TreatmentUltraSound { get; set; }
        string IfTissueDiagnosis { get; set; }
        DateTime? DateTissueDiagnosis { get; set; }
        string ReasonNotDone { get; set; }
        DateTime? FollowUpDate { get; set; }
        string Referred { get; set; }
        string ReasonForReferral { get; set; }

        string RecordUUID { get; set; }
        bool? Voided { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}