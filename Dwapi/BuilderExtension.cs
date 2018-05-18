using Dwapi.ExtractsManagement.Core;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Extractors;
using Dwapi.ExtractsManagement.Core.ExtractValidators;
using Dwapi.ExtractsManagement.Core.Services;
using Dwapi.ExtractsManagement.Infrastructure;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NPoco;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Hangfire.MySql.Core;
using System.Data;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;

namespace Dwapi
{
    public static class BuilderExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var mysqlconnectionString = configuration["connectionStrings:MysqlDwapiConnection"];
            
            services.AddDbContext<SettingsManagement.Infrastructure.SettingsContext>(e => e.UseMySql(mysqlconnectionString, action =>
            {
                action.CommandTimeout(3600);
            }));

            services.AddDbContext<ExtractsContext>(opt =>
            {
                opt.UseMySql(mysqlconnectionString, act =>
                {
                    act.CommandTimeout(3600);
                });
            });

            services.AddScoped(typeof(IGenericExtractRepository<>), typeof(GenericExtractRepository<>));
            services.AddScoped<IExtractUnitOfWork>(c => new ExtractUnitOfWork(c.GetRequiredService<ExtractsContext>()));
            return services;
        }

        public static IServiceCollection AddHangfireIntegration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(c => c.UseStorage(new MySqlStorage(configuration.GetConnectionString("MysqlDwapiConnection"), new MySqlStorageOptions
            {
                TransactionIsolationLevel = IsolationLevel.ReadCommitted,
                QueuePollInterval = TimeSpan.FromSeconds(15),
                JobExpirationCheckInterval = TimeSpan.FromHours(1),
                CountersAggregateInterval = TimeSpan.FromMinutes(5),
                PrepareSchemaIfNecessary = true,
                DashboardJobListLimit = 50000,
                TransactionTimeout = TimeSpan.FromMinutes(1),
            })));

            services.AddScoped<IBackgroundJobInit, BackgroundJobInit>();
            return services;
        }

        public static IServiceCollection AddExtractorAdaptor(this IServiceCollection services)
        {
            services.AddScoped<ProgressHub>();


            services.AddScoped(typeof(ExtractorAdapter), sp =>
            {
                var unitOfWork = sp.GetRequiredService<IExtractUnitOfWork>();
                var extractStatusService = sp.GetRequiredService<IExtractStatusService>();
                var progressHub = sp.GetRequiredService<ProgressHub>();

                var extractorAdapter = new ExtractorAdapter();

                extractorAdapter.RegisterExtractor(ExtractType.Patient, new PatientExtractor(unitOfWork, extractStatusService));
                extractorAdapter.RegisterExtractor(ExtractType.PatientArt, new PatientArtExtractor(unitOfWork, progressHub));
                extractorAdapter.RegisterExtractor(ExtractType.PatientBaseline, new PatientBaselineExtractor(unitOfWork));
                extractorAdapter.RegisterExtractor(ExtractType.PatientLab, new PatientLabExtractor(unitOfWork));
                extractorAdapter.RegisterExtractor(ExtractType.PatientPharmacy, new PatientPharmarcyExtractor(unitOfWork));
                extractorAdapter.RegisterExtractor(ExtractType.PatientStatus, new PatientStatusExtractor(unitOfWork));
                extractorAdapter.RegisterExtractor(ExtractType.PatientVisit, new PatientVisitExtractor(unitOfWork));

                return extractorAdapter;
            });
            
            services.AddScoped(typeof(ExtractorValidatorAdapter), sp =>
            {
                var unitOfWork = sp.GetRequiredService<IExtractUnitOfWork>();
                var extractStatusService = sp.GetRequiredService<IExtractStatusService>();
                var progressHub = sp.GetRequiredService<ProgressHub>();
                var extractorValidatorAdapter = new ExtractorValidatorAdapter();

                extractorValidatorAdapter.RegisterExtractorValidator(ExtractType.Patient, new PatientExtractorAndValidator(unitOfWork, extractStatusService));
                extractorValidatorAdapter.RegisterExtractorValidator(ExtractType.PatientArt, new PatientArtExtractorAndValidator(unitOfWork, progressHub));
                extractorValidatorAdapter.RegisterExtractorValidator(ExtractType.PatientBaseline, new PatientBaselineExtractorAndValidator(unitOfWork));
                extractorValidatorAdapter.RegisterExtractorValidator(ExtractType.PatientLab, new PatientLabExtractorAndValidator(unitOfWork));
                extractorValidatorAdapter.RegisterExtractorValidator(ExtractType.PatientPharmacy, new PatientPharmacyExtractorAndValidator(unitOfWork));
                extractorValidatorAdapter.RegisterExtractorValidator(ExtractType.PatientStatus, new PatientStatusExtractAndValidator(unitOfWork));
                extractorValidatorAdapter.RegisterExtractorValidator(ExtractType.PatientVisit, new PatientVisitExtractorAndValidator(unitOfWork));


                return extractorValidatorAdapter;
            });
            return services;
        }
    }
}
