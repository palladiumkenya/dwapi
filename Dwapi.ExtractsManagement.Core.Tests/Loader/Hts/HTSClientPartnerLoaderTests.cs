using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Hts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SharedKernel.Utility;
using FizzWare.NBuilder;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests.Loader.Hts
{
    [TestFixture]
    public class HTSClientPartnerLoaderTests
    {
        private ExtractsContext _extractsContext, _extractsContextMySql;

        [OneTimeSetUp]
        public void Init()
        {
            var extracts = TestInitializer.Iqtools.Extracts.Where(x => Extentions.IsSameAs(x.DocketId, "HTS"));
            var cleaner = TestInitializer.ServiceProvider.GetService<ICleanHtsExtracts>();
            cleaner.Clean(extracts.Select(x=>x.Id).ToList());

            var extractsMySql = TestInitializer.KenyaEmr.Extracts.Where(x => x.DocketId.IsSameAs("HTS"));
            var cleanerMySql = TestInitializer.ServiceProviderMysql.GetService<ICleanHtsExtracts>();
            cleanerMySql.Clean(extractsMySql.Select(x=>x.Id).ToList());

            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            _extractsContextMySql = TestInitializer.ServiceProviderMysql.GetService<ExtractsContext>();

            var tempHtsClientExtracts = Builder<TempHTSClientExtract>.CreateListOfSize(2).Build().ToList();
            tempHtsClientExtracts[0].EncounterId = (int?) DateTime.Now.Ticks;
            tempHtsClientExtracts[1].EncounterId = (int?)DateTime.Now.Ticks;

            var tempHtsClientPartnerExtracts = Builder<TempHTSClientPartnerExtract>.CreateListOfSize(2).Build().ToList();
            var tempHtsClientLinkageExtracts = Builder<TempHTSClientLinkageExtract>.CreateListOfSize(2).Build().ToList();

            _extractsContext.AddRange(tempHtsClientExtracts);
            _extractsContext.AddRange(tempHtsClientPartnerExtracts);
            _extractsContext.AddRange(tempHtsClientLinkageExtracts);
            _extractsContext.SaveChanges();

            _extractsContextMySql.AddRange(tempHtsClientExtracts);
            _extractsContextMySql.AddRange(tempHtsClientPartnerExtracts);
            _extractsContextMySql.AddRange(tempHtsClientLinkageExtracts);
        }

        [Test]
        public void should_Load_From_Temp_MsSQL()
        {
            Assert.False(_extractsContext.HtsClientPartnerExtracts.Any());
            var extract = TestInitializer.Iqtools.Extracts.First(x => x.Name.IsSameAs(nameof(HTSClientPartnerExtract)));

            var loader = TestInitializer.ServiceProvider.GetService<IHTSClientPartnerLoader>();

            var loadCount = loader.Load(extract.Id, 0).Result;
            Assert.True(_extractsContext.HtsClientPartnerExtracts.Any());
            Console.WriteLine($"extracted {_extractsContext.HtsClientPartnerExtracts.Count()}");
        }

        [Test]
        public void should_Load_From_Temp_MySQL()
        {
            Assert.False(_extractsContextMySql.HtsClientPartnerExtracts.Any());
            var extract = TestInitializer.KenyaEmr.Extracts.First(x => x.Name.IsSameAs(nameof(HTSClientPartnerExtract)));

            var loader = TestInitializer.ServiceProviderMysql.GetService<IHTSClientPartnerLoader>();

            var loadCount = loader.Load(extract.Id, 0).Result;
            Assert.True(_extractsContextMySql.HtsClientPartnerExtracts.Any());
            Console.WriteLine($"extracted {_extractsContextMySql.HtsClientPartnerExtracts.Count()}");
        }
    }
}