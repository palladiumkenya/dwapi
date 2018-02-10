using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class Docket:Entity<Guid>
    {
        [MaxLength(50)]
        public string Code { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public ICollection<Extract> Extracts { get; set; }
    }
}