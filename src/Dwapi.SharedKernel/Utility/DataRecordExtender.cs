using System;
using System.Data;
using Serilog;

namespace Dwapi.SharedKernel.Utility
{
    public static class DataRecordExtender
    {
        public static string GetStringOrDefault(this IDataRecord reader, string columnName)
        {
            object columnValue = reader[reader.GetOrdinal(columnName)];
            if (!(columnValue is DBNull))
            {
                return columnValue.ToString();
            }

            return null;
        }

        public static int? GetNullIntOrDefault(this IDataRecord reader, string columnName)
        {
            object columnValue = reader[reader.GetOrdinal(columnName)];
            if (!(columnValue is DBNull))
            {
                int o;
                if (int.TryParse(columnValue.ToString(), out o))
                    return o;
                return null;
            }

            return null;
        }

        public static decimal? GetNullDecimalOrDefault(this IDataRecord reader, string columnName)
        {
            object columnValue = reader[reader.GetOrdinal(columnName)];
            if (!(columnValue is DBNull))
            {
                decimal o;
                if (decimal.TryParse(columnValue.ToString(), out o))
                    return o;
                return null;
            }

            return null;
        }

        public static DateTime? GetNullDateOrDefault(this IDataRecord reader, string columnName)
        {
            object columnValue = reader[reader.GetOrdinal(columnName)];
            if (!(columnValue is DBNull))
            {
                DateTime o;
                if (DateTime.TryParse(columnValue.ToString(), out o))
                    return o;
                return null;
            }



            return null;
        }

        public static DateTime? GetOptionalNullDateOrDefault(this IDataRecord reader, string columnName)
        {
            try
            {
                return GetNullDateOrDefault(reader, columnName);
            }
            catch (Exception e)
            {
                // Log.Warning(e.Message);
            }

            return null;
        }

        public static int? GetOptionalNullIntOrDefault(this IDataRecord reader, string columnName)
        {
            try
            {
                return GetNullIntOrDefault(reader, columnName);
            }
            catch (Exception e)
            {
                // Log.Warning(e.Message);
            }

            return null;
        }

        public static string GetOptionalStringOrDefault(this IDataRecord reader, string columnName)
        {
            try
            {
                return GetStringOrDefault(reader, columnName);
            }
            catch (Exception e)
            {
              // Log.Warning(e.Message);
            }
            return null;
        }

        public static decimal? GetOptionalNullDecimalOrDefault(this IDataRecord reader, string columnName)
        {
            try
            {
                return GetNullDecimalOrDefault(reader, columnName);
            }
            catch (Exception e)
            {
                // Log.Warning(e.Message);
            }
            return null;
        }

        public static bool HasColumn(this IDataRecord dr, string columnName)
        {
            for (int i=0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

    }
}
