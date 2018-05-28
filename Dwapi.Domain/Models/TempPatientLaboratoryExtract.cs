using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Domain.Utils;

namespace Dwapi.Domain.Models
{
    public class TempPatientLaboratoryExtract
    {

        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}";
        }

        [Key]
        [DoNotRead]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? PatientPK { get; set; }
        public string PatientID { get; set; }
        public int? FacilityId { get; set; }
        public int? SiteCode { get; set; }

        [DoNotRead]
        public DateTime DateExtracted { get; set; }
        [DoNotRead]
        public bool CheckError { get; set; }

        [DoNotRead]
        [NotMapped]
        public bool HasError { get; set; }

        public string FacilityName { get; set; }
        public string SatelliteName { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public int? VisitId { get; set; }
        public DateTime? OrderedByDate { get; set; }
        public DateTime? ReportedByDate { get; set; }
        public string TestName { get; set; }
        public int? EnrollmentTest { get; set; }
        public string TestResult { get; set; }
    }
}
