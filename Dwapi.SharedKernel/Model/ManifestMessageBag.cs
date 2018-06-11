using System.Collections.Generic;

namespace Dwapi.SharedKernel.Model
{
    public class ManifestMessageBag
    {
        public List<ManifestMessage> Messages { get; set; }=new List<ManifestMessage>();

        public ManifestMessageBag()
        {
        }

        public ManifestMessageBag(List<ManifestMessage> messages)
        {
            Messages = messages;
        }
    }
}