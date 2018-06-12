using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Packager;
using Dwapi.ExtractsManagement.Core.Interfaces.Packager.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Packager.Cbs
{
    public class CbsPackager: ICbsPackager
    {
        private readonly IMasterPatientIndexRepository _repository;

        public CbsPackager(IMasterPatientIndexRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Manifest>  Generate()
        {
            var getPks = _repository.GetAll()
                .Select(x => new SitePatientProfile(x.SiteCode, x.FacilityName, x.PatientPk));


            return Manifest.Create(getPks);
        }

        public IEnumerable<MasterPatientIndex> GenerateMpi()
        {
            return _repository.GetAll();
        }
    }
}