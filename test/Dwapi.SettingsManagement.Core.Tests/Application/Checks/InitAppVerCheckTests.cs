using Dwapi.SettingsManagement.Core.Application.Checks.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.SettingsManagement.Core.Tests.Application.Checks
{
    [TestFixture]
    public class InitAppVerCheckTests
    {
        private IMediator _mediator;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
        }

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.ServiceProvider.GetService<IMediator>();
        }

        [Test]
        public void should_Init()
        {
            var request = new InitAppVerCheck();

            var result = _mediator.Send(request).Result;
            Assert.True(result.IsSuccess);
        }
    }
}