using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class MySqlFunctionPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"
                GRANT EXECUTE ON FUNCTION fn_calculateJaro TO '*'@'%';
                GRANT EXECUTE ON FUNCTION fn_calculateJaroWinkler TO '*'@'%';
                GRANT EXECUTE ON FUNCTION fn_calculateMatchWindow TO '*'@'%';
                GRANT EXECUTE ON FUNCTION fn_calculatePrefixLength TO '*'@'%';
                GRANT EXECUTE ON FUNCTION fn_GetCommonCharacters TO '*'@'%';");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
