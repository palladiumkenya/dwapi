using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class TempAdverseEventsColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "VisitDate",
                table: "TempPatientAdverseEventExtracts",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AdverseEventStartDate",
                table: "TempPatientAdverseEventExtracts",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "AdverseEventIsPregnant",
                table: "TempPatientAdverseEventExtracts",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AdverseEventEndDate",
                table: "TempPatientAdverseEventExtracts",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "AdverseEventCause",
                table: "TempPatientAdverseEventExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdverseEventRegimen",
                table: "TempPatientAdverseEventExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdverseEventCause",
                table: "TempPatientAdverseEventExtracts");

            migrationBuilder.DropColumn(
                name: "AdverseEventRegimen",
                table: "TempPatientAdverseEventExtracts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "VisitDate",
                table: "TempPatientAdverseEventExtracts",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AdverseEventStartDate",
                table: "TempPatientAdverseEventExtracts",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "AdverseEventIsPregnant",
                table: "TempPatientAdverseEventExtracts",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AdverseEventEndDate",
                table: "TempPatientAdverseEventExtracts",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
