using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{
    [Table("PatientVisitExtracts")]
    public class PatientVisitExtractView : PatientVisitExtract
    {
        public PatientExtractView PatientExtractView { get; set; }
    }
}
