using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Dwapi.ExtractsManagement.Core.Interfaces.Services.Psmart;
using Dwapi.ExtractsManagement.Core.Interfaces.Source.Psmart.Reader;
using Dwapi.ExtractsManagement.Core.Services.Psmart;
using Dwapi.ExtractsManagement.Core.Stage.Psmart;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.ExtractsManagement.Infrastructure.Source.Psmart.Reader;
using Dwapi.ExtractsManagement.Infrastructure.Stage.Psmart.Repository;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Infrastructure.Tests.TestHelpers;
using Dwapi.SharedKernel.Model;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests.Services.Psmart
{
    [TestFixture]
    public class PsmartExtractServiceTests
    {
        private IPsmartExtractService _psmartExtractService;
    
        private DbProtocol _mssql, _mysql;
        private DbExtract _extractA, _extractB;

        private DbContextOptions<ExtractsContext> _options;
        private ExtractsContext _context;


        [OneTimeSetUp]
        public void Init()
        {
            _options = TestDbOptions.GetInMemoryOptions<ExtractsContext>();
        }

        [SetUp]
        public void SetUp()
        {
            _context = new ExtractsContext(_options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _mssql = new DbProtocol(DatabaseType.MicrosoftSQL, @".\koske14", "sa", "maun", "IQTools_KeHMIS");
            _extractA = new DbExtract {ExtractSql = @" SELECT [Serial],[Demographics],[Encounters] FROM [psmart]"};
            _mysql = new DbProtocol(DatabaseType.MySQL, @"localhost", "root", "root", "testemr");
            _extractB = new DbExtract {ExtractSql = @" select serial,demographics,encounters FROM psmart"};

            _psmartExtractService =
                new PsmartExtractService(new PsmartSourceReader(), new PsmartStageRepository(_context));
        }

        [Test]
        public void should_Load_MsSQL()
        {
            var psmartSources = _psmartExtractService.Extract(_mssql, _extractA).ToList();
            Assert.True(psmartSources.Count > 0);
            _psmartExtractService.Load(psmartSources,false);

            Assert.True(_context.PsmartStages.Any());

            foreach (var psmartStage in _context.PsmartStages)
            {
                Console.WriteLine(psmartStage);
            }
        }
    }
}