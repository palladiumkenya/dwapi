using System;
using System.Data;
using MediatR;

namespace Dwapi.SettingsManagement.Core.Application.Metrics.Events
{
    public class ExtractLoaded : INotification
    {
        public string Name { get; }
        public int NoLoaded { get; }
        public string Version { get; }
        public DateTime ActionDate { get; } = DateTime.Now;

        public ExtractLoaded(string name,  string version)
        {
            Name = name;
            Version = version;
        }

        public ExtractLoaded(string name, int noLoaded, string version)
        {
            Name = name;
            NoLoaded = noLoaded;
            Version = version;
        }
    }
}
