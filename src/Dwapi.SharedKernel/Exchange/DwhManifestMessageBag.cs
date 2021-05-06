using System;
using System.Collections.Generic;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SharedKernel.Exchange
{
    public class DwhManifestMessageBag
    {
        public Guid Session { get;  }
        public List<DwhManifestMessage> Messages { get; set; }=new List<DwhManifestMessage>();

        public DwhManifestMessageBag()
        {
        }

        public DwhManifestMessageBag(List<DwhManifestMessage> messages)
        {
            Session = LiveGuid.NewGuid();
            var sessionStart = DateTime.Now;
            foreach (var message in messages)
            {
                message.Manifest.InitSession(Session,sessionStart,string.Empty);
            }
            Messages = messages;
        }

        public static DwhManifestMessageBag Create(List<DwhManifest> manifests)
        {
            return new DwhManifestMessageBag(DwhManifestMessage.Create(manifests));
        }
    }
}
