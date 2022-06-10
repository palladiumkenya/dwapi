using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using AutoMapper.Data;
using Dapper;
using Dwapi.ExtractsManagement.Core.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Cleaner.Dwh;
using Dwapi.ExtractsManagement.Core.Cleaner.Hts;
using Dwapi.ExtractsManagement.Core.Cleaner.Mgs;
using Dwapi.ExtractsManagement.Core.Cleaner.Mnch;
using Dwapi.ExtractsManagement.Core.Cleaner.Mts;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Extractors.Cbs;
using Dwapi.ExtractsManagement.Core.Extractors.Dwh;
using Dwapi.ExtractsManagement.Core.Extractors.Hts;
using Dwapi.ExtractsManagement.Core.Extractors.Mgs;
using Dwapi.ExtractsManagement.Core.Extractors.Mnch;
using Dwapi.ExtractsManagement.Core.Extractors.Mts;
using Dwapi.ExtractsManagement.Core.Interfaces;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Mts;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Mts;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Mts;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Mts;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Diff;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mts;
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
using Dwapi.ExtractsManagement.Core.Loader.Mts;
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
using Dwapi.ExtractsManagement.Infrastructure.Reader.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Hts;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Mgs;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Mnch;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Mts;
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
using Dwapi.ExtractsManagement.Infrastructure.Repository.Mts.Extracts;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Mts.TempExtracts;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Hts;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Mgs;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Mnch;
using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SettingsManagement.Core.Services;
using Dwapi.SettingsManagement.Infrastructure;
using Dwapi.SettingsManagement.Infrastructure.Repository;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Infrastructure.Tests.TestHelpers;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Hts;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Mgs;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Mnch;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Hts;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Mgs;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Mnch;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Mts;
using Dwapi.UploadManagement.Core.Interfaces.Services;
using Dwapi.UploadManagement.Core.Interfaces.Services.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Services.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Services.Hts;
using Dwapi.UploadManagement.Core.Interfaces.Services.Mgs;
using Dwapi.UploadManagement.Core.Interfaces.Services.Mnch;
using Dwapi.UploadManagement.Core.Interfaces.Services.Mts;
using Dwapi.UploadManagement.Core.Packager.Cbs;
using Dwapi.UploadManagement.Core.Packager.Dwh;
using Dwapi.UploadManagement.Core.Packager.Hts;
using Dwapi.UploadManagement.Core.Packager.Mgs;
using Dwapi.UploadManagement.Core.Packager.Mnch;
using Dwapi.UploadManagement.Core.Packager.Mts;
using Dwapi.UploadManagement.Core.Profiles;
using Dwapi.UploadManagement.Core.Services.Cbs;
using Dwapi.UploadManagement.Core.Services.Dwh;
using Dwapi.UploadManagement.Core.Services.Hts;
using Dwapi.UploadManagement.Core.Services.Mgs;
using Dwapi.UploadManagement.Core.Services.Mnch;
using Dwapi.UploadManagement.Core.Services.Mts;
using Dwapi.UploadManagement.Core.Services.Psmart;
using Dwapi.UploadManagement.Core.Tests.TestArtifacts;
using Dwapi.UploadManagement.Infrastructure.Data;
using Dwapi.UploadManagement.Infrastructure.Reader;
using Dwapi.UploadManagement.Infrastructure.Reader.Cbs;
using Dwapi.UploadManagement.Infrastructure.Reader.Dwh;
using Dwapi.UploadManagement.Infrastructure.Reader.Hts;
using Dwapi.UploadManagement.Infrastructure.Reader.Mgs;
using Dwapi.UploadManagement.Infrastructure.Reader.Mnch;
using Dwapi.UploadManagement.Infrastructure.Reader.Mts;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;
using Z.Dapper.Plus;
using SettingsContext = Dwapi.SettingsManagement.Infrastructure.SettingsContext;

namespace Dwapi.UploadManagement.Core.Tests
{
    [SetUpFixture]
    public class TestInitializer
    {
        public bool GoLive = true;
        public static IServiceProvider ServiceProvider;
        public static string MsSqlConnectionString;
        public static string MySqlConnectionString;
        public static string EmrConnectionString;
        public static string EmrDiffConnectionString;
        public static string ConnectionString;
        public static string DiffConnectionString;
        public static DatabaseProtocol Protocol;
        public static List<Extract> Extracts;
        public static EmrDto IqEmrDto;
        public static EmrDto IqEmrMultiDto;
        public static EmrDto KeEmrCommDto;
        public static EmrDto KeEmrDto;
        public static EmrDto KeEmrMultiDto;
        public static ServiceCollection AllServices;

