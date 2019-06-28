using System;
using System.Collections.Generic;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Model
{
    public class Validator: Entity<Guid>
    {
        public string Extract { get; set; }
        public string Field { get; set; }
        public string Type { get; set; }
        public string Logic { get; set; }
        public string Summary { get; set; }
        public virtual ICollection<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();

        public Validator()
        {
            Id = LiveGuid.NewGuid();
        }

        public string GenerateValidateSql(DatabaseProvider provider)
        {
            string selectSql = string.Empty;

            if (provider == DatabaseProvider.MsSql)
            {
                if (Type.ToLower() != "Complex".ToLower())
                {
                    selectSql =
                        $@" 
                        SELECT 
                            NEWID() as Id,'{Id}',Id as RecordId,GETDATE() as DateGenerated  
                        FROM 
                            {Extract} 
                        WHERE 
                            {Logic}
                    ";
                }
            }

            if (provider == DatabaseProvider.MySql)
            {
                if (Type.ToLower() != "Complex".ToLower())
                {
                    selectSql =
                        $@" 
                        SELECT 
                            UUID() as Id,'{Id}',Id as RecordId,NOW() as DateGenerated  
                        FROM 
                            {Extract} 
                        WHERE 
                            {Logic}
                    ";
                }
            }

            var sql = $"{GenerateInsert()} " +
                      $"{selectSql};" +
                      $"{GenerateUpdateError()} ";

            return sql;
        }

        private string GenerateInsert()
        {
            return $@" 
                    INSERT INTO ValidationError
                        (Id,ValidatorId,RecordId,DateGenerated) ";
        }

        private string GenerateUpdateError()
        {
            return $@"
                    UPDATE 
                        {Extract} 
                    SET 
                        CheckError=1 
                    WHERE 
                        Id IN (SELECT RecordId FROM ValidationError WHERE ValidatorId='{Id}')";

        }

    }
}
