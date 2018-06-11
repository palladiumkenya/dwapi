using System.Collections.Generic;
using Dwapi.SharedKernel.Exchange;

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

        public static ManifestMessageBag Create(List<Manifest> manifests)
        {
            return new ManifestMessageBag(ManifestMessage.Create(manifests));
        }
    }
}