using Dwapi.ExtractsManagement.Core;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Extractors;
using Dwapi.ExtractsManagement.Core.Services;
using Dwapi.ExtractsManagement.Infrastructure;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwapi
{
    public static class BuilderExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericExtractRepository<>), typeof(GenericExtractRepository<>));
            services.AddScoped<IExtractUnitOfWork>(c => new ExtractUnitOfWork(c.GetRequiredService<ExtractsContext>()));
            return services;
        }

        public static IServiceCollection AddHangfireIntegration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(c => c.UseSqlServerStorage(configuration.GetConnectionString("DwapiConnection")));
            services.AddScoped<IBackgroundJobInit, BackgroundJobInit>();
            return services;
        }

        public static IServiceCollection AddExtractorAdaptor(this IServiceCollection services)
        {
            
            services.AddScoped(typeof(ExtractorAdapter), sp =>
            {

                var unitOfWork = sp.GetRequiredService<IExtractUnitOfWork>();
                var bginit = sp.GetRequiredService<IBackgroundJobInit>();

                ExtractorAdapter extractorAdapter = new ExtractorAdapter();

                extractorAdapter.RegisterExtractor(ExtractType.Patient, new PatientExtractor(unitOfWork, bginit));
                extractorAdapter.RegisterExtractor(ExtractType.PatientArt, new PatientArtExtractor(unitOfWork));
                extractorAdapter.RegisterExtractor(ExtractType.PatientBaseline, new PatientBaselineExtractor(unitOfWork));
                extractorAdapter.RegisterExtractor(ExtractType.PatientLab, new PatientLabExtractor(unitOfWork));
                extractorAdapter.RegisterExtractor(ExtractType.PatientPharmacy, new PatientPharmarcyExtractor(unitOfWork));
                extractorAdapter.RegisterExtractor(ExtractType.PatientStatus, new PatientStatusExtractor(unitOfWork));
                extractorAdapter.RegisterExtractor(ExtractType.PatientVisit, new PatientVisitExtractor(unitOfWork));
                
                return extractorAdapter;
            });

            return services;
        }
    }
}
