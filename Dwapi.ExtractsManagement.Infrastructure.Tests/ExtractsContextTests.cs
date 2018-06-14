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

    private IServiceProvider _serviceProvider;
    private IServiceProvider _serviceProviderMysql;
    private const string MssqlConnection = "Data Source=.\\sqlexpress;Initial Catalog=dwapidevx;Persist Security Info=True;User ID=sa;Password=c0nstella;MultipleActiveResultSets=True";
    private const string MysqlConnection = "server=localhost;port=3306;database=dwapiremote;user=root;password=MySQL";

    [OneTimeSetUp]
    public void Init()
    {
      _serviceProvider = new ServiceCollection()
          .AddDbContext<ExtractsContext>(x => x.UseSqlServer(MssqlConnection))
          .BuildServiceProvider();

      _serviceProviderMysql = new ServiceCollection()
          .AddDbContext<ExtractsContext>(x => x.UseMySql(MysqlConnection))
          .BuildServiceProvider();
    }

    [Test]
    public void should_Setup_Mssql_Database()
    {
      var ctx = _serviceProvider.GetService<ExtractsContext>();

      ctx.Database.Migrate();
      ctx.EnsureSeeded();

      Assert.True(ctx.Validator.Any());
      
      Console.WriteLine(ctx.Database.ProviderName);
    }
    [Test]
    public void should_Setup_MySql_Database()
    {
      var ctx = _serviceProviderMysql.GetService<ExtractsContext>();

      ctx.Database.Migrate();
      ctx.EnsureSeeded();

      Assert.True(ctx.Validator.Any());
     
      Console.WriteLine(ctx.Database.ProviderName);
    }
  }
}