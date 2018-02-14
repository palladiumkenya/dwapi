using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Model
{
    [Table("Extracts")]
    public class Extract : Entity<Guid>
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Display { get; set; }
        public ICollection<ExtractHistory> ExtractHistories { get; set; }=new List<ExtractHistory>();
    }
}