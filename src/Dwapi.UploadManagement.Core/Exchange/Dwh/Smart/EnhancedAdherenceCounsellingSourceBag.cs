using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class EnhancedAdherenceCounsellingMessageSourceBag : MessageSourceBag<EnhancedAdherenceCounsellingExtractView>{
        public override string EndPoint => "EnhancedAdherenceCounselling";
        public override string ExtractName => $"{nameof(EnhancedAdherenceCounsellingExtract)}";}
}
