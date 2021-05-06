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
        public Guid Session { get; }
        public HandshakeStart(string name,  string version,Guid session)
        {
            Session = session;
            Name = name;
            Version = version;
        }
    }
}
