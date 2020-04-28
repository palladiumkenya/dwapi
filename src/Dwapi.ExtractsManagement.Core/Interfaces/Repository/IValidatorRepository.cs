using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository
{
    public interface IValidatorRepository: IRepository<Validator,Guid>
    {
        void ClearByDocket(string docketId);
        IEnumerable<Validator> GetByExtract(string extract);
    }
}
