using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{
    [Table("OvcExtracts")]
    public class OvcExtractView : OvcExtract
    {
        public PatientExtractView PatientExtractView { get; set; }
    }
}
