using System.Collections.Generic;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Services
{
    public interface IPrepExtractSentServcie
    {
        void UpdateSendStatus(ExtractType extractType, List<SentItem> sentItems);
    }
}
