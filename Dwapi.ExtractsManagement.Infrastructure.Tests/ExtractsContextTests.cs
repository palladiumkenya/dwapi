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
    private const string MssqlConnection = "Data Source=.\\koske14;Initial Catalog=dwapidevx;Persist Security Info=True;User ID=sa;Password=maun;MultipleActiveResultSets=True";
    private const string MysqlConnection = "server=127.0.0.1;port=3306;database=dwapidevx;user=root;password=test";

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