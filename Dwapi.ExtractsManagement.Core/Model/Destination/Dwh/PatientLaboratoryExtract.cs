using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Dwh
{
    
    public class PatientLaboratoryExtract : TempExtract
    {

        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}";
        }

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
        [DoNotRead]
        [Column(Order = 102)]
        public virtual bool? Processed { get; set; }

        [DoNotRead]
        public virtual string QueueId { get; set; }
        [DoNotRead]
        public virtual string Status { get; set; }
        [DoNotRead]
        public virtual DateTime? StatusDate { get; set; }
        [NotMapped]
        public override bool CheckError { get; set; }
        [NotMapped]
        public bool IsSent
        {
            get { return !string.IsNullOrWhiteSpace(Status) && Status.IsSameAs(nameof(SendStatus.Sent)); }
        }
    }
}
