using System;
using System.Data;
using Dapper;

namespace Dwapi.SharedKernel.Infrastructure.Tests.TestHelpers
{
    public class NullableIntHandler : SqlMapper.TypeHandler<int?>
    {
        public override void SetValue(IDbDataParameter parameter, int? value)
        {
            if (value.HasValue)
                parameter.Value = value.Value;
            else
                parameter.Value = DBNull.Value;
        }

        public override int? Parse(object value)
        {
            if (value == null || value is DBNull) return null;
            return Convert.ToInt32(value);
        }
    }
}