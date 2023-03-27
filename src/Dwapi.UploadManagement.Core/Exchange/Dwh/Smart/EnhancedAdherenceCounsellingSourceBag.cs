using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;
using System.Collections.Generic;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class EnhancedAdherenceCounsellingMessageSourceBag : MessageSourceBag<EnhancedAdherenceCounsellingExtractView>{
        public override string EndPoint => "EnhancedAdherenceCounselling";
        public override string ExtractName => $"{nameof(EnhancedAdherenceCounsellingExtract)}";
        public  override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.EnhancedAdherenceCounselling;

        public List<EnhancedAdherenceCounsellingExtractView> _EnhancedAdherenceCounsellingExtractView { get; set; }
        public EnhancedAdherenceCounsellingMessageSourceBag()
        {
        }
        public EnhancedAdherenceCounsellingMessageSourceBag(List<EnhancedAdherenceCounsellingExtractView> enhancedAdherenceCounsellingExtractView)
        {
            _EnhancedAdherenceCounsellingExtractView = enhancedAdherenceCounsellingExtractView;
        }
    }
}
