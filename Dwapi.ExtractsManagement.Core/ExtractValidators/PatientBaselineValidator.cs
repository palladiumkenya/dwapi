using Dwapi.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dwapi.Domain.Models;
using Dwapi.Domain.Utils;
using Dwapi.ExtractsManagement.Core.Model;
using ValidationError = Dwapi.ExtractsManagement.Core.Model.ValidationError;

namespace Dwapi.ExtractsManagement.Core.ExtractValidators
{
    public class PatientBaselineValidator : IValidator
    {
        private readonly IExtractUnitOfWork _unitOfWork;
        public PatientBaselineValidator(IExtractUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Validate()
        {
            try
            {
                var validations = await _unitOfWork.Repository<Validator>().Get(
                    filter: e => e.Extract == "").ToListAsync();

                List<ValidationError> validationErrors = new List<ValidationError>();

                foreach (var validation in validations)
                {
                    string commandText = $"select * from {validation.Extract} where {validation.Logic}";
                    var list = await _unitOfWork.Context.Set<TempPatientBaselinesExtract>().FromSql(commandText).ToListAsync();
                    validationErrors.AddRange(list.Select(x => new ValidationError
                    {
                        Id = LiveGuid.NewGuid(),
                        ValidatorId = validation.Id,
                        /*ReferencedEntityId = x.Id.ToString(),
                        EntityName = validation.Extract,
                        FieldName = validation.Field,
                        ErrorMessage = validation.Summary*/
                    }));
                }

                await _unitOfWork.Repository<ValidationError>().AddRangeAsync(validationErrors);
                await _unitOfWork.SaveAsync();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    
}
