using System.Linq;
using Dwapi.SettingsManagement.Core.Application.Metrics.Queries;
using Dwapi.SettingsManagement.Infrastructure.Tests.TestArtifacts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.SettingsManagement.Core.Tests.Application.Metrics
{
    [TestFixture]
    public class GetAppMetricHandlerTests
    {
        private IMediator _mediator;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
            TestInitializer.SeedData(TestData.GenerateAppMetrics());
        }

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.ServiceProvider.GetService<IMediator>();
        }

        [Test]
        public void should_Get_Metrics()
        {
            var query = new GetAppMetric();

            var result = _mediator.Send(query).Result;
            Assert.True(result.ToList().Any());
        }
    }
}

