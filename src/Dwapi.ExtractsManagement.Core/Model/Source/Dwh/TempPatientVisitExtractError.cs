using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    [Table("vTempPatientVisitExtractError")]
    public class TempPatientVisitExtractError : TempExtract
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
        public DateTime? RefillDate { get; set; }
        [NotMapped]
        public virtual ICollection<TempPatientVisitExtractErrorSummary> TempPatientVisitExtractErrorSummaries { get; set; } = new List<TempPatientVisitExtractErrorSummary>();
    }
}
