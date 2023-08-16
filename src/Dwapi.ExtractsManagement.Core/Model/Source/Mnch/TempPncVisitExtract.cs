using System;
using Dwapi.Contracts.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Mnch
{
    public class TempPncVisitExtract : TempExtract, IPncVisit
    {
        public string FacilityName { get; set; }
        public string PatientMnchID { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string PNCRegisterNumber { get; set; }
        public int? PNCVisitNo { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string ModeOfDelivery { get; set; }
        public string PlaceOfDelivery { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Temp { get; set; }
        public int? PulseRate { get; set; }
        public int? RespiratoryRate { get; set; }
        public decimal? OxygenSaturation { get; set; }
        public int? MUAC { get; set; }
        public int? BP { get; set; }
        public string BreastExam { get; set; }
        public string GeneralCondition { get; set; }
        public string HasPallor { get; set; }
        public string Pallor { get; set; }
        public string Breast { get; set; }
        public string PPH { get; set; }
        public string CSScar { get; set; }
        public string UterusInvolution { get; set; }
        public string Episiotomy { get; set; }
        public string Lochia { get; set; }
        public string Fistula { get; set; }
        public string MaternalComplications { get; set; }
        public string TBScreening { get; set; }
        public string ClientScreenedCACx { get; set; }
        public string CACxScreenMethod { get; set; }
        public string CACxScreenResults { get; set; }
        public string PriorHIVStatus { get; set; }
        public string HIVTestingDone { get; set; }
        public string HIVTest1 { get; set; }
        public string HIVTest1Result { get; set; }
        public string HIVTest2 { get; set; }
        public string HIVTest2Result { get; set; }
        public string HIVTestFinalResult { get; set; }
        public string InfantProphylaxisGiven { get; set; }
        public string MotherProphylaxisGiven { get; set; }
        public string CoupleCounselled { get; set; }
        public string PartnerHIVTestingPNC { get; set; }
        public string PartnerHIVResultPNC { get; set; }
        public string CounselledOnFP { get; set; }
        public string ReceivedFP { get; set; }
        public string HaematinicsGiven { get; set; }
        public string DeliveryOutcome { get; set; }
        public string BabyConditon { get; set; }
        public string BabyFeeding { get; set; }
        public string UmbilicalCord { get; set; }
        public string Immunization { get; set; }
        public string InfantFeeding { get; set; }
        public string PreventiveServices { get; set; }
        public string ReferredFrom { get; set; }
        public string ReferredTo { get; set; }
        public DateTime? NextAppointmentPNC { get; set; }
        public string ClinicalNotes { get; set; }
        public string VisitTimingMother { get; set; }
        public string VisitTimingBaby { get; set; }
        public string MotherCameForHIVTest { get; set; }
        public string InfactCameForHAART { get; set; }
        public string MotherGivenHAART { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}
