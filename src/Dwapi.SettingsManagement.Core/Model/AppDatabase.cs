using Dwapi.SharedKernel.Enum;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class AppDatabase
    {
        public DatabaseProvider Provider { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public uint Port { get; set; }

        public AppDatabase()
        {
        }

        public AppDatabase(string server, string database, string user, string password, DatabaseProvider provider)
        {
            Server = server;
            Database = database;
            User = user;
            Password = password;
            Provider = provider;
        }

        public AppDatabase(string server, string database, string user, string password, DatabaseProvider provider,
            uint port)
            : this(server, database, user, password, provider)
        {
            Port = port;
        }

        public AppDatabase(string database, DatabaseProvider provider)
        {
            Database = database;
            Provider = provider;
        }

        public override string ToString()
        {
            return $"{Provider} | {Server}{(Port > 0 ? $":{Port}" : "")}";
        }
    }
}
