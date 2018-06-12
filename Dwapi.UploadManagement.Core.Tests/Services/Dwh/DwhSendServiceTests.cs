using System;
using System.Linq;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Tests.TestHelpers;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Services.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Services.Dwh;
using Dwapi.UploadManagement.Core.Services.Cbs;
using Dwapi.UploadManagement.Core.Services.Dwh;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.UploadManagement.Core.Tests.Services.Dwh
{
    [TestFixture]
    public class DwhSendServiceTests
    {
        private readonly string _authToken = @"1ba47c2a-6e05-11e8-adc0-fa7ae01bbebc";
        private readonly string _subId = "DWAPI";
        private readonly string url = "http://192.168.100.99/dwapi";

        private IDwhSendService _dwhSendService; 
        private IServiceProvider _serviceProvider;
        private DwhManifestMessageBag _bag;
        private MpiMessageBag _mpiBag;
        private CentralRegistry _registry;

        [OneTimeSetUp]
        public void Init()
        {
            _serviceProvider = new ServiceCollection()
                .AddTransient<IDwhSendService, DwhSendService>()
                .BuildServiceProvider();

            /*
                22704|TOGONYE DISPENSARY|KIRINYAGA
                22696|HERTLANDS MEDICAL CENTRE|NAROK
            */

            _bag = TestDataFactory.DwhManifestMessageBag(2,10001, 10002);
            _mpiBag = TestDataFactory.MpiMessageBag(5, 10001, 10002);
        }

        [SetUp]
        public void SetUp()
        {
            _registry = new CentralRegistry(url, "NDWH")
            {
                AuthToken = _authToken,
                SubscriberId = _subId
            };
            _dwhSendService = _serviceProvider.GetService<IDwhSendService>();
        }
       
        [Test]
        public void should_Send_Manifest()
        {
            var sendTo=new SendManifestPackageDTO(_registry);

            var responses = _dwhSendService.SendManifestAsync(sendTo, _bag).Result;
            Assert.NotNull(responses);
            Assert.False(responses.Select(x=>x.IsValid()).Any(x=>false));
            foreach (var sendManifestResponse in responses)
            {
                Console.WriteLine(sendManifestResponse);
            }
        }

    }
}