using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Infrastructure.Tests
{
  [TestFixture]
  [Category("Db")]
  public class ExtractsContextTests
  {
    [Test]
    public void should_Setup_Mssql_Database()
    {
      TestInitializer.InitDb();
      var ctx =TestInitializer.ServiceProvider.GetService<ExtractsContext>();

      ctx.Database.Migrate();
      ctx.EnsureSeeded();

      Assert.True(ctx.Validator.Any());

      Console.WriteLine(ctx.Database.ProviderName);
    }

    [Test]
    public void should_Setup_MySql_Database()
    {
      TestInitializer.InitMysQLDb();
      var ctx = TestInitializer.ServiceProviderMysql.GetService<ExtractsContext>();

      ctx.Database.Migrate();
      ctx.EnsureSeeded();

      Assert.True(ctx.Validator.Any());

      Console.WriteLine(ctx.Database.ProviderName);
    }
  }
}
