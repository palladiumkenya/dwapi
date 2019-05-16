using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using FizzWare.NBuilder;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.ExtractsManagement.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class ValidatorRepositoryTests
    {
        private ExtractsContext _extractsContext;
        private ExtractsContext _extractsContextMysql;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.InitDb();
            TestInitializer.InitMysQLDb();

            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            _extractsContextMysql = TestInitializer.ServiceProviderMysql.GetService<ExtractsContext>();

            var validator = _extractsContext.Validator.First(x=>x.Extract=="TempPatientExtracts");

            var validationErrors = Builder<ValidationError>.CreateListOfSize(2).All().With(x=>x.ValidatorId=validator.Id).Build().ToList();

            _extractsContext.ValidationError.AddRange(validationErrors);
            _extractsContext.SaveChanges();

            _extractsContextMysql.ValidationError.AddRange(validationErrors);
            _extractsContextMysql.SaveChanges();
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

        [Test]
        public void should_Clear_By_Docket_MySql()
        {
            Assert.True(_extractsContextMysql.ValidationError.Any());
            var validatorRepository = TestInitializer.ServiceProviderMysql.GetService<IValidatorRepository>();
            validatorRepository.ClearByDocket("NDWH");

            var  context = TestInitializer.ServiceProviderMysql.GetService<ExtractsContext>();
            Assert.False(context.ValidationError.Any());
        }
    }
}
