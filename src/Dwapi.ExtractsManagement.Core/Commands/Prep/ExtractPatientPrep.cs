using Dwapi.SharedKernel.Model;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.Commands.Prep
{
    public class ExtractPatientPrep : IRequest<bool>
    {
        public DbExtract Extract { get; set; }
        public DbProtocol DatabaseProtocol { get; set; }

        public bool IsValid()
        {
            return null != Extract && null != DatabaseProtocol;
        }
    }

}
