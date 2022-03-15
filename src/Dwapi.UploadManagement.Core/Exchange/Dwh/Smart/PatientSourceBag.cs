using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class PatientMessageSourceBag : MessageSourceBag<PatientExtractView>
    {
        public override string EndPoint => "Patient";
        public override string ExtractName => $"{nameof(PatientExtract)}";

        public  override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.Patient;

    }
}
