using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts
{
    public abstract class HtsExtract : Entity<Guid>
    {
        public virtual string FacilityName { get; set; }
        public virtual int SiteCode { get; set; }
        public virtual int PatientPk { get; set; }
        public virtual string HtsNumber { get; set; }
        public virtual string Emr { get; set; }
        public virtual string Project { get; set; }
        public virtual bool? Processed { get; set; }
        public virtual string QueueId { get; set; }
        public virtual string Status { get; set; }
        public virtual DateTime? StatusDate { get; set; }
        public virtual DateTime? DateExtracted { get; set; }


        [NotMapped]
        public bool IsSent => !string.IsNullOrWhiteSpace(Status) && Status.IsSameAs(nameof(SendStatus.Sent));

        public override string ToString()
        {
            return $"{SiteCode}-{HtsNumber}";
        }
    }
}
