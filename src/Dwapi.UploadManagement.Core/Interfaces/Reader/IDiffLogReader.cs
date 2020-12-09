using System.Collections.Generic;
using Dwapi.UploadManagement.Core.Model;

namespace Dwapi.UploadManagement.Core.Interfaces.Reader
{
    public interface IDiffLogReader
    {
        IEnumerable<DiffLogView> ReadAll();
    }
}