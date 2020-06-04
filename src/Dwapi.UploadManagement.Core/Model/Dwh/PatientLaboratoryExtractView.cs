using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{

    [Table("PatientLaboratoryExtracts")]
    public class PatientLaboratoryExtractView : PatientLaboratoryExtract
    {

        public PatientExtractView PatientExtractView { get; set; }
    }
}
