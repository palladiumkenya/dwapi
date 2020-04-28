using System.Collections.Generic;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Builder;

namespace Dwapi.Custom
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