        [OneTimeSetUp]
        public void Setup()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            RegisterLicence();

            var services = new ServiceCollection();

            IqEmrDto = new EmrDto(new Guid("a62216ee-0e85-11e8-ba89-0ed5f89f718b"), "IQCare",
                EmrSetup.SingleFacility);
            IqEmrMultiDto = new EmrDto(new Guid("a62216ee-0e85-11e8-ba89-0ed5f89f718b"), "IQCare",
                EmrSetup.MultiFacility);
            KeEmrDto = new EmrDto(new Guid("a6221856-0e85-11e8-ba89-0ed5f89f718b"), "KenyaEMR",
                EmrSetup.SingleFacility);
            KeEmrMultiDto = new EmrDto(new Guid("a6221856-0e85-11e8-ba89-0ed5f89f718b"), "KenyaEMR",
                EmrSetup.MultiFacility);
            KeEmrCommDto = new EmrDto(new Guid("a6221860-0e85-11e8-ba89-0ed5f89f718b"), "KenyaEMR",
                EmrSetup.Community);

            EmrConnectionString = GenerateConnection(config, "emrConnection", false);
            EmrDiffConnectionString = GenerateConnection(config, "emrDiffConnection", false);
            ConnectionString = GenerateCopyConnection(config, "dwapiConnection");
            DiffConnectionString = GenerateCopyConnection(config, "dwapiDiffConnection");
            MsSqlConnectionString = config.GetConnectionString("mssqlConnection");
            MySqlConnectionString = config.GetConnectionString("mysqlConnection");

            InitSetup(config,services,GoLive);

            #region DI

            var assemblyNames = Assembly.GetEntryAssembly().GetReferencedAssemblies();
            List<Assembly> assemblies = new List<Assembly>();
            foreach (var assemblyName in assemblyNames)
            {
                assemblies.Add(Assembly.Load(assemblyName));
            }

            services.AddMediatR(assemblies);

                 services.AddScoped<ICentralRegistryRepository, CentralRegistryRepository>();
            services.AddScoped<IEmrSystemRepository, EmrSystemRepository>();
            services.AddScoped<IDocketRepository, DocketRepository>();
            services.AddScoped<IDatabaseProtocolRepository, DatabaseProtocolRepository>();
            services.AddScoped<IRestProtocolRepository, RestProtocolRepository>();
            services.AddScoped<IExtractRepository, ExtractRepository>();
            services.AddScoped<IPsmartStageRepository, PsmartStageRepository>();
            services.AddTransient<IExtractHistoryRepository, ExtractHistoryRepository>();
            services.AddTransient<IDiffLogRepository, DiffLogRepository>();
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

            services.AddScoped<IMasterPatientIndexRepository, MasterPatientIndexRepository>();
            services.AddScoped<ITempMasterPatientIndexRepository, TempMasterPatientIndexRepository>();

            services.AddScoped<IDatabaseManager, DatabaseManager>();
            services.AddScoped<IRegistryManagerService, RegistryManagerService>();
            services.AddScoped<IEmrManagerService, EmrManagerService>();
            services.AddScoped<IExtractManagerService, ExtractManagerService>();

            services.AddScoped<IPsmartExtractService, PsmartExtractService>();
            services.AddScoped<IExtractStatusService, ExtractStatusService>();
            services.AddScoped<IPsmartSourceReader, PsmartSourceReader>();
            services.AddScoped<IPsmartSendService, PsmartSendService>();

