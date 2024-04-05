using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;
using System.Collections.Generic;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class AdverseEventMessageSourceBag : MessageSourceBag<PatientAdverseEventView>
    {
        public override string EndPoint => "PatientAdverseEvents";
        public override string ExtractName => $"PatientAdverseEventExtract";
        public override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.PatientAdverseEvent;

        public List<PatientAdverseEventView> _PatientAdverseEventView { get; set; }
        public AdverseEventMessageSourceBag()
        {
        }
        public AdverseEventMessageSourceBag(List<PatientAdverseEventView> patientAdverseEventView)
        {
            _PatientAdverseEventView = patientAdverseEventView;
        }
    }
}
