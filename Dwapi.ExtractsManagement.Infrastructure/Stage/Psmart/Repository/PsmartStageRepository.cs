using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Stage.Psmart.Repository;
using Dwapi.ExtractsManagement.Core.Model.Stage.Psmart;
using Dwapi.SharedKernel.Utility;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.ExtractsManagement.Infrastructure.Stage.Psmart.Repository
{
    public class PsmartStageRepository: IPsmartStageRepository
    {
        private readonly ExtractsContext _context;

        public PsmartStageRepository(ExtractsContext context)
        {
            _context = context;
        }

        public void Clear(string emr)
        {
            var stages = _context.PsmartStages.Where(x => x.Emr.IsSameAs(emr));
            _context.RemoveRange(stages);
            _context.SaveChanges();
            //_context.Database.GetDbConnection().Execute($"DELETE FROM {nameof(PsmartStage)}s WHERE Emr='{emr}'");
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
            return _context.PsmartStages.AsNoTracking();
        }

        public int Count(string emr)
        {
            return _context.PsmartStages.Select(x => x.Id).Count();
        }
    }
}