using Dwapi.SharedKernel.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    public abstract class TempExtract : Entity<Guid>
    {
        public int? PatientPK { get; set; }
        public string PatientID { get; set; }
        public int? FacilityId { get; set; }
        public int? SiteCode { get; set; }

        //[DoNotRead]
        public DateTime DateExtracted { get; set; } = DateTime.Now;

        //[DoNotRead]
       
        public virtual bool CheckError { get; set; }

        //[DoNotRead]
        [NotMapped]
        public bool HasError { get; set; }
    }
}