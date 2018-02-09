using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.Controller;
using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SettingsManagement.Core.Services;
using Dwapi.SettingsManagement.Infrastructure;
using Dwapi.SettingsManagement.Infrastructure.Repository;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Infrastructure.Tests.TestHelpers;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Dwapi.Tests.Controller
{
    [TestFixture]
    public class EmrManagerControllerTests
    {
        private EmrManagerController _emrManagerController;

        private IDatabaseManager _databaseManager;
        private IEmrSystemRepository _emrSystemRepository;
        private IDatabaseProtocolRepository _databaseProtocolRepository;
        private IEmrManagerService _emrManagerService;
        private DbContextOptions<SettingsContext> _options;
        private SettingsContext _context;
        private EmrSystem _iqcareEmr, _kenyaEmr,_otherEmr;

        [OneTimeSetUp]
        public void Init()
        {
            _options = TestDbOptions.GetInMemoryOptions<SettingsContext>();
            var context = new SettingsContext(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            _iqcareEmr =new EmrSystem("IQCare","v1");
            _iqcareEmr.AddProtocol(new DatabaseProtocol(DatabaseType.MicrosoftSQL, @".\koske14", "sa", "maun", "iqcare"));
            _kenyaEmr = new EmrSystem("KenyaEMR", "v1");
            _kenyaEmr.AddProtocol(new DatabaseProtocol(DatabaseType.MySQL, @"localhost", "root", "root", "testemr"));
            _otherEmr=new EmrSystem("xEmr","1");
            _otherEmr.AddProtocol(new DatabaseProtocol(DatabaseType.MySQL, @"localhost", "root", "root", "othertestemr"));
            context.AddRange(new List<EmrSystem>{_iqcareEmr,_kenyaEmr,_otherEmr});
            context.SaveChanges();
        }

        [SetUp]
        public void SetUp()
        {
            _context = new SettingsContext(_options);
            _databaseManager=new DatabaseManager();
            _emrSystemRepository = new EmrSystemRepository(_context);
            _databaseProtocolRepository=new DatabaseProtocolRepository(_context);
            _emrManagerService=new EmrManagerService(_databaseManager,_emrSystemRepository,_databaseProtocolRepository);

            _emrManagerController =new EmrManagerController(_emrManagerService);
        }

        [Test]
        public void should_Get_All_Emr()
        {
            var response = _emrManagerController.Get();
            var result = response as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var emrSystems = result.Value as List<EmrSystem>;
            Assert.NotNull(emrSystems);
            Assert.True(emrSystems.Count > 0);

            foreach (var emrSystem in emrSystems)
            {
                Console.WriteLine($"{emrSystem}  |{emrSystem.DatabaseProtocols.Count}");
            }
                
        }

        [Test]
        public void should_Post_Emr()
        {
            var emr=new EmrSystem("Test","v3");
            var response = _emrManagerController.SaveEmr(emr);

            var result = response as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var emrSystem = _emrSystemRepository.Get(emr.Id);
            Assert.NotNull(emrSystem);
            Console.WriteLine(emrSystem);
        }

        [Test]
        public void should_Delete_Emr()
        {
            var response = _emrManagerController.DeleteEmr(_otherEmr.Id);
            var result = response as OkResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var emrSystem = _emrSystemRepository.Get(_otherEmr.Id);
            Assert.Null(emrSystem);
        }

        [Test]
        public void should_Post_Protocol()
        {
            var databaseProtocol = new DatabaseProtocol(DatabaseType.MicrosoftSQL, @".\koske14", "sa", "maun", "test", _kenyaEmr.Id);
            var response = _emrManagerController.SaveProtocol(databaseProtocol);

            var result = response as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var protocol = _databaseProtocolRepository.Get(databaseProtocol.Id);
            Assert.NotNull(protocol);
            Console.WriteLine(protocol);
        }

        [Test]
        public void should_Delete_Protocol()
        {
            var protocolId = _otherEmr.DatabaseProtocols.First().Id;
            var response = _emrManagerController.DeleteProtoco(protocolId);
            var result = response as OkResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var databaseProtocol = _databaseProtocolRepository.Get(protocolId);
            Assert.Null(databaseProtocol);
        }

        [Test]
        public void should_Post_VerifyEmrConnection()
        {
            var protocol = _iqcareEmr.DatabaseProtocols.First();
            var response = _emrManagerController.Verify(protocol);
            var result = response as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            Assert.True(Convert.ToBoolean(result.Value));
        }
    }
}