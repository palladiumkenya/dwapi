using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;
using System.Collections.Generic;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class RelationshipsMessageSourceBag : MessageSourceBag<RelationshipsExtractView>
    {
        public override string EndPoint => "Relationships";
        public override string ExtractName => $"RelationshipsExtract";
        public override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.Relationships;


        public List<RelationshipsExtractView> _RelationshipsExtractView { get; set; }        
        public RelationshipsMessageSourceBag()
        {
        }
        public RelationshipsMessageSourceBag(List<RelationshipsExtractView> allergiesChronicIllnessExtractView)
        {
            _RelationshipsExtractView = allergiesChronicIllnessExtractView;
        }
    }
}