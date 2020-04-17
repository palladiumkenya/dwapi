using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dapper;
using Dwapi.ExtractsManagement.Core.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Cleaner.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Hts;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Hts;
using Dwapi.ExtractsManagement.Infrastructure.Repository;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Hts;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Hts;
using Dwapi.SettingsManagement.Core.Application.Metrics.Queries.Handlers;
using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SettingsManagement.Core.Services;
using Dwapi.SettingsManagement.Infrastructure;
using Dwapi.SettingsManagement.Infrastructure.Repository;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Infrastructure.Tests.TestHelpers;
using Dwapi.SharedKernel.Utility;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Infrastructure.Tests
{
    [SetUpFixture]
    public class TestInitializer
    {
        public static IServiceProvider ServiceProvider;
        public static string MsSqlConnectionString;
        public static string MySqlConnectionString;
        public static string EmrConnectionString;
        public static string ConnectionString;

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
            SqlMapper.AddTypeHandler<Guid>(new GuidTypeHandler());
            RegisterLicence();
            RemoveTestsFilesDbs();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            EmrConnectionString = GenerateConnection(config, "emrConnection", false);
            ConnectionString = GenerateConnection(config, "dwapiConnection");
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

            services.AddDbContext<ExtractsContext>(x => x.UseSqlite(connection));

            services.AddTransient<IEmrMetricRepository, EmrMetricRepository>();
            services.AddTransient<IExtractHistoryRepository, ExtractHistoryRepository>();
            services.AddTransient<IValidatorRepository, ValidatorRepository>();

            services.AddTransient<IMasterPatientIndexReader, MasterPatientIndexReader>();
            services.AddTransient<IExtractSourceReader, ExtractSourceReader>();
            services.AddTransient<IHTSExtractSourceReader, HTSExtractSourceReader>();

            services.AddScoped<IMasterPatientIndexRepository, MasterPatientIndexRepository>();
            services.AddScoped<ITempMasterPatientIndexRepository, TempMasterPatientIndexRepository>();


             services.AddScoped<ITempPatientExtractRepository, TempPatientExtractRepository>();
            services.AddScoped<ITempPatientArtExtractRepository, TempPatientArtExtractRepository>();
            services.AddScoped<ITempPatientBaselinesExtractRepository, TempPatientBaselinesExtractRepository>();
            services.AddScoped<ITempPatientLaboratoryExtractRepository, TempPatientLaboratoryExtractRepository>();
            services.AddScoped<ITempPatientPharmacyExtractRepository, TempPatientPharmacyExtractRepository>();
            services.AddScoped<ITempPatientStatusExtractRepository, TempPatientStatusExtractRepository>();
            services.AddScoped<ITempPatientVisitExtractRepository, TempPatientVisitExtractRepository>();
            services.AddScoped<ITempPatientAdverseEventExtractRepository, TempPatientAdverseEventExtractRepository>();
            services.AddScoped<IValidatorRepository, ValidatorRepository>();
            services.AddScoped<IPatientExtractRepository, PatientExtractRepository>();
            services.AddScoped<IPatientArtExtractRepository, PatientArtExtractRepository>();
            services.AddScoped<IPatientBaselinesExtractRepository, PatientBaselinesExtractRepository>();
            services.AddScoped<IPatientLaboratoryExtractRepository, PatientLaboratoryExtractRepository>();
            services.AddScoped<IPatientPharmacyExtractRepository, PatientPharmacyExtractRepository>();
            services.AddScoped<IPatientStatusExtractRepository, PatientStatusExtractRepository>();
            services.AddScoped<IPatientVisitExtractRepository, PatientVisitExtractRepository>();
            services.AddScoped<IPatientAdverseEventExtractRepository, PatientAdverseEventExtractRepository>();
            services.AddScoped<ITempPatientExtractErrorSummaryRepository, TempPatientExtractErrorSummaryRepository>();
            services
                .AddScoped<ITempPatientArtExtractErrorSummaryRepository, TempPatientArtExtractErrorSummaryRepository>();
            services
                .AddScoped<ITempPatientBaselinesExtractErrorSummaryRepository,
                    TempPatientBaselinesExtractErrorSummaryRepository>();
            services
                .AddScoped<ITempPatientLaboratoryExtractErrorSummaryRepository,
                    TempPatientLaboratoryExtractErrorSummaryRepository>();
            services
                .AddScoped<ITempPatientPharmacyExtractErrorSummaryRepository,
                    TempPatientPharmacyExtractErrorSummaryRepository>();
            services
                .AddScoped<ITempPatientStatusExtractErrorSummaryRepository,
                    TempPatientStatusExtractErrorSummaryRepository>();
            services
                .AddScoped<ITempPatientVisitExtractErrorSummaryRepository, TempPatientVisitExtractErrorSummaryRepository
                >();
            services
                .AddScoped<ITempPatientAdverseEventExtractErrorSummaryRepository,
                    TempPatientAdverseEventExtractErrorSummaryRepository>();

               services.AddScoped<ITempHTSClientExtractRepository, TempHTSClientExtractRepository>();
            services.AddScoped<ITempHTSClientLinkageExtractRepository, TempHTSClientLinkageExtractRepository>();
            services.AddScoped<ITempHTSClientPartnerExtractRepository, TempHTSClientPartnerExtractRepository>();

            services.AddScoped<IHTSClientExtractRepository, HTSClientExtractRepository>();
            services.AddScoped<IHTSClientLinkageExtractRepository, HTSClientLinkageExtractRepository>();
            services.AddScoped<IHTSClientPartnerExtractRepository, HTSClientPartnerExtractRepository>();

            services.AddScoped<ITempHtsClientsExtractRepository, TempHtsClientsExtractRepository>();
            services.AddScoped<ITempHtsClientsLinkageExtractRepository, TempHtsClientsLinkageExtractRepository>();
            services.AddScoped<ITempHtsClientTestsExtractRepository, TempHtsClientTestsExtractRepository>();
            services.AddScoped<ITempHtsTestKitsExtractRepository, TempHtsTestKitsExtractRepository>();
            services.AddScoped<ITempHtsClientTracingExtractRepository, TempHtsClientTracingExtractRepository>();
            services.AddScoped<ITempHtsPartnerTracingExtractRepository, TempHtsPartnerTracingExtractRepository>();
            services.AddScoped<ITempHtsPartnerNotificationServicesExtractRepository, TempHtsPartnerNotificationServicesExtractRepository>();

            services.AddScoped<IHtsClientsExtractRepository, HtsClientsExtractRepository>();
            services.AddScoped<IHtsClientsLinkageExtractRepository, HtsClientsLinkageExtractRepository>();
            services.AddScoped<IHtsClientTestsExtractRepository, HtsClientTestsExtractRepository>();
            services.AddScoped<IHtsTestKitsExtractRepository, HtsTestKitsExtractRepository>();
            services.AddScoped<IHtsClientTracingExtractRepository, HtsClientTracingExtractRepository>();
            services.AddScoped<IHtsPartnerTracingExtractRepository, HtsPartnerTracingExtractRepository>();
            services.AddScoped<IHtsPartnerNotificationServicesExtractRepository, HtsPartnerNotificationServicesExtractRepository>();

            services.AddScoped<ITempHTSClientExtractErrorSummaryRepository, TempHTSClientExtractErrorSummaryRepository>();
            services.AddScoped<ITempHTSClientLinkageExtractErrorSummaryRepository, TempHTSClientLinkageExtractErrorSummaryRepository>();
            services.AddScoped<ITempHTSClientPartnerExtractErrorSummaryRepository, TempHTSClientPartnerExtractErrorSummaryRepository>();

            services.AddScoped<ITempHtsClientsExtractErrorSummaryRepository, TempHtsClientsExtractErrorSummaryRepository>();
            services.AddScoped<ITempHtsClientLinkageErrorSummaryRepository, TempHtsClientsLinkageExtractErrorSummaryRepository>();
            services.AddScoped<ITempHtsClientTestsErrorSummaryRepository, TempHtsClientTestsExtractErrorSummaryRepository>();
            services.AddScoped<ITempHtsTestKitsErrorSummaryRepository, TempHtsTestKitsExtractErrorSummaryRepository>();
            services.AddScoped<ITempHtsClientTracingErrorSummaryRepository, TempHtsClientTracingExtractErrorSummaryRepository>();
            services.AddScoped<ITempHtsPartnerTracingErrorSummaryRepository, TempHtsPartnerTracingExtractErrorSummaryRepository>();
            services.AddScoped<ITempHtsPartnerNotificationServicesErrorSummaryRepository, TempHtsPartnerNotificationServicesExtractErrorSummaryRepository>();


            services.AddTransient<IMasterPatientIndexValidator, MasterPatientIndexValidator>();
            services.AddTransient<IExtractValidator, ExtractValidator>();
            services.AddTransient<IHtsExtractValidator, HtsExtractValidator>();


            ServiceProvider = services.BuildServiceProvider();
        }

        public static void InitDb()
        {
            var settingsContext = ServiceProvider.GetService<SettingsContext>();
            var extractsContext = ServiceProvider.GetService<ExtractsContext>();

            settingsContext.Database.Migrate();
            settingsContext.EnsureSeeded();


            extractsContext.Database.Migrate();
            extractsContext.EnsureSeeded();


            Iqtools = settingsContext.EmrSystems
                .Include(x => x.DatabaseProtocols)
                .Include(x => x.Extracts)
                .First(x => x.Id == new Guid("a62216ee-0e85-11e8-ba89-0ed5f89f718b"));

            Validator = extractsContext.Validator.First();


            var dbmanager = ServiceProvider.GetService<IAppDatabaseManager>();

            IqToolsDatabase = dbmanager.ReadConnection(settingsContext.Database.GetDbConnection().ConnectionString,
                DatabaseProvider.MsSql);

            IQtoolsDbProtocol =
                Iqtools.DatabaseProtocols.First(x => x.DatabaseName.ToLower().Contains("iqtools".ToLower()));
            IQtoolsDbProtocol.Host = IqToolsDatabase.Server;
            IQtoolsDbProtocol.Username = IqToolsDatabase.User;
            IQtoolsDbProtocol.Password = IqToolsDatabase.Password;
        }

        public static void InitMysQLDb()
        {
            var settingsContextMysql = ServiceProviderMysql.GetService<SettingsContext>();
            var extractsContextMysql = ServiceProviderMysql.GetService<ExtractsContext>();

            settingsContextMysql.Database.Migrate();
            settingsContextMysql.EnsureSeeded();

            extractsContextMysql.Database.Migrate();
            extractsContextMysql.EnsureSeeded();

            KenyaEmr = settingsContextMysql.EmrSystems
                .Include(x => x.DatabaseProtocols)
                .Include(x => x.Extracts)
                .First(x => x.Id == new Guid("a6221856-0e85-11e8-ba89-0ed5f89f718b"));

            Validator = extractsContextMysql.Validator.First();


            var dbmanager= ServiceProvider.GetService<IAppDatabaseManager>();

            KenyaEmrDatabase = dbmanager.ReadConnection(settingsContextMysql.Database.GetDbConnection().ConnectionString, DatabaseProvider.MySql);


            KenyaEmrDbProtocol = KenyaEmr.DatabaseProtocols.First();
            KenyaEmrDbProtocol.Host = KenyaEmrDatabase.Server;
            KenyaEmrDbProtocol.Username = KenyaEmrDatabase.User;
            KenyaEmrDbProtocol.Password = KenyaEmrDatabase.Password;
            KenyaEmrDbProtocol.Port = (int?) KenyaEmrDatabase.Port;
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
            DapperPlusManager.AddLicense("1755;700-ThePalladiumGroup", "2073303b-0cfc-fbb9-d45f-1723bb282a3c");
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
