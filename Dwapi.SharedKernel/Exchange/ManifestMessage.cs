using System.Collections.Generic;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SharedKernel.Exchange
{
    public class ManifestMessage
    {
        public Manifest Manifest { get; set; }

        public ManifestMessage()
        {
        }

        public ManifestMessage(Manifest manifest)
        {
            Manifest = manifest;
        }

        public static List<ManifestMessage> Create(List<Manifest> manifests)
        {
            var list=new List<ManifestMessage>();
            foreach (var manifest in manifests)
            {
                list.Add(new ManifestMessage(manifest));
            }

            return list;
        }
    }
}