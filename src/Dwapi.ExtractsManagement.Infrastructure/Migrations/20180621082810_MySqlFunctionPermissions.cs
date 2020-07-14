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
                GRANT EXECUTE ON FUNCTION fn_calculateJaro TO 'dwapi'@'%';
                GRANT EXECUTE ON FUNCTION fn_calculateJaroWinkler TO 'dwapi'@'%';
                GRANT EXECUTE ON FUNCTION fn_calculateMatchWindow TO 'dwapi'@'%';
                GRANT EXECUTE ON FUNCTION fn_calculatePrefixLength TO 'dwapi'@'%';
                GRANT EXECUTE ON FUNCTION fn_GetCommonCharacters TO 'dwapi'@'%';");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
