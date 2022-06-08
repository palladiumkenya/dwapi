using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Crs;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Crs;
using Serilog;

namespace Dwapi.ExtractsManagement.Infrastructure.Validators.Crs
{
    public class ClientRegistryExtractValidator : IClientRegistryExtractValidator
    {
        private readonly IValidatorRepository _validatorRepository;
        private readonly ITempClientRegistryExtractRepository _tempClientRegistryExtractRepository;

        public ClientRegistryExtractValidator(IValidatorRepository validatorRepository, ITempClientRegistryExtractRepository tempClientRegistryExtractRepository)
        {
            _validatorRepository = validatorRepository;
            _tempClientRegistryExtractRepository = tempClientRegistryExtractRepository;
        }

        public async Task<bool> Validate(string sourceTable)
        {
            try
            {
                var validators = _validatorRepository.GetByExtract(sourceTable);
                int count = 0;
                var validatorList = validators.ToList();

                foreach (var validator in validatorList)
                {
                    using (var command = _validatorRepository.GetConnection().CreateCommand())
                    {
                        var provider = _validatorRepository.GetConnectionProvider();
                        command.CommandText = validator.GenerateValidateSql(provider);

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

        private Task<int> GetTask(IDbCommand command)
        {
            return Task.Run(() => command.ExecuteNonQuery());
        }
    }
}