using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.SharedKernel.DTOs;

namespace Dwapi.UploadManagement.Core.Interfaces.Services.Crs
{
    public interface ICrsSearchService
    {
        Task<List<CrsSearchResultDto>> SearchCrsAsync (CrsSearchPackageDto crsSearchPackage);
    }
}