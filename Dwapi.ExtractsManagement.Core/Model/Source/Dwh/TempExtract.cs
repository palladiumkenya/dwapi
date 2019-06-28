using Dwapi.SharedKernel.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    public abstract class TempExtract : Entity<Guid>
    {
        public virtual int? PatientPK { get; set; }
        public virtual string PatientID { get; set; }
        public virtual int? FacilityId { get; set; }
        public virtual int? SiteCode { get; set; }
        public virtual DateTime DateExtracted { get; set; } = DateTime.Now;
        public virtual string Emr { get; set; }
        public virtual string Project { get; set; }
        public virtual bool CheckError { get; set; }
        [NotMapped]
        public bool HasError { get; set; }
        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}";
        }
    }
}