using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Contracts.Ct;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    [Table("vTempCancerScreeningExtractErrorSummary")]
    public class TempCancerScreeningExtractErrorSummary  : TempExtractErrorSummary,ICancerScreening
    {
       public string FacilityName { get; set; }
        public string VisitType { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string SmokesCigarette { get; set; }
        public int? NumberYearsSmoked { get; set; }
        public int? NumberCigarettesPerDay { get; set; }
        public string OtherFormTobacco { get; set; }
        public string TakesAlcohol { get; set; }
        public string HIVStatus { get; set; }
        public string FamilyHistoryOfCa { get; set; }
        public string PreviousCaTreatment { get; set; }
        public string SymptomsCa { get; set; }
        public string CancerType { get; set; }
        public string FecalOccultBloodTest { get; set; }
        public string TreatmentOccultBlood { get; set; }
        public string Colonoscopy { get; set; }
        public string TreatmentColonoscopy { get; set; }
        public string EUA { get; set; }
        public string TreatmentRetinoblastoma     { get; set; }
        public string RetinoblastomaGene  { get; set; }
        public string TreatmentEUA { get; set; }
        public string DRE { get; set; }
        public string TreatmentDRE { get; set; }
        public string PSA { get; set; }
        public string TreatmentPSA { get; set; }
        public string VisualExamination { get; set; }
        public string TreatmentVE { get; set; }
        public string Cytology { get; set; }
        public string TreatmentCytology { get; set; }
        public string Imaging { get; set; }
        public string TreatmentImaging { get; set; }
        public string Biopsy { get; set; }
        public string TreatmentBiopsy { get; set; }
        public string PostTreatmentComplicationCause { get; set; }
        public string OtherPostTreatmentComplication { get; set; }
        public string ReferralReason { get; set; }
        public string ScreeningMethod { get; set; }
        public string TreatmentToday { get; set; }
        public string ReferredOut { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public string ScreeningType { get; set; }
        public string HPVScreeningResult { get; set; }
        public string TreatmentHPV { get; set; }
        public string VIAScreeningResult { get; set; }
        public string VIAVILIScreeningResult { get; set; }
        public string VIATreatmentOptions { get; set; }
        public string PAPSmearScreeningResult { get; set; }
        public string TreatmentPapSmear { get; set; }
        public string ReferalOrdered { get; set; }
        public string Colposcopy { get; set; }
        public string TreatmentColposcopy { get; set; }
        public string BiopsyCINIIandAbove { get; set; }
        public string BiopsyCINIIandBelow { get; set; }
        public string BiopsyNotAvailable { get; set; }
        public string CBE { get; set; }
        public string TreatmentCBE { get; set; }
        public string Ultrasound { get; set; }
        public string TreatmentUltraSound { get; set; }
        public string IfTissueDiagnosis { get; set; }
        public DateTime? DateTissueDiagnosis { get; set; }
        public string ReasonNotDone { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public string Referred { get; set; }
        public string ReasonForReferral { get; set; }
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}