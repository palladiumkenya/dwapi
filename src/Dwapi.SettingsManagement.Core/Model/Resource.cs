using System;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class Resource : Entity<Guid>
    {
        public string Name { get; set; }
        
        public string EndPoint { get; set; }
        public Guid RestProtocolId { get; set; }

        public Resource()
        {
        }

        public Resource(string name, string endPoint, Guid restProtocolId)
        {
            Name = name;
            RestProtocolId = restProtocolId;
            EndPoint = endPoint;
        }
    }
}