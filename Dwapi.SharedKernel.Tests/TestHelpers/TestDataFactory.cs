using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.Model;
using FizzWare.NBuilder;

namespace Dwapi.SharedKernel.Tests.TestHelpers
{
    public class TestDataFactory
    {
        public static ManifestMessage ManifestMessage(int count,params int[] siteCodes)
        {
            var list = Builder<ManifestMessage>.CreateNew().Build();

            list.Manifests.AddRange(ManifestMessages(count,siteCodes));

            return list;
        }

        private static List<Manifest> ManifestMessages(int count, params int[] siteCodes)
        {
            var list=new List<Manifest>();

            foreach (var siteCode in siteCodes)
            {
                var manifests = Builder<Manifest>.CreateListOfSize(count).All().With(x => x.SiteCode == siteCode).Build()
                    .ToList();
                list.AddRange(manifests);
            }

            return list;
        }
    }
}