using System;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Dwh
{
    public class PatientAdverseEventExtract : ClientExtract
    {
        public string AdverseEvent { get; set; }
        public DateTime AdverseEventStartDate { get; set; }
        public DateTime AdverseEventEndDate { get; set; }
        public string Severity { get; set; }
        public DateTime VisitDate { get; set; }
    }
}