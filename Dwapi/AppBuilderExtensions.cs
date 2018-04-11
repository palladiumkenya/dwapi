using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwapi
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerUi(this IApplicationBuilder app) =>
            app.UseSwagger().UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dwapi API"));

        public static IApplicationBuilder UseHangfire(this IApplicationBuilder app)
        {
            var options = new DashboardOptions()
            {
                Authorization = new List<IDashboardAuthorizationFilter>
                {
                    new HangfireDashboardAuthorizationFilter()
                }
            };
            app.UseHangfireDashboard("/dashboard", options);
            app.UseHangfireServer();
            return app;
        }
    }
}
