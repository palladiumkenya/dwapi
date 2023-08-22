using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class AdverseEventsColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "VisitDate",
                table: "PatientAdverseEventExtracts",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AdverseEventStartDate",
                table: "PatientAdverseEventExtracts",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "AdverseEventIsPregnant",
                table: "PatientAdverseEventExtracts",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AdverseEventEndDate",
                table: "PatientAdverseEventExtracts",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "AdverseEventCause",
                table: "PatientAdverseEventExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdverseEventRegimen",
                table: "PatientAdverseEventExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdverseEventCause",
                table: "PatientAdverseEventExtracts");

            migrationBuilder.DropColumn(
                name: "AdverseEventRegimen",
                table: "PatientAdverseEventExtracts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "VisitDate",
                table: "PatientAdverseEventExtracts",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AdverseEventStartDate",
                table: "PatientAdverseEventExtracts",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "AdverseEventIsPregnant",
                table: "PatientAdverseEventExtracts",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AdverseEventEndDate",
                table: "PatientAdverseEventExtracts",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
