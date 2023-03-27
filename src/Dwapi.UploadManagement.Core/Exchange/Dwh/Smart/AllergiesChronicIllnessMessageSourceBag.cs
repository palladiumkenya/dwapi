using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;
using System.Collections.Generic;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class AllergiesChronicIllnessMessageSourceBag : MessageSourceBag<AllergiesChronicIllnessExtractView>
    {
        public override string EndPoint => "AllergiesChronicIllness";
        public override string ExtractName => $"AllergiesChronicIllnessExtract";
        public override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.AllergiesChronicIllness;


        public List<AllergiesChronicIllnessExtractView> _AllergiesChronicIllnessExtractView { get; set; }        
        public AllergiesChronicIllnessMessageSourceBag()
        {
        }
        public AllergiesChronicIllnessMessageSourceBag(List<AllergiesChronicIllnessExtractView> allergiesChronicIllnessExtractView)
        {
            _AllergiesChronicIllnessExtractView = allergiesChronicIllnessExtractView;
        }
    }
}
