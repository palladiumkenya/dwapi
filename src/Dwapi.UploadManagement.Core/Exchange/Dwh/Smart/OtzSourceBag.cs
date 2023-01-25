using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;
using System.Collections.Generic;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class OtzMessageSourceBag : MessageSourceBag<OtzExtractView>{
        public override string EndPoint => "Otz";
        public override string ExtractName => $"{nameof(OtzExtract)}";
        public  override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.Otz;

        public List<OtzExtractView> _OtzExtractView { get; set; }
        public OtzMessageSourceBag()
        {
        }
        public OtzMessageSourceBag(List<OtzExtractView> otzExtractView)
        {
            _OtzExtractView = otzExtractView;
        }

    }
}
