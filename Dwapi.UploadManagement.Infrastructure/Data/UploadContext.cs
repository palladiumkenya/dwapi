using System.Reflection;
using CsvHelper.Configuration;
using Dwapi.SharedKernel.Infrastructure;
using Dwapi.UploadManagement.Core.Model.Cbs;
using Dwapi.UploadManagement.Core.Model.Dwh;
using EFCore.Seeder.Configuration;
using EFCore.Seeder.Extensions;
using Microsoft.EntityFrameworkCore;
using Z.Dapper.Plus;

namespace Dwapi.UploadManagement.Infrastructure.Data
{
    public class UploadContext : BaseContext
    {
        public virtual DbSet<MasterPatientIndexView> ClientMasterPatientIndices { get; set; }
        public virtual DbSet<PatientExtractView> ClientPatientExtracts { get; set; }
        public virtual DbSet<PatientArtExtractView> ClientPatientArtExtracts { get; set; }
        public virtual DbSet<PatientBaselinesExtractView> ClientPatientBaselinesExtracts { get; set; }
        public virtual DbSet<PatientLaboratoryExtractView> ClientPatientLaboratoryExtracts { get; set; }
        public virtual DbSet<PatientPharmacyExtractView> ClientPatientPharmacyExtracts { get; set; }
        public virtual DbSet<PatientStatusExtractView> ClientPatientStatusExtracts { get; set; }
        public virtual DbSet<PatientVisitExtractView> ClientPatientVisitExtracts { get; set; }

        public UploadContext(DbContextOptions<UploadContext> options) : base(options)
        {
        }
    }
}