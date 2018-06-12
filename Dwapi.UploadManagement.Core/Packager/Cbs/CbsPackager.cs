using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Cbs;

namespace Dwapi.UploadManagement.Core.Packager.Cbs
{
    public class CbsPackager: ICbsPackager
    {
        private readonly ICbsExtractReader _cbsExtractReader;

        public CbsPackager(ICbsExtractReader cbsExtractReader)
        {
            _cbsExtractReader = cbsExtractReader;
        }


        public IEnumerable<Manifest>  Generate()
        {
            var getPks = _cbsExtractReader.ReadAll()
                .Select(x => new SitePatientProfile(x.SiteCode, x.FacilityName, x.PatientPk));


            return Manifest.Create(getPks);
        }

        public IEnumerable<MasterPatientIndex> GenerateMpi()
        {
            return _cbsExtractReader.ReadAll();
        }
    }
}