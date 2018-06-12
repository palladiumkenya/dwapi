using System;
using System.Collections.Generic;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Interfaces.Reader.Dwh
{
    public interface IDwhExtractReader
    {
        IEnumerable<SitePatientProfile> ReadProfiles();
        IEnumerable<PatientExtractView> ReadAll();
        PatientExtractView Read(Guid id);

    }
}