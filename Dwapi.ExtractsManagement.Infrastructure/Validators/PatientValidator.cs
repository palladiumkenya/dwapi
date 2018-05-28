using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Serilog;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Infrastructure.Validators
{
    public class PatientValidator : IPatientValidator
    {
        private readonly IValidatorRepository _validatorRepository;
        private readonly ITempPatientExtractRepository _tempPatientExtractRepository;

        public PatientValidator(IValidatorRepository validatorRepository, ITempPatientExtractRepository tempPatientExtractRepository)
        {
            _validatorRepository = validatorRepository;
            _tempPatientExtractRepository = tempPatientExtractRepository;
        }

        public async Task<bool> Validate()
        {
            try
            {
                var validators = _validatorRepository.GetByExtract($"{nameof(TempPatientExtract)}s");
                int count = 0;
                var validatorList = validators.ToList();

                foreach (var validator in validatorList)
                {
                    using (var command = _validatorRepository.GetConnection().CreateCommand())
                    {
                        command.CommandText = validator.GenerateValidateSql();

                        try
                        {
                            await GetTask(command);
                        }
                        catch (Exception e)
                        {
                            Log.Debug(e.Message);
                            throw;
                        }
                        count++;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public Task<int> GetRejectedCount()
        {
            return Task.Run(() => _tempPatientExtractRepository.GetAll().Count(a => a.CheckError));
        }

        private Task<int> GetTask(IDbCommand command)
        {
            return Task.Run(() => command.ExecuteNonQuery());
        }
    }
}