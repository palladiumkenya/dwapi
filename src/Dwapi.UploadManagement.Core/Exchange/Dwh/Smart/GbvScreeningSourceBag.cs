using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class GbvScreeningMessageSourceBag : MessageSourceBag<GbvScreeningExtractView>{
        public override string EndPoint => "GbvScreening";
        public override string ExtractName => $"{nameof(GbvScreeningExtract)}";}
}
