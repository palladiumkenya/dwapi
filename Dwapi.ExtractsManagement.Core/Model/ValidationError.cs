using System;
using Dwapi.ExtractsManagement.Core.Interfaces;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Model
{
    public class ValidationError: Entity<Guid>, IValidationError
    {
        public Guid ValidatorId { get; set; }
        public Guid RecordId { get; set; }
        public DateTime DateGenerated { get; set; }

        public ValidationError()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}
