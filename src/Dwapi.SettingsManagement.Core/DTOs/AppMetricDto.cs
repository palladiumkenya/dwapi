using System;
using System.Reflection.Metadata.Ecma335;
using Humanizer;

namespace Dwapi.SettingsManagement.Core.DTOs
{
    public class AppMetricDto
    {
        public string Name { get;  }
        public string Action { get;  }
        public DateTime LastAction { get;  }
        public int Rank { get;  }
        public string TimeAgo => GetTimeAgo();

        public AppMetricDto(string name, string action, DateTime lastAction, int rank)
        {
            Name = FormatName(name);
            Action = action;
            LastAction = lastAction;
            Rank = rank;
        }

        private string GetTimeAgo()
        {
            if (LastAction.Year == 1983)
                return string.Empty;

            return LastAction.Humanize(false);
        }

        private string FormatName(string name)
        {
            if (name.Contains("Master Patient Index"))
                return "PKV Services";

            if (name.Contains("Hiv Testing Services"))
                return "HIV Testing Services";

            return name;
        }
    }
}
