using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Hts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SharedKernel.Utility;
using FizzWare.NBuilder;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests.Loader.Hts
{ 
    [TestFixture]
    public class HtsTestKitsLoaderTests
    {
        private ExtractsContext _extractsContext, _extractsContextMySql;

        [OneTimeSetUp]
        public void Init()
        {
            var extracts = TestInitializer.Iqtools.Extracts.Where(x => x.DocketId.IsSameAs("HTS"));
            var cleaner = TestInitializer.ServiceProvider.GetService<IClearHtsExtracts>();
            cleaner.Clear(extracts.Select(x => x.Id).ToList());

            var extractsMySql = TestInitializer.KenyaEmr.Extracts.Where(x => x.DocketId.IsSameAs("HTS"));
            var cleanerMySql = TestInitializer.ServiceProviderMysql.GetService<IClearHtsExtracts>();
            cleanerMySql.Clear(extractsMySql.Select(x => x.Id).ToList());

            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            _extractsContextMySql = TestInitializer.ServiceProviderMysql.GetService<ExtractsContext>();

            var tempHtsClientsExtracts = Builder<TempHtsClients>.CreateListOfSize(2).Build().ToList();
            var tempHtsClientTestsExtracts = Builder<TempHtsClientTests>.CreateListOfSize(2).Build().ToList();
            var tempHtsTestKitsExtracts = Builder<TempHtsTestKits>.CreateListOfSize(2).Build().ToList();
            var tempHtsClientsLinkageExtracts = Builder<TempHtsClientLinkage>.CreateListOfSize(2).Build().ToList();
            var tempHtsClientTracingExtracts = Builder<TempHtsClientTracing>.CreateListOfSize(2).Build().ToList();
            var tempHtsPartnerTracingExtracts = Builder<TempHtsPartnerTracing>.CreateListOfSize(2).Build().ToList();
            var tempHtsPartnerNotificationServicesExtracts = Builder<TempHtsPartnerNotificationServices>.CreateListOfSize(2).Build().ToList();

            _extractsContext.AddRange(tempHtsClientsExtracts);
            _extractsContext.AddRange(tempHtsClientTestsExtracts);
            _extractsContext.AddRange(tempHtsTestKitsExtracts);
            _extractsContext.AddRange(tempHtsClientsLinkageExtracts);
            _extractsContext.AddRange(tempHtsPartnerTracingExtracts);
            _extractsContext.AddRange(tempHtsClientTracingExtracts);
            _extractsContext.AddRange(tempHtsPartnerNotificationServicesExtracts);
            _extractsContext.SaveChanges();

            _extractsContextMySql.AddRange(tempHtsClientsExtracts);
            _extractsContextMySql.AddRange(tempHtsClientTestsExtracts);
            _extractsContextMySql.AddRange(tempHtsTestKitsExtracts);
            _extractsContextMySql.AddRange(tempHtsClientsLinkageExtracts);
            _extractsContextMySql.AddRange(tempHtsPartnerTracingExtracts);
            _extractsContextMySql.AddRange(tempHtsClientTracingExtracts);
            _extractsContextMySql.AddRange(tempHtsPartnerNotificationServicesExtracts);
            _extractsContextMySql.SaveChanges();
        }

        [Test]
        public void should_Load_From_Temp_MsSQL()
        {
            Assert.False(_extractsContext.HtsTestKitsExtracts.Any());
            var extract = TestInitializer.Iqtools.Extracts.First(x => x.Name.IsSameAs(nameof(HtsTestKits)));

            var loader = TestInitializer.ServiceProvider.GetService<IHtsTestKitsLoader>();

            var loadCount = loader.Load(extract.Id, 0).Result;
            Assert.True(_extractsContext.HtsTestKitsExtracts.Any());
            Console.WriteLine($"extracted {_extractsContext.HtsTestKitsExtracts.Count()}");
        }

        [Test]
        public void should_Load_From_Temp_MySQL()
        {
            Assert.False(_extractsContextMySql.HtsTestKitsExtracts.Any());
            var extract = TestInitializer.KenyaEmr.Extracts.First(x => x.Name.IsSameAs(nameof(HtsTestKits)));

            var loader = TestInitializer.ServiceProviderMysql.GetService<IHtsTestKitsLoader>();

            var loadCount = loader.Load(extract.Id, 0).Result;
            Assert.True(_extractsContextMySql.HtsTestKitsExtracts.Any());
            Console.WriteLine($"extracted {_extractsContextMySql.HtsTestKitsExtracts.Count()}");
        }
    }
}
