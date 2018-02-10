using System;
using System.ComponentModel.DataAnnotations;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class ExtractDestination : Entity<Guid>
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public decimal Rank { get; set; }
        public Guid ExtractId { get; set; }
    }
}