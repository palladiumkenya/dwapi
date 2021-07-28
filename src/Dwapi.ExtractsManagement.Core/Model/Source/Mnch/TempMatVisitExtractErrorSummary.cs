using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Contracts.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Mnch
{
    [Table("vTempMatVisitExtractErrorSummary")]public class TempMatVisitExtractErrorSummary:TempMnchExtractErrorSummary,IMatVisit
    {
        public string PatientMnchID { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string AdmissionNumber { get; set; }
        public int? ANCVisits { get; set; }
        public DateTime? DateOfDelivery { get; set; }
        public int? DurationOfDelivery { get; set; }
        public int? GestationAtBirth { get; set; }
        public string ModeOfDelivery { get; set; }
        public string PlacentaComplete { get; set; }
        public string UterotonicGiven { get; set; }
        public string VaginalExamination { get; set; }
        public int? BloodLoss { get; set; }
        public string BloodLossVisual { get; set; }
        public string ConditonAfterDelivery { get; set; }
        public DateTime? MaternalDeath { get; set; }
        public string DeliveryComplications { get; set; }
        public int? NoBabiesDelivered { get; set; }
        public int? BabyBirthNumber { get; set; }
        public string SexBaby { get; set; }
        public string BirthWeight { get; set; }
        public string BirthOutcome { get; set; }
        public string BirthWithDeformity { get; set; }
        public string TetracyclineGiven { get; set; }
        public string InitiatedBF { get; set; }
        public int? ApgarScore1 { get; set; }
        public int? ApgarScore5 { get; set; }
        public int? ApgarScore10 { get; set; }
        public string KangarooCare { get; set; }
        public string ChlorhexidineApplied { get; set; }
        public string VitaminKGiven { get; set; }
        public string StatusBabyDischarge { get; set; }
        public string MotherDischargeDate { get; set; }
        public string SyphilisTestResults { get; set; }
        public string HIVStatusLastANC { get; set; }
        public string HIVTestingDone { get; set; }
        public string HIVTest1 { get; set; }
        public string HIV1Results { get; set; }
        public string HIVTest2 { get; set; }
        public string HIV2Results { get; set; }
        public string HIVTestFinalResult { get; set; }
        public string OnARTANC { get; set; }
        public string BabyGivenProphylaxis { get; set; }
        public string MotherGivenCTX { get; set; }
        public string PartnerHIVTestingMAT { get; set; }
        public string PartnerHIVStatusMAT { get; set; }
        public string CounselledOn { get; set; }
        public string ReferredFrom { get; set; }
        public string ReferredTo { get; set; }
        public string ClinicalNotes { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}
