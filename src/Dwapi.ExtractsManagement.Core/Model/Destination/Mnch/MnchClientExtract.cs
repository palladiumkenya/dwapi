using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Mnch
{
    public abstract class MnchClientExtract : Entity<Guid>
    {
        public virtual int PatientPK { get; set; }
        public virtual int SiteCode { get; set; }

        public virtual string PatientID { get; set; }
        public virtual int? FacilityId { get; set; }
        public virtual bool? Processed { get; set; }
        public virtual string QueueId { get; set; }
        public virtual string Status { get; set; }
        public virtual DateTime? StatusDate { get; set; }
        public virtual DateTime? DateExtracted { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public virtual DateTime? Date_Created { get; set; }
        public virtual DateTime? Date_Last_Modified { get; set; }

        [NotMapped]
        public bool IsSent => !string.IsNullOrWhiteSpace(Status) && Status.IsSameAs(nameof(SendStatus.Sent));

        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}";
        }
    }
}
