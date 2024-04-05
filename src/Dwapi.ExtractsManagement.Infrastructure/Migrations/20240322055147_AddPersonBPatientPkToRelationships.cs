using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class AddPersonBPatientPkToRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<string>(
                name: "PatientRelationshipToOther",
                table: "TempRelationshipsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonAPatientPk",
                table: "TempRelationshipsExtracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonBPatientPk",
                table: "TempRelationshipsExtracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PatientRelationshipToOther",
                table: "RelationshipsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonAPatientPk",
                table: "RelationshipsExtracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonBPatientPk",
                table: "RelationshipsExtracts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientRelationshipToOther",
                table: "TempRelationshipsExtracts");

            migrationBuilder.DropColumn(
                name: "PersonAPatientPk",
                table: "TempRelationshipsExtracts");

            migrationBuilder.DropColumn(
                name: "PersonBPatientPk",
                table: "TempRelationshipsExtracts");

            migrationBuilder.DropColumn(
                name: "PatientRelationshipToOther",
                table: "RelationshipsExtracts");

            migrationBuilder.DropColumn(
                name: "PersonAPatientPk",
                table: "RelationshipsExtracts");

            migrationBuilder.DropColumn(
                name: "PersonBPatientPk",
                table: "RelationshipsExtracts");

        }
    }
}