            services.AddScoped<IDwhExtractSourceReader, DwhExtractSourceReader>();
            services.AddScoped<IPatientSourceExtractor, PatientSourceExtractor>();
            services.AddScoped<IPatientArtSourceExtractor, PatientArtSourceExtractor>();
            services.AddScoped<IPatientBaselinesSourceExtractor, PatientBaselinesSourceExtractor>();
            services.AddScoped<IPatientLaboratorySourceExtractor, PatientLaboratorySourceExtractor>();
            services.AddScoped<IPatientPharmacySourceExtractor, PatientPharmacySourceExtractor>();
            services.AddScoped<IPatientStatusSourceExtractor, PatientStatusSourceExtractor>();
            services.AddScoped<IPatientVisitSourceExtractor, PatientVisitSourceExtractor>();
            services.AddScoped<IPatientAdverseEventSourceExtractor, PatientAdverseEventSourceExtractor>();
            services.AddScoped<IExtractValidator, ExtractValidator>();
            services.AddScoped<IPatientLoader, PatientLoader>();
            services.AddScoped<IPatientArtLoader, PatientArtLoader>();
            services.AddScoped<IPatientBaselinesLoader, PatientBaselinesLoader>();
            services.AddScoped<IPatientLaboratoryLoader, PatientLaboratoryLoader>();
            services.AddScoped<IPatientPharmacyLoader, PatientPharmacyLoader>();
            services.AddScoped<IPatientStatusLoader, PatientStatusLoader>();
            services.AddScoped<IPatientVisitLoader, PatientVisitLoader>();
            services.AddScoped<IPatientAdverseEventLoader, PatientAdverseEventLoader>();
            services.AddScoped<IClearDwhExtracts, ClearDwhExtracts>();

            services.AddScoped<IMasterPatientIndexReader, MasterPatientIndexReader>();
            services.AddScoped<IMasterPatientIndexSourceExtractor, MasterPatientIndexSourceExtractor>();
            services.AddScoped<IMasterPatientIndexValidator, MasterPatientIndexValidator>();
            services.AddScoped<IMasterPatientIndexLoader, MasterPatientIndexLoader>();
            services.AddScoped<IClearCbsExtracts, ClearCbsExtracts>();
            services.AddScoped<ICbsExtractReader, CbsExtractReader>();
            services.AddScoped<ICbsSendService, CbsSendService>();
            services.AddScoped<ICbsPackager, CbsPackager>();
            services.AddScoped<IMpiSearchService, MpiSearchService>();

            services.AddScoped<IDwhExtractReader, DwhExtractReader>();
            services.AddScoped<IDwhPackager, DwhPackager>();
            services.AddScoped<IDwhSendService, DwhSendService>();
            services.AddScoped<ICTSendService, CTSendService>();
            services.AddScoped<IDwhExtractSentServcie, DwhExtractSentServcie>();
            services.AddScoped<IMnchExtractSentServcie, MnchExtractSentServcie>();

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
            services
                .AddScoped<ITempHtsPartnerNotificationServicesExtractRepository,
                    TempHtsPartnerNotificationServicesExtractRepository>();

            services.AddScoped<IHtsClientsExtractRepository, HtsClientsExtractRepository>();
            services.AddScoped<IHtsClientsLinkageExtractRepository, HtsClientsLinkageExtractRepository>();
            services.AddScoped<IHtsClientTestsExtractRepository, HtsClientTestsExtractRepository>();
            services.AddScoped<IHtsTestKitsExtractRepository, HtsTestKitsExtractRepository>();
            services.AddScoped<IHtsClientTracingExtractRepository, HtsClientTracingExtractRepository>();
            services.AddScoped<IHtsPartnerTracingExtractRepository, HtsPartnerTracingExtractRepository>();
            services
                .AddScoped<IHtsPartnerNotificationServicesExtractRepository,
                    HtsPartnerNotificationServicesExtractRepository>();

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
            services.AddScoped<ITempHtsTestKitsErrorSummaryRepository, TempHtsTestKitsExtractErrorSummaryRepository>();
            services
                .AddScoped<ITempHtsClientTracingErrorSummaryRepository,
                    TempHtsClientTracingExtractErrorSummaryRepository>();
            services
                .AddScoped<ITempHtsPartnerTracingErrorSummaryRepository,
                    TempHtsPartnerTracingExtractErrorSummaryRepository>();
            services
                .AddScoped<ITempHtsPartnerNotificationServicesErrorSummaryRepository,
                    TempHtsPartnerNotificationServicesExtractErrorSummaryRepository>();

            //services.AddScoped<ICleanHtsExtracts, CleanHtsExtracts>();
            services.AddScoped<IClearHtsExtracts, ClearHtsExtracts>();

            services.AddScoped<IHTSExtractSourceReader, HTSExtractSourceReader>();

