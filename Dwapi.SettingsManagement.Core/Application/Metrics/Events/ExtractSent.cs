using System;
using MediatR;

namespace Dwapi.SettingsManagement.Core.Application.Metrics.Events
{
    public class ExtractSent : INotification
    {
        public string Name { get; }
        public int NoSent { get; }
        public string Version { get; }
        public DateTime ActionDate { get; }=DateTime.Now;

        public ExtractSent(string name,  string version)
        {
            Name = name;
            Version = version;
        }

        public ExtractSent(string name, int noSent, string version)
        {
            Name = name;
            NoSent = noSent;
            Version = version;
        }
    }
}
