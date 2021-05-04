using System.Linq;
using Dwapi.SettingsManagement.Core.Application.Checks.Queries;
using Dwapi.SettingsManagement.Core.Application.Metrics.Queries;
using Dwapi.SettingsManagement.Infrastructure.Tests.TestArtifacts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.SettingsManagement.Core.Tests.Application.Checks
{
    [TestFixture]
    public class GetCheckSummaryTests
    {
        private IMediator _mediator;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
            TestInitializer.SeedData(TestData.GenerateChecks());
        }

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.ServiceProvider.GetService<IMediator>();
        }

        [Test]
        public void should_Get_Summary()
        {
            var query = new GetCheckSummary();
            var result = _mediator.Send(query).Result;
            Assert.True(result.Value.Any());
        }
    }
}
