using System;
using System.ComponentModel.DataAnnotations;
using Dwapi.ExtractsManagement.Core.Interfaces.Stage.Psmart;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Stage.Psmart
{
    public class PsmartStage : Entity<Guid>, IPsmartStage
    {
        [MaxLength(50)]
        public string Serial { get; set; }
        [MaxLength(100)]
        public string Demographics { get; set; }
        [MaxLength(100)]
        public string Encounters { get; set; }
        [MaxLength(50)]
        public string Emr { get; set; }
        public int? FacilityCode { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime DateStaged { get; set; }

        public PsmartStage()
        {
            DateStaged=DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Id},{Serial} ({DateExtracted:F} >> {DateStaged:F})";
        }
    }
}