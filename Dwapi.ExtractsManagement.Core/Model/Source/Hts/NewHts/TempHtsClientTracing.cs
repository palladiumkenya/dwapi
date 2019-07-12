using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{

            
        public  class TempHtsClientTracing : TempHTSExtract
        {
            
            
            public  DateTime TracingType { get; set; }
            public  DateTime TracingDate { get; set; }
            public  string TracingOutcome { get; set; }
                
            public override string ToString()
            {
                return $"{SiteCode}-{HtsNumber}";
            }
        }
}
