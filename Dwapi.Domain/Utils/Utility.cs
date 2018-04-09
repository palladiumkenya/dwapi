using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;

namespace Dwapi.Domain.Utils
{
    public static class Utility
    {
        //private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> fullBatch, int chunkSize)
        {
            if (chunkSize <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    "chunkSize",
                    chunkSize,
                    "Chunk size cannot be less than or equal to zero.");
            }

            if (fullBatch == null)
            {
                throw new ArgumentNullException("fullBatch", "Input to be split cannot be null.");
            }

            var cellCounter = 0;
            var chunk = new List<T>(chunkSize);

            foreach (var element in fullBatch)
            {
                if (cellCounter++ == chunkSize)
                {
                    yield return chunk;
                    chunk = new List<T>(chunkSize);
                    cellCounter = 1;
                }

                chunk.Add(element);
            }

            yield return chunk;
        }

        //public static Message CreateMessage(object message)
        //{
        //    Message msmqMessage;

        //    try
        //    {
        //        msmqMessage = new Message();
        //        msmqMessage.Label = Utility.GetMessageType(message.GetType());
        //        var jsonBody = JsonConvert.SerializeObject(message);
        //        msmqMessage.BodyStream = new MemoryStream(Encoding.Default.GetBytes(jsonBody));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Debug(ex);
        //        throw;
        //    }

        //    return msmqMessage;
        //}
        //public static string GetMessageType(Type type)
        //{
        //    return $"{type.FullName}, {type.Assembly.GetName().Name}";
        //}

        //public static object Get(this IDataRecord row, string fieldName, Type type, int ord = -1)
        //{
        //    try
        //    {
        //        int ordinal = ord == -1 ? row.GetOrdinal(fieldName) : ord;
        //        var value = row.IsDBNull(ordinal) ? GetDefault(type) : row.GetValue(ordinal);
        //        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        //        {
        //            if (null == value)
        //                return null;
        //        }
        //        return Convert.ChangeType(value, Nullable.GetUnderlyingType(type) ?? type);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex is IndexOutOfRangeException)
        //        {
        //            //Log.Debug($"Column NOT found:{fieldName}");
        //        }
        //        else
        //        {
        //            Log.Debug(ex);
        //        }
        //    }
        //    return GetDefault(type);
        //}

        public static object GetDefault(Type type)
        {
            if (type == typeof(string))
                return string.Empty;

            if (type == typeof(int))
                return default(int);

            if (type == typeof(int?))
                return default(int?);

            if (type == typeof(decimal))
                return default(decimal);

            if (type == typeof(decimal?))
                return default(decimal?);

            if (type == typeof(DateTime))
                return new DateTime(1900, 1, 1);

            if (type == typeof(DateTime?))
                return default(DateTime?);

            return null;
        }

        public static T Get<T>(this IDataRecord row, string fieldName)
        {
            int ordinal = row.GetOrdinal(fieldName);
            return row.Get<T>(ordinal);
        }

        public static T Get<T>(this IDataRecord row, int ordinal)
        {
            var value = row.IsDBNull(ordinal) ? default(T) : row.GetValue(ordinal);
            return (T)Convert.ChangeType(value, typeof(T));
        }
        public static string GetColumns(List<string> columnList)
        {
            return string.Join(",", columnList.ToArray());
        }
        public static string GetColumns(List<string> columnList, string alias)
        {
            return $"{alias}.{string.Join($",{alias}.", columnList.ToArray())}";
        }
        public static string GetParameters(List<string> columnList)
        {
            return $"@{string.Join(",@", columnList.ToArray())}";
        }
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                if (prop.PropertyType.Name.Contains("Nullable"))
                    tb.Columns.Add(prop.Name, typeof(string));
                else
                    tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        public static string GetFolderPath(string folder)
        {
            return folder.EndsWith("\\") ? folder : $"{folder}\\";
        }


