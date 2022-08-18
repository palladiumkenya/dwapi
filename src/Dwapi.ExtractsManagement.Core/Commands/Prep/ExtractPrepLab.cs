using Dwapi.SharedKernel.Model;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.Commands.Prep
{
    public class ExtractPrepLab: IRequest<bool>{public DbExtract Extract { get; set; }public DbProtocol DatabaseProtocol { get; set; }}
}