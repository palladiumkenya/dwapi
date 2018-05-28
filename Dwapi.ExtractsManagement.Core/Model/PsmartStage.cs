using System;
using System.ComponentModel.DataAnnotations;
using Dwapi.SharedKernel.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Dwapi.ExtractsManagement.Core.Model
{
    public class PsmartStage 
    {
        [Key]
        public Guid EId { get; set; }
        public int? Id { get; set; }
        public string Shr { get; set; }
        public DateTime? Date_Created { get; set; }
        [MaxLength(100)]
        public string Status { get; set; }
        public DateTime? Status_Date { get; set; }
        public string Uuid { get; set; }
        public string Emr { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime DateStaged { get; set; }
        public DateTime? DateSent { get; set; }
        public string RequestId { get; set; }

        public PsmartStage()
        {
            DateStaged = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Id}({DateExtracted:F} >> {DateStaged:F})|{EId}";
        }
    }
}