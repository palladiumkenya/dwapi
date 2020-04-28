using System;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Model.Source
{
    public class PsmartSource 
    {
        public Guid EId { get; set; }
        public int? Id { get; set; }
        public string Shr { get; set; }
        public DateTime? Date_Created { get; set; }
        public string Status { get; set; }
        public DateTime? Status_Date { get; set; }
        public string Uuid { get; set; }
        public string Emr { get; set; }
        public DateTime? DateExtracted { get; set; }

        public PsmartSource()
        {
            EId = LiveGuid.NewGuid();
            DateExtracted = DateTime.Now;
        }

        public override string ToString()
        {
            var id = Id.HasValue ? $"{Id.Value}" : string.Empty;
            return $"{id}|{Uuid}|{Status}";
        }
    }
}