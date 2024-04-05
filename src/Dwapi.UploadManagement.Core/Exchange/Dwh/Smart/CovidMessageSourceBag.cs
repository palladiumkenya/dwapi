using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;
using System.Collections.Generic;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class CovidMessageSourceBag : MessageSourceBag<CovidExtractView>{
        public override string EndPoint => "Covid";
        public override string ExtractName => $"{nameof(CovidExtract)}";
        public  override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.Covid;

        public List<CovidExtractView> _CovidExtractView { get; set; }
        public CovidMessageSourceBag()
        {
        }
        public CovidMessageSourceBag(List<CovidExtractView> covidExtractView)
        {
            _CovidExtractView = covidExtractView;
        }
    }
}
