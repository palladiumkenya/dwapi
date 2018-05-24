using System;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh
{
    public class ValidatorRepository:BaseRepository<Validator, Guid> , IValidatorRepository
    {
        public ValidatorRepository(ExtractsContext context) : base(context)
        {
        }
    }
}