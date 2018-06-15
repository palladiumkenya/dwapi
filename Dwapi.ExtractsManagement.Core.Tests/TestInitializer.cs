using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Cleaner.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.ExtractsManagement.Infrastructure.Repository;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SettingsManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests
{
    [SetUpFixture]
    public class TestInitializer
    {
        public static IServiceProvider ServiceProvider;
        public static IServiceProvider ServiceProviderMysql;
        public static EmrSystem Iqtools;
        public static EmrSystem KenyaEmr;
        public static Validator Validator;

        [OneTimeSetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var mysqlConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.mysql.json")
                .Build();


            var serviceProvider = new ServiceCollection()
                .AddDbContext<ExtractsContext>(x => x.UseSqlServer(config["ConnectionStrings:DwapiConnection"]))
                .AddDbContext<SettingsContext>(x => x.UseSqlServer(config["ConnectionStrings:DwapiConnection"]))
                .AddTransient<IExtractHistoryRepository, ExtractHistoryRepository>()
                .AddTransient<ITempMasterPatientIndexRepository, TempMasterPatientIndexRepository>()
                .AddTransient<ITempPatientExtractRepository, TempPatientExtractRepository>()
                .AddTransient<ICleanCbsExtracts, CleanCbsExtracts>()
                .AddTransient<IClearDwhExtracts, ClearDwhExtracts>()
                .BuildServiceProvider();

            var serviceProviderMysql = new ServiceCollection()
                .AddDbContext<ExtractsContext>(x => x.UseMySql(mysqlConfig["ConnectionStrings:DwapiConnection"]))
                .AddDbContext<SettingsContext>(x => x.UseMySql(mysqlConfig["ConnectionStrings:DwapiConnection"]))
                .AddTransient<IExtractHistoryRepository, ExtractHistoryRepository>()
                .AddTransient<ITempMasterPatientIndexRepository, TempMasterPatientIndexRepository>()
                .AddTransient<ITempPatientExtractRepository, TempPatientExtractRepository>()
                .AddTransient<ICleanCbsExtracts, CleanCbsExtracts>()
                .AddTransient<IClearDwhExtracts, ClearDwhExtracts>()
                .BuildServiceProvider();

            ServiceProvider = serviceProvider;
            ServiceProviderMysql = serviceProviderMysql;

            var settingsContext = serviceProvider.GetService<SettingsContext>();
            var settingsContextMysql = serviceProviderMysql.GetService<SettingsContext>();
            var extractsContext = serviceProvider.GetService<ExtractsContext>();
            var extractsContextMysql = serviceProviderMysql.GetService<ExtractsContext>();

            settingsContext.Database.Migrate();
            settingsContext.EnsureSeeded();

            settingsContextMysql.Database.Migrate();
            settingsContextMysql.EnsureSeeded();

            extractsContext.Database.Migrate();
            extractsContext.EnsureSeeded();

            extractsContextMysql.Database.Migrate();
            extractsContextMysql.EnsureSeeded();

            Iqtools = settingsContextMysql.EmrSystems
                .Include(x => x.DatabaseProtocols)
                .Include(x => x.Extracts)
                .First(x => x.Id == new Guid("a62216ee-0e85-11e8-ba89-0ed5f89f718b"));

            KenyaEmr = settingsContextMysql.EmrSystems
                .Include(x => x.DatabaseProtocols)
                .Include(x => x.Extracts)
                .First(x => x.Id == new Guid("a6221856-0e85-11e8-ba89-0ed5f89f718b"));

            Validator = extractsContext.Validator.First();
        }
    }
}