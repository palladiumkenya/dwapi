using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class ChangedChronicOnsetDateToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AlterColumn<string>(
                name: "ChronicOnsetDate",
                table: "TempAllergiesChronicIllnessExtracts",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChronicOnsetDate",
                table: "AllergiesChronicIllnessExtracts",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ChronicOnsetDate",
                table: "TempAllergiesChronicIllnessExtracts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChronicOnsetDate",
                table: "AllergiesChronicIllnessExtracts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
