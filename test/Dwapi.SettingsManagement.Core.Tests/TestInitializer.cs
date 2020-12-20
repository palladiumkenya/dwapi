using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Dapper;
using Dwapi.SettingsManagement.Core.Application.Metrics.Queries.Handlers;
using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Services;
using Dwapi.SettingsManagement.Infrastructure;
using Dwapi.SettingsManagement.Infrastructure.Repository;
using Dwapi.SharedKernel.Infrastructure.Tests.TestHelpers;
using Dwapi.SharedKernel.Utility;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;
using Z.Dapper.Plus;
using SettingsContext = Dwapi.SettingsManagement.Infrastructure.SettingsContext;

namespace Dwapi.SettingsManagement.Core.Tests
{
    [SetUpFixture]
    public class TestInitializer
    {
        public static IServiceProvider ServiceProvider;
        public static string MsSqlConnectionString;
        public static string MySqlConnectionString;
        public static string EmrConnectionString;
        public static string ConnectionString;

        [OneTimeSetUp]
        public void Setup()
        {
            SqlMapper.AddTypeHandler<Guid>(new GuidTypeHandler());
            SqlMapper.AddTypeHandler(new NullableLongHandler());
            SqlMapper.AddTypeHandler(new NullableIntHandler());

            RegisterLicence();
            RemoveTestsFilesDbs();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            EmrConnectionString= GenerateConnection(config,"emrConnection",false);
            ConnectionString = GenerateConnection(config,"dwapiConnection");
            MsSqlConnectionString = config.GetConnectionString("mssqlConnection");
            MySqlConnectionString = config.GetConnectionString("mysqlConnection");

            var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var services = new ServiceCollection();
            services.AddDbContext<SettingsContext>(x => x.UseSqlite(connection));

            services.AddTransient<IAppDatabaseManager, AppDatabaseManager>();
            services.AddTransient<IDatabaseManager, DatabaseManager>();

            services.AddTransient<IAppMetricRepository,AppMetricRepository>();
            services.AddTransient<ICentralRegistryRepository,CentralRegistryRepository>();
            services.AddTransient<IDatabaseProtocolRepository,DatabaseProtocolRepository>();
            services.AddTransient<IDocketRepository,DocketRepository>();
            services.AddTransient<IEmrSystemRepository,EmrSystemRepository>();
            services.AddTransient<IExtractRepository,ExtractRepository>();
            services.AddTransient<IRestProtocolRepository,RestProtocolRepository>();

            services.AddTransient<IEmrManagerService,EmrManagerService>();
            services.AddTransient<IExtractManagerService,ExtractManagerService>();
            services.AddTransient<IRegistryManagerService,RegistryManagerService>();

            services.AddMediatR(typeof(GetAppMetricHandler));

            ServiceProvider = services.BuildServiceProvider();
        }

        public static void ClearDb()
        {
            var context = ServiceProvider.GetService<SettingsContext>();
            context.Database.EnsureCreated();
            context.EnsureSeeded();
        }
        public static void SeedData(params IEnumerable<object>[] entities)
        {
            var context = ServiceProvider.GetService<SettingsContext>();
            foreach (IEnumerable<object> t in entities)
            {
                context.AddRange(t);
            }
            context.SaveChanges();
        }


        private void RegisterLicence()
        {
            DapperPlusManager.AddLicense("1755;700-ThePalladiumGroup", "218460a6-02d0-c26b-9add-e6b8d13ccbf4");
            if (!Z.Dapper.Plus.DapperPlusManager.ValidateLicense(out var licenseErrorMessage))
            {
                throw new Exception(licenseErrorMessage);
            }
        }

        private string GenerateConnection(IConfigurationRoot config, string name, bool createNew = true)
        {
            var dir = $"{TestContext.CurrentContext.TestDirectory.HasToEndWith(@"/")}";

            var connectionString = config.GetConnectionString(name)
                .Replace("#dir#", dir)
                .ToOsStyle();

            if (createNew)
                return connectionString.Replace(".db", $"{DateTime.Now.Ticks}.db");

            return connectionString;
        }

        private void RemoveTestsFilesDbs()
        {
            string[] keyFiles =
                { "dwapi.db","emr.db"};
            string[] keyDirs = { @"TestArtifacts/Database".ToOsStyle()};

            foreach (var keyDir in keyDirs)
            {
                DirectoryInfo di = new DirectoryInfo(keyDir);
                foreach (FileInfo file in di.GetFiles())
                {
                    if (!keyFiles.Contains(file.Name))
                        file.Delete();
                }
            }
        }
    }
}
