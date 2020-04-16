using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class EmrSystem : Entity<Guid>
    {
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Version { get; set; }

        public bool IsMiddleware { get; set; }
        public bool IsDefault { get; set; }

        public ICollection<DatabaseProtocol> DatabaseProtocols { get; set; } = new List<DatabaseProtocol>();
        public ICollection<RestProtocol> RestProtocols { get; set; } = new List<RestProtocol>();
        public ICollection<Extract> Extracts { get; set; } = new List<Extract>();
        public EmrSetup EmrSetup { get; set; }



        public EmrSystem()
        {
        }

        public EmrSystem(string name, string version, bool isMiddleware = false, bool isDefault = false)
        {
            Name = name;
            Version = version;
            IsMiddleware = isMiddleware;
            IsDefault = isDefault;
        }

        public void UpdateTo(EmrSystem emrSystem)
        {
            Name = emrSystem.Name;
            Version = emrSystem.Version;
        }

        public void AddProtocol(DatabaseProtocol protocol)
        {
            protocol.EmrSystemId = Id;
            DatabaseProtocols.Add(protocol);
        }

        public void AddRestProtocol(RestProtocol protocol)
        {
            protocol.EmrSystemId = Id;
            RestProtocols.Add(protocol);
        }

        public void AddExtract(Extract extract)
        {
            extract.EmrSystemId = Id;
            Extracts.Add(extract);
        }

        public override string ToString()
        {
            return $"{Name} {Version}";
        }
    }
}
