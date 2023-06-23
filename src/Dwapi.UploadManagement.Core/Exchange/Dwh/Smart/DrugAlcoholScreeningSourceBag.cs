using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;
using System.Collections.Generic;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class DrugAlcoholScreeningMessageSourceBag : MessageSourceBag<DrugAlcoholScreeningExtractView>{
        public override string EndPoint => "DrugAlcoholScreening";
        public override string ExtractName => $"{nameof(DrugAlcoholScreeningExtract)}";
        public  override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.DrugAlcoholScreening;

        public List<DrugAlcoholScreeningExtractView> _DrugAlcoholScreeningExtractView { get; set; }
        public DrugAlcoholScreeningMessageSourceBag()
        {
        }
        public DrugAlcoholScreeningMessageSourceBag(List<DrugAlcoholScreeningExtractView> drugAlcoholScreeningExtractView)
        {
            _DrugAlcoholScreeningExtractView = drugAlcoholScreeningExtractView;
        }
    }
}
