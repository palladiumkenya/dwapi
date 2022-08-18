using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{
    [Table("CovidExtracts")]
    public class CovidExtractView : CovidExtract
    {
        public PatientExtractView PatientExtractView { get; set; }
    }
}
