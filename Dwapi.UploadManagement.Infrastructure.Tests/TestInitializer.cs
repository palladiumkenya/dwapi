using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using AutoMapper.Data;
using Dapper;
using Dwapi.ExtractsManagement.Core.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Cleaner.Dwh;
using Dwapi.ExtractsManagement.Core.Cleaner.Hts;
using Dwapi.ExtractsManagement.Core.Cleaner.Mgs;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Extractors.Cbs;
using Dwapi.ExtractsManagement.Core.Extractors.Dwh;
using Dwapi.ExtractsManagement.Core.Extractors.Hts;
using Dwapi.ExtractsManagement.Core.Extractors.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Mgs;
using Dwapi.ExtractsManagement.Core.Loader.Cbs;
using Dwapi.ExtractsManagement.Core.Loader.Dwh;
using Dwapi.ExtractsManagement.Core.Loader.Hts;
using Dwapi.ExtractsManagement.Core.Loader.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Destination;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mgs;
using Dwapi.ExtractsManagement.Core.Profiles;
using Dwapi.ExtractsManagement.Core.Profiles.Cbs;
using Dwapi.ExtractsManagement.Core.Profiles.Dwh;
using Dwapi.ExtractsManagement.Core.Profiles.Hts;
using Dwapi.ExtractsManagement.Core.Profiles.Mgs;
using Dwapi.ExtractsManagement.Core.Services;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.ExtractsManagement.Infrastructure.Reader;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Hts;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Mgs;
using Dwapi.ExtractsManagement.Infrastructure.Reader.SmartCard;
using Dwapi.ExtractsManagement.Infrastructure.Repository;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Extracts;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.TempExtracts;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Validations;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Hts;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Hts.Extracts;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Hts.TempExtracts;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Hts.Validations;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Mgs.Extracts;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Mgs.TempExtracts;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Mgs.Validations;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Hts;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Mgs;
using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SettingsManagement.Infrastructure;
using Dwapi.SettingsManagement.Infrastructure.Repository;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Infrastructure.Tests.TestHelpers;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Hts;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Hts;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Mgs;
using Dwapi.UploadManagement.Core.Interfaces.Services.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Services.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Services.Hts;
using Dwapi.UploadManagement.Core.Interfaces.Services.Mgs;
using Dwapi.UploadManagement.Core.Packager.Cbs;
using Dwapi.UploadManagement.Core.Packager.Dwh;
using Dwapi.UploadManagement.Core.Packager.Hts;
using Dwapi.UploadManagement.Core.Profiles;
using Dwapi.UploadManagement.Core.Services.Cbs;
using Dwapi.UploadManagement.Core.Services.Dwh;
using Dwapi.UploadManagement.Core.Services.Hts;
using Dwapi.UploadManagement.Core.Services.Mgs;
using Dwapi.UploadManagement.Infrastructure.Data;
using Dwapi.UploadManagement.Infrastructure.Reader;
using Dwapi.UploadManagement.Infrastructure.Reader.Cbs;
using Dwapi.UploadManagement.Infrastructure.Reader.Dwh;
using Dwapi.UploadManagement.Infrastructure.Reader.Hts;
using Dwapi.UploadManagement.Infrastructure.Reader.Mgs;
using Dwapi.UploadManagement.Infrastructure.Tests.TestArtifacts;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.UploadManagement.Infrastructure.Tests
{
    [SetUpFixture]
    public class TestInitializer
    {
        public static IServiceProvider ServiceProvider;
        public static string MsSqlConnectionString;
        public static string MySqlConnectionString;
        public static string EmrConnectionString;
        public static string ConnectionString;
        public static DatabaseProtocol Protocol;
        public static List<Extract> Extracts;

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

            EmrConnectionString = GenerateConnection(config, "emrConnection", false);
            ConnectionString = GenerateCopyConnection(config, "dwapiConnection");
            MsSqlConnectionString = config.GetConnectionString("mssqlConnection");
            MySqlConnectionString = config.GetConnectionString("mysqlConnection");

            var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var services = new ServiceCollection();

            services.AddDbContext<UploadContext>(x => x.UseSqlite(connection));

            #region Setttings.Infrastructure

            services.AddDbContext<SettingsContext>(x => x.UseSqlite(connection));

            services.AddTransient<IAppDatabaseManager, AppDatabaseManager>();
            services.AddTransient<IDatabaseManager, DatabaseManager>();

            services.AddTransient<IAppMetricRepository, AppMetricRepository>();
            services.AddTransient<ICentralRegistryRepository, CentralRegistryRepository>();
            services.AddTransient<IDatabaseProtocolRepository, DatabaseProtocolRepository>();
            services.AddTransient<IDocketRepository, DocketRepository>();
            services.AddTransient<IEmrSystemRepository, EmrSystemRepository>();
            services.AddTransient<IExtractRepository, ExtractRepository>();
            services.AddTransient<IRestProtocolRepository, RestProtocolRepository>();

            #endregion

            #region Extracts.Infrastructure

            services.AddDbContext<ExtractsContext>(x => x.UseSqlite(connection));

            #region Readers

            services.AddTransient<IMasterPatientIndexReader, MasterPatientIndexReader>();
            services.AddTransient<IDwhExtractSourceReader, DwhExtractSourceReader>();
            services.AddTransient<IHTSExtractSourceReader, HTSExtractSourceReader>();
            services.AddTransient<IPsmartSourceReader, PsmartSourceReader>();
            // NEW
            services.AddScoped<IMgsExtractSourceReader,MgsExtractSourceReader>();
            #endregion

            services.AddTransient<IEmrMetricRepository, EmrMetricRepository>();
            services.AddTransient<IExtractHistoryRepository, ExtractHistoryRepository>();
            services.AddTransient<IValidatorRepository, ValidatorRepository>();
            services.AddTransient<IPsmartStageRepository, PsmartStageRepository>();

            #region CBS

            services.AddTransient<IMasterPatientIndexRepository, MasterPatientIndexRepository>();
            services.AddTransient<ITempMasterPatientIndexRepository, TempMasterPatientIndexRepository>();

            #endregion

            #region NDWH

            #region Extracts

            services.AddTransient<IPatientAdverseEventExtractRepository, PatientAdverseEventExtractRepository>();
            services.AddTransient<IPatientArtExtractRepository, PatientArtExtractRepository>();
            services.AddTransient<IPatientBaselinesExtractRepository, PatientBaselinesExtractRepository>();
            services.AddTransient<IPatientExtractRepository, PatientExtractRepository>();
            services.AddTransient<IPatientLaboratoryExtractRepository, PatientLaboratoryExtractRepository>();
            services.AddTransient<IPatientPharmacyExtractRepository, PatientPharmacyExtractRepository>();
            services.AddTransient<IPatientStatusExtractRepository, PatientStatusExtractRepository>();
            services.AddTransient<IPatientVisitExtractRepository, PatientVisitExtractRepository>();

            #endregion

            #region TempExtracts

            services
                .AddTransient<ITempPatientAdverseEventExtractRepository, TempPatientAdverseEventExtractRepository>();
            services.AddTransient<ITempPatientArtExtractRepository, TempPatientArtExtractRepository>();
            services.AddTransient<ITempPatientBaselinesExtractRepository, TempPatientBaselinesExtractRepository>();
            services.AddTransient<ITempPatientExtractRepository, TempPatientExtractRepository>();
            services.AddTransient<ITempPatientLaboratoryExtractRepository, TempPatientLaboratoryExtractRepository>();
            services.AddTransient<ITempPatientPharmacyExtractRepository, TempPatientPharmacyExtractRepository>();
            services.AddTransient<ITempPatientStatusExtractRepository, TempPatientStatusExtractRepository>();
            services.AddTransient<ITempPatientVisitExtractRepository, TempPatientVisitExtractRepository>();

            #endregion

            #region Validations

            services
                .AddScoped<ITempPatientAdverseEventExtractErrorSummaryRepository,
                    TempPatientAdverseEventExtractErrorSummaryRepository>();
            services
                .AddScoped<ITempPatientArtExtractErrorSummaryRepository, TempPatientArtExtractErrorSummaryRepository>();
            services
                .AddScoped<ITempPatientBaselinesExtractErrorSummaryRepository,
                    TempPatientBaselinesExtractErrorSummaryRepository>();
            services.AddScoped<ITempPatientExtractErrorSummaryRepository, TempPatientExtractErrorSummaryRepository>();
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

            #endregion

            #endregion

            #region HTS

            #region Extracts

            services.AddScoped<IHTSClientExtractRepository, HTSClientExtractRepository>();
            services.AddScoped<IHTSClientLinkageExtractRepository, HTSClientLinkageExtractRepository>();
            services.AddScoped<IHTSClientPartnerExtractRepository, HTSClientPartnerExtractRepository>();
            services.AddScoped<IHtsClientsExtractRepository, HtsClientsExtractRepository>();
            services.AddScoped<IHtsClientsLinkageExtractRepository, HtsClientsLinkageExtractRepository>();
            services.AddScoped<IHtsClientTestsExtractRepository, HtsClientTestsExtractRepository>();
            services.AddScoped<IHtsClientTracingExtractRepository, HtsClientTracingExtractRepository>();
            services.AddScoped<IHtsPartnerNotificationServicesExtractRepository, HtsPartnerNotificationServicesExtractRepository>();
            services.AddScoped<IHtsPartnerTracingExtractRepository, HtsPartnerTracingExtractRepository>();
            services.AddScoped<IHtsTestKitsExtractRepository, HtsTestKitsExtractRepository>();

            #endregion

            #region TempExtracts
            services.AddScoped<ITempHTSClientExtractRepository, TempHTSClientExtractRepository>();
            services.AddScoped<ITempHTSClientLinkageExtractRepository, TempHTSClientLinkageExtractRepository>();
            services.AddScoped<ITempHTSClientPartnerExtractRepository, TempHTSClientPartnerExtractRepository>();
            services.AddScoped<ITempHtsClientsExtractRepository, TempHtsClientsExtractRepository>();
            services.AddScoped<ITempHtsClientsLinkageExtractRepository, TempHtsClientsLinkageExtractRepository>();
            services.AddScoped<ITempHtsClientTestsExtractRepository, TempHtsClientTestsExtractRepository>();
            services.AddScoped<ITempHtsClientTracingExtractRepository, TempHtsClientTracingExtractRepository>();
            services.AddScoped<ITempHtsPartnerNotificationServicesExtractRepository, TempHtsPartnerNotificationServicesExtractRepository>();
            services.AddScoped<ITempHtsPartnerTracingExtractRepository, TempHtsPartnerTracingExtractRepository>();
            services.AddScoped<ITempHtsTestKitsExtractRepository, TempHtsTestKitsExtractRepository>();
            #endregion

            #region Validations
            services.AddScoped<ITempHTSClientExtractErrorSummaryRepository, TempHTSClientExtractErrorSummaryRepository>();
            services.AddScoped<ITempHTSClientLinkageExtractErrorSummaryRepository, TempHTSClientLinkageExtractErrorSummaryRepository>();
            services.AddScoped<ITempHTSClientPartnerExtractErrorSummaryRepository, TempHTSClientPartnerExtractErrorSummaryRepository>();
            services.AddScoped<ITempHtsClientsExtractErrorSummaryRepository, TempHtsClientsExtractErrorSummaryRepository>();
            services.AddScoped<ITempHtsClientLinkageErrorSummaryRepository, TempHtsClientsLinkageExtractErrorSummaryRepository>();
            services.AddScoped<ITempHtsClientTestsErrorSummaryRepository, TempHtsClientTestsExtractErrorSummaryRepository>();
            services.AddScoped<ITempHtsClientTracingErrorSummaryRepository, TempHtsClientTracingExtractErrorSummaryRepository>();
            services.AddScoped<ITempHtsPartnerNotificationServicesErrorSummaryRepository, TempHtsPartnerNotificationServicesExtractErrorSummaryRepository>();
            services.AddScoped<ITempHtsPartnerTracingErrorSummaryRepository, TempHtsPartnerTracingExtractErrorSummaryRepository>();
            services.AddScoped<ITempHtsTestKitsErrorSummaryRepository, TempHtsTestKitsExtractErrorSummaryRepository>();
            #endregion

            #endregion

            #region MGS

            #region Extracts
            services.AddScoped<IMetricMigrationExtractRepository, MetricMigrationExtractRepository>();
            #endregion

            #region TempExtracts
            services.AddScoped<ITempMetricMigrationExtractRepository, TempMetricMigrationExtractRepository>();
            #endregion

            #region Validations
            services.AddScoped<ITempMetricMigrationExtractErrorSummaryRepository, TempMetricMigrationExtractErrorSummaryRepository>();
            #endregion

            #endregion

            #region Validators
            services.AddTransient<IMasterPatientIndexValidator, MasterPatientIndexValidator>();
            services.AddTransient<IExtractValidator, ExtractValidator>();
            services.AddTransient<IHtsExtractValidator, HtsExtractValidator>();
            // NEW
            services.AddScoped<IMetricExtractValidator,MetricExtractValidator>();
            #endregion

            #endregion

            #region Cleaners

            services.AddScoped<ICleanCbsExtracts, CleanCbsExtracts>();
            services.AddScoped<IClearDwhExtracts, ClearDwhExtracts>();
            services.AddScoped<IClearHtsExtracts, ClearHtsExtracts>();
            services.AddScoped<ICleanMgsExtracts, CleanMgsExtracts>();
            services.AddScoped<IClearMgsExtracts, ClearMgsExtracts>();
            #endregion
            #region Extractors
            services.AddScoped<IMasterPatientIndexSourceExtractor, MasterPatientIndexSourceExtractor>();

            services.AddScoped<IPatientAdverseEventSourceExtractor, PatientAdverseEventSourceExtractor>();
            services.AddScoped<IPatientArtSourceExtractor, PatientArtSourceExtractor>();
            services.AddScoped<IPatientBaselinesSourceExtractor, PatientBaselinesSourceExtractor>();
            services.AddScoped<IPatientLaboratorySourceExtractor, PatientLaboratorySourceExtractor>();
            services.AddScoped<IPatientPharmacySourceExtractor, PatientPharmacySourceExtractor>();
            services.AddScoped<IPatientSourceExtractor, PatientSourceExtractor>();
            services.AddScoped<IPatientStatusSourceExtractor, PatientStatusSourceExtractor>();
            services.AddScoped<IPatientVisitSourceExtractor, PatientVisitSourceExtractor>();

            services.AddScoped<IHtsClientsSourceExtractor, HtsClientsSourceExtractor>();
            services.AddScoped<IHtsClientTestsSourceExtractor, HtsClientTestsSourceExtractor>();
            services.AddScoped<IHtsClientsLinkageSourceExtractor, HtsClientsLinkageSourceExtractor>();
            services.AddScoped<IHtsTestKitsSourceExtractor, HtsTestKitsSourceExtractor>();
            services.AddScoped<IHtsClientTracingSourceExtractor, HtsClientTracingSourceExtractor>();
            services.AddScoped<IHtsPartnerTracingSourceExtractor, HtsPartnerTracingSourceExtractor>();
            services.AddScoped<IHtsPartnerNotificationServicesSourceExtractor, HtsPartnerNotificationServicesSourceExtractor>();

            services.AddScoped<IMetricMigrationSourceExtractor,MetricMigrationSourceExtractor>();
            #endregion

            #region Loaders
            services.AddScoped<IPatientLoader, PatientLoader>();
            services.AddScoped<IPatientArtLoader, PatientArtLoader>();
            services.AddScoped<IPatientBaselinesLoader, PatientBaselinesLoader>();
            services.AddScoped<IPatientLaboratoryLoader, PatientLaboratoryLoader>();
            services.AddScoped<IPatientPharmacyLoader, PatientPharmacyLoader>();
            services.AddScoped<IPatientStatusLoader, PatientStatusLoader>();
            services.AddScoped<IPatientVisitLoader, PatientVisitLoader>();
            services.AddScoped<IPatientAdverseEventLoader, PatientAdverseEventLoader>();
            services.AddScoped<IMasterPatientIndexLoader, MasterPatientIndexLoader>();
/*services.AddScoped<IHTSClientLoader, HTSClientLoader>();
services.AddScoped<IHTSClientLinkageLoader, HTSClientLinkageLoader>();
services.AddScoped<IHTSClientPartnerLoader, HTSClientPartnerLoader>();*/
            services.AddScoped<IHtsClientsLoader, HtsClientsLoader>();
            services.AddScoped<IHtsClientTestsLoader, HtsClientTestsLoader>();
            services.AddScoped<IHtsClientsLinkageLoader, HtsClientsLinkageLoader>();
            services.AddScoped<IHtsTestKitsLoader,   HtsTestKitsLoader>();
            services.AddScoped<IHtsClientTracingLoader, HtsClientTracingLoader>();
            services.AddScoped<IHtsPartnerTracingLoader, HtsPartnerTracingLoader>();
            services.AddScoped<IHtsPartnerNotificationServicesLoader, HtsPartnerNotificationServicesLoader >();
            services.AddScoped<IMetricMigrationLoader, MetricMigrationLoader>();
            #endregion
            #region Services
            services.AddScoped<ICbsSendService, CbsSendService>();
            services.AddScoped<IMpiSearchService, MpiSearchService>();
            services.AddScoped<IDwhSendService, DwhSendService>();
            services.AddScoped<IHtsSendService, HtsSendService>();
            services.AddScoped<IHtsSendService, HtsSendService>();
            services.AddScoped<IHtsSendService, HtsSendService>();
            services.AddScoped<IEmrMetricsService, EmrMetricsService>();
            // NEW
            services.AddScoped<IMgsExtractReader,MgsExtractReader>();
            services.AddScoped<IMgsSendService, MgsSendService>();
            #endregion


            services.AddMediatR(typeof(LoadFromEmrCommand));


            services.AddTransient<IDwhExtractReader, DwhExtractReader>();
           // services.AddTransient<ICTExtractReader, CTExtractReader>();
            services.AddTransient<IDwhPackager, DwhPackager>();
          //  services.AddTransient<ICTPackager, CTPackager>();
          //  services.AddTransient<ICTSendService, CTSendService>();
            services.AddTransient<ICbsSendService, CbsSendService>();
            services.AddTransient<ICbsPackager, CbsPackager>();
            services.AddTransient<ICbsExtractReader, CbsExtractReader>();
            services.AddTransient<IHtsSendService, HtsSendService>();
            services.AddTransient<IHtsPackager, HtsPackager>();
            services.AddTransient<IEmrMetricReader, EmrMetricReader>();
            services.AddTransient<IHtsExtractReader, HtsExtractReader>();
            services.AddTransient<IExtractStatusService, ExtractStatusService>();
            services.AddTransient<IExtractHistoryRepository, ExtractHistoryRepository>();
             services   .AddTransient<IEmrSystemRepository,EmrSystemRepository>();

            ServiceProvider = services.BuildServiceProvider();

              Mapper.Initialize(cfg =>
                            {
                                cfg.AddDataReaderMapping();
                                cfg.AddProfile<TempExtractProfile>();
                                cfg.AddProfile<TempMasterPatientIndexProfile>();
                                cfg.AddProfile<EmrProfiles>();
                                cfg.AddProfile<TempHtsExtractProfile>();
                                cfg.AddProfile<MasterPatientIndexProfile>();
                                cfg.AddProfile<TempMetricExtractProfile>();
                            }
                        );

              ClearDb();
        }

        public static void ClearDb()
        {
            var context = ServiceProvider.GetService<SettingsContext>();
            context.EnsureSeeded();

            var econtext = ServiceProvider.GetService<ExtractsContext>();
            econtext.EnsureSeeded();
            /*
            econtext.Database.GetDbConnection().Execute($"DELETE FROM {nameof(ExtractsContext.TempPatientExtracts)}");
            econtext.Database.GetDbConnection().Execute($"DELETE FROM {nameof(ExtractsContext.PatientExtracts)}");
            econtext.Database.GetDbConnection().Execute($"DELETE FROM {nameof(ExtractsContext.TempHtsClientsExtracts)}");
            econtext.Database.GetDbConnection().Execute($"DELETE FROM {nameof(ExtractsContext.HtsClientsExtracts)}");
            */
            SeedData(TestData.GenerateEmrSystems(EmrConnectionString));
            SeedData(TestData.GenerateData<AppMetric>());
            SeedData<ExtractsContext>(TestData.GenerateData<EmrMetric>());
            Protocol = context.DatabaseProtocols.AsNoTracking().First(x => x.DatabaseType == DatabaseType.Sqlite);
            Extracts = context.Extracts.AsNoTracking().Where(x => x.DatabaseProtocolId == Protocol.Id).ToList();
            LoadMpi();
            LoadHts();
            LoadCt();
            LoadMgs();
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

        public static void SeedData<T>(params IEnumerable<object>[] entities) where T:DbContext
        {
            var context = ServiceProvider.GetService<T>();
            foreach (IEnumerable<object> t in entities)
            {
                context.AddRange(t);
            }
            context.SaveChanges();
        }

        private void RegisterLicence()
        {
            DapperPlusManager.AddLicense("1755;700-ThePalladiumGroup", "2073303b-0cfc-fbb9-d45f-1723bb282a3c");
            if (!DapperPlusManager.ValidateLicense(out var licenseErrorMessage))
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

        private string GenerateCopyConnection(IConfigurationRoot config, string name)
        {
            var dir = $"{TestContext.CurrentContext.TestDirectory.HasToEndWith(@"/")}";

            var connectionString = config.GetConnectionString(name)
                .Replace("#dir#", dir)
                .ToOsStyle();

            var cn = connectionString.Split("=");
            var newCn = connectionString.Replace(".db", $"{DateTime.Now.Ticks}.db");
            var db = newCn.Split("=");
            File.Copy(cn[1], db[1], true);
            return newCn;
        }

        private void RemoveTestsFilesDbs()
        {
            string[] keyFiles =
                {"dwapi.db", "emr.db"};
            string[] keyDirs = {@"TestArtifacts/Database".ToOsStyle()};

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

        private static void LoadMpi()
        {
            LoadData(ServiceProvider.GetService<IMasterPatientIndexLoader>(), ServiceProvider.GetService<IMasterPatientIndexSourceExtractor>(), nameof(MasterPatientIndex));
        }
        private static void LoadHts()
        {
            LoadData(ServiceProvider.GetService<IHtsClientsLoader>(), ServiceProvider.GetService<IHtsClientsSourceExtractor>(), "HtsClient");

            LoadData(ServiceProvider.GetService<IHtsClientsLinkageLoader>(), ServiceProvider.GetService<IHtsClientsLinkageSourceExtractor>(), nameof(HtsClientLinkage));
            LoadData(ServiceProvider.GetService<IHtsClientTestsLoader>(), ServiceProvider.GetService<IHtsClientTestsSourceExtractor>(), nameof(HtsClientTests));
            LoadData(ServiceProvider.GetService<IHtsClientTracingLoader>(), ServiceProvider.GetService<IHtsClientTracingSourceExtractor>(), nameof(HtsClientTracing));
            LoadData(ServiceProvider.GetService<IHtsPartnerNotificationServicesLoader>(), ServiceProvider.GetService<IHtsPartnerNotificationServicesSourceExtractor>(), nameof(HtsPartnerNotificationServices));
            LoadData(ServiceProvider.GetService<IHtsPartnerTracingLoader>(), ServiceProvider.GetService<IHtsPartnerTracingSourceExtractor>(), nameof(HtsPartnerTracing));
            LoadData(ServiceProvider.GetService<IHtsTestKitsLoader>(), ServiceProvider.GetService<IHtsTestKitsSourceExtractor>(), nameof(HtsTestKits));

        }
        private static void LoadCt()
        {
            LoadData(ServiceProvider.GetService<IPatientLoader>(), ServiceProvider.GetService<IPatientSourceExtractor>(), nameof(PatientExtract));

            LoadData(ServiceProvider.GetService<IPatientAdverseEventLoader>(), ServiceProvider.GetService<IPatientAdverseEventSourceExtractor>(), nameof(PatientAdverseEventExtract));
            LoadData(ServiceProvider.GetService<IPatientArtLoader>(), ServiceProvider.GetService<IPatientArtSourceExtractor>(), nameof(PatientArtExtract));
            LoadData(ServiceProvider.GetService<IPatientBaselinesLoader>(), ServiceProvider.GetService<IPatientBaselinesSourceExtractor>(), "PatientBaselineExtract");
            LoadData(ServiceProvider.GetService<IPatientLaboratoryLoader>(), ServiceProvider.GetService<IPatientLaboratorySourceExtractor>(), "PatientLabExtract");
            LoadData(ServiceProvider.GetService<IPatientPharmacyLoader>(), ServiceProvider.GetService<IPatientPharmacySourceExtractor>(), nameof(PatientPharmacyExtract));
            LoadData(ServiceProvider.GetService<IPatientStatusLoader>(), ServiceProvider.GetService<IPatientStatusSourceExtractor>(), nameof(PatientStatusExtract));
            LoadData(ServiceProvider.GetService<IPatientVisitLoader>(), ServiceProvider.GetService<IPatientVisitSourceExtractor>(), nameof(PatientVisitExtract));
        }

        private static void LoadMgs()
        {
            LoadData(ServiceProvider.GetService<IMetricMigrationLoader>(), ServiceProvider.GetService<IMetricMigrationSourceExtractor>(), nameof(MetricMigrationExtract));
        }

        private static void LoadData<TM,T>(ILoader<TM> loader, ISourceExtractor<T> extractor,string extractName) where TM : class
        {
            var extract = Extracts.First(x => x.Name.IsSameAs(extractName));
            var countA = extractor.Extract(extract, Protocol).Result;
            var countB = loader.Load(extract.Id,countA).Result;
        }
    }
}
