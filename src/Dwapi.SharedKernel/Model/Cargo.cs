using System;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SharedKernel.Model
{
    public class Cargo
    {
        public Guid Id { get; set; } = LiveGuid.NewGuid();
        public CargoType Type { get; set; }
        public string Items { get; set; }
        public Guid ManifestId { get; set; }

        public Cargo(CargoType type, string items, Guid manifestId)
        {
            Type = type;
            Items = items;
            ManifestId = manifestId;
        }
    }
}