using Dwapi.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dwapi.Domain.Utils;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Core.ExtractValidators
{
    public class PatientExtractValidator : IValidator
    {
        private readonly IExtractUnitOfWork _unitOfWork;
        private string _extractName => nameof(TempPatientExtract);

        public PatientExtractValidator(IExtractUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Validate()
        {
            try
            {
                var validations = await _unitOfWork.Repository<Validator>().Get(
                    filter: e => e.Extract == _extractName).ToListAsync();

                List<ValidationError> validationErrors = new List<ValidationError>();

                foreach (var validation in validations)
                {
                    string commandText = $"select * from {validation.Extract} where {validation.Logic}";
                    var list = await _unitOfWork.Context.Set<TempPatientExtract>().FromSql(commandText).ToListAsync();
                    validationErrors.AddRange(list.Select(x => new ValidationError
                    {
                        Id = LiveGuid.NewGuid(),
                        ValidatorId = validation.Id,
                        ReferencedEntityId = x.Id.ToString(),
                        EntityName = validation.Extract,
                        FieldName = validation.Field,
                        ErrorMessage = validation.Summary
                    }));
                }

                await _unitOfWork.Repository<ValidationError>().AddRangeAsync(validationErrors);
                await _unitOfWork.SaveAsync();
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
