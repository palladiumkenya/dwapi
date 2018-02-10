

using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SettingsManagement.Infrastructure.Repository;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Infrastructure.Tests.TestHelpers;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Dwapi.SettingsManagement.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class EmrSystemRepositoryTests
    {
        private IEmrSystemRepository _emrRepository;
        private DbContextOptions<SettingsContext> _options;
        private SettingsContext _context;
        private EmrSystem _iqcareEmr;

        [OneTimeSetUp]
        public void Init()
        {
            _options = TestDbOptions.GetInMemoryOptions<SettingsContext>();
            var context = new SettingsContext(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            _iqcareEmr = new EmrSystem("IQCare", "v1");
            _iqcareEmr.AddProtocol(new DatabaseProtocol(DatabaseType.MicrosoftSQL, @".\koske14", "sa", "maun", "iqcare"));
            _iqcareEmr.AddRestProtocol(new RestProtocol("http://192.168.1.10/api","xys"));
            context.Add(_iqcareEmr);
            context.SaveChanges();
        }

        [SetUp]
        public void SetUp()
        {
            _context = new SettingsContext(_options);
            _emrRepository =new EmrSystemRepository(_context);
        }

        [Test]
        public void should_Get_All_Emrs_With_Protocols()
        {
            var emr = _emrRepository.GetAll().FirstOrDefault(x=>x.Id== _iqcareEmr.Id);
            Assert.NotNull(emr);
            Assert.True(emr.DatabaseProtocols.Any());
            Assert.True(emr.RestProtocols.Any());
            Console.WriteLine(emr);
            foreach (var databaseProtocol in emr.DatabaseProtocols)
            {
                Console.WriteLine($" Database > {databaseProtocol}");
            }
            foreach (var restProtocol in emr.RestProtocols)
            {
                Console.WriteLine($" Rest API > {restProtocol}");
            }
        }

        [Test]
        public void should_Get_All_Emrs_Count()
        {
            var emr = _emrRepository.Count();
            Assert.True(emr>0);
        }
    }
}