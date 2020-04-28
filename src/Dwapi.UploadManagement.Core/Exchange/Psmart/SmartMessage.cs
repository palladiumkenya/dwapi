using System;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Psmart
{
    public class SmartMessage
    {
        [JsonIgnore]
        public Guid Eid { get; set; }

        public string Id { get; set; }
        public string PayLoad { get; set; }
    }
}