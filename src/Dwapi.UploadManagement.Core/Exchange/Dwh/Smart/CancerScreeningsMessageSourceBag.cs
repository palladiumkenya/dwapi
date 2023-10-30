using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;
using System.Collections.Generic;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class CancerScreeningMessageSourceBag : MessageSourceBag<CancerScreeningExtractView>{
        public override string EndPoint => "CancerScreening";
        public override string ExtractName => $"{nameof(CancerScreeningExtract)}";
        public  override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.CancerScreening;

        public List<CancerScreeningExtractView> _CancerScreeningExtractView { get; set; }
        public CancerScreeningMessageSourceBag()
        {
        }
        public CancerScreeningMessageSourceBag(List<CancerScreeningExtractView> CancerScreeningExtractView)
        {
            _CancerScreeningExtractView = CancerScreeningExtractView;
        }
    }
}
