using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
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
        private readonly IPatientExtractRepository _repository;

        public MtsExtractReader(UploadContext context,IPatientExtractRepository repository)
        {
            _context = context;
            _repository = repository;

        }

        public IEnumerable<IndicatorExtractView> ReadAll()
        {
            int sitecode = _repository.GetSiteCode();

            return _context.IndicatorExtracts.Where(x => !x.IsSent && x.SiteCode==sitecode).AsNoTracking();
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
