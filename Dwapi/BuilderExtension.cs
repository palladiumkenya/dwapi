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
            return services;
        }

        public static IServiceCollection AddHangfireIntegration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(c => c.UseSqlServerStorage(configuration.GetConnectionString("")));
            return services;
        }
    }
}
