using System;

namespace Dwapi.Contracts.Prep
{
    public interface IPrepVisit
    {
        string FacilityName { get; set; }
        string PrepNumber { get; set; }
        string HtsNumber { get; set; }
        string EncounterId { get; set; }
        string VisitID { get; set; }
        DateTime? VisitDate { get; set; }
        string BloodPressure { get; set; }
        string Temperature { get; set; }
        decimal? Weight { get; set; }
        decimal? Height { get; set; }
        decimal? BMI { get; set; }
        string STIScreening { get; set; }
        string STISymptoms { get; set; }
        string STITreated { get; set; }
        string Circumcised { get; set; }
        string VMMCReferral { get; set; }
        DateTime? LMP { get; set; }
        string MenopausalStatus { get; set; }
        string PregnantAtThisVisit { get; set; }
        DateTime? EDD { get; set; }
        string PlanningToGetPregnant { get; set; }
        string PregnancyPlanned { get; set; }
        string PregnancyEnded { get; set; }
        DateTime? PregnancyEndDate { get; set; }
        string PregnancyOutcome { get; set; }
        string BirthDefects { get; set; }
        string Breastfeeding { get; set; }
        string FamilyPlanningStatus { get; set; }
        string FPMethods { get; set; }
        string AdherenceDone { get; set; }
        string AdherenceOutcome { get; set; }
        string AdherenceReasons { get; set; }
        string SymptomsAcuteHIV { get; set; }
        string ContraindicationsPrep { get; set; }
        string PrepTreatmentPlan { get; set; }
        string PrepPrescribed { get; set; }
        string RegimenPrescribed { get; set; }
        string MonthsPrescribed { get; set; }
        string CondomsIssued { get; set; }
        string Tobegivennextappointment { get; set; }
        string Reasonfornotgivingnextappointment { get; set; }
        string HepatitisBPositiveResult { get; set; }
        string HepatitisCPositiveResult { get; set; }
        string VaccinationForHepBStarted { get; set; }
        string TreatedForHepB { get; set; }
        string VaccinationForHepCStarted { get; set; }
        string TreatedForHepC { get; set; }
        DateTime? NextAppointment { get; set; }
        string ClinicalNotes { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}
