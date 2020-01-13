using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Data;
using Dwapi.ExtractsManagement.Core.Profiles;
using Dwapi.ExtractsManagement.Core.Profiles.Cbs;
using Dwapi.ExtractsManagement.Core.Profiles.Dwh;
using Dwapi.ExtractsManagement.Core.Profiles.Hts;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Tests.TestHelpers;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Services;
using Dwapi.UploadManagement.Core.Interfaces.Services.Cbs;
using Dwapi.UploadManagement.Core.Packager.Cbs;
using Dwapi.UploadManagement.Core.Profiles;
using Dwapi.UploadManagement.Core.Services;
using Dwapi.UploadManagement.Core.Services.Cbs;
using Dwapi.UploadManagement.Infrastructure.Data;
using Dwapi.UploadManagement.Infrastructure.Reader.Cbs;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.UploadManagement.Core.Tests.Services.Cbs
{
    [TestFixture]
    [Category("Cbs")]
    public class CbsSendServiceTests
    {
        private readonly string _authToken = @"1983aeda-6a96-11e8-adc0-fa7ae01bbebc";
        private readonly string _subId = "DWAPI";
        private readonly string url = "http://localhost:6767";

        private ICbsSendService _cbsSendService;
        private IServiceProvider _serviceProvider;
        private ManifestMessageBag _bag;
        private MpiMessageBag _mpiBag;
        private CentralRegistry _registry;

        [OneTimeSetUp]
        public void Init()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["ConnectionStrings:DwapiConnection"];


            _serviceProvider = new ServiceCollection()
                .AddDbContext<UploadContext>(o => o.UseSqlServer(connectionString))
                .AddTransient<ICbsSendService,CbsSendService>()
            .AddTransient<ICbsPackager, CbsPackager>()
            .AddTransient<ICbsExtractReader, CbsExtractReader>()

                .BuildServiceProvider();

            /*
                22704|TOGONYE DISPENSARY|KIRINYAGA
                22696|HERTLANDS MEDICAL CENTRE|NAROK
            */

            _bag = TestDataFactory.ManifestMessageBag(2,10001,10002);
            _mpiBag = TestDataFactory.MpiMessageBag(5, 10001, 10002);

            Mapper.Initialize(cfg =>
                {
                    cfg.AddProfile<MasterPatientIndexProfile>();
                }
            );

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

            var responses = _cbsSendService.SendManifestAsync(sendTo, _bag).Result;
            Assert.NotNull(responses);
            Assert.False(responses.Select(x=>x.IsValid()).Any(x=>false));
            foreach (var sendManifestResponse in responses)
            {
                Console.WriteLine(sendManifestResponse);
            }
        }


        [Test]
        public void should_Send_Mpi()
        {
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _cbsSendService.SendMpiAsync(sendTo, _mpiBag).Result;
            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
            foreach (var sendManifestResponse in responses)
            {
                Console.WriteLine(sendManifestResponse);
            }
        }
    }
}
