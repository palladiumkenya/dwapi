using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dwapi.Domain.Models
{
    public class ExtractSetting
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Display { get; set; }
        public string ExtractCsv { get; set; }
        [MaxLength(8000)]
        public string ExtractSql { get; set; }
        public string Destination { get; set; }
        public decimal Rank { get; set; }
        public bool IsActive { get; set; }
        public bool IsPriority { get; set; }
        public Guid EmrId { get; set; }
        public ICollection<EventHistory> Events { get; set; } = new List<EventHistory>();

        public ExtractSetting()
        {
            Id = Guid.NewGuid();
        }

        public ExtractSetting(Type type) : this()
        {
            Destination = type.Name;
        }

        public ExtractSetting(string destination) : this()
        {
            Destination = destination;
        }

        public ExtractSetting(string name, string display, string extractCsv, string extractSql, string destination, decimal rank, bool isActive, Guid emrId) : this()
        {
            Name = name;
            Display = display;
            ExtractCsv = extractCsv;
            ExtractSql = extractSql;
            Destination = destination;
            Rank = rank;
            IsActive = isActive;
            EmrId = emrId;
        }

        public override string ToString()
        {
            return $"{Display}";
        }

        public void UpdateTo(ExtractSetting setting)
        {

            Name = setting.Name;
            Display = setting.Display;
            ExtractCsv = setting.ExtractCsv;
            ExtractSql = setting.ExtractSql;
            Destination = setting.Destination;
            Rank = setting.Rank;
            IsActive = setting.IsActive;
        }
    }
}
