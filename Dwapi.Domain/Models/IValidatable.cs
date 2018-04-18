using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.Domain
{
    public interface IValidatable
    {
        Guid Id { get; set; }
    }
}
