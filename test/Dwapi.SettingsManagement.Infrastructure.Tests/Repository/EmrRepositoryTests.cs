using System;
using System.Linq;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.SettingsManagement.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class EmrSystemRepositoryTests
    {
        private IEmrSystemRepository _emrRepository;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
        }

        [SetUp]
        public void SetUp()
        {
            _emrRepository = TestInitializer.ServiceProvider.GetService<IEmrSystemRepository>();
        }

        [Test]
        public void should_Get_All_Emrs_With_Protocols()
        {
            var emr = _emrRepository.GetAll().FirstOrDefault(x=>x.Id== new Guid("a62216ee-0e85-11e8-ba89-0ed5f89f718b"));
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
