using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class CovidMessageSourceBag : MessageSourceBag<CovidExtractView>{
        public override string EndPoint => "Covid";
        public override string ExtractName => $"{nameof(CovidExtract)}";}
}
