using System;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Dwh
{

    public class PatientLaboratoryExtract :ClientExtract
    {
        public string FacilityName { get; set; }
        public string SatelliteName { get; set; }
        public string Reason { get; set; }
        public int? VisitId { get; set; }
        public DateTime? OrderedByDate { get; set; }
        public DateTime? ReportedByDate { get; set; }
        public string TestName { get; set; }
        public int? EnrollmentTest { get; set; }
        public string TestResult { get; set; }
    }
}
