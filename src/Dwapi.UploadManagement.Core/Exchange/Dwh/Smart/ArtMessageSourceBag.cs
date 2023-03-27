using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;
using System.Collections.Generic;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class ArtMessageSourceBag : MessageSourceBag<PatientArtExtractView>
    {
        public override string EndPoint => "PatientArt";
        public override string ExtractName => $"PatientArtExtract";
        public override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.PatientArt;

        public List<PatientArtExtractView> _PatientArtExtractView { get; set; }
        public ArtMessageSourceBag()
        {
        }
        public ArtMessageSourceBag(List<PatientArtExtractView> patientArtExtractView)
        {
            _PatientArtExtractView = patientArtExtractView;
        }
    }
}
