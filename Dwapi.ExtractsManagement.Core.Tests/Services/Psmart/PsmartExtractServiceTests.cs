using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Services.Psmart;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Core.Services.Psmart;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.ExtractsManagement.Infrastructure.Repository;
using Dwapi.ExtractsManagement.Infrastructure.Source.Psmart.Reader;
using Dwapi.ExtractsManagement.Infrastructure.Stage.Psmart.Repository;
using Dwapi.SharedKernel.DTOs;
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
        private IExtractHistoryRepository _extractHistoryRepository;
    
        private DbProtocol _mssql, _mysql;
        private DbExtract _extractA, _extractB;
        private readonly Guid _iqcareId=new Guid("A6222210-0E85-11E8-BA89-0ED5F89F718B");
        private readonly Guid _kenyaEmrId = new Guid("A6226EB4-0E85-11E8-BA89-0ED5F89F718B");


        private DbContextOptions<ExtractsContext> _options;
        private ExtractsContext _context;
        private List<DbExtractProtocolDTO> _dbExtractProtocolDtos;


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

            _dbExtractProtocolDtos=new List<DbExtractProtocolDTO>();
            _mssql = new DbProtocol(DatabaseType.MicrosoftSQL, @".\koske14", "sa", "maun", "IQTools_KeHMIS");
            _extractA = new DbExtract {Id=_iqcareId, Emr = "IQCare",ExtractSql = @" SELECT [Serial],[Demographics],[Encounters] FROM [psmart]"};
            _mysql = new DbProtocol(DatabaseType.MySQL, @"localhost", "root", "root", "testemr");
            _extractB = new DbExtract {Id=_kenyaEmrId, Emr = "KenyaEMR",ExtractSql = @" select serial,demographics,encounters FROM psmart"};
            _dbExtractProtocolDtos.Add(new DbExtractProtocolDTO(_extractA,_mssql));
            _dbExtractProtocolDtos.Add(new DbExtractProtocolDTO(_extractB, _mysql));
            _extractHistoryRepository =new ExtractHistoryRepository(_context);
            _psmartExtractService =
                new PsmartExtractService(new PsmartSourceReader(), new PsmartStageRepository(_context), _extractHistoryRepository);
        }

        [Test]
        public void should_Find_With_History()
        {
            var dbe = _dbExtractProtocolDtos.Take(1).ToList();

            _psmartExtractService.Find(dbe);
            var history = _extractHistoryRepository.GetLatest(dbe.First().Extract.Id);
            Assert.NotNull(history);
            Assert.AreEqual(ExtractStatus.Found,history.Status);
            Assert.True(history.Stats.HasValue);
            Console.WriteLine(history);
        }

        [Test]
        public void should_Find_With_History_MySQL()
        {
            var dbe = _dbExtractProtocolDtos.TakeLast(1).ToList();

            _psmartExtractService.Find(dbe);
            var history = _extractHistoryRepository.GetLatest(dbe.First().Extract.Id);
            Assert.NotNull(history);
            Assert.AreEqual(ExtractStatus.Found, history.Status);
            Assert.True(history.Stats.HasValue);
            Console.WriteLine(history);
        }

        [Test]
        public void should_Sync_With_History_MsSQL()
        {
            var dbe = _dbExtractProtocolDtos.Take(1).ToList();

            _psmartExtractService.Sync(dbe);

            var history = _extractHistoryRepository.GetLatest(dbe.First().Extract.Id);
            Assert.NotNull(history);
            Assert.AreEqual(ExtractStatus.Loaded, history.Status);
            Assert.True(history.Stats.HasValue);
            Console.WriteLine(history);

            Assert.True(_context.PsmartStages.Any());

            foreach (var psmartStage in _context.PsmartStages)
            {
                Console.WriteLine(psmartStage);
            }
        }

        [Test]
        public void should_Sync_With_History_MySQL()
        {
            var dbe = _dbExtractProtocolDtos.TakeLast(1).ToList();

            _psmartExtractService.Sync(dbe);

            var history = _extractHistoryRepository.GetLatest(dbe.First().Extract.Id);
            Assert.NotNull(history);
            Assert.AreEqual(ExtractStatus.Loaded, history.Status);
            Assert.True(history.Stats.HasValue);
            Console.WriteLine(history);

            Assert.True(_context.PsmartStages.Any());

            foreach (var psmartStage in _context.PsmartStages)
            {
                Console.WriteLine(psmartStage);
            }
        }

        [Test]
        public void should_Get_events()
        {
            _psmartExtractService.Find(_dbExtractProtocolDtos);
            var eventFound = _psmartExtractService.GetStatus(_extractA.Id);
            Assert.NotNull(eventFound);
            _psmartExtractService.Sync(_dbExtractProtocolDtos);
            var eventSync = _psmartExtractService.GetStatus(_extractA.Id);
            Assert.NotNull(eventSync);

            Console.WriteLine(eventFound);
            Console.WriteLine(eventSync);
        }
    }
}