        public static string HasToEndsWith(this string value, string end)
        {
            return value.EndsWith(end) ? value : $"{value}{end}";
        }

        public static string ReplaceFromEnd(this string s, string suffix, string replaceWith, int number = 1)
        {
            var finalString = s;

            if (s.EndsWith(suffix))
            {
                finalString = s.Substring(0, s.Length - number);
                finalString = $@"{finalString}.zip";
            }

            return finalString;
        }

        //public static void ReportStatus(this IProgress<DProgress> progress, string status, decimal? count = null, decimal? total = null, object valueObject = null)
        //{
        //    var dp = DProgress.Report(status);
        //    if (count.HasValue && total.HasValue)
        //    {
        //        decimal percentage = decimal.Divide(count.Value, total.Value) * 100;
        //        dp = DProgress.Report(status, (int)percentage);
        //    }

        //    dp.ValueObject = valueObject;
        //    progress.Report(dp);
        //}

        //public static void AttachValueObjectReportStatus(this IProgress<DProgress> progress, string status, decimal? count = null, decimal? total = null)
        //{
        //    var dp = DProgress.Report(status);

        //    if (count.HasValue && total.HasValue)
        //    {
        //        decimal percentage = decimal.Divide(count.Value, total.Value) * 100;
        //        dp = DProgress.Report(status, (int)percentage);
        //    }

        //    progress.Report(dp);
        //}

        public static int GetPercentage(decimal count, decimal total)
        {
            decimal percentage = decimal.Divide(count, total) * 100;
            return (int)percentage;
        }

        public static string GetErrorMessage(Exception ex)
        {
            if (null == ex.InnerException)
            {
                return ex.Message;
            }
            return ex.InnerException.Message;
        }

        public static string GetTiming(this DateTime? yourDate, string prefix = "")
        {
            if (!yourDate.HasValue)
                return string.Empty;

            if (string.IsNullOrWhiteSpace(prefix))
                return $"{CalculateTiming(yourDate.Value)}";

            return $" {prefix} {CalculateTiming(yourDate.Value)}";
        }
        public static string GetTiming(this DateTime yourDate, string prefix = "")
        {
            if (string.IsNullOrWhiteSpace(prefix))
                return $"{CalculateTiming(yourDate)}";

            return $" {prefix} {CalculateTiming(yourDate)}";
        }
        private static string CalculateTiming(DateTime yourDate)
        {
            const int second = 1;
            const int minute = 60 * second;
            const int hour = 60 * minute;
            const int day = 24 * hour;
            const int month = 30 * day;

            var ts = new TimeSpan(DateTime.Now.Ticks - yourDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * minute)
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * minute)
                return "a minute ago";

            if (delta < 45 * minute)
                return ts.Minutes + " minutes ago";

            if (delta < 90 * minute)
                return "an hour ago";

            if (delta < 24 * hour)
                return ts.Hours + " hours ago";

            if (delta < 48 * hour)
                return "yesterday";

            if (delta < 30 * day)
                return ts.Days + " days ago";

            if (delta < 12 * month)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }

        public static string StoreMessage(object objMessage, string objLocation, string objFile)
        {
            string result;
            objLocation = objLocation.EndsWith(@"\") ? objLocation : $"{objLocation}{@"\"}";
            string msgFile = $"{objLocation}{objFile}.backlog";
            try
            {
                Directory.CreateDirectory(objLocation);
            }
            catch (Exception ex)
            {

                //Log.Debug(ex);
            }

            using (StreamWriter file = File.CreateText(msgFile))
            {
                try
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    serializer.Serialize(file, objMessage);
                    result = $"{msgFile} | OK";
                }
                catch (Exception ex)
                {
                    result = $"{msgFile} | FAIL";
                    //Log.Debug($"Error creating message store {msgFile}");
                    //Log.Debug(ex);
                }
            }

            return result;
        }
    }
}
