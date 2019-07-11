using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{

            
        public abstract class TempHtsClientTracing : TempHTSExtract
        {
            
            
            public virtual DateTime TracingType { get; set; }
            public virtual DateTime TracingDate { get; set; }
            public virtual string TracingOutcome { get; set; }
                
            public override string ToString()
            {
                return $"{SiteCode}-{HtsNumber}";
            }
        }
}
