﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dapper;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Diff;
using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Dwapi.UploadManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Dwapi.UploadManagement.Infrastructure.Reader.Dwh
{
    public class DwhExtractReader:IDwhExtractReader
    {
        private readonly UploadContext _context;
        private readonly IConfiguration _configuration;

        public DwhExtractReader(UploadContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        public IEnumerable<SitePatientProfile> ReadProfiles()
        {
            return
                _context.ClientPatientExtracts.AsNoTracking()
                .Select(x =>
                    new SitePatientProfile(x.SiteCode, x.FacilityName, x.PatientPK)
                );
        }

        public IEnumerable<Guid> ReadAllIds()
        {
            return _context.ClientPatientExtracts.Where(x=>!x.IsSent).AsNoTracking().Select(x=>x.Id);
        }

        public PatientExtractView Read(Guid id)
        {
            var patientExtractView = _context.ClientPatientExtracts
                .Include(x => x.PatientArtExtracts)
                .Include(x => x.PatientBaselinesExtracts)
                .Include(x => x.PatientLaboratoryExtracts)
                .Include(x => x.PatientPharmacyExtracts)
                .Include(x => x.PatientStatusExtracts)
                .Include(x => x.PatientVisitExtracts)
                .Include(x => x.PatientAdverseEventExtracts)
                .Include(x => x.AllergiesChronicIllnessExtracts)
                .Include(x => x.IptExtracts)
                .Include(x => x.DepressionScreeningExtracts)
                .Include(x => x.ContactListingExtracts)
                .Include(x => x.GbvScreeningExtracts)
                .Include(x => x.EnhancedAdherenceCounsellingExtracts)
                .Include(x => x.DrugAlcoholScreeningExtracts)
                .Include(x => x.OvcExtracts)
                .Include(x => x.OtzExtracts)
                .Include(x => x.CovidExtracts)
                .Include(x => x.DefaulterTracingExtracts)
                .Include(x => x.CancerScreeningExtracts)
                .Include(x => x.IITRiskScoresExtracts)
                .Include(x => x.ArtFastTrackExtracts)
                .Include(x => x.RelationshipsExtracts)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
            return patientExtractView;
        }

        public IEnumerable<Site> GetSites()
        {
            var sql = @"
                SELECT
                    SiteCode,MAX(FacilityName) AS SiteName,Count(Id) AS PatientCount
                FROM
                    PatientExtracts
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
                    PatientExtracts
            ";
            return _context.Database.GetDbConnection()
                .Query<SitePatientProfile>(sql).ToList();
        }

        public IEnumerable<T> Read<T, TId>(int page, int pageSize) where T : Entity<TId>
        {
            return _context.Set<T>()
                .Include(nameof(PatientExtractView))
                .Skip((page - 1) * pageSize).Take(pageSize)
                .OrderBy(x => x.Id)
                .AsNoTracking().ToList();
        }


        public IEnumerable<T> Read<T, TId>(int page, int pageSize, Expression<Func<T, bool>> predicate) where T : Entity<TId>
        {
            return _context.Set<T>()
                .Include(nameof(PatientExtractView))
                .Where(predicate)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .OrderBy(x => x.Id)
                .AsNoTracking().ToList();
        }

        public IEnumerable<T> ReadSmart<T, TId>(int page, int pageSize) where T : Entity<TId>
        {
            var skip = (page - 1) * pageSize;

            return _context.Set<T>()
                .OrderBy(x => x.Id)
                .Skip(skip).Take(pageSize)
                .AsNoTracking().ToList();
        }

        public IEnumerable<T> ReadSmart<T>(int page, int pageSize) where T : ClientExtract
        {
            var skip = (page - 1) * pageSize;

            return _context.Set<T>()
                .OrderBy(x => x.SiteCode)
                .ThenBy(p=>p.PatientPK)
                .Skip(skip).Take(pageSize)
                .AsNoTracking().ToList();
        }

        public IEnumerable<T> ReadSmart<T, TId>(int page, int pageSize, Expression<Func<T, bool>> predicate) where T : Entity<TId>
        {
            return _context.Set<T>()
                .Where(predicate)
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .AsNoTracking().ToList();
        }

        public IEnumerable<T> ReadMainExtract<T, TId>(int page, int pageSize, Expression<Func<T, bool>> predicate) where T : Entity<TId>
        {
            return _context.Set<T>()
                .Where(predicate)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .OrderBy(x => x.Id)
                .AsNoTracking().ToList();
        }
        public IEnumerable<T> ReadMainExtract<T, TId>(int page, int pageSize) where T : Entity<TId>
        {
            return _context.Set<T>()
                .Skip((page - 1) * pageSize).Take(pageSize)
                .OrderBy(x => x.Id)
                .AsNoTracking().ToList();
        }

        public IDataReader GetSmartReader(string extractName)
        {
            var defaultDbProtocol=_configuration["ConnectionStrings:Provider"];
            var provider = Convert.ToInt32(defaultDbProtocol);
            
            if (provider == 1)
            {
                var connectionString = _context.Database.GetDbConnection().ConnectionString;
                var cn = new MySqlConnection(connectionString);
                cn.Open();
                var sql = $"select * from {extractName}s";
                var cmd = new MySqlCommand(sql, cn);
                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            else
            {
                var connectionString = _context.Database.GetDbConnection().ConnectionString;
                var cn = new SqlConnection(connectionString);
                cn.Open();
                var sql = $"select * from {extractName}s";
                var cmd = new SqlCommand(sql, cn);
                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
        }
        
        public long GetTotalRecords<T, TId>() where T : Entity<TId>
        {
            return _context.Set<T>()
                .Select(x => x.Id)
                .LongCount();
        }
    }
}
