using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository
{
    public class PsmartStageRepository : IPsmartStageRepository
    {
        private readonly ExtractsContext _context;

        public PsmartStageRepository(ExtractsContext context)
        {
            _context = context;
        }

        public void Clear(string emr="")
        {
            if (!string.IsNullOrWhiteSpace(emr))
            {
                var stages = _context.Set<PsmartStage>().Where(x => x.Emr.IsSameAs(emr));
                _context.RemoveRange(stages);
                _context.SaveChanges();
            }

            else
            {
                var stages = _context.Set<PsmartStage>().ToList();
                _context.RemoveRange(stages);
                _context.SaveChanges();
            }
        }

        public void Load(PsmartStage entity)
        {
            _context.AddRange(entity);
        }

        public void Load(IEnumerable<PsmartStage> entities)
        {
            _context.AddRange(entities);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<PsmartStage> GetAll()
        {
            return _context.Set<PsmartStage>().AsNoTracking();
        }

        public int Count(string emr)
        {
            return _context.Set<PsmartStage>().Select(x => x.Id).Count();
        }

        public void UpdateStatus(IEnumerable<Guid> eids, bool sent, string requestId)
        {
            if (!sent)
                return;

            var toUodate = _context.Set<PsmartStage>().Where(x => eids.Contains(x.EId)).ToList();
            foreach (var psmartStage in toUodate)
            {
                psmartStage.DateSent = DateTime.Now;
                psmartStage.RequestId = requestId;
            }

            _context.SaveChanges();
        }
    }
}