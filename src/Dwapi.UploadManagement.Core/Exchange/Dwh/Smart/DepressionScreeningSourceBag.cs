using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;
using System.Collections.Generic;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class DepressionScreeningMessageSourceBag : MessageSourceBag<DepressionScreeningExtractView>{
        public override string EndPoint => "DepressionScreening";
        public override string ExtractName => $"{nameof(DepressionScreeningExtract)}";
        public  override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.DepressionScreening;

        public List<DepressionScreeningExtractView> _DepressionScreeningExtractView { get; set; }
        public DepressionScreeningMessageSourceBag()
        {
        }
        public DepressionScreeningMessageSourceBag(List<DepressionScreeningExtractView> depressionScreeningExtractView)
        {
            _DepressionScreeningExtractView = depressionScreeningExtractView;
        }
    }
}
