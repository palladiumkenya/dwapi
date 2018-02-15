using System.Collections.Generic;

namespace Dwapi.SharedKernel.Model
{
    public class SmartMessageBag
    {
        public List<SmartMessage> Messages { get; set; }

        public SmartMessageBag(List<SmartMessage> messages)
        {
            Messages = messages;
        }
    }
}