using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Crs;
using Dwapi.UploadManagement.Core.Model.Crs;
using Dwapi.UploadManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.UploadManagement.Infrastructure.Reader.Crs
{
    public class CrsExtractReader:ICrsExtractReader
    {

        private readonly UploadContext _context;

        public CrsExtractReader(UploadContext context)
        {
            _context = context;
        }

        public IEnumerable<ClientRegistryExtractView> ReadAll()
        {
            return _context.ClientClientRegistryExtracts.Where(x=>!x.IsSent).AsNoTracking();
        }

        public IEnumerable<Site> GetSites()
        {
            var sql = @"
                SELECT
                    SiteCode,MAX(FacilityName) AS SiteName,Count(Id) AS PatientCount
                FROM
                    ClientRegistryExtracts
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
                    ClientRegistryExtracts
            ";
            return _context.Database.GetDbConnection()
                .Query<SitePatientProfile>(sql).ToList();
        }
    }
}
