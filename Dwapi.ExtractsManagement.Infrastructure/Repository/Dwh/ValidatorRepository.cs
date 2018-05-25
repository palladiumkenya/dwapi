using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh
{
    public class ValidatorRepository:BaseRepository<Validator, Guid> , IValidatorRepository
    {
        private ExtractsContext Context;
        public ValidatorRepository(ExtractsContext context) : base(context)
        {
            Context = context;
        }

        public IEnumerable<Validator> GetByExtract(string extract)
        {
            return Context.Validator
                .Where(x => x.Extract.ToLower() == extract.ToLower());
        }
    }
}