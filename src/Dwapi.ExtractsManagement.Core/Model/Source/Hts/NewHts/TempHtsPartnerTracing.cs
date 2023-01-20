using System; 

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts
{
    public  class TempHtsPartnerTracing : TempHtsExtract
    {
        public  string TraceType { get; set; }
        public int? PartnerPersonId { get; set; }
        public  DateTime? TraceDate { get; set; }
        public  string TraceOutcome { get; set; }
        public  DateTime? BookingDate { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}
