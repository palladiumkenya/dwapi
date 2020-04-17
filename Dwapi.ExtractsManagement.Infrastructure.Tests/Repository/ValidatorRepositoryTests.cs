using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Infrastructure.Tests.TestArtifacts;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Infrastructure.Tests.Repository
{
    [TestFixture]
    [Category("Cbs")]
    public class ValidatorRepositoryTests
    {
        private IValidatorRepository _repository;
        private List<ValidationError> _indices;
        private ExtractsContext _context;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
            _indices = TestData.GenerateErrors();
            TestInitializer.SeedData<ExtractsContext>(_indices);
            _context=TestInitializer.ServiceProvider.GetService<ExtractsContext>();
        }

        [SetUp]
        public void SetUp()
        {
            _repository = TestInitializer.ServiceProvider.GetService<IValidatorRepository>();
        }

        [Test]
        public void should_Clear_By_Docket()
        {
            Assert.True(_context.ValidationError.Any());
            var validatorRepository = TestInitializer.ServiceProvider.GetService<IValidatorRepository>();
            validatorRepository.ClearByDocket("NDWH");

            var  context = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            Assert.False(context.ValidationError.Any());
        }
    }
}
