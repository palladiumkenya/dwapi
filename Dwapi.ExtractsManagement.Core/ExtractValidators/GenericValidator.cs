using Dwapi.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.ExtractValidators
{
    public interface IGenericValidator
    {
        Task Validate(string type);
    }
    public class GenericValidator : IValidator
    {
        private readonly IExtractUnitOfWork _unitOfWork;
        private string _type;

        public GenericValidator(IExtractUnitOfWork unitOfWork, string type)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _type = type;
        }

        public async Task Validate()
        {
            try
            {
                var validations = await _unitOfWork.Repository<Domain.Validator>().Get(
                    filter: e => e.Extract == _type).ToListAsync();

                List<ValidationError> validationErrors = new List<ValidationError>();

                foreach (var validation in validations)
                {
                    using(var command = _unitOfWork.Context.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = $"select Id from {validation.Extract} where {validation.Logic}";
                        command.CommandTimeout = 3600;
                        _unitOfWork.Context.Database.OpenConnection();
                        using (SqlDataReader result = await command.ExecuteReaderAsync() as SqlDataReader)
                        {
                            while(result != null && result.Read())
                            {
                                var id = result.GetGuid(0);
                                var validationError = new ValidationError
                                {
                                    Id = LiveGuid.NewGuid(),
                                    ValidatorId = validation.Id,
                                    ReferencedEntityId = id.ToString(),
                                    EntityName = validation.Extract,
                                    FieldName = validation.Field,
                                    ErrorMessage = validation.Summary
                                };
                                validationErrors.Add(validationError);
                            }
                            //validationErrors.AddRange(ids.Select(x => new ValidationError
                            //{
                            //    Id = LiveGuid.NewGuid(),
                            //    ValidatorId = validation.Id,
                            //    ReferencedEntityId = x.ToString(),
                            //    EntityName = validation.Extract,
                            //    FieldName = validation.Field,
                            //    ErrorMessage = validation.Summary
                            //}));
                        }
                    }
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
