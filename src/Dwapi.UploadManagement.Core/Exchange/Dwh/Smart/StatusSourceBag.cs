using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class StatusMessageSourceBag : MessageSourceBag<PatientStatusExtractView>
    {
        public override string EndPoint => "PatientStatus";
        public override string ExtractName => $"PatientStatusExtract";
    }
}
