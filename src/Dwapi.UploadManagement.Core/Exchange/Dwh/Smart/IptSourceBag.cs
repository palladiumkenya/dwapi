using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class IptMessageSourceBag : MessageSourceBag<IptExtractView>{
        public override string EndPoint => "Ipt";
        public override string ExtractName => $"{nameof(IptExtract)}";}
}
