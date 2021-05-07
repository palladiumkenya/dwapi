using System;

namespace Dwapi.Contracts.Mnch
{
    public interface IMatVisit
    {
        string PatientMnchID { get; set; }
        string FacilityName { get; set; }
        int? VisitID { get; set; }
        DateTime? VisitDate { get; set; }
        string AdmissionNumber { get; set; }
        int? ANCVisits { get; set; }
        DateTime? DateOfDelivery { get; set; }
        int? DurationOfDelivery { get; set; }
        int? GestationAtBirth { get; set; }
        string ModeOfDelivery { get; set; }
        string PlacentaComplete { get; set; }
        string UterotonicGiven { get; set; }
        string VaginalExamination { get; set; }
        int? BloodLoss { get; set; }
        string BloodLossVisual { get; set; }
        string ConditonAfterDelivery { get; set; }
        DateTime? MaternalDeath { get; set; }
        string DeliveryComplications { get; set; }
        int? NoBabiesDelivered { get; set; }
        int? BabyBirthNumber { get; set; }
        string SexBaby { get; set; }
        string BirthWeight { get; set; }
        string BirthOutcome { get; set; }
        string BirthWithDeformity { get; set; }
        string TetracyclineGiven { get; set; }
        string InitiatedBF { get; set; }
        int? ApgarScore1 { get; set; }
        int? ApgarScore5 { get; set; }
        int? ApgarScore10 { get; set; }
        string KangarooCare { get; set; }
        string ChlorhexidineApplied { get; set; }
        string VitaminKGiven { get; set; }
        string StatusBabyDischarge { get; set; }
        string MotherDischargeDate { get; set; }
        string SyphilisTestResults { get; set; }
        string HIVStatusLastANC { get; set; }
        string HIVTestingDone { get; set; }
        string HIVTest1 { get; set; }
        string HIV1Results { get; set; }
        string HIVTest2 { get; set; }
        string HIV2Results { get; set; }
        string HIVTestFinalResult { get; set; }
        string OnARTANC { get; set; }
        string BabyGivenProphylaxis { get; set; }
        string MotherGivenCTX { get; set; }
        string PartnerHIVTestingMAT { get; set; }
        string PartnerHIVStatusMAT { get; set; }
        string CounselledOn { get; set; }
        string ReferredFrom { get; set; }
        string ReferredTo { get; set; }
        string ClinicalNotes { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}
