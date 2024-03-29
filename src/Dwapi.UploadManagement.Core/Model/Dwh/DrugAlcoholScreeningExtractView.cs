using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{
    [Table("DrugAlcoholScreeningExtracts")]
    public class DrugAlcoholScreeningExtractView : DrugAlcoholScreeningExtract
    {
        public PatientExtractView PatientExtractView { get; set; }
    }
}
