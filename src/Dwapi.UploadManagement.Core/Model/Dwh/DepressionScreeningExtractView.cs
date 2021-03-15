using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{
    [Table("DepressionScreeningExtracts")]
    public class DepressionScreeningExtractView : DepressionScreeningExtract
    {
        public PatientExtractView PatientExtractView { get; set; }
    }
}
