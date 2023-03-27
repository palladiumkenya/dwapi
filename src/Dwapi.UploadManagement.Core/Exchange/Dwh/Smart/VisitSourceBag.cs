using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;
using System.Collections.Generic;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class VisitMessageSourceBag : MessageSourceBag<PatientVisitExtractView>
    {
        public override string EndPoint => "PatientVisits";
        public override string ExtractName => $"PatientVisitExtract";

        public  override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.PatientVisit;

        public List<PatientVisitExtractView> _PatientVisitExtractView { get; set; }
        public VisitMessageSourceBag()
        {
        }
        public VisitMessageSourceBag(List<PatientVisitExtractView> patientVisitExtractView)
        {
            _PatientVisitExtractView = patientVisitExtractView;
        }
    }
}
