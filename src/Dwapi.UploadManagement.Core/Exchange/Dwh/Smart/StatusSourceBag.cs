using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class StatusMessageSourceBag : MessageSourceBag<PatientStatusExtractView>
    {
        public override string EndPoint => "PatientStatus";
        public override string ExtractName => $"PatientStatusExtract";
        public  override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.PatientStatus;
    }
}
