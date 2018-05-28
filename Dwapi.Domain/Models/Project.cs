using System;
using System.Collections.Generic;

namespace Dwapi.Domain.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual ICollection<EMR> Emrs { get; set; }
    }
}
