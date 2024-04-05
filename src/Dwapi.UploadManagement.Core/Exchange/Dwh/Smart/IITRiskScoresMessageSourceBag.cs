using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;
using System.Collections.Generic;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class IITRiskScoresMessageSourceBag : MessageSourceBag<IITRiskScoresExtractView>{
        public override string EndPoint => "IITRiskScores";
        public override string ExtractName => $"{nameof(IITRiskScoresExtract)}";
        public  override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.IITRiskScores;

        public List<IITRiskScoresExtractView> _IITRiskScoresExtractView { get; set; }
        public IITRiskScoresMessageSourceBag()
        {
        }
        public IITRiskScoresMessageSourceBag(List<IITRiskScoresExtractView> IITRiskScoresExtractView)
        {
            _IITRiskScoresExtractView = IITRiskScoresExtractView;
        }
    }
}
