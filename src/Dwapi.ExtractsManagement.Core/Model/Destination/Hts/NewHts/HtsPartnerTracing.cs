using System;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts
{

            
        public  class HtsPartnerTracing : HtsExtract
        {
           
            public  string TraceType { get; set; }
            public  DateTime? TraceDate { get; set; }
            public int? PartnerPersonId { get; set; }
            public  string TraceOutcome { get; set; }
            public  DateTime? BookingDate { get; set; }  
        }
}
