using System.Collections.Generic;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.UploadManagement.Core.Model.Cbs;
using Dwapi.UploadManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.UploadManagement.Infrastructure.Reader.Cbs
{
    public class CbsExtractReader:ICbsExtractReader
    {

        private readonly UploadContext _context;

        public CbsExtractReader(UploadContext context)
        {
            _context = context;
        }

        public IEnumerable<MasterPatientIndexView> ReadAll()
        {
            return _context.ClientMasterPatientIndices.AsNoTracking();
        }
    }
}