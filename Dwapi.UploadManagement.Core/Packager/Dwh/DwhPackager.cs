using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Packager.Dwh
{
    public class DwhPackager : IDwhPackager
    {
        private readonly IDwhExtractReader _reader;

        public DwhPackager(IDwhExtractReader reader)
        {
            _reader = reader;
        }


        public IEnumerable<DwhManifest> Generate()
        {
            var patientProfiles = _reader.ReadProfiles();

            return DwhManifest.Create(patientProfiles);
        }

        public IEnumerable<PatientExtractView> GenerateExtracts()
        {
            return _reader.ReadAll();
        }
    }
}