            /*services.AddScoped<IHTSClientSourceExtractor, HTSClientSourceExtractor>();
            services.AddScoped<IHTSClientLinkageSourceExtractor, HTSClientLinkageSourceExtractor>();
            services.AddScoped<IHTSClientPartnerSourceExtractor, HTSClientPartnerSourceExtractor>();*/

            services.AddScoped<IHtsClientsSourceExtractor, HtsClientsSourceExtractor>();
            services.AddScoped<IHtsClientTestsSourceExtractor, HtsClientTestsSourceExtractor>();
            services.AddScoped<IHtsClientsLinkageSourceExtractor, HtsClientsLinkageSourceExtractor>();
            services.AddScoped<IHtsTestKitsSourceExtractor, HtsTestKitsSourceExtractor>();
            services.AddScoped<IHtsClientTracingSourceExtractor, HtsClientTracingSourceExtractor>();
            services.AddScoped<IHtsPartnerTracingSourceExtractor, HtsPartnerTracingSourceExtractor>();
            services
                .AddScoped<IHtsPartnerNotificationServicesSourceExtractor, HtsPartnerNotificationServicesSourceExtractor
                >();

            /*services.AddScoped<IHTSClientLoader, HTSClientLoader>();
            services.AddScoped<IHTSClientLinkageLoader, HTSClientLinkageLoader>();
            services.AddScoped<IHTSClientPartnerLoader, HTSClientPartnerLoader>();*/

            services.AddScoped<IHtsClientsLoader, HtsClientsLoader>();
            services.AddScoped<IHtsClientTestsLoader, HtsClientTestsLoader>();
            services.AddScoped<IHtsClientsLinkageLoader, HtsClientsLinkageLoader>();
            services.AddScoped<IHtsTestKitsLoader, HtsTestKitsLoader>();
            services.AddScoped<IHtsClientTracingLoader, HtsClientTracingLoader>();
            services.AddScoped<IHtsPartnerTracingLoader, HtsPartnerTracingLoader>();
            services.AddScoped<IHtsPartnerNotificationServicesLoader, HtsPartnerNotificationServicesLoader>();

            services.AddScoped<IHtsPackager, HtsPackager>();
            services.AddScoped<IHtsSendService, HtsSendService>();
            services.AddScoped<IHtsExtractValidator, HtsExtractValidator>();
            services.AddScoped<IHtsSendService, HtsSendService>();
            services.AddScoped<IHtsExtractReader, HtsExtractReader>();


            services.AddScoped<ITempMetricMigrationExtractRepository, TempMetricMigrationExtractRepository>();
            services.AddScoped<IMetricMigrationExtractRepository, MetricMigrationExtractRepository>();
            services
                .AddScoped<ITempMetricMigrationExtractErrorSummaryRepository,
                    TempMetricMigrationExtractErrorSummaryRepository>();
            services.AddScoped<IClearMgsExtracts, ClearMgsExtracts>();


            services.AddScoped<IMgsExtractSourceReader, MgsExtractSourceReader>();
            services.AddScoped<IMetricMigrationSourceExtractor, MetricMigrationSourceExtractor>();
            services.AddScoped<IMgsExtractReader, MgsExtractReader>();
            services.AddScoped<IMetricMigrationLoader, MetricMigrationLoader>();
            services.AddScoped<IMgsPackager, MgsPackager>();
            services.AddScoped<IMgsSendService, MgsSendService>();
            services.AddScoped<IMetricExtractValidator, MetricExtractValidator>();


            services.AddScoped<IHtsSendService, HtsSendService>();

            services.AddScoped<IAppDatabaseManager, AppDatabaseManager>();

            services.AddScoped<IEmrMetricRepository, EmrMetricRepository>();
            services.AddScoped<IEmrMetricsService, EmrMetricsService>();
            services.AddScoped<IEmrMetricReader, EmrMetricReader>();
            services.AddScoped<IDiffLogReader, DiffLogReader>();

            services.AddScoped<IAppMetricRepository, AppMetricRepository>();

