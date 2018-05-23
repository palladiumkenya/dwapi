using System;

namespace Dwapi.ExtractsManagement.Core.Model
{
    public class ValidationError
    {
        public Guid Id { get; set; }
        public Guid ValidatorId { get; set; }
        public string EntityName { get; set; }
        public string FieldName { get; set; }
        public string ReferencedEntityId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
