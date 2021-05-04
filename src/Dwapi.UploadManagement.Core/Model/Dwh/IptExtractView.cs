using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{
    [Table("IptExtracts")]
    public class IptExtractView : IptExtract
    {
        public PatientExtractView PatientExtractView { get; set; }
    }
}
