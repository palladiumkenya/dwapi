using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    [Table("vTempPatientPharmacyExtractError")]
    public class TempPatientPharmacyExtractError : TempExtract
    {
        public int? VisitID { get; set; }
        public string Drug { get; set; }
        public string Provider { get; set; }
        public DateTime? DispenseDate { get; set; }
        public decimal? Duration { get; set; }
        public DateTime? ExpectedReturn { get; set; }
        public string TreatmentType { get; set; }
        public string RegimenLine { get; set; }
        public string PeriodTaken { get; set; }
        public string ProphylaxisType { get; set; }
        [NotMapped]
        public virtual ICollection<TempPatientPharmacyExtractErrorSummary> TempPatientPharmacyExtractErrorSummaries { get; set; } = new List<TempPatientPharmacyExtractErrorSummary>();
    }
}
