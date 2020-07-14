using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{

    [Table("PatientArtExtracts")]
    public class PatientArtExtractView : PatientArtExtract
    {
        public PatientExtractView PatientExtractView { get; set; }
    }
}
