using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class LaboratoryMessageSourceBag : MessageSourceBag<PatientLaboratoryExtractView>{
        public override string EndPoint => "PatientLabs";
        public override string ExtractName => $"PatientLabExtract";
        public  override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.PatientLab;
    }
}
