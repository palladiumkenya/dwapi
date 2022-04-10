using Dwapi.SharedKernel.Model;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.Commands.Prep
{
    public class ExtractPrepPharmacy: IRequest<bool>{public DbExtract Extract { get; set; }public DbProtocol DatabaseProtocol { get; set; }}
}