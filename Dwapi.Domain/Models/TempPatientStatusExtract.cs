using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Domain.Utils;

namespace Dwapi.Domain.Models
{
    public class TempPatientStatusExtract
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
        public string Emr { get; set; }
        public string Project { get; set; }
        public string ExitDescription { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }
    }
}
