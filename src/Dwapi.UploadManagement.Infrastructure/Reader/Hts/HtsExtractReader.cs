using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Hts;
using Dwapi.UploadManagement.Core.Model.Hts;
using Dwapi.UploadManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Dapper;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Infrastructure.Reader.Hts
{
    public class HtsExtractReader:IHtsExtractReader
    {

        private readonly UploadContext _context;

        public HtsExtractReader(UploadContext context)
        {
            _context = context;
        }

        public IEnumerable<HtsClientsExtractView> ReadAllClients()
        {
            return _context.ClientExtracts.Where(x=>!x.IsSent).AsNoTracking();
        }

        public IEnumerable<HtsClientTestsExtractView> ReadAllClientTests()
        {
            return _context.Database.GetDbConnection()
                .Query<HtsClientTestsExtractView>("Select * From HtsClientTestsExtracts")
                .ToList()
                .Where(x => !x.IsSent);
        }
        public IEnumerable<HtsTestKitsExtractView> ReadAllTestKits()
        {
            return _context.Database.GetDbConnection()
                .Query<HtsTestKitsExtractView>("Select * From HtsTestKitsExtracts")
                .ToList()
                .Where(x => !x.IsSent);
            //return _context.TestKitsExtracts.Where(x=>!x.IsSent).AsNoTracking();
        }

        public IEnumerable<HtsClientTracingExtractView> ReadAllClientTracing()
        {
            return _context.Database.GetDbConnection()
                .Query<HtsClientTracingExtractView>("Select * From HtsClientTracingExtracts")
                .ToList()
                .Where(x => !x.IsSent);
            //return _context.ClientTracingExtracts.Where(x => !x.IsSent).AsNoTracking();
        }

        public IEnumerable<HtsPartnerTracingExtractView> ReadAllPartnerTracing()
        {
            return _context.Database.GetDbConnection()
                .Query<HtsPartnerTracingExtractView>("Select * From HtsPartnerTracingExtracts")
                .ToList()
                .Where(x => !x.IsSent);
            //return _context.PartnerTracingExtracts.Where(x => !x.IsSent).AsNoTracking();
        }

        public IEnumerable<HtsPartnerNotificationServicesExtractView> ReadAllPartnerNotificationServices()
        {
            return _context.Database.GetDbConnection()
                .Query<HtsPartnerNotificationServicesExtractView>("Select * From HtsPartnerNotificationServicesExtracts")
                .ToList()
                .Where(x => !x.IsSent);
            //return _context.PartnerNotificationServicesExtracts.Where(x => !x.IsSent).AsNoTracking();
        }
        public IEnumerable<HtsClientsLinkageExtractView> ReadAllClientsLinkage()
        {
            return _context.Database.GetDbConnection()
                .Query<HtsClientsLinkageExtractView>("Select * From HtsClientsLinkageExtracts")
                .ToList()
                .Where(x => !x.IsSent);
            //return _context.ClientLinkageExtracts.Where(x => !x.IsSent).AsNoTracking();
        }

        public IEnumerable<HtsEligibilityExtractView> ReadAllHtsEligibilityExtracts()
        {
            return _context.Database.GetDbConnection()
                .Query<HtsEligibilityExtractView>("Select * From HtsEligibilityExtracts")
                .ToList()
                .Where(x => !x.IsSent);
            //return _context.ClientLinkageExtracts.Where(x => !x.IsSent).AsNoTracking();
        }
        
        public IEnumerable<HtsRiskScoresView> ReadAllHtsRiskScores()
        {
            return _context.Database.GetDbConnection()
                .Query<HtsRiskScoresView>("Select * From HtsRiskScoresExtracts")
                .ToList()
                .Where(x => !x.IsSent);
            //return _context.ClientLinkageExtracts.Where(x => !x.IsSent).AsNoTracking();
        }

        public IEnumerable<Site> GetSites()
        {
            var sql = @"
                SELECT
                    SiteCode,MAX(FacilityName) AS SiteName,Count(Id) AS PatientCount
                FROM
                    HtsClientsExtracts
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
                    HtsClientsExtracts
            ";
            return _context.Database.GetDbConnection()
                .Query<SitePatientProfile>(sql).ToList();
        }
    }
}
