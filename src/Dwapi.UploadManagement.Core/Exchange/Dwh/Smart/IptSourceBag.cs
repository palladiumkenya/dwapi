using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;
using System.Collections.Generic;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class IptMessageSourceBag : MessageSourceBag<IptExtractView>{
        public override string EndPoint => "Ipt";
        public override string ExtractName => $"{nameof(IptExtract)}";
        public  override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.Ipt;

        public List<IptExtractView> _IptExtractView { get; set; }
        public IptMessageSourceBag()
        {
        }
        public IptMessageSourceBag(List<IptExtractView> iptExtractView)
        {
            _IptExtractView = iptExtractView;
        }
    }
}
