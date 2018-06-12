using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Packager.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Packager.Dwh
{
    public class DwhPackager : IDwhPackager
    {
        private readonly IPatientExtractRepository _repository;

        public DwhPackager(IPatientExtractRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<DwhManifest> Generate()
        {

            var getPks = _repository.GetAll()
                .Select(x => new SitePatientProfile(x.SiteCode, x.FacilityName, x.PatientPK));


            return DwhManifest.Create(getPks);
        }
    }
}