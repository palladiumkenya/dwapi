using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class DrugAlcoholScreeningMessageSourceBag : MessageSourceBag<DrugAlcoholScreeningExtractView>{
        public override string EndPoint => "DrugAlcoholScreening";
        public override string ExtractName => $"{nameof(DrugAlcoholScreeningExtract)}";}
}
