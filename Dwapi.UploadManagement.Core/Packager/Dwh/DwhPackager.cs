using System;
using System.Collections.Generic;
using Dwapi.SharedKernel.Exchange;
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
        public PatientExtractView GenerateExtracts(Guid id)
        {
            return _reader.Read(id);
        }
    }
}