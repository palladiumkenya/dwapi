using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;
using System.Collections.Generic;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class PharmacyMessageSourceBag : MessageSourceBag<PatientPharmacyExtractView>{
        public override string EndPoint => "PatientPharmacy";
        public override string ExtractName => $"PatientPharmacyExtract";

        public  override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.PatientPharmacy;

        public List<PatientPharmacyExtractView> _PatientPharmacyExtractView { get; set; }
        public PharmacyMessageSourceBag()
        {
        }
        public PharmacyMessageSourceBag(List<PatientPharmacyExtractView> patientPharmacyExtractView)
        {
            _PatientPharmacyExtractView = patientPharmacyExtractView;
        }
    }
}
