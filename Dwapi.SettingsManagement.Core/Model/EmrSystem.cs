using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class EmrSystem : Entity<Guid>
    {
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Version { get; set; }

        public ICollection<DatabaseProtocol> DatabaseProtocols { get; set; } = new List<DatabaseProtocol>();
        public ICollection<RestProtocol> RestProtocols { get; set; } = new List<RestProtocol>();
    }
}