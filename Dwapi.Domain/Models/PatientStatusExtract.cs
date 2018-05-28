using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Domain.Utils;

namespace Dwapi.Domain.Models
{
    public class PatientStatusExtract
    {
        [Key]
        public Guid Id { get; set; }
        public int PatientPK { get; set; }
        public string PatientID { get; set; }
        public int SiteCode { get; set; }
        [Column(Order = 100)]
        public string Emr { get; set; }
        [Column(Order = 101)]
        public string Project { get; set; }
        [DoNotRead]
        [Column(Order = 102)]
        public bool? Processed { get; set; }
        [DoNotRead]
        public string QueueId { get; set; }
        [DoNotRead]
        public string Status { get; set; }
        [DoNotRead]
        public DateTime? StatusDate { get; set; }
        
        public string ExitDescription { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }

        public PatientStatusExtract()
        {
        }

        public PatientStatusExtract(int patientPk, string patientId, int siteCode, string exitDescription, DateTime? exitDate, string exitReason, string emr, string project)
        {
            PatientPK = patientPk;
            PatientID = patientId;
            SiteCode = siteCode;
            ExitDescription = exitDescription;
            ExitDate = exitDate;
            ExitReason = exitReason;
            Emr = emr;
            Project = project;
        }

        public PatientStatusExtract(TempPatientStatusExtract extract)
        {
            PatientPK = extract.PatientPK.Value;
            PatientID = extract.PatientID;
            SiteCode = extract.SiteCode.Value;
            ExitDescription = extract.ExitDescription;
            ExitDate = extract.ExitDate;
            ExitReason = extract.ExitReason;
            Emr = extract.Emr;
            Project = extract.Project;
        }

    }
}
