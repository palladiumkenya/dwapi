using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class MpiViewInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create view
            vMasterPatientIndices
                as
                SELECT A.*
                FROM[MasterPatientIndices] A
                INNER JOIN
            (
                SELECT  sxdmPKValueDoB, COUNT(*) Number  from[MasterPatientIndices]
            GROUP BY sxdmPKValueDoB having COUNT(*) > 1
                ) AS  B
            ON A.sxdmPKValueDoB = B.sxdmPKValueDoB
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP View vMasterPatientIndices");
        }
    }
}
