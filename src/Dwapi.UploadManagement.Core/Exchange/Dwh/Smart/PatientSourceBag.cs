using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class PatientMessageSourceBag : MessageSourceBag<PatientExtractView>
    {
        public override string EndPoint => "Patient";
        public override string ExtractName => $"{nameof(PatientExtract)}";

    }
}
