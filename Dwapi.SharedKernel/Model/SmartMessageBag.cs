using System.Collections.Generic;

namespace Dwapi.SharedKernel.Model
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