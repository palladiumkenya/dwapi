using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Hts; 
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts; 
using Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SharedKernel.Utility;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests.Cleaner.Hts
{
    [TestFixture]
    public class ClearHtsExtractsTests
    {
        private IServiceProvider _serviceProvider;
        private IServiceProvider _serviceProviderMysql;
        private ExtractsContext _extractsContext;
        private ExtractsContext _extractsContextMysql;

        [OneTimeSetUp]
        public void Init()
        {
            _serviceProvider = TestInitializer.ServiceProvider;
            _serviceProviderMysql = TestInitializer.ServiceProviderMysql;

            var tempHtsClientsExtracts = Builder<TempHtsClients>.CreateListOfSize(2).Build().ToList();
            var htsClientsExtracts = Builder<HtsClients>.CreateListOfSize(2).Build().ToList();

            var tempHtsClientTestsExtracts = Builder<TempHtsClientTests>.CreateListOfSize(2).Build().ToList();
            var htsClientTestsExtracts = Builder<HtsClientTests>.CreateListOfSize(2).Build().ToList();

            var tempHtsClientsLinkageExtracts = Builder<TempHtsClientLinkage>.CreateListOfSize(2).Build().ToList();
            var htsClientsLinkageExtracts = Builder<HtsClientLinkage>.CreateListOfSize(2).Build().ToList();

            var tempHtsTestKitsExtracts = Builder<TempHtsTestKits>.CreateListOfSize(2).Build().ToList();
            var htsTestKitsExtracts = Builder<HtsTestKits>.CreateListOfSize(2).Build().ToList();

            var tempHtsClientTracingExtracts = Builder<TempHtsClientTracing>.CreateListOfSize(2).Build().ToList();
            var htsClientTracingExtracts = Builder<HtsClientTracing>.CreateListOfSize(2).Build().ToList();

            var tempHtsPartnerTracingExtracts = Builder<TempHtsPartnerTracing>.CreateListOfSize(2).Build().ToList();
            var htsPartnerTracingExtracts = Builder<HtsPartnerTracing>.CreateListOfSize(2).Build().ToList();

            var tempHtsPartnerNotificationServicesExtracts = Builder<TempHtsPartnerNotificationServices>.CreateListOfSize(2).Build().ToList();
            var htsPartnerNotificationServicesExtracts = Builder<HtsPartnerNotificationServices>.CreateListOfSize(2).Build().ToList();

            _extractsContext = _serviceProvider.GetService<ExtractsContext>();
            _extractsContext.AddRange(tempHtsClientsExtracts);
            _extractsContext.AddRange(tempHtsClientTestsExtracts);
            _extractsContext.AddRange(tempHtsClientsLinkageExtracts);
            _extractsContext.AddRange(tempHtsTestKitsExtracts);
            _extractsContext.AddRange(tempHtsClientTracingExtracts);
            _extractsContext.AddRange(tempHtsPartnerTracingExtracts);
            _extractsContext.AddRange(tempHtsPartnerNotificationServicesExtracts); 
            _extractsContext.AddRange(htsClientsExtracts);
            _extractsContext.AddRange(htsClientTestsExtracts);
            _extractsContext.AddRange(htsClientsLinkageExtracts);
            _extractsContext.AddRange(htsTestKitsExtracts);
            _extractsContext.AddRange(htsClientTracingExtracts);
            _extractsContext.AddRange(htsPartnerTracingExtracts);
            _extractsContext.AddRange(htsPartnerNotificationServicesExtracts);
            _extractsContext.SaveChanges();

            _extractsContextMysql = _serviceProviderMysql.GetService<ExtractsContext>();
            _extractsContext.AddRange(tempHtsClientsExtracts);
            _extractsContext.AddRange(tempHtsClientTestsExtracts);
            _extractsContext.AddRange(tempHtsClientsLinkageExtracts);
            _extractsContext.AddRange(tempHtsTestKitsExtracts);
            _extractsContext.AddRange(tempHtsClientTracingExtracts);
            _extractsContext.AddRange(tempHtsPartnerTracingExtracts);
            _extractsContext.AddRange(tempHtsPartnerNotificationServicesExtracts);
            _extractsContext.AddRange(htsClientsExtracts);
            _extractsContext.AddRange(htsClientTestsExtracts);
            _extractsContext.AddRange(htsClientsLinkageExtracts);
            _extractsContext.AddRange(htsTestKitsExtracts);
            _extractsContext.AddRange(htsClientTracingExtracts);
            _extractsContext.AddRange(htsPartnerTracingExtracts);
            _extractsContext.AddRange(htsPartnerNotificationServicesExtracts);
            _extractsContextMysql.SaveChanges();

        }

        [Test]
        public void should_clear_extracts_MsSQL()
        {
            var extracts = TestInitializer.Iqtools.Extracts.Where(x => x.DocketId.IsSameAs("HTS"));
            CleanExtracts(_extractsContext, _serviceProvider, extracts.Select(x => x.Id));
        }

        [Test]
        public void should_clear_extracts_MySQL()
        {
            var extracts = TestInitializer.KenyaEmr.Extracts.Where(x => x.DocketId.IsSameAs("HTS"));
            CleanExtracts(_extractsContextMysql, _serviceProviderMysql, extracts.Select(x => x.Id));
        }

        private void CleanExtracts(DbContext context, IServiceProvider serviceProvider, IEnumerable<Guid> extractIds)
        {
            Assert.True(context.Set<TempHtsClients>().Any());
            Assert.True(context.Set<TempHtsClientLinkage>().Any());
            Assert.True(context.Set<TempHtsClientTests>().Any());
            Assert.True(context.Set<TempHtsTestKits>().Any());
            Assert.True(context.Set<TempHtsClientTracing>().Any());
            Assert.True(context.Set<TempHtsPartnerNotificationServices>().Any());
            Assert.True(context.Set<TempHtsPartnerTracing>().Any());

            Assert.True(context.Set<HtsClients>().Any());
            Assert.True(context.Set<HtsClientLinkage>().Any());
            Assert.True(context.Set<HtsClientTests>().Any());
            Assert.True(context.Set<HtsTestKits>().Any());
            Assert.True(context.Set<HtsClientTracing>().Any());
            Assert.True(context.Set<HtsPartnerNotificationServices>().Any());
            Assert.True(context.Set<HtsPartnerTracing>().Any());

            var cleanCbsExtracts = serviceProvider.GetService<ICleanHtsExtracts>();
            cleanCbsExtracts.Clean(extractIds.ToList()).Wait();

            Assert.False(context.Set<TempHtsClients>().Any());
            Assert.False(context.Set<TempHtsClientLinkage>().Any());
            Assert.False(context.Set<TempHtsClientTests>().Any());
            Assert.False(context.Set<TempHtsTestKits>().Any());
            Assert.False(context.Set<TempHtsClientTracing>().Any());
            Assert.False(context.Set<TempHtsPartnerNotificationServices>().Any());
            Assert.False(context.Set<TempHtsPartnerTracing>().Any());

            Assert.False(context.Set<HtsClients>().Any());
            Assert.False(context.Set<HtsClientLinkage>().Any());
            Assert.False(context.Set<HtsClientTests>().Any());
            Assert.False(context.Set<HtsTestKits>().Any());
            Assert.False(context.Set<HtsClientTracing>().Any());
            Assert.False(context.Set<HtsPartnerNotificationServices>().Any());
            Assert.False(context.Set<HtsPartnerTracing>().Any()); 

            Console.WriteLine(context.Database.ProviderName);
            Console.WriteLine(context.Database.GetDbConnection().ConnectionString);
        }
    }
}
