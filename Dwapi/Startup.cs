using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.ExtractsManagement.Core.Services;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.ExtractsManagement.Infrastructure.Reader;
using Dwapi.ExtractsManagement.Infrastructure.Repository;
using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Services;
using Dwapi.SettingsManagement.Infrastructure;
using Dwapi.SettingsManagement.Infrastructure.Repository;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Infrastructure;
using Dwapi.UploadManagement.Core.Interfaces.Services;
using Dwapi.UploadManagement.Core.Services;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace Dwapi
{
    public class Startup
    {
        public static IConfiguration Configuration;
        public IServiceCollection Service;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // -------------NEW---------------
            var assemblyNames = Assembly.GetEntryAssembly().GetReferencedAssemblies();
            List<Assembly> assemblies = new List<Assembly>();
            foreach (var assemblyName in assemblyNames)
            {
                assemblies.Add(Assembly.Load(assemblyName));
            }
            
            services.AddMediatR(assemblies);
            //services.AddDatabase(Configuration);
            
            services.AddExtractorAdaptor();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Dwapi API Test",
                    Version = "v1",
                    Description = "Dwapi API. Exposes endpoints used in datawarehouse operations"
                });
            });
            services.AddSignalR();
            // -------------------------------

            services.AddMvc()
              .AddMvcOptions(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()))
              .AddJsonOptions(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            var connectionString = Startup.Configuration["connectionStrings:DwapiConnection"];
            

            services.AddDbContext<SettingsContext>(o => o.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(SettingsContext).GetTypeInfo().Assembly.GetName().Name)));
            services.AddDbContext<ExtractsContext>(o => o.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(ExtractsContext).GetTypeInfo().Assembly.GetName().Name)));

            
            services.AddScoped<ICentralRegistryRepository, CentralRegistryRepository>();
            services.AddScoped<IEmrSystemRepository, EmrSystemRepository>();
            services.AddScoped<IDocketRepository, DocketRepository>();
            services.AddScoped<IDatabaseProtocolRepository, DatabaseProtocolRepository>();
            services.AddScoped<IExtractRepository, ExtractRepository>();
            services.AddScoped<IPsmartStageRepository, PsmartStageRepository>();
            services.AddScoped<IExtractHistoryRepository, ExtractHistoryRepository>();

            services.AddScoped<IDatabaseManager, DatabaseManager>();
            services.AddScoped<IRegistryManagerService, RegistryManagerService>();
            services.AddScoped<IEmrManagerService, EmrManagerService>();
            services.AddScoped<IExtractManagerService, ExtractManagerService>();

            services.AddScoped<IPsmartExtractService, PsmartExtractService>();
            services.AddScoped<IExtractStatusService, ExtractStatusService>();
            services.AddScoped<IPsmartSourceReader, PsmartSourceReader>();
            services.AddScoped<IPsmartSendService, PsmartSendService>();
            //services.AddHangfireIntegration(Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 &&
                    !Path.HasExtension(context.Request.Path.Value) &&
                    !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseMvcWithDefaultRoute();
            app.UseDefaultFiles();

            app.UseStaticFiles()
                .UseSwaggerUi();
            
            

            Log.Debug(@"initializing Database...");

            EnsureMigrationOfContext<SettingsContext>(serviceProvider);
            EnsureMigrationOfContext<ExtractsContext>(serviceProvider);

            
            //app.UseHangfire();

            Log.Debug(@"initializing Database [Complete]");
            Log.Debug(@"---------------------------------------------------------------------------------------------------");
            Log.Debug
      (@"

                                          _____                      _ 
                                         |  __ \                    (_)
                                         | |  | |_      ____ _ _ __  _ 
                                         | |  | \ \ /\ / / _` | '_ \| |
                                         | |__| |\ V  V / (_| | |_) | |
                                         |_____/  \_/\_/ \__,_| .__/|_|
                                                              | |      
                                                              |_|      
");
            Log.Debug(@"---------------------------------------------------------------------------------------------------");
            Log.Debug("Dwapi started !");

       
        }

        public static void EnsureMigrationOfContext<T>(IServiceProvider app) where T : BaseContext
        {
            var contextName = typeof(T).Name;
            Log.Debug($"initializing Database context: {contextName}");
            var context = app.GetService<T>();
            try
            {
                context.Database.Migrate();
                context.EnsureSeeded();
                Log.Debug($"initializing Database context: {contextName} [OK]");
            }
            catch (Exception e)
            {
                Log.Debug($"initializing Database context: {contextName} Error");
                Log.Debug($"{e}");
            }

        }
        
    }
}
