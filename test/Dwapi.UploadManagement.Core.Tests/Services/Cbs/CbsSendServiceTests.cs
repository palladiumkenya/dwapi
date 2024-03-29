﻿using System;
using System.Linq;
using System.Net.Http;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Tests.TestHelpers;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Services.Cbs;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Serilog;

namespace Dwapi.UploadManagement.Core.Tests.Services.Cbs
{
    [TestFixture]
    [Category("Cbs")]
    public class CbsSendServiceTests
    {
        private readonly string _authToken = Guid.NewGuid().ToString();
        private readonly string _subId = "DWAPI";
        private readonly string url = "https://kenyahmis.org/api";

        private ICbsSendService _sendService;
        private CentralRegistry _registry;
        private Mock<HttpMessageHandler> _manifestHandlerMock,_mpiHandlerMock;

        [SetUp]
        public void SetUp()
        {
            _registry = new CentralRegistry(url, "CBS")
            {
                AuthToken = _authToken,
                SubscriberId = _subId
            };
            _sendService =TestInitializer.ServiceProvider.GetService<ICbsSendService>();
            _manifestHandlerMock = MockHelpers.HttpHandler(new StringContent("{"+$"\"{nameof(SendManifestResponse.FacilityKey)}\":\"{Guid.Empty}\""+"}"));
            _mpiHandlerMock = MockHelpers.HttpHandler(new StringContent("{"+$"\"{nameof(SendMpiResponse.BatchKey)}\":\"{Guid.Empty}\""+"}"));
        }

        [Test]
        public void should_Send_Manifest()
        {
            _sendService.Client = new HttpClient(_manifestHandlerMock.Object);
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _sendService.SendManifestAsync(sendTo,"v1").Result;

            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
            responses.ForEach(sendManifestResponse => Log.Debug($"SENT! > {sendManifestResponse}"));
        }

        [Test]
        public void should_Send_Mpi()
        {
            _sendService.Client = new HttpClient(_mpiHandlerMock.Object);
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _sendService.SendMpiAsync(sendTo).Result;

            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
            responses.ForEach(sendMpiResponse => Log.Debug($"SENT! > {sendMpiResponse}"));
        }
     }
}
