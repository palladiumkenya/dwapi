using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Mnch;
using Dwapi.UploadManagement.Core.Model.Mnch;
using Dwapi.UploadManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Dapper;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Infrastructure.Reader.Mnch
{
    public class MnchExtractReader : IMnchExtractReader
    {

        private readonly UploadContext _context;

        public MnchExtractReader(UploadContext context)
        {
            _context = context;
        }

        public IEnumerable<Site> GetSites()
        {
            var sql = @"
                SELECT
                    SiteCode,MAX(FacilityName) AS SiteName,Count(Id) AS PatientCount
                FROM
                    PatientMnchExtracts
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
                    PatientMnchExtracts
            ";
            return _context.Database.GetDbConnection()
                .Query<SitePatientProfile>(sql).ToList();
        }

        public IEnumerable<PatientMnchExtractView> ReadAllPatientMnchs()
        {
            return _context.Database.GetDbConnection()
                .Query<PatientMnchExtractView>("Select * From PatientMnchExtracts").ToList().Where(x => !x.IsSent);
        }

        public IEnumerable<MnchEnrolmentExtractView> ReadAllMnchEnrolments()
        {
            return _context.Database.GetDbConnection()
                .Query<MnchEnrolmentExtractView>("Select * From MnchEnrolmentExtracts").ToList().Where(x => !x.IsSent);
        }

        public IEnumerable<MnchArtExtractView> ReadAllMnchArts()
        {
            return _context.Database.GetDbConnection().Query<MnchArtExtractView>("Select * From MnchArtExtracts")
                .ToList().Where(x => !x.IsSent);
        }

        public IEnumerable<AncVisitExtractView> ReadAllAncVisits()
        {
            return _context.Database.GetDbConnection().Query<AncVisitExtractView>("Select * From AncVisitExtracts")
                .ToList().Where(x => !x.IsSent);
        }

        public IEnumerable<MatVisitExtractView> ReadAllMatVisits()
        {
            return _context.Database.GetDbConnection().Query<MatVisitExtractView>("Select * From MatVisitExtracts")
                .ToList().Where(x => !x.IsSent);
        }

        public IEnumerable<PncVisitExtractView> ReadAllPncVisits()
        {
            return _context.Database.GetDbConnection().Query<PncVisitExtractView>("Select * From PncVisitExtracts")
                .ToList().Where(x => !x.IsSent);
        }

        public IEnumerable<MotherBabyPairExtractView> ReadAllMotherBabyPairs()
        {
            return _context.Database.GetDbConnection()
                .Query<MotherBabyPairExtractView>("Select * From MotherBabyPairExtracts").ToList()
                .Where(x => !x.IsSent);
        }

        public IEnumerable<CwcEnrolmentExtractView> ReadAllCwcEnrolments()
        {
            return _context.Database.GetDbConnection()
                .Query<CwcEnrolmentExtractView>("Select * From CwcEnrolmentExtracts").ToList().Where(x => !x.IsSent);
        }

        public IEnumerable<CwcVisitExtractView> ReadAllCwcVisits()
        {
            return _context.Database.GetDbConnection().Query<CwcVisitExtractView>("Select * From CwcVisitExtracts")
                .ToList().Where(x => !x.IsSent);
        }

        public IEnumerable<HeiExtractView> ReadAllHeis()
        {
            return _context.Database.GetDbConnection().Query<HeiExtractView>("Select * From HeiExtracts").ToList()
                .Where(x => !x.IsSent);
        }

        public IEnumerable<MnchLabExtractView> ReadAllMnchLabs()
        {
            return _context.Database.GetDbConnection().Query<MnchLabExtractView>("Select * From MnchLabExtracts")
                .ToList().Where(x => !x.IsSent);
        }
        
        public IEnumerable<MnchImmunizationExtractView> ReadAllMnchImmunizations()
        {
            return _context.Database.GetDbConnection().Query<MnchImmunizationExtractView>("Select * From MnchImmunizationExtracts")
                .ToList().Where(x => !x.IsSent);
        }

    }
}
