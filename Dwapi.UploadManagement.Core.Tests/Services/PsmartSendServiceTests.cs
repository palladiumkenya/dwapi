using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Interfaces.Services;
using Dwapi.UploadManagement.Core.Services;
using FizzWare.NBuilder;
using NUnit.Framework;

namespace Dwapi.UploadManagement.Core.Tests.Services
{
    [TestFixture]
    public class PsmartSendServiceTests
    {
        private readonly string _authToken = @"268DFA3EB92BC53FAE94A048E23112A1";
        private readonly string _subId = "DWAPI";
        private readonly string url = "http://52.178.24.227:8026";

        private IPsmartSendService _psmartSendService; 
        private Registry _registry;
        private SendPackageDTO _sendPackageDTO;
        private PsmartMessage _psmartMessage;
   

        [SetUp]
        public void SetUp()
        {
            _registry=new Registry(url);
            _registry.AuthToken = _authToken;
            _registry.SubscriberId = _subId;
            _psmartSendService = new PsmartSendService();
            _sendPackageDTO = Builder<SendPackageDTO>.CreateNew().With(x=>x.Destination=_registry).Build();
            var pMessages = new List<string>{"message 1", "message 1" };
            _psmartMessage=new PsmartMessage(pMessages);
        }

       
        [Test]
        public void should_SendAsync()
        {

            var smartMessages = Builder<SmartMessage>.CreateListOfSize(10).Build().ToList();
            var bag=new SmartMessageBag(smartMessages);

            var responses = _psmartSendService.SendAsync(_sendPackageDTO, bag).Result;
            Assert.NotNull(responses);
            Console.WriteLine(responses);
        }
    }
}