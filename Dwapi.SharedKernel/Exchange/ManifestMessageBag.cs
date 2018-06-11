using System.Collections.Generic;
using Dwapi.SharedKernel.Exchange;

namespace Dwapi.SharedKernel.Model
{
    public class ManifestMessageBag
    {
        public IEnumerable<ManifestMessage> Messages { get; set; }=new List<ManifestMessage>();

        public ManifestMessageBag()
        {
        }

        public ManifestMessageBag(IEnumerable<ManifestMessage> messages)
        {
            Messages = messages;
        }

        public static ManifestMessageBag Create(IEnumerable<Manifest> manifests)
        {
            return new ManifestMessageBag(ManifestMessage.Create(manifests));
        }
    }
}