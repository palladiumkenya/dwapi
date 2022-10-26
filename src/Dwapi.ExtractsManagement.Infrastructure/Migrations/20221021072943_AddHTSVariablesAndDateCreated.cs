using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class AddHTSVariablesAndDateCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "TempHtsTestKitsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "TempHtsTestKitsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SyphilisResult",
                table: "TempHtsTestKitsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "TempHtsPartnerTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "TempHtsPartnerTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "TempHtsPartnerNotificationServicesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "TempHtsPartnerNotificationServicesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "TempHtsClientTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "TempHtsClientTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Approach",
                table: "TempHtsClientTestsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "TempHtsClientTestsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "TempHtsClientTestsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Setting",
                table: "TempHtsClientTestsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "TempHtsClientsLinkageExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "TempHtsClientsLinkageExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "TempHtsClientsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "TempHtsClientsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "TempHtsClientsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PriorityPopulationType",
                table: "TempHtsClientsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "HtsTestKitsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "HtsTestKitsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SyphilisResult",
                table: "HtsTestKitsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "HtsPartnerTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "HtsPartnerTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "HtsPartnerNotificationServicesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "HtsPartnerNotificationServicesExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "HtsClientTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "HtsClientTracingExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Approach",
                table: "HtsClientTestsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "HtsClientTestsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "HtsClientTestsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Setting",
                table: "HtsClientTestsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Created",
                table: "HtsClientsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Last_Modified",
                table: "HtsClientsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "HtsClientsExtracts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PriorityPopulationType",
                table: "HtsClientsExtracts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "TempHtsTestKitsExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "TempHtsTestKitsExtracts");

            migrationBuilder.DropColumn(
                name: "SyphilisResult",
                table: "TempHtsTestKitsExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "TempHtsPartnerTracingExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "TempHtsPartnerTracingExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "TempHtsPartnerNotificationServicesExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "TempHtsPartnerNotificationServicesExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "TempHtsClientTracingExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "TempHtsClientTracingExtracts");

            migrationBuilder.DropColumn(
                name: "Approach",
                table: "TempHtsClientTestsExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "TempHtsClientTestsExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "TempHtsClientTestsExtracts");

            migrationBuilder.DropColumn(
                name: "Setting",
                table: "TempHtsClientTestsExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "TempHtsClientsLinkageExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "TempHtsClientsLinkageExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "TempHtsClientsExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "TempHtsClientsExtracts");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "TempHtsClientsExtracts");

            migrationBuilder.DropColumn(
                name: "PriorityPopulationType",
                table: "TempHtsClientsExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "HtsTestKitsExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "HtsTestKitsExtracts");

            migrationBuilder.DropColumn(
                name: "SyphilisResult",
                table: "HtsTestKitsExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "HtsPartnerTracingExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "HtsPartnerTracingExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "HtsPartnerNotificationServicesExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "HtsPartnerNotificationServicesExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "HtsClientTracingExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "HtsClientTracingExtracts");

            migrationBuilder.DropColumn(
                name: "Approach",
                table: "HtsClientTestsExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "HtsClientTestsExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "HtsClientTestsExtracts");

            migrationBuilder.DropColumn(
                name: "Setting",
                table: "HtsClientTestsExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Created",
                table: "HtsClientsExtracts");

            migrationBuilder.DropColumn(
                name: "Date_Last_Modified",
                table: "HtsClientsExtracts");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "HtsClientsExtracts");

            migrationBuilder.DropColumn(
                name: "PriorityPopulationType",
                table: "HtsClientsExtracts");
        }
    }
}
