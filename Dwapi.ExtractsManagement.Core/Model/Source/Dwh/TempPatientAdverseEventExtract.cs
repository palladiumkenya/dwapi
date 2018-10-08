using System;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    
    public class TempPatientAdverseEventExtract : TempExtract
    {
        public string AdverseEvent { get; set; }
        public DateTime AdverseEventStartDate { get; set; }
        public DateTime AdverseEventEndDate { get; set; }
        public string Severity { get; set; }
        public DateTime VisitDate { get; set; }
    }
}
