using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.ExtractValidators
{
    public interface IValidator
    {
        Task Validate();
    }
}
