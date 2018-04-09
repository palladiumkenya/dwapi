using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dwapi.Domain
{
    [Table("PatientStatusExtract")]
    public class ClientPatientStatusExtract : ClientExtract
    {
        [Key]
        public override Guid Id { get; set; }
        public string ExitDescription { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }

        public ClientPatientStatusExtract()
        {
        }

        public ClientPatientStatusExtract(int patientPk, string patientId, int siteCode, string exitDescription, DateTime? exitDate, string exitReason, string emr, string project)
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

        public ClientPatientStatusExtract(TempPatientStatusExtract extract)
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
