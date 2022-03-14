using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class PharmacyMessageSourceBag : MessageSourceBag<PatientPharmacyExtractView>{
        public override string EndPoint => "PatientPharmacy";
        public override string ExtractName => $"PatientPharmacyExtract";}
}
