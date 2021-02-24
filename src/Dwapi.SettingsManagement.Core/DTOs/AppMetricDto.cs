using System;
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
            Name = name.Contains("Master Patient Index") ? "PKV Services" : name;
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
    }
}
