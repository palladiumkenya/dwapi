using System.Collections.Generic;

namespace Dwapi.SharedKernel.Model
{
    public class ManifestMessage
    {
        public List<Manifest> Manifests { get; set; }=new List<Manifest>();

        public ManifestMessage()
        {
        }
        public ManifestMessage(List<Manifest> manifests)
        {
            Manifests = manifests;
        }
    }
}