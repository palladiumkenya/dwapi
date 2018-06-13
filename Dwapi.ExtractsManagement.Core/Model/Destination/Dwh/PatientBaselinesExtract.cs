using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Dwh
{

    public class PatientBaselinesExtract : TempExtract
    {

        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}";
        }

        public int? bCD4 { get; set; }
        public DateTime? bCD4Date { get; set; }
        public int? bWAB { get; set; }
        public DateTime? bWABDate { get; set; }
        public int? bWHO { get; set; }
        public DateTime? bWHODate { get; set; }
        public int? eWAB { get; set; }
        public DateTime? eWABDate { get; set; }
        public int? eCD4 { get; set; }
        public DateTime? eCD4Date { get; set; }
        public int? eWHO { get; set; }
        public DateTime? eWHODate { get; set; }
        public int? lastWHO { get; set; }
        public DateTime? lastWHODate { get; set; }
        public int? lastCD4 { get; set; }
        public DateTime? lastCD4Date { get; set; }
        public int? lastWAB { get; set; }
        public DateTime? lastWABDate { get; set; }
        public int? m12CD4 { get; set; }
        public DateTime? m12CD4Date { get; set; }
        public int? m6CD4 { get; set; }
        public DateTime? m6CD4Date { get; set; }
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
