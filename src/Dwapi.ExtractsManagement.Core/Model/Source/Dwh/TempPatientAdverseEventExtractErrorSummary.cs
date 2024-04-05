using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    [Table("vTempPatientAdverseEventExtractErrorSummary")]
    public class TempPatientAdverseEventExtractErrorSummary : TempExtractErrorSummary
    {
        public string AdverseEvent { get; set; }
        public DateTime AdverseEventStartDate { get; set; }
        public DateTime AdverseEventEndDate { get; set; }
        public string Severity { get; set; }
        public DateTime VisitDate { get; set; }
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }

    }
}
