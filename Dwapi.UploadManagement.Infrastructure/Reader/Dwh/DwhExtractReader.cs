using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Dwapi.UploadManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.UploadManagement.Infrastructure.Reader.Dwh
{
    public class DwhExtractReader:IDwhExtractReader
    {
        private readonly UploadContext _context;

        public DwhExtractReader(UploadContext context)
        {
            _context = context;
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
                .AsNoTracking()
                .SingleOrDefault(x => x.Id == id);
            return patientExtractView;
        }
    }
}