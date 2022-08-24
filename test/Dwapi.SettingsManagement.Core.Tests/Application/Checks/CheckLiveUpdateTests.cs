using System;
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
    public class CheckLiveUpdateTests
    {
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.ServiceProvider.GetService<IMediator>();
        }

        [Test]
        public void should_Get_Live_Version()
        {
            var query = new CheckLiveUpdate("2.x.0.3");

            var result = _mediator.Send(query).Result;
            Assert.True(result.IsSuccess);
            Console.WriteLine(result.Value.ToString());
        }
    }
}

