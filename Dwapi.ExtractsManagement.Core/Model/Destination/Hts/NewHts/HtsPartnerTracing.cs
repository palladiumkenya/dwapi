using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{

            
        public  class HtsPartnerTracing : TempHTSExtract
        {
           
            public  string TraceType { get; set; }
            public  DateTime TraceDate { get; set; }
            public  string TraceOutcome { get; set; }
            public  DateTime BookingDate { get; set; } 
                
            public override string ToString()
            {
                return $"{SiteCode}-{HtsNumber}";
            }
        }
}
