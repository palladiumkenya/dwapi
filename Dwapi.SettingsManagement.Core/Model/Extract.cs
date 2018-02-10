using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class Extract : Entity<Guid>
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Display { get; set; }
        [MaxLength(8000)]
        public string ExtractSql { get; set; }
        public decimal Rank { get; set; }
        public bool IsPriority { get; set; }
        public ICollection<ExtractDestination> Destinations { get; set; }=new List<ExtractDestination>();
        public Guid DocketId { get; set; }
    }
}