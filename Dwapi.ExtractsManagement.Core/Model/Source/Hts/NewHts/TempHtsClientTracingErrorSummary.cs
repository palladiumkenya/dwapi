using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts
{
    [Table("vTempHtsClientTracingErrorSummary")]
    public class TempHtsClientTracingErrorSummary : TempHTSExtractErrorSummary
    {
        public DateTime? TracingType { get; set; }
        public DateTime? TracingDate { get; set; }
        public string TracingOutcome { get; set; }
    }
}
