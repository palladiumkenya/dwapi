using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class OvcMessageSourceBag : MessageSourceBag<OvcExtractView>{
        public override string EndPoint => "Ovc";
        public override string ExtractName => $"{nameof(OvcExtract)}";
        public  override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.Ovc;
    }
}
