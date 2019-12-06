using System;
using System.Linq;
using Dwapi.SettingsManagement.Core.Application.Metrics.Queries;
using Dwapi.SettingsManagement.Core.Application.Metrics.Queries.Handlers;
using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SettingsManagement.Infrastructure;
using Dwapi.SettingsManagement.Infrastructure.Repository;
using Dwapi.SharedKernel.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Z.Dapper.Plus;

namespace Dwapi.SettingsManagement.Core.Tests
{
    [SetUpFixture]
    public class TestInitializer
    {
        public static IServiceProvider ServiceProvider;

        [OneTimeSetUp]
        public void Setup()
        {
            ServiceProvider = new ServiceCollection()
                .AddDbContext<SettingsContext>(x => x.UseInMemoryDatabase("demodwapi"))
                .AddTransient<IAppMetricRepository, AppMetricRepository>()
                .AddMediatR(typeof(GetAppMetricHandler))
                .BuildServiceProvider();
        }

    }
}
