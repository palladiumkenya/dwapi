using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Interfaces.Services.Cbs;
using Serilog;

namespace Dwapi.UploadManagement.Core.Services.Cbs
{
    [Obsolete]
    public class MpiSearchService : IMpiSearchService
    {
        private readonly string _endPoint;

        public MpiSearchService()
        {
            _endPoint = "api/cbs/";
        }

        public async Task<List<MpiSearchResultDto>> SearchMpiAsync(MpiSearchPackageDto mpiSearchPackage)
        {
            var result = new List<MpiSearchResultDto>();
            var client = new HttpClient();
            try
            {
                string url = mpiSearchPackage.GetUrl($"{_endPoint.HasToEndsWith("/")}mpiSearch");
                var data = mpiSearchPackage.MpiSearch;
                var response = await client.PostAsJsonAsync(url, data);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsJsonAsync<List<MpiSearchResultDto>>();
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
