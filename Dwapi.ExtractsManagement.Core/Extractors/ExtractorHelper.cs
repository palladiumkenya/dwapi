using Dwapi.SharedKernel.Model;
using NPoco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Extractors
{
    internal static class ExtractorHelper
    {
        internal static Func<IDatabase> NPocoDataFactory(DbProtocol dbProtocol)
        {
            Func<IDatabase> _databaseFactory;
            switch (dbProtocol.DatabaseType)
            {
                case SharedKernel.Enum.DatabaseType.MicrosoftSQL:
                    _databaseFactory = ()
                        => new Database(dbProtocol.GetConnectionString(), NPoco.DatabaseType.SqlServer2012, SqlClientFactory.Instance) { CommandTimeout = 3600 };
                    break;

                case SharedKernel.Enum.DatabaseType.MySQL:
                    _databaseFactory = ()
                        => new Database(dbProtocol.GetConnectionString(), NPoco.DatabaseType.MySQL, SqlClientFactory.Instance) { CommandTimeout = 3600 };
                    break;

                default:
                    throw new InvalidOperationException();
            }
            return _databaseFactory;
        }
    }
}
