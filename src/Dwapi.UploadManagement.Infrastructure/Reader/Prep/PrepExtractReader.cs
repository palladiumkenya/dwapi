using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Prep;
using Dwapi.UploadManagement.Core.Model.Prep;
using Dwapi.UploadManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Dapper;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Infrastructure.Reader.Prep
{
    public class PrepExtractReader : IPrepExtractReader
    {

        private readonly UploadContext _context;

        public PrepExtractReader(UploadContext context)
        {
            _context = context;
        }

        public IEnumerable<Site> GetSites()
        {
            var sql = @"
                SELECT
                    SiteCode,MAX(FacilityName) AS SiteName,Count(Id) AS PatientCount
                FROM
                    PatientPrepExtracts
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
                    PatientPrepExtracts
            ";
            return _context.Database.GetDbConnection()
                .Query<SitePatientProfile>(sql).ToList();
        }

        public IEnumerable<PatientPrepExtractView> ReadAllPatientPreps()
        {
            return _context.Database.GetDbConnection()
                .Query<PatientPrepExtractView>("Select * From PatientPrepExtracts").ToList().Where(x => !x.IsSent);
        }

        public IEnumerable<PrepAdverseEventExtractView> readAllAdverseEventExtracts()
        {
            return _context.Database.GetDbConnection().Query<PrepAdverseEventExtractView>("Select * From PrepAdverseEventExtracts")
                .ToList().Where(x => !x.IsSent);
        }

        public IEnumerable<PrepBehaviourRiskExtractView> readAllPrepBehaviourRisks()
        {
            return _context.Database.GetDbConnection().Query<PrepBehaviourRiskExtractView>("Select * From PrepBehaviourRiskExtracts")
                .ToList().Where(x => !x.IsSent);
        }

        public IEnumerable<PrepCareTerminationExtractView> readAllPrepCareTerminations()
        {
            return _context.Database.GetDbConnection().Query<PrepCareTerminationExtractView>("Select * From PrepCareTerminationExtracts")
                .ToList().Where(x => !x.IsSent);
        }

        public IEnumerable<PrepLabExtractView> ReadAllPrepLabs()
        {
            return _context.Database.GetDbConnection().Query<PrepLabExtractView>("Select * From PrepLabExtracts")
                .ToList().Where(x => !x.IsSent);
        }

        public IEnumerable<PrepPharmacyExtractView> readAllPrepPharmacys()
        {
            return _context.Database.GetDbConnection().Query<PrepPharmacyExtractView>("Select * From PrepPharmacyExtracts")
                .ToList().Where(x => !x.IsSent);
        }

        public IEnumerable<PrepVisitExtractView> ReadAllPrepVisits()
        {
            return _context.Database.GetDbConnection().Query<PrepVisitExtractView>("Select * From PrepVisitExtracts")
                .ToList().Where(x => !x.IsSent);
        }



    }
}
