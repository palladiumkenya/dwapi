using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{

            
        public abstract class TempHtsPartnerTracing : TempHTSExtract
        {
           
            public virtual string TraceType { get; set; }
            public virtual DateTime TraceDate { get; set; }
            public virtual string TraceOutcome { get; set; }
            public virtual DateTime BookingDate { get; set; }
                
            public override string ToString()
            {
                return $"{SiteCode}-{HtsNumber}";
            }
        }
}
