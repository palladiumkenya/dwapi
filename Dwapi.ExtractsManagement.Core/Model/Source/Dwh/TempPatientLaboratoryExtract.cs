using System;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    
    public class TempPatientLaboratoryExtract : TempExtract
    {
        public string FacilityName { get; set; }
        public string SatelliteName { get; set; }
        public int? VisitId { get; set; }
        public DateTime? OrderedByDate { get; set; }
        public DateTime? ReportedByDate { get; set; }
        public string TestName { get; set; }
        public int? EnrollmentTest { get; set; }
        public string TestResult { get; set; }
    }
}
