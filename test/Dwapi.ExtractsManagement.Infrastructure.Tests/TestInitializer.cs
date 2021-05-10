using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Diff;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Mnch;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Hts;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Mgs;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Mnch;
using Dwapi.ExtractsManagement.Infrastructure.Reader.SmartCard;
using Dwapi.ExtractsManagement.Infrastructure.Repository;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Diff;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Extracts;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.TempExtracts;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Validations;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Hts.Extracts;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Hts.TempExtracts;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Hts.Validations;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Mgs.Extracts;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Mgs.TempExtracts;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Mgs.Validations;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Mnch.Extracts;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Mnch.TempExtracts;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Mnch.Validations;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Hts;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Mgs;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Mnch;
using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SettingsManagement.Infrastructure;
using Dwapi.SettingsManagement.Infrastructure.Repository;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Infrastructure;
using Dwapi.SharedKernel.Infrastructure.Tests.TestHelpers;
using Dwapi.SharedKernel.Utility;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
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
        public static string EmrDiffConnectionString;
        public static string ConnectionString;
        public static string DiffConnectionString;
        public static DatabaseProtocol Protocol;
        public static List<Extract> Extracts;
        public static ServiceCollection AllServices;

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
            EmrDiffConnectionString = GenerateConnection(config, "emrDiffConnection", false);
            ConnectionString = GenerateConnection(config, "dwapiConnection", true);
            DiffConnectionString = GenerateConnection(config, "dwapiDiffConnection", true);
            MsSqlConnectionString = config.GetConnectionString("mssqlConnection");
            MySqlConnectionString = config.GetConnectionString("mysqlConnection");

            var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var services = new ServiceCollection();

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

            services.AddDbContext<ExtractsContext>(x => x.UseSqlite(connection));

            #region Readers

            services.AddTransient<IMasterPatientIndexReader, MasterPatientIndexReader>();
            services.AddTransient<IDwhExtractSourceReader, DwhExtractSourceReader>();
            services.AddTransient<IHTSExtractSourceReader, HTSExtractSourceReader>();
            services.AddTransient<IPsmartSourceReader, PsmartSourceReader>();
            services.AddScoped<IMgsExtractSourceReader, MgsExtractSourceReader>();
            services.AddScoped<IMnchExtractSourceReader, MnchExtractSourceReader>();

            #endregion

            #region Sys

            services.AddTransient<IEmrMetricRepository, EmrMetricRepository>();
            services.AddTransient<IExtractHistoryRepository, ExtractHistoryRepository>();
            services.AddTransient<IValidatorRepository, ValidatorRepository>();
            services.AddTransient<IPsmartStageRepository, PsmartStageRepository>();
            services.AddTransient<IDiffLogRepository, DiffLogRepository>();

            #endregion

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

            services.AddTransient<IAllergiesChronicIllnessExtractRepository, AllergiesChronicIllnessExtractRepository>();
            services.AddTransient<IContactListingExtractRepository, ContactListingExtractRepository>();
            services.AddTransient<IDepressionScreeningExtractRepository, DepressionScreeningExtractRepository>();
            services.AddTransient<IDrugAlcoholScreeningExtractRepository, DrugAlcoholScreeningExtractRepository>();
            services.AddTransient<IEnhancedAdherenceCounsellingExtractRepository, EnhancedAdherenceCounsellingExtractRepository>();
            services.AddTransient<IGbvScreeningExtractRepository, GbvScreeningExtractRepository>();
            services.AddTransient<IIptExtractRepository, IptExtractRepository>();
            services.AddTransient<IOtzExtractRepository, OtzExtractRepository>();
            services.AddTransient<IOvcExtractRepository, OvcExtractRepository>();

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

            services.AddTransient<ITempAllergiesChronicIllnessExtractRepository, TempAllergiesChronicIllnessExtractRepository>();
            services.AddTransient<ITempContactListingExtractRepository, TempContactListingExtractRepository>();
            services.AddTransient<ITempDepressionScreeningExtractRepository, TempDepressionScreeningExtractRepository>();
            services.AddTransient<ITempDrugAlcoholScreeningExtractRepository, TempDrugAlcoholScreeningExtractRepository>();
            services.AddTransient<ITempEnhancedAdherenceCounsellingExtractRepository, TempEnhancedAdherenceCounsellingExtractRepository>();
            services.AddTransient<ITempGbvScreeningExtractRepository, TempGbvScreeningExtractRepository>();
            services.AddTransient<ITempIptExtractRepository, TempIptExtractRepository>();
            services.AddTransient<ITempOtzExtractRepository, TempOtzExtractRepository>();
            services.AddTransient<ITempOvcExtractRepository, TempOvcExtractRepository>();

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

            services.AddTransient<ITempAllergiesChronicIllnessExtractErrorSummaryRepository, TempAllergiesChronicIllnessExtractErrorSummaryRepository>();
            services.AddTransient<ITempContactListingExtractErrorSummaryRepository, TempContactListingExtractErrorSummaryRepository>();
            services.AddTransient<ITempDepressionScreeningExtractErrorSummaryRepository, TempDepressionScreeningExtractErrorSummaryRepository>();
            services.AddTransient<ITempDrugAlcoholScreeningExtractErrorSummaryRepository, TempDrugAlcoholScreeningExtractErrorSummaryRepository>();
            services.AddTransient<ITempEnhancedAdherenceCounsellingExtractErrorSummaryRepository, TempEnhancedAdherenceCounsellingExtractErrorSummaryRepository>();
            services.AddTransient<ITempGbvScreeningExtractErrorSummaryRepository, TempGbvScreeningExtractErrorSummaryRepository>();
            services.AddTransient<ITempIptExtractErrorSummaryRepository, TempIptExtractErrorSummaryRepository>();
            services.AddTransient<ITempOtzExtractErrorSummaryRepository, TempOtzExtractErrorSummaryRepository>();
            services.AddTransient<ITempOvcExtractErrorSummaryRepository, TempOvcExtractErrorSummaryRepository>();

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
            services
                .AddScoped<IHtsPartnerNotificationServicesExtractRepository,
                    HtsPartnerNotificationServicesExtractRepository>();
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
            services
                .AddScoped<ITempHtsPartnerNotificationServicesExtractRepository,
                    TempHtsPartnerNotificationServicesExtractRepository>();
            services.AddScoped<ITempHtsPartnerTracingExtractRepository, TempHtsPartnerTracingExtractRepository>();
            services.AddScoped<ITempHtsTestKitsExtractRepository, TempHtsTestKitsExtractRepository>();

            #endregion

            #region Validations

            services
                .AddScoped<ITempHTSClientExtractErrorSummaryRepository, TempHTSClientExtractErrorSummaryRepository>();
            services
                .AddScoped<ITempHTSClientLinkageExtractErrorSummaryRepository,
                    TempHTSClientLinkageExtractErrorSummaryRepository>();
            services
                .AddScoped<ITempHTSClientPartnerExtractErrorSummaryRepository,
                    TempHTSClientPartnerExtractErrorSummaryRepository>();
            services
                .AddScoped<ITempHtsClientsExtractErrorSummaryRepository, TempHtsClientsExtractErrorSummaryRepository>();
            services
                .AddScoped<ITempHtsClientLinkageErrorSummaryRepository,
                    TempHtsClientsLinkageExtractErrorSummaryRepository>();
            services
                .AddScoped<ITempHtsClientTestsErrorSummaryRepository, TempHtsClientTestsExtractErrorSummaryRepository
                >();
            services
                .AddScoped<ITempHtsClientTracingErrorSummaryRepository,
                    TempHtsClientTracingExtractErrorSummaryRepository>();
            services
                .AddScoped<ITempHtsPartnerNotificationServicesErrorSummaryRepository,
                    TempHtsPartnerNotificationServicesExtractErrorSummaryRepository>();
            services
                .AddScoped<ITempHtsPartnerTracingErrorSummaryRepository,
                    TempHtsPartnerTracingExtractErrorSummaryRepository>();
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

            services
                .AddScoped<ITempMetricMigrationExtractErrorSummaryRepository,
                    TempMetricMigrationExtractErrorSummaryRepository>();

            #endregion

            #endregion

            #region MNCH

            #region Extracts

            services.AddTransient<IPatientMnchExtractRepository, PatientMnchExtractRepository>();
            services.AddTransient<IMnchEnrolmentExtractRepository, MnchEnrolmentExtractRepository>();
            services.AddTransient<IMnchArtExtractRepository, MnchArtExtractRepository>();
            services.AddTransient<IAncVisitExtractRepository, AncVisitExtractRepository>();
            services.AddTransient<IMatVisitExtractRepository, MatVisitExtractRepository>();
            services.AddTransient<IPncVisitExtractRepository, PncVisitExtractRepository>();
            services.AddTransient<IMotherBabyPairExtractRepository, MotherBabyPairExtractRepository>();
            services.AddTransient<ICwcEnrolmentExtractRepository, CwcEnrolmentExtractRepository>();
            services.AddTransient<ICwcVisitExtractRepository, CwcVisitExtractRepository>();
            services.AddTransient<IHeiExtractRepository, HeiExtractRepository>();
            services.AddTransient<IMnchLabExtractRepository, MnchLabExtractRepository>();

            #endregion

            #region TempExtracts

            services.AddTransient<ITempPatientMnchExtractRepository, TempPatientMnchExtractRepository>();
            services.AddTransient<ITempMnchEnrolmentExtractRepository, TempMnchEnrolmentExtractRepository>();
            services.AddTransient<ITempMnchArtExtractRepository, TempMnchArtExtractRepository>();
            services.AddTransient<ITempAncVisitExtractRepository, TempAncVisitExtractRepository>();
            services.AddTransient<ITempMatVisitExtractRepository, TempMatVisitExtractRepository>();
            services.AddTransient<ITempPncVisitExtractRepository, TempPncVisitExtractRepository>();
            services.AddTransient<ITempMotherBabyPairExtractRepository, TempMotherBabyPairExtractRepository>();
            services.AddTransient<ITempCwcEnrolmentExtractRepository, TempCwcEnrolmentExtractRepository>();
            services.AddTransient<ITempCwcVisitExtractRepository, TempCwcVisitExtractRepository>();
            services.AddTransient<ITempHeiExtractRepository, TempHeiExtractRepository>();
            services.AddTransient<ITempMnchLabExtractRepository, TempMnchLabExtractRepository>();

            #endregion

            #region Validations

            services.AddTransient<ITempPatientMnchExtractErrorSummaryRepository, TempPatientMnchExtractErrorSummaryRepository>();
            services.AddTransient<ITempMnchEnrolmentExtractErrorSummaryRepository, TempMnchEnrolmentExtractErrorSummaryRepository>();
            services.AddTransient<ITempMnchArtExtractErrorSummaryRepository, TempMnchArtExtractErrorSummaryRepository>();
            services.AddTransient<ITempAncVisitExtractErrorSummaryRepository, TempAncVisitExtractErrorSummaryRepository>();
            services.AddTransient<ITempMatVisitExtractErrorSummaryRepository, TempMatVisitExtractErrorSummaryRepository>();
            services.AddTransient<ITempPncVisitExtractErrorSummaryRepository, TempPncVisitExtractErrorSummaryRepository>();
            services.AddTransient<ITempMotherBabyPairExtractErrorSummaryRepository, TempMotherBabyPairExtractErrorSummaryRepository>();
            services.AddTransient<ITempCwcEnrolmentExtractErrorSummaryRepository, TempCwcEnrolmentExtractErrorSummaryRepository>();
            services.AddTransient<ITempCwcVisitExtractErrorSummaryRepository, TempCwcVisitExtractErrorSummaryRepository>();
            services.AddTransient<ITempHeiExtractErrorSummaryRepository, TempHeiExtractErrorSummaryRepository>();
            services.AddTransient<ITempMnchLabExtractErrorSummaryRepository, TempMnchLabExtractErrorSummaryRepository>();


            #endregion

            #endregion
            #region Validators

            services.AddTransient<IMasterPatientIndexValidator, MasterPatientIndexValidator>();
            services.AddTransient<IExtractValidator, ExtractValidator>();
            services.AddTransient<IHtsExtractValidator, HtsExtractValidator>();
            services.AddScoped<IMetricExtractValidator, MetricExtractValidator>();
            services.AddScoped<IMnchExtractValidator, MnchExtractValidator>();
            #endregion

            AllServices = services;
            ServiceProvider = services.BuildServiceProvider();
        }

        public static void ClearDb()
        {
            NewDb();
            ServiceProvider.GetService<ExtractsContext>().EnsureSeeded();
        }

        public static void ClearDiffDb()
        {
            AllServices.RemoveService(typeof(SettingsContext));
            AllServices.RemoveService(typeof(ExtractsContext));
            AllServices.RemoveService(typeof(DbContextOptions<SettingsContext>));
            AllServices.RemoveService(typeof(DbContextOptions<ExtractsContext>));

            var diffConnection = new SqliteConnection(DiffConnectionString);
            diffConnection.Open();

            AllServices.AddDbContext<SettingsContext>(x => x.UseSqlite(diffConnection));
            AllServices.AddDbContext<ExtractsContext>(x => x.UseSqlite(diffConnection));

            ServiceProvider = AllServices.BuildServiceProvider();

            NewDb();
            ServiceProvider.GetService<ExtractsContext>().EnsureSeeded();
        }

        public static void NewDb()
        {
            CreateDb(ServiceProvider.GetService<SettingsContext>(), true);
            CreateDb(ServiceProvider.GetService<ExtractsContext>());
        }

        public static void SeedData(params IEnumerable<object>[] entities)
        {
            var context = ServiceProvider.GetService<Dwapi.SettingsManagement.Infrastructure.SettingsContext>();
            if (entities.Any(x => x.First().GetType() == typeof(EmrSystem)))
            {
                context.EmrSystems.RemoveRange(context.EmrSystems);
                context.SaveChanges();
            }

            foreach (IEnumerable<object> t in entities)
            {
                context.AddRange(t);
            }

            context.SaveChanges();

            Protocol = context.DatabaseProtocols.AsNoTracking().First(x => x.DatabaseType == DatabaseType.Sqlite);
            Extracts = context.Extracts.AsNoTracking().Where(x => x.DatabaseProtocolId == Protocol.Id).ToList();
        }

        public static void SeedData<T>(params IEnumerable<object>[] entities) where T : DbContext
        {
            var context = ServiceProvider.GetService<T>();
            foreach (IEnumerable<object> t in entities)
            {
                context.AddRange(t);
            }

            context.SaveChanges();
        }

        private static void CreateDb(BaseContext context, bool seed = false)
        {
            try
            {
                var databaseCreator = (RelationalDatabaseCreator) context.Database.GetService<IDatabaseCreator>();
                databaseCreator.CreateTables();
                if (context is ExtractsContext)
                {
                    context.Database.ExecuteSqlCommand(@"
                        CREATE VIEW vMasterPatientIndicesJaro
                        AS
                        SELECT 
	                        *,0 AS [JaroWinklerScore]
                        FROM   
	                        [MasterPatientIndices]
                        ");
                }
            }
            catch (Exception e)
            {
                Log.Debug("Tables already Exist");
            }

            if (seed)
                context.EnsureSeeded();
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
                {"dwapi.db", "dwapidiff.db", "emr.db", "emrdiff.db"};
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
    }
}
