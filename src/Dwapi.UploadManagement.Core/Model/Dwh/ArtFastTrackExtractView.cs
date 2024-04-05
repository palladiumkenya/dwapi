using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{
    [Table("ArtFastTrackExtracts")]
    public class ArtFastTrackExtractView : ArtFastTrackExtract
    {
        public PatientExtractView PatientExtractView { get; set; }
    }
}
