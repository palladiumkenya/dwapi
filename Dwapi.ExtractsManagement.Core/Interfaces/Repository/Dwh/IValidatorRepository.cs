using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh
{
    public interface IValidatorRepository: IRepository<Validator,Guid>
    {
        IEnumerable<Validator> GetByExtract(string extract);
    }
}