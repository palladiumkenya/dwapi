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
using Dwapi.ExtractsManagement.Core.Cleaner.Mnch;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Extractors.Cbs;
using Dwapi.ExtractsManagement.Core.Extractors.Dwh;
using Dwapi.ExtractsManagement.Core.Extractors.Hts;
using Dwapi.ExtractsManagement.Core.Extractors.Mgs;
using Dwapi.ExtractsManagement.Core.Extractors.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Mnch;
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
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Mnch;
using Dwapi.ExtractsManagement.Core.Loader.Cbs;
using Dwapi.ExtractsManagement.Core.Loader.Dwh;
using Dwapi.ExtractsManagement.Core.Loader.Hts;
using Dwapi.ExtractsManagement.Core.Loader.Mgs;
using Dwapi.ExtractsManagement.Core.Loader.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mnch;
using Dwapi.ExtractsManagement.Core.Profiles;
using Dwapi.ExtractsManagement.Core.Profiles.Cbs;
using Dwapi.ExtractsManagement.Core.Profiles.Dwh;
using Dwapi.ExtractsManagement.Core.Profiles.Hts;
using Dwapi.ExtractsManagement.Core.Profiles.Mgs;
using Dwapi.ExtractsManagement.Core.Profiles.Mnch;
using Dwapi.ExtractsManagement.Core.Services;
using Dwapi.ExtractsManagement.Core.Tests.TestArtifacts;
using Dwapi.ExtractsManagement.Infrastructure;
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
using Dwapi.SharedKernel.Infrastructure.Tests.TestHelpers;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Interfaces.Services.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Services.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Services.Hts;
using Dwapi.UploadManagement.Core.Profiles;
using Dwapi.UploadManagement.Core.Services.Cbs;
using Dwapi.UploadManagement.Core.Services.Dwh;
using Dwapi.UploadManagement.Core.Services.Hts;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Core.Tests
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
            ConnectionString = GenerateCopyConnection(config, "dwapiConnection");
            DiffConnectionString = GenerateCopyConnection(config, "dwapiDiffConnection");
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

            #region Extracts.Infrastructure

            services.AddDbContext<ExtractsContext>(x => x.UseSqlite(connection));

            #region Readers

            services.AddTransient<IMasterPatientIndexReader, MasterPatientIndexReader>();
            services.AddTransient<IDwhExtractSourceReader, DwhExtractSourceReader>();
            services.AddTransient<IHTSExtractSourceReader, HTSExtractSourceReader>();
            services.AddTransient<IPsmartSourceReader, PsmartSourceReader>();
            services.AddScoped<IMgsExtractSourceReader,MgsExtractSourceReader>();
            // NEW
            services.AddScoped<IMnchExtractSourceReader, MnchExtractSourceReader>();
            #endregion

            services.AddTransient<IEmrMetricRepository, EmrMetricRepository>();
            services.AddTransient<IExtractHistoryRepository, ExtractHistoryRepository>();
            services.AddTransient<IValidatorRepository, ValidatorRepository>();
            services.AddTransient<IPsmartStageRepository, PsmartStageRepository>();
            services.AddTransient<IDiffLogRepository, DiffLogRepository>();

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
            services.AddScoped<IMetricExtractValidator,MetricExtractValidator>();
            // NEW
            services.AddScoped<IMnchExtractValidator, MnchExtractValidator>();
            #endregion

            #endregion

            #region Cleaners

            services.AddScoped<IClearCbsExtracts, ClearCbsExtracts>();
            services.AddScoped<IClearDwhExtracts, ClearDwhExtracts>();
            services.AddScoped<IClearHtsExtracts, ClearHtsExtracts>();
            services.AddScoped<IClearMgsExtracts, ClearMgsExtracts>();
            //NEW
            services.AddScoped<IClearMnchExtracts, ClearMnchExtracts>();

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


            services.AddScoped<IAllergiesChronicIllnessSourceExtractor, AllergiesChronicIllnessSourceExtractor>();
            services.AddScoped<IContactListingSourceExtractor, ContactListingSourceExtractor>();
            services.AddScoped<IDepressionScreeningSourceExtractor, DepressionScreeningSourceExtractor>();
            services.AddScoped<IDrugAlcoholScreeningSourceExtractor, DrugAlcoholScreeningSourceExtractor>();
            services.AddScoped<IEnhancedAdherenceCounsellingSourceExtractor, EnhancedAdherenceCounsellingSourceExtractor>();
            services.AddScoped<IGbvScreeningSourceExtractor, GbvScreeningSourceExtractor>();
            services.AddScoped<IIptSourceExtractor, IptSourceExtractor>();
            services.AddScoped<IOtzSourceExtractor, OtzSourceExtractor>();
            services.AddScoped<IOvcSourceExtractor, OvcSourceExtractor>();

            //NEW
            services.AddScoped<IPatientMnchSourceExtractor, PatientMnchSourceExtractor>();
            services.AddScoped<IMnchEnrolmentSourceExtractor, MnchEnrolmentSourceExtractor>();
            services.AddScoped<IMnchArtSourceExtractor, MnchArtSourceExtractor>();
            services.AddScoped<IAncVisitSourceExtractor, AncVisitSourceExtractor>();
            services.AddScoped<IMatVisitSourceExtractor, MatVisitSourceExtractor>();
            services.AddScoped<IPncVisitSourceExtractor, PncVisitSourceExtractor>();
            services.AddScoped<IMotherBabyPairSourceExtractor, MotherBabyPairSourceExtractor>();
            services.AddScoped<ICwcEnrolmentSourceExtractor, CwcEnrolmentSourceExtractor>();
            services.AddScoped<ICwcVisitSourceExtractor, CwcVisitSourceExtractor>();
            services.AddScoped<IHeiSourceExtractor, HeiSourceExtractor>();
            services.AddScoped<IMnchLabSourceExtractor, MnchLabSourceExtractor>();

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

            services.AddScoped<IAllergiesChronicIllnessLoader, AllergiesChronicIllnessLoader>();
            services.AddScoped<IContactListingLoader, ContactListingLoader>();
            services.AddScoped<IDepressionScreeningLoader, DepressionScreeningLoader>();
            services.AddScoped<IDrugAlcoholScreeningLoader, DrugAlcoholScreeningLoader>();
            services.AddScoped<IEnhancedAdherenceCounsellingLoader, EnhancedAdherenceCounsellingLoader>();
            services.AddScoped<IGbvScreeningLoader, GbvScreeningLoader>();
            services.AddScoped<IIptLoader, IptLoader>();
            services.AddScoped<IOtzLoader, OtzLoader>();
            services.AddScoped<IOvcLoader, OvcLoader>();

            //NEW
            services.AddScoped<IPatientMnchLoader, PatientMnchLoader>();
            services.AddScoped<IMnchEnrolmentLoader, MnchEnrolmentLoader>();
            services.AddScoped<IMnchArtLoader, MnchArtLoader>();
            services.AddScoped<IAncVisitLoader, AncVisitLoader>();
            services.AddScoped<IMatVisitLoader, MatVisitLoader>();
            services.AddScoped<IPncVisitLoader, PncVisitLoader>();
            services.AddScoped<IMotherBabyPairLoader, MotherBabyPairLoader>();
            services.AddScoped<ICwcEnrolmentLoader, CwcEnrolmentLoader>();
            services.AddScoped<ICwcVisitLoader, CwcVisitLoader>();
            services.AddScoped<IHeiLoader, HeiLoader>();
            services.AddScoped<IMnchLabLoader, MnchLabLoader>();
            #endregion
            #region Services
            services.AddScoped<ICbsSendService, CbsSendService>();
            services.AddScoped<IMpiSearchService, MpiSearchService>();
            services.AddScoped<IDwhSendService, DwhSendService>();
            services.AddScoped<ICTSendService, CTSendService>();
            services.AddScoped<IHtsSendService, HtsSendService>();
            services.AddScoped<IEmrMetricsService, EmrMetricsService>();
           // services.AddScoped<IMgsSendService, MgsSendService>();

            #endregion


            services.AddMediatR(typeof(LoadFromEmrCommand),typeof(TestDiffEventHandler));

            AllServices = services;
            ServiceProvider = services.BuildServiceProvider();

              Mapper.Initialize(cfg =>
                            {
                                cfg.AddDataReaderMapping();
                                cfg.AddProfile<DiffCtExtractProfile>();
                                cfg.AddProfile<TempMasterPatientIndexProfile>();
                                cfg.AddProfile<EmrProfiles>();
                                cfg.AddProfile<TempHtsExtractProfile>();
                                cfg.AddProfile<MasterPatientIndexProfile>();
                                cfg.AddProfile<TempMetricExtractProfile>();
                                cfg.AddProfile<DiffMnchExtractProfile>();
                            }
                        );

              var context = ServiceProvider.GetService<SettingsContext>();
              context.EnsureSeeded();

        }

        public static void ClearDb()
        {
            var econtext = ServiceProvider.GetService<ExtractsContext>();
            econtext.EnsureSeeded();
            econtext.Database.GetDbConnection().Execute($"DELETE FROM {nameof(ExtractsContext.TempPatientExtracts)}");
            econtext.Database.GetDbConnection().Execute($"DELETE FROM {nameof(ExtractsContext.PatientExtracts)}");
            econtext.Database.GetDbConnection().Execute($"DELETE FROM {nameof(ExtractsContext.TempHtsClientsExtracts)}");
            econtext.Database.GetDbConnection().Execute($"DELETE FROM {nameof(ExtractsContext.HtsClientsExtracts)}");
            econtext.Database.GetDbConnection().Execute($"DELETE FROM {nameof(ExtractsContext.TempPatientMnchExtracts)}");
            econtext.Database.GetDbConnection().Execute($"DELETE FROM {nameof(ExtractsContext.PatientMnchExtracts)}");
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

            var context = ServiceProvider.GetService<SettingsContext>();
            context.EnsureSeeded();

            var econtext = ServiceProvider.GetService<ExtractsContext>();
            econtext.EnsureSeeded();
            econtext.Database.GetDbConnection().Execute($"DELETE FROM {nameof(ExtractsContext.TempPatientExtracts)}");
            econtext.Database.GetDbConnection().Execute($"DELETE FROM {nameof(ExtractsContext.PatientExtracts)}");
            econtext.Database.GetDbConnection().Execute($"DELETE FROM {nameof(ExtractsContext.TempHtsClientsExtracts)}");
            econtext.Database.GetDbConnection().Execute($"DELETE FROM {nameof(ExtractsContext.HtsClientsExtracts)}");
            econtext.Database.GetDbConnection().Execute($"DELETE FROM {nameof(ExtractsContext.TempPatientMnchExtracts)}");
            econtext.Database.GetDbConnection().Execute($"DELETE FROM {nameof(ExtractsContext.PatientMnchExtracts)}");
        }

        public static void SeedData(params IEnumerable<object>[] entities)
        {
            var context = ServiceProvider.GetService<SettingsContext>();

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
                {"dwapi.db", "dwapi-diff.db", "emr.db", "emr-diff.db"};
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
        public static void LoadHts()
        {
            LoadData(ServiceProvider.GetService<IHtsClientsLoader>(), ServiceProvider.GetService<IHtsClientsSourceExtractor>(), "HtsClient");

            LoadData(ServiceProvider.GetService<IHtsClientsLinkageLoader>(), ServiceProvider.GetService<IHtsClientsLinkageSourceExtractor>(), nameof(HtsClientLinkage));
            LoadData(ServiceProvider.GetService<IHtsClientTestsLoader>(), ServiceProvider.GetService<IHtsClientTestsSourceExtractor>(), nameof(HtsClientTests));
            LoadData(ServiceProvider.GetService<IHtsClientTracingLoader>(), ServiceProvider.GetService<IHtsClientTracingSourceExtractor>(), nameof(HtsClientTracing));
            LoadData(ServiceProvider.GetService<IHtsPartnerNotificationServicesLoader>(), ServiceProvider.GetService<IHtsPartnerNotificationServicesSourceExtractor>(), nameof(HtsPartnerNotificationServices));
            LoadData(ServiceProvider.GetService<IHtsPartnerTracingLoader>(), ServiceProvider.GetService<IHtsPartnerTracingSourceExtractor>(), nameof(HtsPartnerTracing));
            LoadData(ServiceProvider.GetService<IHtsTestKitsLoader>(), ServiceProvider.GetService<IHtsTestKitsSourceExtractor>(), nameof(HtsTestKits));

        }
        public static void LoadCt()
        {
            LoadData(ServiceProvider.GetService<IPatientLoader>(), ServiceProvider.GetService<IPatientSourceExtractor>(), nameof(PatientExtract));

            LoadData(ServiceProvider.GetService<IPatientAdverseEventLoader>(), ServiceProvider.GetService<IPatientAdverseEventSourceExtractor>(), nameof(PatientAdverseEventExtract));
            LoadData(ServiceProvider.GetService<IPatientArtLoader>(), ServiceProvider.GetService<IPatientArtSourceExtractor>(), nameof(PatientArtExtract));
            LoadData(ServiceProvider.GetService<IPatientBaselinesLoader>(), ServiceProvider.GetService<IPatientBaselinesSourceExtractor>(), "PatientBaselineExtract");
            LoadData(ServiceProvider.GetService<IPatientLaboratoryLoader>(), ServiceProvider.GetService<IPatientLaboratorySourceExtractor>(), "PatientLabExtract");
            LoadData(ServiceProvider.GetService<IPatientPharmacyLoader>(), ServiceProvider.GetService<IPatientPharmacySourceExtractor>(), nameof(PatientPharmacyExtract));
            LoadData(ServiceProvider.GetService<IPatientStatusLoader>(), ServiceProvider.GetService<IPatientStatusSourceExtractor>(), nameof(PatientStatusExtract));
            LoadData(ServiceProvider.GetService<IPatientVisitLoader>(), ServiceProvider.GetService<IPatientVisitSourceExtractor>(), nameof(PatientVisitExtract));

            LoadData(ServiceProvider.GetService<IAllergiesChronicIllnessLoader>(), ServiceProvider.GetService<IAllergiesChronicIllnessSourceExtractor>(), nameof(AllergiesChronicIllnessExtract));
            LoadData(ServiceProvider.GetService<IIptLoader>(), ServiceProvider.GetService<IIptSourceExtractor>(), nameof(IptExtract));
            LoadData(ServiceProvider.GetService<IDepressionScreeningLoader>(), ServiceProvider.GetService<IDepressionScreeningSourceExtractor>(), nameof(DepressionScreeningExtract));
            LoadData(ServiceProvider.GetService<IContactListingLoader>(), ServiceProvider.GetService<IContactListingSourceExtractor>(), nameof(ContactListingExtract));
            LoadData(ServiceProvider.GetService<IGbvScreeningLoader>(), ServiceProvider.GetService<IGbvScreeningSourceExtractor>(), nameof(GbvScreeningExtract));
            LoadData(ServiceProvider.GetService<IEnhancedAdherenceCounsellingLoader>(), ServiceProvider.GetService<IEnhancedAdherenceCounsellingSourceExtractor>(), nameof(EnhancedAdherenceCounsellingExtract));
            LoadData(ServiceProvider.GetService<IDrugAlcoholScreeningLoader>(), ServiceProvider.GetService<IDrugAlcoholScreeningSourceExtractor>(), nameof(DrugAlcoholScreeningExtract));
            LoadData(ServiceProvider.GetService<IOvcLoader>(), ServiceProvider.GetService<IOvcSourceExtractor>(), nameof(OvcExtract));
            LoadData(ServiceProvider.GetService<IOtzLoader>(), ServiceProvider.GetService<IOtzSourceExtractor>(), nameof(OtzExtract));

        }

        public static void LoadMgs()
        {
            LoadData(ServiceProvider.GetService<IMetricMigrationLoader>(), ServiceProvider.GetService<IMetricMigrationSourceExtractor>(), nameof(MetricMigrationExtract));
        }

          public static void LoadMnch()
        {
            LoadData(ServiceProvider.GetService<IPatientMnchLoader>(), ServiceProvider.GetService<IPatientMnchSourceExtractor>(), nameof(PatientMnchExtract));

            LoadData(ServiceProvider.GetService<IMnchEnrolmentLoader>(), ServiceProvider.GetService<IMnchEnrolmentSourceExtractor>(), nameof(MnchEnrolmentExtract));
            LoadData(ServiceProvider.GetService<IMnchArtLoader>(), ServiceProvider.GetService<IMnchArtSourceExtractor>(), nameof(MnchArtExtract));
            LoadData(ServiceProvider.GetService<IAncVisitLoader>(), ServiceProvider.GetService<IAncVisitSourceExtractor>(), nameof(AncVisitExtract));
            LoadData(ServiceProvider.GetService<IMatVisitLoader>(), ServiceProvider.GetService<IMatVisitSourceExtractor>(), nameof(MatVisitExtract));
            LoadData(ServiceProvider.GetService<IPncVisitLoader>(), ServiceProvider.GetService<IPncVisitSourceExtractor>(), nameof(PncVisitExtract));
            LoadData(ServiceProvider.GetService<IMotherBabyPairLoader>(), ServiceProvider.GetService<IMotherBabyPairSourceExtractor>(), nameof(MotherBabyPairExtract));
            LoadData(ServiceProvider.GetService<ICwcEnrolmentLoader>(), ServiceProvider.GetService<ICwcEnrolmentSourceExtractor>(), nameof(CwcEnrolmentExtract));
            LoadData(ServiceProvider.GetService<ICwcVisitLoader>(), ServiceProvider.GetService<ICwcVisitSourceExtractor>(), nameof(CwcVisitExtract));
            LoadData(ServiceProvider.GetService<IHeiLoader>(), ServiceProvider.GetService<IHeiSourceExtractor>(), nameof(HeiExtract));
            LoadData(ServiceProvider.GetService<IMnchLabLoader>(), ServiceProvider.GetService<IMnchLabSourceExtractor>(), nameof(MnchLabExtract));
        }

        private static void LoadData<TM, T>(ILoader<TM> loader, ISourceExtractor<T> extractor, string extractName) where TM : class
        {
            var extract = Extracts.First(x => x.Name.IsSameAs(extractName));
            var countA = extractor.Extract(extract, Protocol).Result;
            var countB = loader.Load(extract.Id, countA, false).Result;
        }
    }
}
