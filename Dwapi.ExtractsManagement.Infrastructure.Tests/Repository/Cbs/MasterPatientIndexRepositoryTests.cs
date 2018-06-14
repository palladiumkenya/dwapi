using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Cbs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Infrastructure.Tests.Repository.Cbs
{
    [TestFixture]
    public class MasterPatientIndexRepositoryTests
    {

        private IMasterPatientIndexRepository _repository;
        private IServiceProvider _serviceProvider;
        private ExtractsContext _extractsContext;
        private List<Guid> _mpiIds = new List<Guid>();
        private List<SentItem> _sentItems = new List<SentItem>();

        [OneTimeSetUp]
        public void Init()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["ConnectionStrings:DwapiConnection"];

            _serviceProvider = new ServiceCollection()
                .AddDbContext<ExtractsContext>(o => o.UseSqlServer(connectionString))
                .AddTransient<IMasterPatientIndexRepository, MasterPatientIndexRepository>()
                .BuildServiceProvider();

            _extractsContext = _serviceProvider.GetService<ExtractsContext>();
            
        }

        [SetUp]
        public void SetUp()
        {
            _mpiIds = _extractsContext.MasterPatientIndices.AsNoTracking().Take(2).Select(x => x.Id).ToList();
            
            _repository =_serviceProvider.GetService<IMasterPatientIndexRepository>();
        }

        [Test]
        public void shoul_Update_Sent_Items()
        {
            _sentItems = _mpiIds.Select(x => new SentItem(x, SendStatus.Sent)).ToList();

            _repository.UpdateSendStatus(_sentItems);

            var updated = _repository.GetAll(x => x.Status == $"{nameof(SendStatus.Sent)}").ToList();
            Assert.True(updated.Count==2);
        }

        [TearDown]
        public void TearDown()
        {
            _extractsContext.Database.GetDbConnection().Execute($"update {nameof(ExtractsContext.MasterPatientIndices)} set {nameof(MasterPatientIndex.Status)}=''");
        }
    }
}