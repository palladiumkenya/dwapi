using System;

namespace Dwapi.Contracts.Mnch
{
    public interface IAncVisit
    {
        string PatientMnchID { get; set; }
        string FacilityName { get; set; }
        int? VisitID { get; set; }
        DateTime? VisitDate { get; set; }
        string ANCClinicNumber { get; set; }
        int? ANCVisitNo { get; set; }
        int? GestationWeeks { get; set; }
        decimal? Height { get; set; }
        decimal? Weight { get; set; }
        decimal? Temp { get; set; }
        int? PulseRate { get; set; }
        int? RespiratoryRate { get; set; }
        decimal? OxygenSaturation { get; set; }
        int? MUAC { get; set; }
        int? BP { get; set; }
        string BreastExam { get; set; }
        string AntenatalExercises { get; set; }
        string FGM { get; set; }
        string FGMComplications { get; set; }
        decimal? Haemoglobin { get; set; }
        string DiabetesTest { get; set; }
        string TBScreening { get; set; }
        string CACxScreen { get; set; }
        string CACxScreenMethod { get; set; }
        int? WHOStaging { get; set; }
        string VLSampleTaken { get; set; }
        DateTime? VLDate { get; set; }
        string VLResult { get; set; }
        string SyphilisTreatment { get; set; }
        string HIVStatusBeforeANC { get; set; }
        string HIVTestingDone { get; set; }
        string HIVTestType { get; set; }
        string HIVTest1 { get; set; }
        string HIVTest1Result { get; set; }
        string HIVTest2 { get; set; }
        string HIVTest2Result { get; set; }
        string HIVTestFinalResult { get; set; }
        string SyphilisTestDone { get; set; }
        string SyphilisTestType { get; set; }
        string SyphilisTestResults { get; set; }
        string SyphilisTreated { get; set; }
        string MotherProphylaxisGiven { get; set; }
        DateTime? MotherGivenHAART { get; set; }
        string AZTBabyDispense { get; set; }
        string NVPBabyDispense { get; set; }
        string ChronicIllness { get; set; }
        string CounselledOn { get; set; }
        string PartnerHIVTestingANC { get; set; }
        string PartnerHIVStatusANC { get; set; }
        string PostParturmFP { get; set; }
        string Deworming { get; set; }
        string MalariaProphylaxis { get; set; }
        string TetanusDose { get; set; }
        string IronSupplementsGiven { get; set; }
        string ReceivedMosquitoNet { get; set; }
        string PreventiveServices { get; set; }
        string UrinalysisVariables { get; set; }
        string ReferredFrom { get; set; }
        string ReferredTo { get; set; }
        string ReferralReasons { get; set; }
        DateTime? NextAppointmentANC { get; set; }
        string ClinicalNotes { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}
