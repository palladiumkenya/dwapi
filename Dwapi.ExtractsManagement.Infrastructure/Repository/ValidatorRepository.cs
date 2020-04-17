using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure.Repository;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository
{
    public class ValidatorRepository:BaseRepository<Validator, Guid> , IValidatorRepository
    {
        private ExtractsContext Context;
        public ValidatorRepository(ExtractsContext context) : base(context)
        {
            Context = context;
        }

        public void ClearByDocket(string docketId)
        {
            var sqlExtractNames = @"SELECT distinct name FROM Extracts where DocketId=@DocketId";

            var extractNames = GetConnection()
                .Query<string>(sqlExtractNames, new {DocketId = docketId})
                .Select(x => $"Temp{x}s")
                .ToList();

            var sql = @"
                delete from ValidationError where ValidatorId in (
                select id from Validator where Extract in @extractNames)";

            GetConnection().Execute(sql,new { extractNames});

        }

        public IEnumerable<Validator> GetByExtract(string extract)
        {
            return null;
            // return Context.Validator
            //     .Where(x => x.Extract.ToLower() == extract.ToLower());
        }
    }
}
