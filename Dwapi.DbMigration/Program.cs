using DbUp;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;
using System.Reflection;
using MySql.Data.MySqlClient; 

namespace Dwapi.DbMigration
{
    class Program
    {
        static IConfigurationRoot Config { get; } = new ConfigurationBuilder()
           .AddJsonFile("settings.json")
           .Build();

        static int Main(string[] args)
        {
            var connectionString = Config["connectionStrings:dwapi"];
            bool interactive = false;
            if (bool.TryParse(Config["runner:interactive"], out bool value))
                interactive = value;

            //var schemaName = GetDatabaseName(connectionString);
            var schemaName = "dwapiremote";
            // DeployChanges.To.MySqlDatabase()
            using (MySqlConnection con = new MySqlConnection(connectionString)) {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("CREATE DATABASE IF NOT EXISTS dwapiremote", con);
                cmd.ExecuteNonQuery();
            };

            // Add newly created database name 
            MySqlConnectionStringBuilder cb = new MySqlConnectionStringBuilder(connectionString);
            cb.Database = schemaName;
            connectionString = cb.ConnectionString.ToString(); 


                var upgrader =
                        DeployChanges.To.MySqlDatabase(connectionString, schemaName)
                            .LogToConsole()
                            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                            .LogScriptOutput()
                            .LogToConsole()
                            .Build();
        
            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                if (interactive)
                    Console.ReadLine();
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            if (interactive)
                Console.ReadKey();
            return 0;
        }

        static string GetDatabaseName(string connectionString)
        {
            var builder = new DbConnectionStringBuilder();
            builder.ConnectionString = connectionString;
            if (builder.TryGetValue("initial catalog", out var value))
                return (string)value;
            if (builder.TryGetValue("database", out value))
                return (string)value;
            if (builder.TryGetValue("schema", out value))
                return (string)value;
            throw new ArgumentException($"Unable to find db name, checked for [initial catalog], [database] & [schema] variables");
        }


    }
}
