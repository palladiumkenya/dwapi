using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{
    [Table("DefaulterTracingExtracts")]
    public class DefaulterTracingExtractView : DefaulterTracingExtract
    {
        public PatientExtractView PatientExtractView { get; set; }
    }
}
