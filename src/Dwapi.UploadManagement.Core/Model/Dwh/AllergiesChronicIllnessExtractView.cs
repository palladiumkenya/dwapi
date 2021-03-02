using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{
    [Table("AllergiesChronicIllnessExtracts")]
    public class AllergiesChronicIllnessExtractView : AllergiesChronicIllnessExtract
    {
        public PatientExtractView PatientExtractView { get; set; }
    }
}
