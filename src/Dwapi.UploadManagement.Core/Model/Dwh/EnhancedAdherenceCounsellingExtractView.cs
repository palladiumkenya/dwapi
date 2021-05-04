using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{
    [Table("EnhancedAdherenceCounsellingExtracts")]
    public class EnhancedAdherenceCounsellingExtractView : EnhancedAdherenceCounsellingExtract
    {
        public PatientExtractView PatientExtractView { get; set; }
    }
}