            #region newCT
            services.AddTransient<IAllergiesChronicIllnessExtractRepository, AllergiesChronicIllnessExtractRepository>();
            services.AddTransient<IContactListingExtractRepository, ContactListingExtractRepository>();
            services.AddTransient<IDepressionScreeningExtractRepository, DepressionScreeningExtractRepository>();
            services.AddTransient<IDrugAlcoholScreeningExtractRepository, DrugAlcoholScreeningExtractRepository>();
            services.AddTransient<IEnhancedAdherenceCounsellingExtractRepository, EnhancedAdherenceCounsellingExtractRepository>();
            services.AddTransient<IGbvScreeningExtractRepository, GbvScreeningExtractRepository>();
            services.AddTransient<IIptExtractRepository, IptExtractRepository>();
            services.AddTransient<IOtzExtractRepository, OtzExtractRepository>();
            services.AddTransient<IOvcExtractRepository, OvcExtractRepository>();

            //DefaulterTracing
            services.AddTransient<ICovidExtractRepository, CovidExtractRepository>();
            services.AddTransient<IDefaulterTracingExtractRepository, DefaulterTracingExtractRepository>();

            services.AddTransient<ITempAllergiesChronicIllnessExtractRepository, TempAllergiesChronicIllnessExtractRepository>();
            services.AddTransient<ITempContactListingExtractRepository, TempContactListingExtractRepository>();
            services.AddTransient<ITempDepressionScreeningExtractRepository, TempDepressionScreeningExtractRepository>();
            services.AddTransient<ITempDrugAlcoholScreeningExtractRepository, TempDrugAlcoholScreeningExtractRepository>();
            services.AddTransient<ITempEnhancedAdherenceCounsellingExtractRepository, TempEnhancedAdherenceCounsellingExtractRepository>();
            services.AddTransient<ITempGbvScreeningExtractRepository, TempGbvScreeningExtractRepository>();
            services.AddTransient<ITempIptExtractRepository, TempIptExtractRepository>();
            services.AddTransient<ITempOtzExtractRepository, TempOtzExtractRepository>();
            services.AddTransient<ITempOvcExtractRepository, TempOvcExtractRepository>();

            //DefaulterTracing
            services.AddTransient<ITempDefaulterTracingExtractRepository, TempDefaulterTracingExtractRepository>();
            services.AddTransient<ITempCovidExtractRepository, TempCovidExtractRepository>();

            services.AddTransient<ITempAllergiesChronicIllnessExtractErrorSummaryRepository, TempAllergiesChronicIllnessExtractErrorSummaryRepository>();
            services.AddTransient<ITempContactListingExtractErrorSummaryRepository, TempContactListingExtractErrorSummaryRepository>();
            services.AddTransient<ITempDepressionScreeningExtractErrorSummaryRepository, TempDepressionScreeningExtractErrorSummaryRepository>();
            services.AddTransient<ITempDrugAlcoholScreeningExtractErrorSummaryRepository, TempDrugAlcoholScreeningExtractErrorSummaryRepository>();
            services.AddTransient<ITempEnhancedAdherenceCounsellingExtractErrorSummaryRepository, TempEnhancedAdherenceCounsellingExtractErrorSummaryRepository>();
            services.AddTransient<ITempGbvScreeningExtractErrorSummaryRepository, TempGbvScreeningExtractErrorSummaryRepository>();
            services.AddTransient<ITempIptExtractErrorSummaryRepository, TempIptExtractErrorSummaryRepository>();
            services.AddTransient<ITempOtzExtractErrorSummaryRepository, TempOtzExtractErrorSummaryRepository>();
            services.AddTransient<ITempOvcExtractErrorSummaryRepository, TempOvcExtractErrorSummaryRepository>();


            //DefaulterTracing
            services.AddTransient<ITempCovidExtractErrorSummaryRepository, TempCovidExtractErrorSummaryRepository>();
            services.AddTransient<ITempDefaulterTracingExtractErrorSummaryRepository, TempDefaulterTracingExtractErrorSummaryRepository>();


            services.AddScoped<IAllergiesChronicIllnessSourceExtractor, AllergiesChronicIllnessSourceExtractor>();
            services.AddScoped<IContactListingSourceExtractor, ContactListingSourceExtractor>();
            services.AddScoped<IDepressionScreeningSourceExtractor, DepressionScreeningSourceExtractor>();
            services.AddScoped<IDrugAlcoholScreeningSourceExtractor, DrugAlcoholScreeningSourceExtractor>();
            services.AddScoped<IEnhancedAdherenceCounsellingSourceExtractor, EnhancedAdherenceCounsellingSourceExtractor>();
            services.AddScoped<IGbvScreeningSourceExtractor, GbvScreeningSourceExtractor>();
            services.AddScoped<IIptSourceExtractor, IptSourceExtractor>();
            services.AddScoped<IOtzSourceExtractor, OtzSourceExtractor>();
            services.AddScoped<IOvcSourceExtractor, OvcSourceExtractor>();

