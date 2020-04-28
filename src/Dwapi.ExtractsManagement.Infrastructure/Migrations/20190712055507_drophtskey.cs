using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class drophtskey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"DROP INDEX AK_HtsClientExtracts_Id ON HtsClientExtracts");
                migrationBuilder.Sql(@"ALTER TABLE HtsClientExtracts DROP PRIMARY KEY");
                migrationBuilder.Sql(@"ALTER TABLE HtsClientExtracts ADD PRIMARY KEY (Id)");
            }
            else
            {
                migrationBuilder.DropUniqueConstraint(
                    name: "AK_HtsClientExtracts_Id",
                    table: "HtsClientExtracts");

                migrationBuilder.DropPrimaryKey(
                    name: "PK_HtsClientExtracts",
                    table: "HtsClientExtracts");

                migrationBuilder.AddPrimaryKey(
                    name: "PK_HtsClientExtracts",
                    table: "HtsClientExtracts",
                    column: "Id");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"ALTER TABLE HtsClientExtracts DROP PRIMARY KEY");
                migrationBuilder.Sql(@"ALTER TABLE HtsClientExtracts ADD CONSTRAINT AK_HtsClientExtracts_Id UNIQUE (Id)");
                migrationBuilder.Sql(@"ALTER TABLE HtsClientExtracts ADD PRIMARY KEY (SiteCode,PatientPk,EncounterId)");
            }
            else
            {
                migrationBuilder.DropPrimaryKey(
                    name: "PK_HtsClientExtracts",
                    table: "HtsClientExtracts");

                migrationBuilder.AddUniqueConstraint(
                    name: "AK_HtsClientExtracts_Id",
                    table: "HtsClientExtracts",
                    column: "Id");

                migrationBuilder.AddPrimaryKey(
                    name: "PK_HtsClientExtracts",
                    table: "HtsClientExtracts",
                    columns: new[] {"SiteCode", "PatientPk", "EncounterId"});
            }
        }
    }
}
