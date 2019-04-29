using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Hts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SharedKernel.Utility;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests.Cleaner.Hts
{
    [TestFixture]
    public class CleanHtsExtractsTests
    {
        private IServiceProvider _serviceProvider;
        private IServiceProvider _serviceProviderMysql;
        private ExtractsContext _extractsContext;
        private ExtractsContext  _extractsContextMysql;

        [OneTimeSetUp]
        public void Init()
        {
            _serviceProvider = TestInitializer.ServiceProvider;
            _serviceProviderMysql = TestInitializer.ServiceProviderMysql;

            var tempHtsClientExtracts = Builder<TempHTSClientExtract>.CreateListOfSize(2).Build().ToList();
            var htsClientExtracts = Builder<HTSClientExtract>.CreateListOfSize(2).Build().ToList();

            var tempHtsClientPartnerExtracts = Builder<TempHTSClientPartnerExtract>.CreateListOfSize(2).Build().ToList();
            var htsClientPartnerExtracts = Builder<HTSClientPartnerExtract>.CreateListOfSize(2).Build().ToList();

            var tempHtsClientLinkageExtracts = Builder<TempHTSClientLinkageExtract>.CreateListOfSize(2).Build().ToList();
            var htsClientLinkageExtracts = Builder<HTSClientLinkageExtract>.CreateListOfSize(2).Build().ToList();

            _extractsContext = _serviceProvider.GetService<ExtractsContext>();
            _extractsContext.AddRange(tempHtsClientExtracts);
            _extractsContext.AddRange(tempHtsClientPartnerExtracts);
            _extractsContext.AddRange(tempHtsClientLinkageExtracts);
            _extractsContext.AddRange(htsClientExtracts);
            _extractsContext.AddRange(htsClientPartnerExtracts);
            _extractsContext.AddRange(htsClientLinkageExtracts);
            _extractsContext.SaveChanges();

            _extractsContextMysql = _serviceProviderMysql.GetService<ExtractsContext>();
            _extractsContextMysql.AddRange(tempHtsClientExtracts);
            _extractsContextMysql.AddRange(tempHtsClientPartnerExtracts);
            _extractsContextMysql.AddRange(tempHtsClientLinkageExtracts);
            _extractsContextMysql.AddRange(htsClientExtracts);
            _extractsContextMysql.AddRange(htsClientPartnerExtracts);
            _extractsContextMysql.AddRange(htsClientLinkageExtracts);
            _extractsContextMysql.SaveChanges();

        }

        [Test]
        public void should_clear_extracts_MsSQL()
        {
            var extracts = TestInitializer.Iqtools.Extracts.Where(x => x.DocketId.IsSameAs("HTS"));
            CleanExtracts(_extractsContext, _serviceProvider, extracts.Select(x=>x.Id));
        }

        [Test]
        public void should_clear_extracts_MySQL()
        {
            var extracts = TestInitializer.KenyaEmr.Extracts.Where(x => x.DocketId.IsSameAs("HTS"));
            CleanExtracts(_extractsContextMysql, _serviceProviderMysql,  extracts.Select(x=>x.Id));
        }

        private void CleanExtracts(DbContext context,IServiceProvider serviceProvider,IEnumerable<Guid > extractIds)
        {
            Assert.True(context.Set<TempHTSClientExtract>().Any());
            Assert.True(context.Set<TempHTSClientLinkageExtract>().Any());
            Assert.True(context.Set<TempHTSClientPartnerExtract>().Any());

            Assert.True(context.Set<HTSClientExtract>().Any());
            Assert.True(context.Set<HTSClientLinkageExtract>().Any());
            Assert.True(context.Set<HTSClientPartnerExtract>().Any());

            var cleanCbsExtracts = serviceProvider.GetService<ICleanHtsExtracts>();
            cleanCbsExtracts.Clean(extractIds.ToList()).Wait();

            Assert.False(context.Set<TempHTSClientExtract>().Any());
            Assert.False(context.Set<TempHTSClientLinkageExtract>().Any());
            Assert.False(context.Set<TempHTSClientPartnerExtract>().Any());

            Assert.False(context.Set<HTSClientExtract>().Any());
            Assert.False(context.Set<HTSClientLinkageExtract>().Any());
            Assert.False(context.Set<HTSClientPartnerExtract>().Any());

            Console.WriteLine(context.Database.ProviderName);
            Console.WriteLine(context.Database.GetDbConnection().ConnectionString);
        }
    }
}
