using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Cbs;
using Dwapi.SettingsManagement.Infrastructure;
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

        private ExtractsContext _extractsContext;
        private ExtractsContext _extractsContextMysql;
        private DbProtocol _iQtoolsDb, _kenyaEmrDb;

        private IMasterPatientIndexReader _reader;

        [OneTimeSetUp]
        public void Init()
        {
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            _extractsContextMysql = TestInitializer.ServiceProviderMysql.GetService<ExtractsContext>();

            _iQtoolsDb = TestInitializer.Iqtools.DatabaseProtocols.First(x => x.DatabaseName.ToLower().Contains("iqtools".ToLower()));
            _iQtoolsDb.Host = ".\\Koske14";
            _iQtoolsDb.Username = "sa";
            _iQtoolsDb.Password = "maun";

            _kenyaEmrDb = TestInitializer.KenyaEmr.DatabaseProtocols.First();
            _kenyaEmrDb.Host = "127.0.0.1";
            _kenyaEmrDb.Username = "root";
            _kenyaEmrDb.Password = "test";
            _kenyaEmrDb.DatabaseName = "openmrs";

        }


        [Test]
        public void should_GetView_MsSQL()
        {
            var repository = TestInitializer.ServiceProvider.GetService<IMasterPatientIndexRepository>();
            var mpis = repository.GetView().ToList();
            Assert.True(mpis.Any());
        }

        [Test]
        public void should_GetView_MySQL()
        {
            var repository = TestInitializer.ServiceProviderMysql.GetService<IMasterPatientIndexRepository>();
            var mpis = repository.GetView().ToList();
            Assert.True(mpis.Any());
        }

        [Test]
        public void shoul_Update_Sent_Items_MsSQL()
        {
            var mpiIds = _extractsContext.MasterPatientIndices.AsNoTracking().Take(2).Select(x => x.Id).ToList();
           var  sentItems = mpiIds.Select(x => new SentItem(x, SendStatus.Sent)).ToList();
            var repository = TestInitializer.ServiceProvider.GetService<IMasterPatientIndexRepository>();
            repository.UpdateSendStatus(sentItems);

            var updated = repository.GetAll(x => x.Status == $"{nameof(SendStatus.Sent)}").ToList();
            Assert.True(updated.Count==2);
        }

        [Test]
        public void shoul_Update_Sent_Items_MySQL()
        {
            var mpiIds = _extractsContextMysql.MasterPatientIndices.AsNoTracking().Take(2).Select(x => x.Id).ToList();
            var sentItems = mpiIds.Select(x => new SentItem(x, SendStatus.Sent)).ToList();
            var repository = TestInitializer.ServiceProviderMysql.GetService<IMasterPatientIndexRepository>();
            repository.UpdateSendStatus(sentItems);

            var updated = repository.GetAll(x => x.Status == $"{nameof(SendStatus.Sent)}").ToList();
            Assert.True(updated.Count == 2);
        }

        [TearDown]
        public void TearDown()
        {
            _extractsContext.Database.GetDbConnection().Execute($"update {nameof(ExtractsContext.MasterPatientIndices)} set {nameof(MasterPatientIndex.Status)}=''");
        }
    }
}