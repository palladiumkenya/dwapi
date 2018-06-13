using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Dwh
{
    
    public class PatientPharmacyExtract : TempExtract
    {
     


        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}";
        }

        public int? VisitID { get; set; }
        public string Drug { get; set; }
        public string Provider { get; set; }
        public DateTime? DispenseDate { get; set; }
        public decimal? Duration { get; set; }
        public DateTime? ExpectedReturn { get; set; }
        public string TreatmentType { get; set; }
        public string RegimenLine { get; set; }
        public string PeriodTaken { get; set; }
        public string ProphylaxisType { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
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
