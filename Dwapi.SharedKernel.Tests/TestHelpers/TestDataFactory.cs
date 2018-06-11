using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.Model;
using FizzWare.NBuilder;

namespace Dwapi.SharedKernel.Tests.TestHelpers
{
    public class TestDataFactory
    {
        public static ManifestMessageBag ManifestMessageBag(int count,params int[] siteCodes)
        {
            var list = Builder<ManifestMessageBag>.CreateNew().Build();

            list.Messages.AddRange(ManifestMessages(count,siteCodes));

            return list;
        }

        private static List<ManifestMessage> ManifestMessages(int count, params int[] siteCodes)
        {
            var list=new List<ManifestMessage>();

            foreach (var siteCode in siteCodes)
            {
                var manifests = Builder<Manifest>.CreateListOfSize(count).All().With(x => x.SiteCode = siteCode).Build()
                    .ToList();
                foreach (var manifest in manifests)
                {
                    list.Add(new ManifestMessage(manifest));
                }
            }
            return list;
        }
    }
}