using System;
using System.Linq;
using AutoMapper;
using AutoMapper.Data;
using Dwapi.ExtractsManagement.Core.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Cleaner.Dwh;
using Dwapi.ExtractsManagement.Core.Cleaner.Hts;
using Dwapi.ExtractsManagement.Core.ComandHandlers.Cbs;
using Dwapi.ExtractsManagement.Core.Extractors.Cbs;
using Dwapi.ExtractsManagement.Core.Extractors.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Loader.Cbs;
using Dwapi.ExtractsManagement.Core.Loader.Dwh;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Core.Profiles;
using Dwapi.ExtractsManagement.Core.Profiles.Cbs;
using Dwapi.ExtractsManagement.Core.Profiles.Dwh;
using Dwapi.ExtractsManagement.Core.Services;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Hts;
using Dwapi.ExtractsManagement.Infrastructure.Repository;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Hts;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SettingsManagement.Infrastructure;
using MediatR;
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
            // return;

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
                .AddTransient<ITempPatientArtExtractRepository, TempPatientArtExtractRepository>()
                .AddTransient<ITempPatientBaselinesExtractRepository, TempPatientBaselinesExtractRepository>()
                .AddTransient<ITempPatientLaboratoryExtractRepository, TempPatientLaboratoryExtractRepository>()
                .AddTransient<ITempPatientPharmacyExtractRepository, TempPatientPharmacyExtractRepository>()
                .AddTransient<ITempPatientStatusExtractRepository, TempPatientStatusExtractRepository>()
                .AddTransient<ITempPatientVisitExtractRepository, TempPatientVisitExtractRepository>()

                .AddTransient<IPatientExtractRepository, PatientExtractRepository>()
                .AddTransient<IPatientArtExtractRepository, PatientArtExtractRepository>()
                .AddTransient<IPatientBaselinesExtractRepository, PatientBaselinesExtractRepository>()
                .AddTransient<IPatientLaboratoryExtractRepository, PatientLaboratoryExtractRepository>()
                .AddTransient<IPatientPharmacyExtractRepository, PatientPharmacyExtractRepository>()
                .AddTransient<IPatientStatusExtractRepository, PatientStatusExtractRepository>()
                .AddTransient<IPatientVisitExtractRepository, PatientVisitExtractRepository>()

                .AddTransient<ITempHTSClientExtractRepository, TempHTSClientExtractRepository>()
                .AddTransient<ITempHTSClientLinkageExtractRepository, TempHTSClientLinkageExtractRepository>()
                .AddTransient<ITempHTSClientPartnerExtractRepository, TempHTSClientPartnerExtractRepository>()

                .AddTransient<IHTSClientExtractRepository, HTSClientExtractRepository>()
                .AddTransient<IHTSClientLinkageExtractRepository, HTSClientLinkageExtractRepository>()
                .AddTransient<IHTSClientPartnerExtractRepository, HTSClientPartnerExtractRepository>()

                .AddTransient<ICleanCbsExtracts, CleanCbsExtracts>()
                .AddTransient<IClearDwhExtracts, ClearDwhExtracts>()
                .AddTransient<ICleanHtsExtracts, CleanHtsExtracts>()

                .AddTransient<IMasterPatientIndexReader, MasterPatientIndexReader>()
                .AddTransient<IExtractSourceReader, ExtractSourceReader>()
                .AddTransient<IHTSExtractSourceReader, HTSExtractSourceReader>()

                .AddTransient<IPatientSourceExtractor, PatientSourceExtractor>()
                .AddTransient<IPatientStatusSourceExtractor, PatientStatusSourceExtractor>()
                .AddTransient<IPatientArtSourceExtractor, PatientArtSourceExtractor>()
                .AddTransient<IPatientBaselinesSourceExtractor, PatientBaselinesSourceExtractor>()
                .AddTransient<IPatientPharmacySourceExtractor, PatientPharmacySourceExtractor>()
                .AddTransient<IPatientVisitSourceExtractor, PatientVisitSourceExtractor>()
                .AddTransient<IPatientLaboratorySourceExtractor, PatientLaboratorySourceExtractor>()

                .AddTransient<IMasterPatientIndexSourceExtractor, MasterPatientIndexSourceExtractor>()
                .AddTransient<IMasterPatientIndexLoader, MasterPatientIndexLoader>()

                .AddTransient<IPatientLoader, PatientLoader>()
                .AddTransient<IPatientArtLoader, PatientArtLoader>()
                .AddTransient<IPatientBaselinesLoader, PatientBaselinesLoader>()
                .AddTransient<IPatientLaboratoryLoader, PatientLaboratoryLoader>()
                .AddTransient<IPatientPharmacyLoader, PatientPharmacyLoader>()
                .AddTransient<IPatientStatusLoader, PatientStatusLoader>()
                .AddTransient<IPatientVisitLoader, PatientVisitLoader>()


                .AddTransient<IEmrMetricRepository, EmrMetricRepository>()
                .AddTransient<IEmrMetricsService, EmrMetricsService>()

                .AddMediatR(typeof(ExtractMasterPatientIndexHandler))
                .BuildServiceProvider();

            var serviceProviderMysql = new ServiceCollection()
                .AddDbContext<ExtractsContext>(x => x.UseMySql(mysqlConfig["ConnectionStrings:DwapiConnection"]))
                .AddDbContext<SettingsContext>(x => x.UseMySql(mysqlConfig["ConnectionStrings:DwapiConnection"]))
                .AddTransient<ExtractsContext>()
                .AddTransient<SettingsContext>()
                .AddTransient<IExtractHistoryRepository, ExtractHistoryRepository>()

                .AddTransient<ITempMasterPatientIndexRepository, TempMasterPatientIndexRepository>()
                .AddTransient<IMasterPatientIndexRepository, MasterPatientIndexRepository>()

                .AddTransient<ITempPatientExtractRepository, TempPatientExtractRepository>()
                .AddTransient<ITempPatientArtExtractRepository, TempPatientArtExtractRepository>()
                .AddTransient<ITempPatientBaselinesExtractRepository, TempPatientBaselinesExtractRepository>()
                .AddTransient<ITempPatientLaboratoryExtractRepository, TempPatientLaboratoryExtractRepository>()
                .AddTransient<ITempPatientPharmacyExtractRepository, TempPatientPharmacyExtractRepository>()
                .AddTransient<ITempPatientStatusExtractRepository, TempPatientStatusExtractRepository>()
                .AddTransient<ITempPatientVisitExtractRepository, TempPatientVisitExtractRepository>()

                .AddTransient<IPatientExtractRepository, PatientExtractRepository>()
                .AddTransient<IPatientArtExtractRepository, PatientArtExtractRepository>()
                .AddTransient<IPatientBaselinesExtractRepository, PatientBaselinesExtractRepository>()
                .AddTransient<IPatientLaboratoryExtractRepository, PatientLaboratoryExtractRepository>()
                .AddTransient<IPatientPharmacyExtractRepository, PatientPharmacyExtractRepository>()
                .AddTransient<IPatientStatusExtractRepository, PatientStatusExtractRepository>()
                .AddTransient<IPatientVisitExtractRepository, PatientVisitExtractRepository>()


                .AddTransient<ITempHTSClientExtractRepository, TempHTSClientExtractRepository>()
                .AddTransient<ITempHTSClientLinkageExtractRepository, TempHTSClientLinkageExtractRepository>()
                .AddTransient<ITempHTSClientPartnerExtractRepository, TempHTSClientPartnerExtractRepository>()

                .AddTransient<IHTSClientExtractRepository, HTSClientExtractRepository>()
                .AddTransient<IHTSClientLinkageExtractRepository, HTSClientLinkageExtractRepository>()
                .AddTransient<IHTSClientPartnerExtractRepository, HTSClientPartnerExtractRepository>()

                .AddTransient<ICleanCbsExtracts, CleanCbsExtracts>()
                .AddTransient<IClearDwhExtracts, ClearDwhExtracts>()
                .AddTransient<ICleanHtsExtracts, CleanHtsExtracts>()

                .AddTransient<IMasterPatientIndexReader, MasterPatientIndexReader>()
                .AddTransient<IExtractSourceReader, ExtractSourceReader>()
                .AddTransient<IHTSExtractSourceReader, HTSExtractSourceReader>()

                .AddTransient<IPatientSourceExtractor, PatientSourceExtractor>()
                .AddTransient<IPatientStatusSourceExtractor, PatientStatusSourceExtractor>()
                .AddTransient<IPatientArtSourceExtractor, PatientArtSourceExtractor>()
                .AddTransient<IPatientBaselinesSourceExtractor, PatientBaselinesSourceExtractor>()
                .AddTransient<IPatientPharmacySourceExtractor, PatientPharmacySourceExtractor>()
                .AddTransient<IPatientVisitSourceExtractor, PatientVisitSourceExtractor>()
                .AddTransient<IPatientLaboratorySourceExtractor, PatientLaboratorySourceExtractor>()

                .AddMediatR(typeof(ExtractMasterPatientIndexHandler))
                .AddTransient<IMasterPatientIndexSourceExtractor, MasterPatientIndexSourceExtractor>()
                .AddTransient<IMasterPatientIndexLoader, MasterPatientIndexLoader>()

                .AddTransient<IPatientLoader, PatientLoader>()
                .AddTransient<IPatientArtLoader, PatientArtLoader>()
                .AddTransient<IPatientBaselinesLoader, PatientBaselinesLoader>()
                .AddTransient<IPatientLaboratoryLoader, PatientLaboratoryLoader>()
                .AddTransient<IPatientPharmacyLoader, PatientPharmacyLoader>()
                .AddTransient<IPatientStatusLoader, PatientStatusLoader>()
                .AddTransient<IPatientVisitLoader, PatientVisitLoader>()


                .AddTransient<IEmrMetricRepository, EmrMetricRepository>()
                .AddTransient<IEmrMetricsService, EmrMetricsService>()


                .BuildServiceProvider();

            ServiceProvider = serviceProvider;
            ServiceProviderMysql = serviceProviderMysql;

            var settingsContext = serviceProvider.GetService<SettingsContext>();
            var settingsContextMysql = serviceProviderMysql.GetService<SettingsContext>();
            var extractsContext = serviceProvider.GetService<ExtractsContext>();
            var extractsContextMysql = serviceProviderMysql.GetService<ExtractsContext>();

            /*try
            {
                settingsContext.Database.Migrate();
                settingsContext.EnsureSeeded();

                settingsContextMysql.Database.Migrate();
                settingsContextMysql.EnsureSeeded();

                extractsContext.Database.Migrate();
                extractsContext.EnsureSeeded();

                extractsContextMysql.Database.Migrate();
                extractsContextMysql.EnsureSeeded();

                Iqtools = settingsContext.EmrSystems
                    .Include(x => x.DatabaseProtocols)
                    .Include(x => x.Extracts)
                    .First(x => x.Id == new Guid("a62216ee-0e85-11e8-ba89-0ed5f89f718b"));

                KenyaEmr = settingsContextMysql.EmrSystems
                    .Include(x => x.DatabaseProtocols)
                    .Include(x => x.Extracts)
                    .First(x => x.Id == new Guid("a6221856-0e85-11e8-ba89-0ed5f89f718b"));

                Validator = extractsContext.Validator.First();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }*/


            Mapper.Initialize(cfg =>
                {
                    cfg.AddDataReaderMapping();
                    cfg.AddProfile<TempMasterPatientIndexProfile>();
                    cfg.AddProfile<TempExtractProfile>();
                    cfg.AddProfile<EmrProfiles>();
                }
            );

        }
    }
}
