using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AutoMapper;
using AutoMapper.Data;
using Dwapi.Custom;
using Dwapi.ExtractsManagement.Core.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Cleaner.Dwh;
using Dwapi.ExtractsManagement.Core.Extractors.Cbs;
using Dwapi.ExtractsManagement.Core.Extractors.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Cbs;
using Dwapi.ExtractsManagement.Core.Loader.Cbs;
using Dwapi.ExtractsManagement.Core.Loader.Dwh;
using Dwapi.ExtractsManagement.Core.Profiles.Cbs;
using Dwapi.ExtractsManagement.Core.Profiles.Dwh;
using Dwapi.ExtractsManagement.Core.Services;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.ExtractsManagement.Infrastructure.Reader;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Reader.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Repository;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Validators.Dwh;
using Dwapi.Hubs.Cbs;
using Dwapi.Hubs.Dwh;
using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SettingsManagement.Core.Services;
using Dwapi.SettingsManagement.Infrastructure;
using Dwapi.SettingsManagement.Infrastructure.Repository;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Infrastructure;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Services;
using Dwapi.UploadManagement.Core.Interfaces.Services.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Services.Dwh;
using Dwapi.UploadManagement.Core.Packager.Cbs;
using Dwapi.UploadManagement.Core.Packager.Dwh;
using Dwapi.UploadManagement.Core.Services.Cbs;
using Dwapi.UploadManagement.Core.Services.Dwh;
using Dwapi.UploadManagement.Core.Services.Psmart;
using Dwapi.UploadManagement.Infrastructure.Data;
using Dwapi.UploadManagement.Infrastructure.Reader.Cbs;
using Dwapi.UploadManagement.Infrastructure.Reader.Dwh;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using StructureMap;
using Swashbuckle.AspNetCore.Swagger;

namespace Dwapi
{
    public class Startup
    {
        public static IConfiguration Configuration;
        public IServiceCollection Service;
        public static IServiceProvider ServiceProvider;
        public static IHubContext<ExtractActivity> HubContext;
        public static IHubContext<DwhSendActivity> DwhSendHubContext;
        public static IHubContext<CbsActivity> CbsHubContext;
        public static IHubContext<CbsSendActivity> CbsSendHubContext;


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

            var assemblyNames = Assembly.GetEntryAssembly().GetReferencedAssemblies();
            List<Assembly> assemblies = new List<Assembly>();
            foreach (var assemblyName in assemblyNames)
            {
                assemblies.Add(Assembly.Load(assemblyName));
            }

            services.AddMediatR(assemblies);
            

            
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
            

