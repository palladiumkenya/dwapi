using System;
using MediatR;

namespace Dwapi.SettingsManagement.Core.Application.Metrics.Events
{
    public class HandshakeStart : INotification
    {
        public string Name { get; }
        public DateTime Start { get; }=DateTime.Now;
        public string Version { get; }
        public DateTime ActionDate { get; }=DateTime.Now;

        public HandshakeStart(string name,  string version)
        {
            Name = name;
            Version = version;
        }
    }
}
