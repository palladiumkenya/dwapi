using System;

namespace Dwapi.SettingsManagement.Core.DTOs
{
    public class AppMetricDto
    {
        public string Name { get;  }
        public string Action { get;  }
        public DateTime LastAction { get;  }
        public int Rank { get;  }

        public AppMetricDto(string name, string action, DateTime lastAction, int rank)
        {
            Name = name;
            Action = action;
            LastAction = lastAction;
            Rank = rank;
        }
    }
}
