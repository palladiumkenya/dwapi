using System;
using Newtonsoft.Json;

namespace Dwapi.SharedKernel.Model
{
    public class SmartMessage
    {
        [JsonIgnore]
        public Guid Eid { get; set; }

        public string Id { get; set; }
        public string PayLoad { get; set; }
    }
}