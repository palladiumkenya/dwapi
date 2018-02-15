using System.Configuration;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Infrastructure.Repository;
using Dwapi.SharedKernel.Infrastructure.Tests.TestHelpers;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Infrastructure.Tests.Stage.Psmart
{
    [TestFixture]
    public class PsmartStageRepositoryTests
    {
        private IPsmartStageRepository _psmartStageRepository;
        private DbContextOptions<ExtractsContext> _options;
        private ExtractsContext _context;
       

        [OneTimeSetUp]
        public void Init()
        {
            _options = TestDbOptions.GetInMemoryOptions<ExtractsContext>();
            var context = new ExtractsContext(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var psmartStages = Builder<PsmartStage>.CreateListOfSize(2).All().With(x=>x.Emr="iqcare").Build().ToList();
            context.AddRange(psmartStages);
            context.SaveChanges();
        }

        [SetUp]
        public void SetUp()
        {
            _context = new ExtractsContext(_options);
            _psmartStageRepository = new PsmartStageRepository(_context);
            var psmartStages = Builder<PsmartStage>.CreateListOfSize(2).All().With(x => x.Emr = "iqcare").Build().ToList();
            _context.AddRange(psmartStages);
            _context.SaveChanges();
        }


        [Test]
        [Ignore("requires relational db")]
     
        public void should_Clear()
        {
            _psmartStageRepository.Clear("iqcare");
            Assert.False(_context.PsmartStages.Any());
        }
    }
}