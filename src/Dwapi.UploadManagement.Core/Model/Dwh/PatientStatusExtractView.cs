using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{
    [Table("PatientStatusExtracts")]
    public class PatientStatusExtractView : PatientStatusExtract
    {
        public PatientExtractView PatientExtractView { get; set; }
    }
}
