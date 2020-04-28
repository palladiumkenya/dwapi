using System.Collections.Generic;

namespace Dwapi.SharedKernel.Exchange
{
    public class DwhManifestMessageBag
    {
        public List<DwhManifestMessage> Messages { get; set; }=new List<DwhManifestMessage>();

        public DwhManifestMessageBag()
        {
        }

        public DwhManifestMessageBag(List<DwhManifestMessage> messages)
        {
            Messages = messages;
        }

        public static DwhManifestMessageBag Create(List<DwhManifest> manifests)
        {
            return new DwhManifestMessageBag(DwhManifestMessage.Create(manifests));
        }
    }
}