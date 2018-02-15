using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.Controller;
using Dwapi.SettingsManagement.Core.DTOs;
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
    public class ExtractManagerControllerTests
    {
        private ExtractManagerController _extractManagerController;

        private IDatabaseManager _databaseManager;
        private IExtractRepository _extractRepository;
        private IDatabaseProtocolRepository _databaseProtocolRepository;
        private IExtractManagerService _extractManagerService;
        private DbContextOptions<SettingsContext> _options;
        private SettingsContext _context;
        private Extract _iqcareExtract, _kenyaExtract;
        private DatabaseProtocol _mssql,_mysql;
        private List<EmrSystem> _emrs;
        private Docket _docket;

        [OneTimeSetUp]
        public void Init()
        {
            _options = TestDbOptions.GetInMemoryOptions<SettingsContext>();
            var context = new SettingsContext(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            _emrs = Builder<EmrSystem>.CreateListOfSize(2).Build().ToList();
            _docket= Builder<Docket>.CreateNew().Build();

            _iqcareExtract = Builder<Extract>.CreateNew().With(x=>x.DocketId=_docket.Id).With(x=>x.EmrSystemId=_emrs.First().Id).Build();
            _iqcareExtract.ExtractSql = @"SELECT * FROM [AppAdmin]";
            _kenyaExtract = Builder<Extract>.CreateNew().With(x => x.DocketId = _docket.Id).With(x => x.EmrSystemId = _emrs.Last().Id).Build();
            _kenyaExtract.ExtractSql = @"SELECT * FROM psmart";

            _mssql =
                new DatabaseProtocol(DatabaseType.MicrosoftSQL, @".\koske14", "sa", "maun", "iqcare");
            _mysql =
                new DatabaseProtocol(DatabaseType.MySQL, @"localhost", "root", "root", "openmrs");

            context.Add(_docket);
            context.AddRange(_emrs);
            context.AddRange(new List<Extract> { _iqcareExtract, _kenyaExtract });
            context.SaveChanges();
        }

        [SetUp]
        public void SetUp()
        {
            _context = new SettingsContext(_options);
            _databaseManager=new DatabaseManager();
            _extractRepository = new ExtractRepository(_context);
            _databaseProtocolRepository=new DatabaseProtocolRepository(_context);
            _extractManagerService=new ExtractManagerService(_extractRepository, _databaseManager);
            _extractManagerController =new ExtractManagerController(_extractManagerService);
        }

        [Test]
        public void should_Get_All_Extract()
        {
            var response = _extractManagerController.Get(_emrs.First().Id,_docket.Id);
            var result = response as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var extracts = result.Value as List<Extract>;
            Assert.NotNull(extracts);
            Assert.True(extracts.Count > 0);

            foreach (var extract in extracts)
            {
                Console.WriteLine($"{extract}");
            }
                
        }

     
        [Test]
        public void should_Post_Verify_Extract_Query()
        {
            var extractDbProtocolDTO =new ExtractDbProtocolDTO(_iqcareExtract,_mssql);
            var response = _extractManagerController.Verify(extractDbProtocolDTO);
            var result = response as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            Assert.True(Convert.ToBoolean(result.Value));
        }
    }
}