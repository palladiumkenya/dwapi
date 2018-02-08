using Microsoft.EntityFrameworkCore;

namespace Dwapi.SharedKernel.Infrastructure.Tests.TestHelpers
{
    public class TestDbOptions
    {
        public static DbContextOptions<T> GetInMemoryOptions<T>(string dbName= "DevDb") where T:DbContext
        {
            var options = new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return options;
        }

        public static DbContextOptions<T> GetOptions<T>(string dbName = "DevDb") where T : DbContext
        {
            /*
           var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();
    
           var connectionString = config["connectionStrings:hAPIConnection"];
    
           var options = new DbContextOptionsBuilder<LiveHAPIContext>()
               .UseSqlServer(connectionString)
               .Options;
            */

            return GetInMemoryOptions<T>();
        }
    }
}