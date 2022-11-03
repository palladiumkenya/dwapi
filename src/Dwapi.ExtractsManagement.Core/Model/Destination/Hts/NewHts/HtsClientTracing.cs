using System;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts
{

            
        public  class HtsClientTracing : HtsExtract
        {
            
            
            public  string TracingType { get; set; }
            public  DateTime? TracingDate { get; set; }
            public  string TracingOutcome { get; set; }
            public DateTime? Date_Created { get; set; }
            public DateTime? Date_Last_Modified { get; set; }
        }
}
 