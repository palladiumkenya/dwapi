using System.Collections.Generic;
using Dwapi.UploadManagement.Core.Model.Crs.Dtos;

namespace Dwapi.UploadManagement.Core.Exchange.Crs
{
    public class CrsMessageBag
    {
        public List<CrsMessage> Messages { get; set; } = new List<CrsMessage>();

        public CrsMessageBag()
        {
        }

        public CrsMessageBag(List<CrsMessage> messages)
        {
            Messages = messages;
        }

        public static CrsMessageBag Create(List<ClientRegistryExtractDto> clientRegistryExtracts)
        {
            return new CrsMessageBag(CrsMessage.Create(clientRegistryExtracts));
        }
    }
}
