using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using System;
using System.Collections.Generic;

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

        public void GenerateId()
        {
            Manifest.Id = LiveGuid.NewGuid();


            Manifest.Cargoes[0].Id=LiveGuid.NewGuid();
            Manifest.Cargoes[0].ManifestId = Manifest.Id;
        }
    }
}