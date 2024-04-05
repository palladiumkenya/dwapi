using System; 

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts
{
    public  class TempHtsClientTracing : TempHtsExtract
    {
        public  string TracingType { get; set; }
        public  DateTime? TracingDate { get; set; }
        public  string TracingOutcome { get; set; }
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}
