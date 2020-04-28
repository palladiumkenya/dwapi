using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dwapi.SharedKernel.Model;
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
            return _context.ClientMasterPatientIndices.Where(x=>!x.IsSent).AsNoTracking();
        }

        public IEnumerable<Site> GetSites()
        {
            var sql = @"
                SELECT
                    SiteCode,MAX(FacilityName) AS SiteName,Count(Id) AS PatientCount
                FROM
                    MasterPatientIndices
                GROUP BY
                    SiteCode
            ";
            return _context.Database.GetDbConnection()
                .Query<Site>(sql).ToList();
        }

        public IEnumerable<SitePatientProfile> GetSitePatientProfiles()
        {
            var sql = @"
                SELECT
                   SiteCode,FacilityName AS SiteName,PatientPk
                FROM
                    MasterPatientIndices
            ";
            return _context.Database.GetDbConnection()
                .Query<SitePatientProfile>(sql).ToList();
        }
    }
}
