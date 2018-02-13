using System;
using System.ComponentModel.DataAnnotations;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class CentralRegistry:Registry
    {
        public override string SubscriberId { get; set; } = "DWAPI";

        public CentralRegistry()
        {
        }

        public CentralRegistry(string url) : base(url)
        {
        }

        public CentralRegistry(string name, string url) : base(name, url)
        {
        }

        public CentralRegistry(string name, string url, string authToken) : base(name, url, authToken)
        {
        }
    }
}