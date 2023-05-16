using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{
    [Table("CervicalCancerScreeningExtracts")]
    public class CervicalCancerScreeningExtractView : CervicalCancerScreeningExtract
    {
        public PatientExtractView PatientExtractView { get; set; }
    }
}
