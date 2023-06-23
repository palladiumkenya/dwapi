using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;
using System.Collections.Generic;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class BaselineMessageSourceBag : MessageSourceBag<PatientBaselinesExtractView>{
        public override string EndPoint => "PatientBaselines";
        public override string ExtractName => $"PatientBaselineExtract";
        public override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.PatientBaseline;

        public List<PatientBaselinesExtractView> _PatientBaselinesExtractView { get; set; }
        public BaselineMessageSourceBag()
        {
        }
        public BaselineMessageSourceBag(List<PatientBaselinesExtractView> patientBaselinesExtractView)
        {
            _PatientBaselinesExtractView = patientBaselinesExtractView;
        }

    }
}
