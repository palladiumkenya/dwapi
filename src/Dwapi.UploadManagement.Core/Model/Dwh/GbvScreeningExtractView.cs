using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{
    [Table("GbvScreeningExtracts")]
    public class GbvScreeningExtractView : GbvScreeningExtract
    {
        public PatientExtractView PatientExtractView { get; set; }
    }
}
