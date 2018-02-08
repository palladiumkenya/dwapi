using System;
using System.ComponentModel.DataAnnotations;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class RestProtocol : Entity<Guid>
    {
        [MaxLength(100)]
        public string Url { get; set; }

        [MaxLength(100)]
        public string AuthToken { get; set; }

        public Guid EmrSystemId { get; set; }
    }
}