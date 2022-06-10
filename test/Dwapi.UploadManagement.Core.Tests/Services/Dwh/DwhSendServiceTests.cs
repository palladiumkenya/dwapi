using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Tests.TestHelpers;
using Dwapi.UploadManagement.Core.Interfaces.Services.Dwh;
using Dwapi.UploadManagement.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Serilog;

namespace Dwapi.UploadManagement.Core.Tests.Services.Dwh
{
    [TestFixture]
    public class DwhSendServiceTests
    {

        private readonly string _authToken = "1ba47c2a-6e05-11e8-adc0-fa7ae01bbebc";
        private readonly string _subId = "DWAPI";
        private readonly string url = "http://localhost:21751";

        private IDwhSendService _sendService;
        private CentralRegistry _registry;
        private Mock<HttpMessageHandler> _manifestHandlerMock, _handlerMock;

        [SetUp]
        public void SetUp()
        {
            _registry = new CentralRegistry(url, "NDWH")
            {
                AuthToken = _authToken,
                SubscriberId = _subId
            };
            _sendService = TestInitializer.ServiceProvider.GetService<IDwhSendService>();
        }

        [Test]
        public void should_Send_Manifest()
        {
            // _sendService.Client = new HttpClient(_manifestHandlerMock.Object);
            var sendTo = new SendManifestPackageDTO(_registry);

            // endManifestPackageDTO sendTo,string  version,string apiVersion=""
            var responses = _sendService.SendManifestAsync(sendTo, "2.8.0.0", "3").Result;

            var ctx = TestInitializer.ServiceProvider.GetService<UploadContext>();
            var log = ctx.TransportLogs.ToList();
            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
            responses.ForEach(sendManifestResponse => Log.Debug($"SENT! > {sendManifestResponse.ManifestResponse}"));
            Assert.True(log.Count==1);

        }
    }
}
