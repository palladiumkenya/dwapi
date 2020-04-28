using System;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts
{

            
        public  class HtsClientTracing : HtsExtract
        {
            
            
            public  string TracingType { get; set; }
            public  DateTime? TracingDate { get; set; }
            public  string TracingOutcome { get; set; }
        }
}
 