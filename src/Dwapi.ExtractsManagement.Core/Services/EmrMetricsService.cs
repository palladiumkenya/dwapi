using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.ExtractsManagement.Core.Model.Destination;
using Dwapi.ExtractsManagement.Core.Model.Source;
using Dwapi.ExtractsManagement.Core.Profiles;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Services
{
    public class EmrMetricsService:IEmrMetricsService
    {
        private HttpClient _httpClient;
        private readonly IEmrMetricRepository _emrMetricRepository;

        public HttpClient Client
        {
            get { return _httpClient; }
            set { _httpClient = value; }
        }

        public EmrMetricsService(IEmrMetricRepository emrMetricRepository)
        {
            _emrMetricRepository = emrMetricRepository;
            _httpClient=new HttpClient();
        }

        public async Task<EmrMetricSource> Read(AuthProtocol authProtocol, string url, bool diffSupport)
        {
            EmrMetricSource metricSoruce = null;
            string metricsUrl=string.Empty;
            var mapper = diffSupport ? ExtractDiffMapper.Instance : ExtractMapper.Instance;

            if (authProtocol.HasAuth)
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{authProtocol.UserName}:{authProtocol.Password}")));
            }

            if (authProtocol.HasToken)
            {
                _httpClient.DefaultRequestHeaders.Add("Token", authProtocol.AuthToken);
            }

            try
            {
                 metricsUrl = GetUrl(authProtocol.Url, url);
                var response = await _httpClient.GetAsync(metricsUrl);
                if (response.IsSuccessStatusCode)
                {
                    metricSoruce = await response.Content.ReadAsJsonAsync<EmrMetricSource>();
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception e)
            {
                Log.Error(new string('-',40));
                Log.Error($"Error Reading metrics on [{metricsUrl}]");
                Log.Error(e.Message,"Metrics Error");
                Log.Error(new string('-',40));
            }

            if (null != metricSoruce)
            {
                try
                {
                    var metric = mapper.Map<EmrMetricSource, EmrMetric>(metricSoruce);
                    _emrMetricRepository.CreateOrUpdate(metric);

                }
                catch (Exception e)
                {
                    Log.Error(e, "Error Saving metrics");
                }
            }

            return metricSoruce;
        }

        private string GetUrl(string baseUrl, string endpoint)
        {
            return $"{baseUrl.HasToEndsWith("/")}{endpoint}";
        }
    }
}
