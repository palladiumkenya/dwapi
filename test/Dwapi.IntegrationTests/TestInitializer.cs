using System;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SettingsManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.IntegrationTests
{
    [SetUpFixture]
    public class TestInitializer
    {
        public static IServiceProvider MsSqlServiceProvider;
        public static IServiceProvider MySqlServiceProvider;

        public static string MsSqlConnectionString;
        public static string MySqlConnectionString;

        [OneTimeSetUp]
        public void Setup()
        {
            RegisterLicence();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            MsSqlConnectionString = config.GetConnectionString("mssqlConnection");
            MySqlConnectionString = config.GetConnectionString("mysqlConnection");

            var mssqlServices = new ServiceCollection();
            mssqlServices.AddDbContext<SettingsContext>(x => x.UseSqlServer(MsSqlConnectionString));
            mssqlServices.AddDbContext<ExtractsContext>(x => x.UseSqlServer(MsSqlConnectionString));
            MsSqlServiceProvider = mssqlServices.BuildServiceProvider();

            var mysqlServices = new ServiceCollection();
            mysqlServices.AddDbContext<SettingsContext>(x => x.UseMySql(MySqlConnectionString));
            mysqlServices.AddDbContext<ExtractsContext>(x => x.UseMySql(MySqlConnectionString));
            MySqlServiceProvider = mysqlServices.BuildServiceProvider();
        }

        public static void SetupMsSql()
        {
            var context = MsSqlServiceProvider.GetService<SettingsContext>();
            context.Database.EnsureDeleted();
        }

        public static void SetupMySql()
        {
            var context = MySqlServiceProvider.GetService<SettingsContext>();
            context.Database.EnsureDeleted();
        }

        private void RegisterLicence()
        {
            DapperPlusManager.AddLicense("1755;700-ThePalladiumGroup", "2073303b-0cfc-fbb9-d45f-1723bb282a3c");
            if (!DapperPlusManager.ValidateLicense(out var licenseErrorMessage))
            {
                throw new Exception(licenseErrorMessage);
            }
        }
    }
}
