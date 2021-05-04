using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Mts;
using Dwapi.UploadManagement.Core.Model.Mts;
using Dwapi.UploadManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.UploadManagement.Infrastructure.Reader.Mts
{
    public class MtsExtractReader : IMtsExtractReader
    {

        private readonly UploadContext _context;

        public MtsExtractReader(UploadContext context)
        {
            _context = context;
        }

        public IEnumerable<IndicatorExtractView> ReadAll()
        {
            return _context.IndicatorExtracts.Where(x => !x.IsSent).AsNoTracking();
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
