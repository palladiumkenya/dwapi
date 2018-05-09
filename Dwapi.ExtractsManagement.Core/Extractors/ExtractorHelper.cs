using Dwapi.SharedKernel.Model;
using Microsoft.EntityFrameworkCore;
using NPoco;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Extractors
{
    internal static class ExtractorHelper
    {
        internal static Func<IDatabase> NPocoEmrDataFactory(DbProtocol dbProtocol)
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

        internal static Func<IDatabase> NpocoDwapiDataFactory(DbConnection dbConnection)
        {
            Func<IDatabase> _databaseFactory;
            _databaseFactory = () 
                => new Database(dbConnection, NPoco.DatabaseType.SqlServer2012) { CommandTimeout = 3600};
            return _databaseFactory;
        }

        internal static DbConnection GetDbConnection(DbProtocol dbProtocol)
            => new SqlConnection(dbProtocol.GetConnectionString());

    }
}
