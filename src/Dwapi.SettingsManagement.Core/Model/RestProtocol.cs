using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class RestProtocol : AuthProtocol
    {
        public Guid EmrSystemId { get; set; }

        public ICollection<Resource> Resources { get; set; }=new List<Resource>();

        [NotMapped]
        public Resource Metric => Resources.FirstOrDefault(x => x.Name.IsSameAs("EMR Metrics"));

        [NotMapped] public bool SupportsDifferential => CheckDiffSupport();

        private bool CheckDiffSupport()
        {
            return EmrSystemId == new Guid("a6221856-0e85-11e8-ba89-0ed5f89f718b");
        }

        public RestProtocol()
        {
        }

        public RestProtocol(string url, string authToken)
        {
            Url = url;
            AuthToken = authToken;
        }

        public RestProtocol(string url, string authToken, Guid emrSystemId):this(url,authToken)
        {
            EmrSystemId = emrSystemId;
        }

        public void UpdateResources(Resource resource)
        {
            if (!Resources.Any(x => x.Name.IsSameAs(resource.Name)))
            {
                resource.RestProtocolId = Id;
                Resources.Add(resource);
            }
        }
    }
}