            //DefaulterTracing
            services.AddScoped<ICovidSourceExtractor, CovidSourceExtractor>();
            services.AddScoped<IDefaulterTracingSourceExtractor, DefaulterTracingSourceExtractor>();

            services.AddScoped<IAllergiesChronicIllnessLoader, AllergiesChronicIllnessLoader>();
            services.AddScoped<IContactListingLoader, ContactListingLoader>();
            services.AddScoped<IDepressionScreeningLoader, DepressionScreeningLoader>();
            services.AddScoped<IDrugAlcoholScreeningLoader, DrugAlcoholScreeningLoader>();
            services.AddScoped<IEnhancedAdherenceCounsellingLoader, EnhancedAdherenceCounsellingLoader>();
            services.AddScoped<IGbvScreeningLoader, GbvScreeningLoader>();
            services.AddScoped<IIptLoader, IptLoader>();
            services.AddScoped<IOtzLoader, OtzLoader>();
            services.AddScoped<IOvcLoader, OvcLoader>();

            //DefaulterTracing
            services.AddScoped<ICovidLoader, CovidLoader>();
            services.AddScoped<IDefaulterTracingLoader, DefaulterTracingLoader>();

            #endregion



            services.AddScoped<ITempIndicatorExtractRepository, TempIndicatorExtractRepository>();
            services.AddScoped<IIndicatorExtractRepository, IndicatorExtractRepository>();
            services.AddScoped<IClearMtsExtracts, ClearMtsExtracts>();

            services.AddScoped<IMtsExtractSourceReader, MtsExtractSourceReader>();
            services.AddScoped<IIndicatorSourceExtractor, IndicatorSourceExtractor>();
            services.AddScoped<IMtsMigrationLoader, MtsMigrationLoader>();

            services.AddScoped<IMtsExtractReader, MtsExtractReader>();
            services.AddScoped<IMtsPackager, MtsPackager>();
            services.AddScoped<IMtsSendService, MtsSendService>();

            services.AddScoped<IIntegrityCheckRepository, IntegrityCheckRepository>();

             #region Extracts

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
            services.AddScoped<IMnchExtractSourceReader, MnchExtractSourceReader>();
            services.AddScoped<IMnchExtractValidator, MnchExtractValidator>();
            services.AddScoped<IClearMnchExtracts, ClearMnchExtracts>();

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

            services.AddScoped<IMnchExtractReader, MnchExtractReader>();
            services.AddScoped<IMnchPackager, MnchPackager>();
            services.AddScoped<IMnchSendService, MnchSendService>();

