using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    [Table("vTempPatientAdverseEventExtractError")]
    public class TempPatientAdverseEventExtractError : TempExtract
    {
        public string AdverseEvent { get; set; }
        public DateTime AdverseEventStartDate { get; set; }
        public DateTime AdverseEventEndDate { get; set; }
        public string Severity { get; set; }
        public DateTime VisitDate { get; set; }
        public string RecordUUID { get; set; }

        [NotMapped]
        public virtual ICollection<TempPatientAdverseEventExtractErrorSummary> TempPatientAdverseEventExtractErrorSummaries { get; set; } = new List<TempPatientAdverseEventExtractErrorSummary>();
    }
}
