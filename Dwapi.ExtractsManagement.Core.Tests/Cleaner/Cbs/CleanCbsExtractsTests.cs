using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SharedKernel.Utility;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests.Cleaner.Cbs
{
    [TestFixture]
    public class CleanCbsExtractsTests
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

            var tempMpis = Builder<TempMasterPatientIndex>.CreateListOfSize(2).Build().ToList();
            var mpis = Builder<MasterPatientIndex>.CreateListOfSize(2).Build().ToList();

            _extractsContext = _serviceProvider.GetService<ExtractsContext>();
            _extractsContext.TempMasterPatientIndices.AddRange(tempMpis);
            _extractsContext.MasterPatientIndices.AddRange(mpis);
            _extractsContext.SaveChanges();

            _extractsContextMysql = _serviceProviderMysql.GetService<ExtractsContext>();
            _extractsContextMysql.TempMasterPatientIndices.AddRange(tempMpis);
            _extractsContextMysql.MasterPatientIndices.AddRange(mpis);
            _extractsContextMysql.SaveChanges();

        }

        [Test]
        public void should_clear_extracts_MsSQL()
        {
            var extractId = TestInitializer.Iqtools.Extracts.First(x => x.Name.IsSameAs(nameof(MasterPatientIndex))).Id;
            CleanExtracts(_extractsContext, _serviceProvider, extractId);
        }

        [Test]
        public void should_clear_extracts_MySQL()
        {
            var extractId = TestInitializer.KenyaEmr.Extracts.First(x => x.Name.IsSameAs(nameof(MasterPatientIndex))).Id;
            CleanExtracts(_extractsContextMysql, _serviceProviderMysql, extractId);
        }

        private void CleanExtracts(DbContext context,IServiceProvider serviceProvider,Guid extractId)
        {
            Assert.True(context.Set<TempMasterPatientIndex>().Any());
            Assert.True(context.Set<MasterPatientIndex>().Any());

            var cleanCbsExtracts = serviceProvider.GetService<ICleanCbsExtracts>();
            cleanCbsExtracts.Clean(extractId).Wait();

            Assert.False(context.Set<TempMasterPatientIndex>().Any());
            Assert.False(context.Set<MasterPatientIndex>().Any());

            Console.WriteLine(context.Database.ProviderName);
            Console.WriteLine(context.Database.GetDbConnection().ConnectionString);
        }
    }
}