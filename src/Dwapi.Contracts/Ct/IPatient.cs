using System;

namespace Dwapi.Contracts.Ct
{
    public interface IPatient
    {
        string Pkv { get; set; }
        string Occupation { get; set; }
    }

    public interface IPharmacy
    {
        string RegimenChangedSwitched { get; set; }
        string RegimenChangeSwitchReason { get; set; }
        string StopRegimenReason { get; set; }
        DateTime? StopRegimenDate { get; set; }
    }

    public interface ILab
    {
        DateTime? DateSampleTaken { get; set; }
        string SampleType { get; set; }
    }

    public interface IStatus
    {
        string TOVerified { get; set; }
        DateTime? TOVerifiedDate { get; set; }
    }

    public interface IVisit
    {
        string VisitType { get; set; }
        string VisitBy { get; set; }
        string WHOStage { get; set; }
        string WABStage { get; set; }
        string Pregnant { get; set; }
        string LMP { get; set; }
        string EDD { get; set; }
        string Height { get; set; }
        string Weight { get; set; }
        string BP { get; set; }
        string Temp { get; set; }
        string PulseRate { get; set; }
        string RespiratoryRate { get; set; }
        string OxygenSaturation { get; set; }
        string muac { get; set; }
        string nutritional_status { get; set; }
        string EverHadMenses { get; set; }
        string Breastfeeding { get; set; }
        string Menopausal { get; set; }
        string ProphylaxisUsed { get; set; }
        string CTXAdherence { get; set; }
        string CurrentRegimen { get; set; }
        string NoFPReason { get; set; }
        string AdherenceCategory { get; set; }
        string Adherence { get; set; }
        string TCAReason { get; set; }
        string ClinicalNotes { get; set; }
        string OI { get; set; }
        string OIDate { get; set; }
        string FamilyPlanningMethod { get; set; }
        string PWP { get; set; }
        string GestationAge { get; set; }
        string NextAppointmentDate { get; set; }
        string SubstitutionFirstlineRegimenDate { get; set; }
        string SubstitutionFirstlineRegimenReason { get; set; }
        string SubstitutionSecondlineRegimenDate { get; set; }
        string SubstitutionSecondlineRegimenReason { get; set; }
        string SecondlineRegimenChangeDate { get; set; }
        string SecondlineRegimenChangeReason { get; set; }
        string StabilityAssessment { get; set; }
        string DifferentiatedCare { get; set; }
        string PopulationType { get; set; }
        string KeyPopulationType { get; set; }

    }
}
