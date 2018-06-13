using System.Collections.Generic;
using Dwapi.UploadManagement.Core.Model.Cbs;

namespace Dwapi.UploadManagement.Core.Interfaces.Reader.Cbs
{
    public interface ICbsExtractReader
    {
        IEnumerable<MasterPatientIndexView> ReadAll();
    }
}