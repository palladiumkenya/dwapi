using System.Collections.Generic;

namespace Dwapi.SharedKernel.Exchange
{
    public class DwhManifestMessage
    {
        public DwhManifest Manifest { get; set; }

        public DwhManifestMessage()
        {
        }

        public DwhManifestMessage(DwhManifest manifest)
        {
            Manifest = manifest;
        }

        public static List<DwhManifestMessage> Create(List<DwhManifest> manifests)
        {
            var list=new List<DwhManifestMessage>();
            foreach (var manifest in manifests)
            {
                list.Add(new DwhManifestMessage(manifest));
            }

            return list;
        }
    }
}