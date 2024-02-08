using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{
    [Table("RelationshipsExtracts")]
    public class RelationshipsExtractView : RelationshipsExtract
    {
        public PatientExtractView PatientExtractView { get; set; }
    }
}