            services.AddScoped<ITransportLogRepository, TransportLogRepository>();

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
                }
            );
            #endregion

            if(!GoLive)
                ClearDb();
        }

        public void InitSetup(IConfigurationRoot config, ServiceCollection services,
            bool goLive = false)
        {
            if (goLive)
            {
                /*
                services.AddDbContext<UploadContext>(x => x.UseSqlServer(MsSqlConnectionString));
                services.AddDbContext<SettingsContext>(x => x.UseSqlServer(MsSqlConnectionString));
                services.AddDbContext<ExtractsContext>(x => x.UseSqlServer(MsSqlConnectionString));
                */
                services.AddDbContext<UploadContext>(x => x.UseMySql(MySqlConnectionString));
                services.AddDbContext<SettingsContext>(x => x.UseMySql(MySqlConnectionString));
                services.AddDbContext<ExtractsContext>(x => x.UseMySql(MySqlConnectionString));
                return;

                return;
            }

            SqlMapper.AddTypeHandler<Guid>(new GuidTypeHandler());
            SqlMapper.AddTypeHandler(new NullableLongHandler());
            SqlMapper.AddTypeHandler(new NullableIntHandler());
            RemoveTestsFilesDbs();

            var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            services.AddDbContext<UploadContext>(x => x.UseSqlite(connection));
            services.AddDbContext<SettingsContext>(x => x.UseSqlite(connection));
            services.AddDbContext<ExtractsContext>(x => x.UseSqlite(connection));
         }

        public static void ClearDb()
        {
            var context = ServiceProvider.GetService<SettingsContext>();
            context.EnsureSeeded();

            var econtext = ServiceProvider.GetService<ExtractsContext>();
            econtext.EnsureSeeded();

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

         public static void ClearDiffDb()
        {
            AllServices.RemoveService(typeof(UploadContext));
            AllServices.RemoveService(typeof(SettingsContext));
            AllServices.RemoveService(typeof(ExtractsContext));

            AllServices.RemoveService(typeof(DbContextOptions<UploadContext>));
            AllServices.RemoveService(typeof(DbContextOptions<SettingsContext>));
            AllServices.RemoveService(typeof(DbContextOptions<ExtractsContext>));

            var diffConnection = new SqliteConnection(DiffConnectionString);
            diffConnection.Open();

            AllServices.AddDbContext<UploadContext>(x => x.UseSqlite(diffConnection));
            AllServices.AddDbContext<SettingsContext>(x => x.UseSqlite(diffConnection));
            AllServices.AddDbContext<ExtractsContext>(x => x.UseSqlite(diffConnection));


            ServiceProvider = AllServices.BuildServiceProvider();

            var context = ServiceProvider.GetService<SettingsContext>();
            context.EnsureSeeded();

            var econtext = ServiceProvider.GetService<ExtractsContext>();
            econtext.EnsureSeeded();

            SeedData(TestData.GenerateEmrSystems(EmrDiffConnectionString));
            SeedData(TestData.GenerateData<AppMetric>());
            SeedData<ExtractsContext>(TestData.GenerateData<EmrMetric>());
            Protocol = context.DatabaseProtocols.AsNoTracking().First(x => x.DatabaseType == DatabaseType.Sqlite);
            Extracts = context.Extracts.AsNoTracking().Where(x => x.DatabaseProtocolId == Protocol.Id).ToList();

            LoadDiffs(DateTime.Now.Date.AddMonths(-1), DateTime.Now.Date.AddMonths(-1));

            LoadCt();
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
            DapperPlusManager.AddLicense("1755;700-ThePalladiumGroup", "218460a6-02d0-c26b-9add-e6b8d13ccbf4");
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
                {"dwapi.db", "dwapizdiff.db", "emr.db", "emrzdiff.db"};
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
            var countB = loader.Load(extract.Id,countA, false).Result;
        }

        public static void LoadDiffs(DateTime dateCreated, DateTime dateModified)
        {
            var context = ServiceProvider.GetService<SettingsContext>();
            var db = context.EmrSystems.First(x => x.Name == "KenyaCare");
            var connection = db.DatabaseProtocols.First().GetConnectionString();

            var sql = $@"
                update TempPatientAdverseEventExtracts set {nameof(PatientExtract.Date_Created)}=@dateCreated, {nameof(PatientExtract.Date_Last_Modified)}=@dateModified;
                update TempPatientArtExtracts set {nameof(PatientExtract.Date_Created)}=@dateCreated, {nameof(PatientExtract.Date_Last_Modified)}=@dateModified;
                update TempPatientBaselinesExtracts set {nameof(PatientExtract.Date_Created)}=@dateCreated, {nameof(PatientExtract.Date_Last_Modified)}=@dateModified;
                update TempPatientExtracts set {nameof(PatientExtract.Date_Created)}=@dateCreated, {nameof(PatientExtract.Date_Last_Modified)}=@dateModified;
                update TempPatientLaboratoryExtracts set {nameof(PatientExtract.Date_Created)}=@dateCreated, {nameof(PatientExtract.Date_Last_Modified)}=@dateModified;
                update TempPatientPharmacyExtracts set {nameof(PatientExtract.Date_Created)}=@dateCreated, {nameof(PatientExtract.Date_Last_Modified)}=@dateModified;
                update TempPatientStatusExtracts set {nameof(PatientExtract.Date_Created)}=@dateCreated, {nameof(PatientExtract.Date_Last_Modified)}=@dateModified;
                update TempPatientVisitExtracts set {nameof(PatientExtract.Date_Created)}=@dateCreated, {nameof(PatientExtract.Date_Last_Modified)}=@dateModified;
             ";

            using (var dbConnection = new SqliteConnection(connection))
            {
                dbConnection.Open();
                dbConnection.Execute(sql, new {dateCreated, dateModified});
            }
        }
    }
}
