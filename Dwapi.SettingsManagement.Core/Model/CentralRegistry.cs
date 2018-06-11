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

        public CentralRegistry(string url, string docketId) : base(url, docketId)
        {
        }

        public CentralRegistry(string name, string url, string docketId) : base(name, url, docketId)
        {
        }

        public CentralRegistry(string name, string url, string docketId, string authToken) : base(name, url, docketId, authToken)
        {
        }
    }
}