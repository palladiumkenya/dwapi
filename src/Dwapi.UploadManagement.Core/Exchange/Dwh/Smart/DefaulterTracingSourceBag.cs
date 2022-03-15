using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class DefaulterTracingMessageSourceBag : MessageSourceBag<DefaulterTracingExtractView>{
        public override string EndPoint => "DefaulterTracing";
        public override string ExtractName => $"{nameof(DefaulterTracingExtract)}";
        public  override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.DefaulterTracing;
    }
}