            services.AddMvc()
              .AddMvcOptions(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()))
              .AddJsonOptions(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            services.ConfigureWritable<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            var connectionString = Startup.Configuration["ConnectionStrings:DwapiConnection"];
            DatabaseProvider provider = (DatabaseProvider)Convert.ToInt32(Configuration["ConnectionStrings:Provider"]);

            try
            {
                if (provider == DatabaseProvider.MySql)
                {
                    services.AddDbContext<SettingsContext>(o => o.UseMySql(connectionString,x => x.MigrationsAssembly(typeof(SettingsContext).GetTypeInfo().Assembly.GetName().Name)));
                    services.AddDbContext<ExtractsContext>(o => o.UseMySql(connectionString, x => x.MigrationsAssembly(typeof(ExtractsContext).GetTypeInfo().Assembly.GetName().Name)));
                    services.AddDbContext<UploadContext>(o => o.UseMySql(connectionString, x => x.MigrationsAssembly(typeof(UploadContext).GetTypeInfo().Assembly.GetName().Name)));
                }

                if (provider == DatabaseProvider.MsSql)
                {
                    services.AddDbContext<SettingsContext>(o => o.UseSqlServer(connectionString,x => x.MigrationsAssembly(typeof(SettingsContext).GetTypeInfo().Assembly.GetName().Name)));
                    services.AddDbContext<ExtractsContext>(o => o.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(ExtractsContext).GetTypeInfo().Assembly.GetName().Name)));
                    services.AddDbContext<UploadContext>(o => o.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(UploadContext).GetTypeInfo().Assembly.GetName().Name)));
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Connections not Initialized");
            }


            services.AddTransient<ExtractsContext>();
            services.AddScoped<ICentralRegistryRepository, CentralRegistryRepository>();
            services.AddScoped<IEmrSystemRepository, EmrSystemRepository>();
            services.AddScoped<IDocketRepository, DocketRepository>();
            services.AddScoped<IDatabaseProtocolRepository, DatabaseProtocolRepository>();
            services.AddScoped<IExtractRepository, ExtractRepository>();
            services.AddScoped<IPsmartStageRepository, PsmartStageRepository>();
            services.AddTransient<IExtractHistoryRepository, ExtractHistoryRepository>();
            services.AddScoped<ITempPatientExtractRepository, TempPatientExtractRepository>();
            services.AddScoped<ITempPatientArtExtractRepository, TempPatientArtExtractRepository>();
            services.AddScoped<ITempPatientBaselinesExtractRepository, TempPatientBaselinesExtractRepository>();
            services.AddScoped<ITempPatientLaboratoryExtractRepository, TempPatientLaboratoryExtractRepository>();
            services.AddScoped<ITempPatientPharmacyExtractRepository, TempPatientPharmacyExtractRepository>();
            services.AddScoped<ITempPatientStatusExtractRepository, TempPatientStatusExtractRepository>();
            services.AddScoped<ITempPatientVisitExtractRepository, TempPatientVisitExtractRepository>();
            services.AddScoped<IValidatorRepository, ValidatorRepository>();
            services.AddScoped<IPatientExtractRepository, PatientExtractRepository>();
            services.AddScoped<IPatientArtExtractRepository, PatientArtExtractRepository>();
            services.AddScoped<IPatientBaselinesExtractRepository, PatientBaselinesExtractRepository>();
            services.AddScoped<IPatientLaboratoryExtractRepository, PatientLaboratoryExtractRepository>();
            services.AddScoped<IPatientPharmacyExtractRepository, PatientPharmacyExtractRepository>();
            services.AddScoped<IPatientStatusExtractRepository, PatientStatusExtractRepository>();
            services.AddScoped<IPatientVisitExtractRepository, PatientVisitExtractRepository>();
            services.AddScoped<ITempPatientExtractErrorSummaryRepository, TempPatientExtractErrorSummaryRepository>();
            services.AddScoped<ITempPatientArtExtractErrorSummaryRepository, TempPatientArtExtractErrorSummaryRepository>();
            services.AddScoped<ITempPatientBaselinesExtractErrorSummaryRepository, TempPatientBaselinesExtractErrorSummaryRepository>();
            services.AddScoped<ITempPatientLaboratoryExtractErrorSummaryRepository, TempPatientLaboratoryExtractErrorSummaryRepository>();
            services.AddScoped<ITempPatientPharmacyExtractErrorSummaryRepository, TempPatientPharmacyExtractErrorSummaryRepository>();
            services.AddScoped<ITempPatientStatusExtractErrorSummaryRepository, TempPatientStatusExtractErrorSummaryRepository>();
            services.AddScoped<ITempPatientVisitExtractErrorSummaryRepository, TempPatientVisitExtractErrorSummaryRepository>();

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

            services.AddScoped<IExtractSourceReader, ExtractSourceReader>();
            services.AddScoped<IPatientSourceExtractor, PatientSourceExtractor>();
            services.AddScoped<IPatientArtSourceExtractor, PatientArtSourceExtractor>();
            services.AddScoped<IPatientBaselinesSourceExtractor, PatientBaselinesSourceExtractor>();
            services.AddScoped<IPatientLaboratorySourceExtractor, PatientLaboratorySourceExtractor>();
            services.AddScoped<IPatientPharmacySourceExtractor, PatientPharmacySourceExtractor>();
            services.AddScoped<IPatientStatusSourceExtractor, PatientStatusSourceExtractor>();
            services.AddScoped<IPatientVisitSourceExtractor, PatientVisitSourceExtractor>();
            services.AddScoped<IExtractValidator, ExtractValidator>();
            services.AddScoped<IPatientLoader, PatientLoader>();
            services.AddScoped<IPatientArtLoader, PatientArtLoader>();
            services.AddScoped<IPatientBaselinesLoader, PatientBaselinesLoader>();
            services.AddScoped<IPatientLaboratoryLoader, PatientLaboratoryLoader>();
            services.AddScoped<IPatientPharmacyLoader, PatientPharmacyLoader>();
            services.AddScoped<IPatientStatusLoader, PatientStatusLoader>();
            services.AddScoped<IPatientVisitLoader, PatientVisitLoader>();
            services.AddScoped<IClearDwhExtracts, ClearDwhExtracts>();

            services.AddScoped<IMasterPatientIndexReader, MasterPatientIndexReader>();
            services.AddScoped<IMasterPatientIndexSourceExtractor, MasterPatientIndexSourceExtractor>();
            services.AddScoped<IMasterPatientIndexValidator,MasterPatientIndexValidator>();
            services.AddScoped<IMasterPatientIndexLoader, MasterPatientIndexLoader>();
            services.AddScoped<ICleanCbsExtracts, CleanCbsExtracts>();
            services.AddScoped<ICbsExtractReader, CbsExtractReader>();
            services.AddScoped<ICbsSendService, CbsSendService>();
            services.AddScoped<ICbsPackager, CbsPackager>();

            services.AddScoped<IDwhExtractReader, DwhExtractReader>();
            services.AddScoped<IDwhPackager, DwhPackager>();
            services.AddScoped<IDwhSendService, DwhSendService>();

            services.AddScoped<IAppDatabaseManager, AppDatabaseManager>();

            var container = new Container();
            container.Populate(services);
            ServiceProvider = container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            //ServiceProvider = serviceProvider;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseCors(
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials())
                .UseStaticFiles()
                .UseWebSockets();

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


            


            app.UseSignalR(
                routes =>
                {
                    routes.MapHub<ExtractActivity>($"/{nameof(ExtractActivity).ToLower()}");
                    routes.MapHub<CbsActivity>($"/{nameof(CbsActivity).ToLower()}");
                    routes.MapHub<DwhSendActivity>($"/{nameof(DwhSendActivity).ToLower()}");
                    routes.MapHub<CbsSendActivity>($"/{nameof(CbsSendActivity).ToLower()}");
                }
            );


            Mapper.Initialize(cfg =>
                {
                    cfg.AddDataReaderMapping();
                    cfg.AddProfile<TempExtractProfile>();
                    cfg.AddProfile<TempMasterPatientIndexProfile>();
                }
            );

            DomainEvents.Init();

            Log.Debug(@"initializing Database [Complete]");
            Log.Debug(
                @"---------------------------------------------------------------------------------------------------");
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
            Log.Debug(
                @"---------------------------------------------------------------------------------------------------");
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
