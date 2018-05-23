using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Model;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.Commands
{
    public class ExtractPatient : IRequest
    {
        public DwhExtract Extract { get; set; }
        public DbProtocol DatabaseProtocol { get; set; }
    }
}