using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Dwh
{
    
    public class PatientStatusExtract : ClientExtract
    {
        public string FacilityName { get; set; }
        public string ExitDescription { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }
    }
}
