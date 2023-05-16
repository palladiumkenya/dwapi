using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;
using System.Collections.Generic;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class CervicalCancerScreeningMessageSourceBag : MessageSourceBag<CervicalCancerScreeningExtractView>{
        public override string EndPoint => "CervicalCancerScreening";
        public override string ExtractName => $"{nameof(CervicalCancerScreeningExtract)}";
        public  override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.CervicalCancerScreening;

        public List<CervicalCancerScreeningExtractView> _CervicalCancerScreeningExtractView { get; set; }
        public CervicalCancerScreeningMessageSourceBag()
        {
        }
        public CervicalCancerScreeningMessageSourceBag(List<CervicalCancerScreeningExtractView> CervicalCancerScreeningExtractView)
        {
            _CervicalCancerScreeningExtractView = CervicalCancerScreeningExtractView;
        }
    }
}
