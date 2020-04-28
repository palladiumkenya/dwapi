using System.Collections.Generic;

namespace Dwapi.UploadManagement.Core.Exchange.Psmart
{
    public class SmartMessageBag
    {
        public List<SmartMessage> Message { get; set; }

        public SmartMessageBag(List<SmartMessage> message)
        {
            Message = message;
        }
    }
}