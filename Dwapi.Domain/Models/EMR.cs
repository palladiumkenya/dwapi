using System;
using System.Collections.Generic;
using System.Linq;

namespace Dwapi.Domain.Models
{ 
    public class EMR
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string ConnectionKey { get; set; }
        public bool IsDefault { get; set; }
        public virtual ICollection<ExtractSetting> ExtractSettings { get; set; } = new List<ExtractSetting>();
        public Guid ProjectId { get; set; }

        public EMR()
        {
            Id = Guid.NewGuid();
        }

        public EMR(string name, string version, string connectionKey, bool isDefault, Guid projectId) : this()
        {
            Name = name;
            Version = version;
            IsDefault = isDefault;
            ConnectionKey = connectionKey;
            ProjectId = projectId;
        }





        public void AddExtractSetting(ExtractSetting setting)
        {
            if (ExtractSettings.Any(c => c.Name.ToLower() == setting.Name.ToLower()))
            {
                throw new ArgumentException($"Cannot add duplicate Extract Setting.", "ExtractSettings");
            }
            setting.EmrId = Id;
            ExtractSettings.Add(setting);
        }

        public void AddExtractSetting(List<ExtractSetting> extractSettings)
        {
            foreach (var e in extractSettings)
            {
                AddExtractSetting(e);
            }
        }

        public void UpdateExtractSetting(ExtractSetting setting)
        {
            var otherSubCounties = ExtractSettings.Where(x => x.Id != setting.Id);
            if (otherSubCounties.Any(c => c.Name.ToLower() == setting.Name.ToLower()))
            {
                throw new ArgumentException($"Cannot update Extract Setting. {setting.Name} exists !",
                    "Extract Setting");
            }

            var sateliteFacility = ExtractSettings.FirstOrDefault(a => a.Id == setting.Id);
            sateliteFacility?.UpdateTo(setting);
        }

        public List<ExtractSetting> GetActiveExtractSettings()
        {
            return ExtractSettings
                .Where(x => x.IsActive)
                .ToList();
        }

        public ExtractSetting GetActiveExtractSetting(string destination)
        {
            return ExtractSettings
                .FirstOrDefault(x => x.IsActive &&
                                     x.Destination.ToLower() == destination.ToLower());
        }


        public override string ToString()
        {
            return $"{Name} ({Version})";
        }
    }
}
