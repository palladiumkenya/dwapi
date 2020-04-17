using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Infrastructure.Tests.TestArtifacts;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.ExtractsManagement.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class ValidatorRepositoryTests
    {
        private IValidatorRepository _repository;
        private ExtractsContext _extractsContext;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
            TestInitializer.SeedData(TestData.GenerateErrors());
        }

        [SetUp]
        public void SetUp()
        {
            _repository = TestInitializer.ServiceProvider.GetService<IValidatorRepository>();
            _extractsContext=TestInitializer.ServiceProvider.GetService<ExtractsContext>();
        }

        [Test]
        public void should_Clear_By_Docket_MsSql()
        {
            Assert.True(_extractsContext.ValidationError.Any());
            var validatorRepository = TestInitializer.ServiceProvider.GetService<IValidatorRepository>();
            validatorRepository.ClearByDocket("NDWH");

            var  context = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            Assert.False(context.ValidationError.Any());
        }

    }
}
