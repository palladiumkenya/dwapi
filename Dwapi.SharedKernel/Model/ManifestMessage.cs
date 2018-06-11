using System.Collections.Generic;

namespace Dwapi.SharedKernel.Model
{
    public class ManifestMessage
    {
  public  Manifest Manifest { get; set; }

        public ManifestMessage()
        {
        }
        public ManifestMessage(Manifest manifest)
        {
            Manifest = manifest;
        }
    }
}