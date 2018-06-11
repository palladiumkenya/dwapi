using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Tests.TestHelpers;
using Dwapi.UploadManagement.Core.Interfaces.Services;
using Dwapi.UploadManagement.Core.Interfaces.Services.Cbs;
using Dwapi.UploadManagement.Core.Services;
using Dwapi.UploadManagement.Core.Services.Cbs;
using FizzWare.NBuilder;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.UploadManagement.Core.Tests.Services.Cbs
{
    [TestFixture]
    public class CbsSendServiceTests
    {
        private readonly string _authToken = @"1983aeda-6a96-11e8-adc0-fa7ae01bbebc";
        private readonly string _subId = "DWAPI";
        private readonly string url = "http://localhost:5000";

        private ICbsSendService _cbsSendService; 
        private IServiceProvider _serviceProvider;
        private ManifestMessage _manifestMessage;
        private CentralRegistry _registry;

        [OneTimeSetUp]
        public void Init()
        {
            _serviceProvider = new ServiceCollection()
                .AddTransient<ICbsSendService,CbsSendService>()
                .BuildServiceProvider();

            /*
                22704|TOGONYE DISPENSARY|KIRINYAGA
                22696|HERTLANDS MEDICAL CENTRE|NAROK
            */

            _manifestMessage = TestDataFactory.ManifestMessage(2,22704,22696);
        }

        [SetUp]
        public void SetUp()
        {
            _registry = new CentralRegistry(url, "CBS")
            {
                AuthToken = _authToken,
                SubscriberId = _subId
            };
            _cbsSendService = _serviceProvider.GetService<ICbsSendService>();
        }
       
        [Test]
        public void should_Send_Manifest()
        {
            var sendTo=new SendManifestPackageDTO(_registry);

            var responses = _cbsSendService.SendManifestAsync(sendTo, _manifestMessage).Result;
            Assert.NotNull(responses);
            Console.WriteLine(responses);
        }
    }
}