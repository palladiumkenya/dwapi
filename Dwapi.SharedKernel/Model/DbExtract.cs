using System;
using System.ComponentModel.DataAnnotations;

namespace Dwapi.SharedKernel.Model
{
    public class DbExtract : Entity<Guid>
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Display { get; set; }
        [MaxLength(8000)]
        public string ExtractSql { get; set; }
        public decimal Rank { get; set; }
        public bool IsPriority { get; set; }
  
    
        public override string ToString()
        {
            return Display;
        }
    }
}