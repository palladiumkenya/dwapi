using System;
using System.Linq;
using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.SettingsManagement.Infrastructure.Tests
{
    [SetUpFixture]
    public class TestInitializer
    {
        public static IServiceProvider ServiceProvider;
        public static IServiceProvider ServiceProviderMysql;
        public static EmrSystem Iqtools;
        public static EmrSystem KenyaEmr;

        public static AppDatabase IqToolsDatabase;
        public static AppDatabase KenyaEmrDatabase;
        public static DatabaseProtocol IQtoolsDbProtocol;
        public static DatabaseProtocol KenyaEmrDbProtocol;

        public static string MsSqlConnectionString;
        public static string MySqlConnectionString;

        [OneTimeSetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var mysqlConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.mysql.json")
                .Build();

            ServiceProvider = new ServiceCollection()
                .AddDbContext<SettingsContext>(x =>
                    x.UseSqlServer(MsSqlConnectionString = config["ConnectionStrings:DwapiConnection"]))
                .AddTransient<IAppDatabaseManager, AppDatabaseManager>()
                .BuildServiceProvider();

            ServiceProviderMysql = new ServiceCollection()
                .AddDbContext<SettingsContext>(x =>
                    x.UseMySql(MySqlConnectionString = mysqlConfig["ConnectionStrings:DwapiConnection"]))
                .AddTransient<IAppDatabaseManager, AppDatabaseManager>()
                .BuildServiceProvider();
        }

        public static void InitConnections()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            MsSqlConnectionString = config["ConnectionStrings:DwapiConnection"];

            var mysqlConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.mysql.json")
                .Build();

            MySqlConnectionString = mysqlConfig["ConnectionStrings:DwapiConnection"];
        }
        public static void InitDbMsSql()
        {
            var  settingsContext=ServiceProvider.GetService<SettingsContext>();
            var dbmanager = ServiceProvider.GetService<IAppDatabaseManager>();
            Iqtools = settingsContext.EmrSystems.Include(x => x.DatabaseProtocols).FirstOrDefault(x=>x.Name=="IQCare");

            IqToolsDatabase = dbmanager.ReadConnection(settingsContext.Database.GetDbConnection().ConnectionString,
                DatabaseProvider.MsSql);

            IQtoolsDbProtocol =
                Iqtools.DatabaseProtocols.First(x => x.DatabaseName.ToLower().Contains("iqtools".ToLower()));
            IQtoolsDbProtocol.Host = IqToolsDatabase.Server;
            IQtoolsDbProtocol.Username = IqToolsDatabase.User;
            IQtoolsDbProtocol.Password = IqToolsDatabase.Password;
        }
        public static void InitDbMySql()
        {
            var settingsContext = ServiceProviderMysql.GetService<SettingsContext>();
            var dbmanager = ServiceProviderMysql.GetService<IAppDatabaseManager>();
            KenyaEmr = settingsContext.EmrSystems.Include(x => x.DatabaseProtocols).FirstOrDefault(x=>x.Name=="KenyaEMR");
            KenyaEmrDatabase = dbmanager.ReadConnection(settingsContext.Database.GetDbConnection().ConnectionString,
                DatabaseProvider.MySql);

            KenyaEmrDbProtocol = KenyaEmr.DatabaseProtocols.First();
            KenyaEmrDbProtocol.Host = KenyaEmrDatabase.Server;
            KenyaEmrDbProtocol.Username = KenyaEmrDatabase.User;
            KenyaEmrDbProtocol.Password = KenyaEmrDatabase.Password;

        }
    }
}
