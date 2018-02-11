using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class Extract : DbExtract
    {
        public string Destination { get; set; }
        public Guid EmrSystemId { get; set; }
        public string DocketId { get; set; }
    }
}