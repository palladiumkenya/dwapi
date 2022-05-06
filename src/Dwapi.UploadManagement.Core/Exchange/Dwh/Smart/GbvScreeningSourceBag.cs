using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class GbvScreeningMessageSourceBag : MessageSourceBag<GbvScreeningExtractView>{
        public override string EndPoint => "GbvScreening";
        public override string ExtractName => $"{nameof(GbvScreeningExtract)}";
        public  override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.GbvScreening;
    }
}
