using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Mgs;
using Dwapi.UploadManagement.Core.Model.Mgs;
using Dwapi.UploadManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Dapper;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Infrastructure.Reader.Mgs
{
    public class MgsExtractReader : IMgsExtractReader
    {

        private readonly UploadContext _context;

        public MgsExtractReader(UploadContext context)
        {
            _context = context;
        }

        public IEnumerable<MetricMigrationExtractView> ReadAllMigrations()
        {
            return _context.MetricMigrationExtracts.Where(x => !x.IsSent).AsNoTracking();
        }

        public IEnumerable<Site> GetSites()
        {
            var sql = @"
                SELECT
                    SiteCode,MAX(FacilityName) AS SiteName,Count(Id) AS MetricCount
                FROM
                    MetricMigrationExtracts
                GROUP BY
                    SiteCode
            ";
            return _context.Database.GetDbConnection()
                .Query<Site>(sql).ToList();
        }

        public IEnumerable<SiteMetricProfile> GetSitePatientProfiles()
        {
            var sql = @"
                SELECT
                   SiteCode,FacilityName AS SiteName,MetricId
                FROM
                    MetricMigrationExtracts
            ";
            return _context.Database.GetDbConnection()
                .Query<SiteMetricProfile>(sql).ToList();
        }
    }
}
