using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Dwapi.SharedKernel.Utility
{
    public static class Extentions
    {
        public static bool IsSameAs(this string value, string other)
        {
            if (value == null)
                return false;
            return value.Trim().ToLower() == other.Trim().ToLower();
        }

        public static string HasToEndsWith(this string value, string end)
        {
            return value.EndsWith(end) ? value : $"{value}{end}";
        }

        public static string HasToStartWith(this string value, string start)
        {
            return value.StartsWith(start) ? value : $"{start}{value}";
        }

        /// <summary>
        /// Determines if a nullable Guid (Guid?) is null or Guid.Empty
        /// </summary>
        public static bool IsNullOrEmpty(this Guid? guid)
        {
            return !guid.HasValue || guid.Value == Guid.Empty;
        }

        /// <summary>
        /// Determines if Guid is Guid.Empty
        /// </summary>
        public static bool IsNullOrEmpty(this Guid guid)
        {
            return guid == Guid.Empty;
        }

        /// <summary>
        /// Determines if a nullable Date (Date?) is null or Empty
        /// </summary>
        public static bool IsNullOrEmpty(this DateTime? dateTime)
        {
            DateTime? defDate = null;

            if (null != dateTime || dateTime.HasValue)
                return dateTime == defDate.GetValueOrDefault();

            return null == dateTime || !dateTime.HasValue;
        }

        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(
            this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return httpClient.PostAsync(url, content);
        }

        public static Task<HttpResponseMessage> PostAsCompressedAsync<T>(
            this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(dataAsString);
            MemoryStream ms = new MemoryStream();
            using (GZipStream gzip = new GZipStream(ms, CompressionMode.Compress, true))
            {
                gzip.Write(jsonBytes, 0, jsonBytes.Length);
            }
            ms.Position = 0;
            StreamContent content = new StreamContent(ms);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            content.Headers.ContentEncoding.Add("gzip");
            return httpClient.PostAsync(url, content);
        }

        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            var dataAsString = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(dataAsString);
        }
        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new {Index = i, Value = x})
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        public static string GetFolderPath(string folder)
        {
            return folder.EndsWith("\\") ? folder : $"{folder}\\";
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

        public static string ToOsStyle(this string value)
        {
            if (value == null)
                return string.Empty;

            if(Environment.OSVersion.Platform==PlatformID.Unix||Environment.OSVersion.Platform==PlatformID.MacOSX)
                return value.Replace(@"\", @"/");

            return value.Replace(@"/", @"\");
        }

        public static string HasToEndWith(this string value, string end)
        {
            return HasToEndsWith(value, end);
        }

        public static DateTime? CastDateTime(dynamic source)
        {
            DateTime dateValue;

            if (string.IsNullOrWhiteSpace(source.ToString()))
                return null;

            if (DateTime.TryParse(source, out dateValue))
                return dateValue as DateTime?;


            return null;
        }

        public static void RemoveService(this ServiceCollection services, Type serviceType)
        {
            var descriptors = services
                .Where(x => x.ServiceType == serviceType)
                .ToList();

            if (descriptors.Any())
            {
                foreach (var descriptor in descriptors)
                {
                    services.Remove(descriptor);
                }
            }

        }
    }
}
