using System;
using MediatR;

namespace Dwapi.SettingsManagement.Core.Application.Metrics.Events
{
    public class HandshakeEnd : INotification
    {
        public string Name { get; }
        public DateTime End { get; }=DateTime.Now;
        public string Version { get; }
        public DateTime ActionDate { get; }=DateTime.Now;
        public Guid Session { get; private set; }
        public string EndName => Name.Replace("End", "Start");

        public HandshakeEnd(string name, string version)
        {
            Name = name;
            Version = version;
        }

        public void UpdateSession(Guid session)
        {
            Session = session;
        }
    }
}
