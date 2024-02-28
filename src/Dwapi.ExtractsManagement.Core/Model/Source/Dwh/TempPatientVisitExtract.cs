using System;
using Dwapi.Contracts.Ct;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{

    public class TempPatientVisitExtract : TempExtract,IVisit
    {
        public string FacilityName { get; set; }
        public int? VisitId { get; set; }
        public DateTime? VisitDate { get; set; }
        public string Service { get; set; }
        public string VisitType { get; set; }
        public int? WHOStage { get; set; }
        public string WABStage { get; set; }
        public string Pregnant { get; set; }
        public DateTime? LMP { get; set; }
        public DateTime? EDD { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string BP { get; set; }
        public string OI { get; set; }
        public DateTime? OIDate { get; set; }
        public string Adherence { get; set; }
        public string AdherenceCategory { get; set; }
        public DateTime? SubstitutionFirstlineRegimenDate { get; set; }
        public string SubstitutionFirstlineRegimenReason { get; set; }
        public DateTime? SubstitutionSecondlineRegimenDate { get; set; }
        public string SubstitutionSecondlineRegimenReason { get; set; }
        public DateTime? SecondlineRegimenChangeDate { get; set; }
        public string SecondlineRegimenChangeReason { get; set; }
        public string FamilyPlanningMethod { get; set; }
        public string PwP { get; set; }
        public decimal? GestationAge { get; set; }
        public DateTime? NextAppointmentDate { get; set; }

        public string StabilityAssessment { get; set; }
        public string DifferentiatedCare { get; set; }
        public string PopulationType { get; set; }
        public string KeyPopulationType { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string VisitBy { get; set; }
        public decimal? Temp { get; set; }
        public int? PulseRate { get; set; }
        public int? RespiratoryRate { get; set; }
        public decimal? OxygenSaturation { get; set; }
        public int? Muac { get; set; }
        public string NutritionalStatus { get; set; }
        public string EverHadMenses { get; set; }
        public string Breastfeeding { get; set; }
        public string Menopausal { get; set; }
        public string NoFPReason { get; set; }
        public string ProphylaxisUsed { get; set; }
        public string CTXAdherence { get; set; }
        public string CurrentRegimen { get; set; }
        public string HCWConcern { get; set; }
        public string TCAReason { get; set; }
        public string ClinicalNotes { get; set; }
        public string GeneralExamination { get; set; }
        public string SystemExamination { get; set; }
        public string Skin { get; set; }
        public string Eyes { get; set; }
        public string ENT { get; set; }
        public string Chest { get; set; }
        public string CVS { get; set; }
        public string Abdomen { get; set; }
        public string CNS { get; set; }
        public string Genitourinary { get; set; }
        public DateTime? RefillDate { get; set; }
        public string ZScore { get; set; }
        public string PaedsDisclosure { get; set; }
        public int? ZScoreAbsolute { get; set; }
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }
        public string WHOStagingOI  { get; set; }

    }
}
