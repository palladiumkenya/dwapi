using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{
    [Table("OtzExtracts")]
    public class OtzExtractView : OtzExtract
    {
        public PatientExtractView PatientExtractView { get; set; }
    }
}
