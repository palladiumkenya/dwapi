using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{
    [Table("IITRiskScoresExtracts")]
    public class IITRiskScoresExtractView : IITRiskScoresExtract
    {
        public PatientExtractView PatientExtractView { get; set; }
    }
}
