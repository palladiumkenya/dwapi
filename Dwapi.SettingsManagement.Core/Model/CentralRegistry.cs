using System;
using System.ComponentModel.DataAnnotations;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class CentralRegistry:Entity<Guid>
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Url { get; set; }
        [MaxLength(100)]
        public string AuthToken { get; set; }

        [MaxLength(50)]
        public string SubscriberId { get; set; } = "DWAPI";
        

        public CentralRegistry()
        {
        }

        public CentralRegistry(string name,string url)
        {
            Url = url;
            Name = name;
        }

        public CentralRegistry(string name, string url, string authToken):this(name,url)
        {
            AuthToken = authToken;
        }

        public bool RequiresAuthentication()
        {
            return !string.IsNullOrWhiteSpace(AuthToken);
        }

        public override string ToString()
        {
            return $"{Name} {Url}";
        }
    }
}