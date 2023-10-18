using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;
using System.Collections.Generic;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class ArtFastTrackMessageSourceBag : MessageSourceBag<ArtFastTrackExtractView>
    {
        public override string EndPoint => "ArtFastTrack";
        public override string ExtractName => $"ArtFastTrackExtract";
        public override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.ArtFastTrack;


        public List<ArtFastTrackExtractView> _ArtFastTrackExtractView { get; set; }        
        public ArtFastTrackMessageSourceBag()
        {
        }
        public ArtFastTrackMessageSourceBag(List<ArtFastTrackExtractView> artFastTrackExtractView)
        {
            _ArtFastTrackExtractView = artFastTrackExtractView;
        }
    }
}
