using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{
    public abstract class TempHTSExtract : Entity<Guid>
    {
        public virtual int? SiteCode { get; set; }
        public virtual int? PatientPk { get; set; }
        public virtual string HtsNumber { get; set; }
        public virtual string Emr { get; set; }
        public virtual string Project { get; set; }
        public virtual bool CheckError { get; set; }
        public virtual DateTime DateExtracted { get; set; } = DateTime.Now;
        [NotMapped]
        public virtual bool HasError { get; set; }

        public override string ToString()
        {
            return $"{SiteCode}-{HtsNumber}";
        }
    }
}
