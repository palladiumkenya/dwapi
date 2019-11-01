using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    [Table("vTempPatientLaboratoryExtractError")]
    public class TempPatientLaboratoryExtractError : TempExtract
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
        [NotMapped]
        public virtual ICollection<TempPatientLaboratoryExtractErrorSummary> TempPatientLaboratoryExtractErrorSummaries { get; set; } = new List<TempPatientLaboratoryExtractErrorSummary>(); 
    }
}
