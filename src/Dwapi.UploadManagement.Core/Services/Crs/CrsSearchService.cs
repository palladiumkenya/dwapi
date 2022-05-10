using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Interfaces.Services.Crs;
using Serilog;

namespace Dwapi.UploadManagement.Core.Services.Crs
{
    [Obsolete]
    public class CrsSearchService : ICrsSearchService
    {
        private readonly string _endPoint;

        public CrsSearchService()
        {
            _endPoint = "api/crs/";
        }

        public async Task<List<CrsSearchResultDto>> SearchCrsAsync(CrsSearchPackageDto crsSearchPackage)
        {
            var result = new List<CrsSearchResultDto>();
            var client = new HttpClient();
            try
            {
                string url = crsSearchPackage.GetUrl($"{_endPoint.HasToEndsWith("/")}crsSearch");
                var data = crsSearchPackage.CrsSearch;
                var response = await client.PostAsJsonAsync(url, data);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsJsonAsync<List<CrsSearchResultDto>>();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception(error);
                }
            }
            catch (Exception e)
            {
                Log.Error(e, $"Search MPI Error");
                throw;
            }

            return result;
        }

    }
}
