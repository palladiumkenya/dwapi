using System;
using Dwapi.ExtractsManagement.Core.Interfaces.Stage.Source.Psmart;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Psmart
{
    public class PsmartSource : IPsmartSource
    {
        public Guid Id { get; set; }
        public int Serial { get; set; }
        public string Demographics { get; set; }
        public string Encounters { get; set; }
        public string Emr { get; set; }
        public int? FacilityCode { get; set; }
        public DateTime? DateExtracted { get; set; }

        public PsmartSource()
        {
            Id = LiveGuid.NewGuid();
            DateExtracted=DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Serial} | {Demographics} | {Encounters} ({DateExtracted:F},{Id})";
        }
    }
}