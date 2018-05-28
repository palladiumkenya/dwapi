using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class updatedValidationError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityName",
                table: "ValidationError");

            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                table: "ValidationError");

            migrationBuilder.DropColumn(
                name: "FieldName",
                table: "ValidationError");

            migrationBuilder.DropColumn(
                name: "ReferencedEntityId",
                table: "ValidationError");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateGenerated",
                table: "ValidationError",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "RecordId",
                table: "ValidationError",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateGenerated",
                table: "ValidationError");

            migrationBuilder.DropColumn(
                name: "RecordId",
                table: "ValidationError");

            migrationBuilder.AddColumn<string>(
                name: "EntityName",
                table: "ValidationError",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                table: "ValidationError",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FieldName",
                table: "ValidationError",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferencedEntityId",
                table: "ValidationError",
                nullable: true);
        }
    }
}
