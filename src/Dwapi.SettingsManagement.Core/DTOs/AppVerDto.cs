using System;
using System.Linq;

namespace Dwapi.SettingsManagement.Core.DTOs
{
    public class AppVerDto
    {
        public string LocalVersion { get; }
        public string LiveVersion { get;  }
        public bool UpdateAvailable => CompareVersions();

        public AppVerDto(string localVersion, string content)
        {
            LocalVersion = localVersion;
            if (!string.IsNullOrWhiteSpace(content))
            {
                var lines = content.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                var ver = lines.FirstOrDefault(x => x.Contains("ProductVersion"));
                if (null != ver)
                    LiveVersion = ver.Split("=").LastOrDefault();
            }

        }

        private bool CompareVersions()
        {
            if (string.IsNullOrWhiteSpace(LiveVersion))
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(LiveVersion))
            {
                return LiveVersion.Trim() != LocalVersion.Trim();
            }

            return true;
        }

        public override string ToString()
        {
            return $"Current:{LiveVersion}, Local:{LocalVersion}, UpdateAvailable:{UpdateAvailable}";
        }
    }
}
