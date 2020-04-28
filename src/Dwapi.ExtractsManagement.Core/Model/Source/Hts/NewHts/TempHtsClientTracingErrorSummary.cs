using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts
{
    [Table("vTempHtsClientTracingExtractErrorSummary")]
    public class TempHtsClientTracingErrorSummary : TempHTSExtractErrorSummary
    {
        public string TracingType { get; set; }
        public DateTime? TracingDate { get; set; }
        public string TracingOutcome { get; set; }
    }
}
