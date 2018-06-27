using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.SharedKernel.DTOs;

namespace Dwapi.UploadManagement.Core.Interfaces.Services.Cbs
{
    public interface IMpiSearchService
    {
        Task<List<MpiSearchResultDto>> SearchMpiAsync (MpiSearchPackageDto mpiSearchPackage);
    }
}