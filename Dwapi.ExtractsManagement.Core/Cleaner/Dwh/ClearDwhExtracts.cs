using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Cleaner.Dwh
{
    public class ClearDwhExtracts : IClearDwhExtracts
    {
        private readonly SqlConnection _connection;

        public ClearDwhExtracts(IEmrSystemRepository emrSystemRepository)
        {
            _connection = new SqlConnection(emrSystemRepository.GetConnectionString());
        }

        public async Task<int> Clear()
        {
            Log.Debug($"Executing ClearDwhExtracts command...");

            DomainEvents.Dispatch(
                new ExtractActivityNotification(new DwhProgress(
                    nameof(PatientExtract),
                    nameof(ExtractStatus.Clearing),
                     0, 0, 0, 0, 0)));

            int totalCount = 0;
            var truncates = new List<string>
            {  $"{nameof(TempPatientExtract)}s",
               $"{nameof(TempPatientArtExtract)}s",
               $"{nameof(PatientArtExtract)}s",
               $"{nameof(TempPatientBaselinesExtract)}s",
               $"{nameof(PatientBaselinesExtract)}s",
               $"{nameof(TempPatientStatusExtract)}s",
               $"{nameof(PatientStatusExtract)}s",
               $"{nameof(TempPatientLaboratoryExtract)}s",
               $"{nameof(PatientLaboratoryExtract)}s",
               $"{nameof(TempPatientPharmacyExtract)}s",
               $"{nameof(PatientPharmacyExtract)}s",
               $"{nameof(TempPatientVisitExtract)}s",
               $"{nameof(PatientVisitExtract)}s",
                //todo specfy clean for dw extracts only
                nameof(ExtractHistory),
                nameof(ValidationError)
            };

            var deletes = new List<string> { $"{nameof(PatientExtract)}s" };

            using (_connection)
            {
                if (_connection.State != ConnectionState.Open)
                {
                    await _connection.OpenAsync();
                }

                var command = _connection.CreateCommand();
                command.CommandTimeout = 0;

                var parallelTasks = new List<Task<int>>();

                foreach (var name in truncates)
                {
                    parallelTasks.Add(TruncateCommand(name));
                }

                var orderdTasks = new List<Task<int>>();

                foreach (var name in deletes)
                {
                    orderdTasks.Add(DeleteCommand(name));
                }

                foreach (var t in orderdTasks)
                {
                    totalCount += await t;
                }
            }
            DomainEvents.Dispatch(
                new ExtractActivityNotification(new DwhProgress(
                    nameof(PatientExtract),
                    nameof(ExtractStatus.Cleared),
                    0, 0, 0, 0, 0)));
            return totalCount;
        }

        private Task<int> TruncateCommand(string extract)
        {
            var command = GetCommand(extract, "TRUNCATE TABLE");
            return command.ExecuteNonQueryAsync();
        }

        private Task<int> DeleteCommand(string extract)
        {
            var command = GetCommand(extract, "DELETE FROM");
            return command.ExecuteNonQueryAsync();
        }

        private SqlCommand GetCommand(string extract, string action)
        {
            var command = _connection.CreateCommand();
            command.CommandTimeout = 0;
            command.CommandText = $@" {action} {extract}; ";
            return command;
        }
    }
}