using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class Docket:Entity<string>
    {
        [MaxLength(50)]
        public override string Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public ICollection<Extract> Extracts { get; set; }=new List<Extract>();
        public ICollection<CentralRegistry> Registries { get; set; }=new List<CentralRegistry>();
    }
}