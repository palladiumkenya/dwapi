using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Cleaner.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Repository;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh;
using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SettingsManagement.Infrastructure;
using Dwapi.SharedKernel.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Infrastructure.Tests
{
    [SetUpFixture]
    public class TestInitializer
    {
        public static IServiceProvider ServiceProvider;
        public static IServiceProvider ServiceProviderMysql;
        public static EmrSystem Iqtools;
        public static EmrSystem KenyaEmr;
        public static Validator Validator;
        public static AppDatabase IqToolsDatabase;
        public static AppDatabase KenyaEmrDatabase;
        public static DatabaseProtocol IQtoolsDbProtocol;
        public static DatabaseProtocol KenyaEmrDbProtocol;

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
                .AddTransient<IMasterPatientIndexRepository, MasterPatientIndexRepository>()
                .AddTransient<ITempPatientExtractRepository, TempPatientExtractRepository>()
                .AddTransient<ICleanCbsExtracts, CleanCbsExtracts>()
                .AddTransient<IClearDwhExtracts, ClearDwhExtracts>()
                .AddTransient<IMasterPatientIndexReader, MasterPatientIndexReader>()
                .AddTransient<IExtractSourceReader, ExtractSourceReader>()
                .AddTransient<IAppDatabaseManager, AppDatabaseManager>()
                .BuildServiceProvider();

            var serviceProviderMysql = new ServiceCollection()
                .AddDbContext<ExtractsContext>(x => x.UseMySql(mysqlConfig["ConnectionStrings:DwapiConnection"]))
                .AddDbContext<SettingsContext>(x => x.UseMySql(mysqlConfig["ConnectionStrings:DwapiConnection"]))
                .AddTransient<IExtractHistoryRepository, ExtractHistoryRepository>()
                .AddTransient<ITempMasterPatientIndexRepository, TempMasterPatientIndexRepository>()
                .AddTransient<IMasterPatientIndexRepository, MasterPatientIndexRepository>()
                .AddTransient<ITempPatientExtractRepository, TempPatientExtractRepository>()
                .AddTransient<ICleanCbsExtracts, CleanCbsExtracts>()
                .AddTransient<IClearDwhExtracts, ClearDwhExtracts>()
                .AddTransient<IMasterPatientIndexReader, MasterPatientIndexReader>()
                .AddTransient<IExtractSourceReader, ExtractSourceReader>()
                .AddTransient<IAppDatabaseManager, AppDatabaseManager>()
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


            var dbmanager= serviceProvider.GetService<IAppDatabaseManager>();

            IqToolsDatabase = dbmanager.ReadConnection(settingsContext.Database.GetDbConnection().ConnectionString,DatabaseProvider.MsSql);
            KenyaEmrDatabase = dbmanager.ReadConnection(settingsContext.Database.GetDbConnection().ConnectionString, DatabaseProvider.MySql);

            IQtoolsDbProtocol = Iqtools.DatabaseProtocols.First(x => x.DatabaseName.ToLower().Contains("iqtools".ToLower()));
            IQtoolsDbProtocol.Host = IqToolsDatabase.Server;
            IQtoolsDbProtocol.Username = IqToolsDatabase.User;
            IQtoolsDbProtocol.Password = IqToolsDatabase.Password;
            //_iQtoolsDb.Password = TestInitializer.IqToolsDatabase.Database;

            KenyaEmrDbProtocol = KenyaEmr.DatabaseProtocols.First();
            KenyaEmrDbProtocol.Host = KenyaEmrDatabase.Server;
            KenyaEmrDbProtocol.Username = KenyaEmrDatabase.User;
            KenyaEmrDbProtocol.Password = KenyaEmrDatabase.Password;
            KenyaEmrDbProtocol.DatabaseName = KenyaEmrDatabase.Database;
        }
    }
}