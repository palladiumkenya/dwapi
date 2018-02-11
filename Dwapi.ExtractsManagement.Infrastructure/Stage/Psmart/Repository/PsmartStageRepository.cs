using System.Collections.Generic;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Stage.Psmart.Repository;
using Dwapi.ExtractsManagement.Core.Stage.Psmart;
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

        public void Clear()
        {
            _context.Database.GetDbConnection().Execute($"DELETE FROM {nameof(PsmartStage)}s");
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
    }
}