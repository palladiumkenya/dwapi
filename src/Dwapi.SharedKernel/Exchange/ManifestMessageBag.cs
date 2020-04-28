using System.Collections.Generic;

namespace Dwapi.SharedKernel.Exchange
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

        public static ManifestMessageBag Create(List<Manifest> manifests)
        {
            return new ManifestMessageBag(ManifestMessage.Create(manifests));
        }
    }
}