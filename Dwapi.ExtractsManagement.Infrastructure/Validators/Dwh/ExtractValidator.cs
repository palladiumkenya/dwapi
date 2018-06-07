using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Serilog;

namespace Dwapi.ExtractsManagement.Infrastructure.Validators.Dwh
{
    public class ExtractValidator : IExtractValidator
    {
        private readonly IValidatorRepository _validatorRepository;

        public ExtractValidator(IValidatorRepository validatorRepository)
        {
            _validatorRepository = validatorRepository;
        }

        public async Task<bool> Validate(int extracted, string extractName, string sourceTable)
        {
            DomainEvents.Dispatch(
                new ExtractActivityNotification(new DwhProgress(
                    extractName,
                    nameof(ExtractStatus.Validating),
                    extracted, 0, 0, 0, 0)));
            try
            {
                var validators = _validatorRepository.GetByExtract(sourceTable);
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
                DomainEvents.Dispatch(
                    new ExtractActivityNotification(new DwhProgress(
                        extractName,
                        nameof(ExtractStatus.Validated),
                        extracted, 0, 0, 0, 0)));
                return true;
            }
            catch (Exception e)
            {
                DomainEvents.Dispatch(
                    new ExtractActivityNotification(new DwhProgress(
                        extractName,
                        nameof(ExtractStatus.Idle),
                        extracted, 0, 0, 0, 0)));
                Console.WriteLine(e);
                return false;
            }
        }

        private Task<int> GetTask(IDbCommand command)
        {
            return Task.Run(() => command.ExecuteNonQuery());
        }
    }
}