using System;
using System.ComponentModel.DataAnnotations;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Model
{
    public class ExtractHistory:Entity<Guid>
    {
        [MaxLength(100)]
        public string Status { get; set; }
        public int Found { get; set; }
        public DateTime DateFound { get; set; }
        public int Loaded { get; set; }
        public DateTime DateLoaded { get; set; }
        public int Rejected { get; set; }
        public int Queued { get; set; }
        public DateTime DateQueued { get; set; }
        public int Sent { get; set; }
        public DateTime DateSent { get; set; }
        public Guid ExtractId { get; set; }
    }
}