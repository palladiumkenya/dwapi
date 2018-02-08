using System;
using System.ComponentModel.DataAnnotations;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class CentralRegistry:Entity<Guid>
    {
        [MaxLength(100)]
        public string Url { get; set; }
        [MaxLength(100)]
        public string AuthToken { get; set; }

        public CentralRegistry()
        {
        }

        public CentralRegistry(string url)
        {
            Url = url;
        }

        public CentralRegistry(string url, string authToken):this(url)
        {
            AuthToken = authToken;
        }

        public override string ToString()
        {
            return Url;
        }
    }
}