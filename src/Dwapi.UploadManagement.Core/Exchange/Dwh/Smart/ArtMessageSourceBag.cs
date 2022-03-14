using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class ArtMessageSourceBag : MessageSourceBag<PatientArtExtractView>
    {
        public override string EndPoint => "PatientArt";
        public override string ExtractName => $"PatientArtExtract";
    }
}
