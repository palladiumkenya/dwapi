using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.ExtractsManagement.Infrastructure.Migrations
{
    public partial class CrsInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientRegistryExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientPK = table.Column<int>(nullable: false),
                    SiteCode = table.Column<int>(nullable: false),
                    PatientID = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    Processed = table.Column<bool>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: true),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    CCCNumber = table.Column<string>(nullable: true),
                    NationalId = table.Column<string>(nullable: true),
                    Passport = table.Column<string>(nullable: true),
                    HudumaNumber = table.Column<string>(nullable: true),
                    BirthCertificateNumber = table.Column<string>(nullable: true),
                    AlienIdNo = table.Column<string>(nullable: true),
                    DrivingLicenseNumber = table.Column<string>(nullable: true),
                    PatientClinicNumber = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    HighestLevelOfEducation = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    AlternativePhoneNumber = table.Column<string>(nullable: true),
                    SpousePhoneNumber = table.Column<string>(nullable: true),
                    NameOfNextOfKin = table.Column<string>(nullable: true),
                    NextOfKinRelationship = table.Column<string>(nullable: true),
                    NextOfKinTelNo = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    SubCounty = table.Column<string>(nullable: true),
                    Ward = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Village = table.Column<string>(nullable: true),
                    Landmark = table.Column<string>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    MFLCode = table.Column<string>(nullable: true),
                    DateOfInitiation = table.Column<DateTime>(nullable: true),
                    TreatmentOutcome = table.Column<string>(nullable: true),
                    DateOfLastEncounter = table.Column<DateTime>(nullable: true),
                    DateOfLastViralLoad = table.Column<DateTime>(nullable: true),
                    NextAppointmentDate = table.Column<DateTime>(nullable: true),
                    LastRegimen = table.Column<string>(nullable: true),
                    LastRegimenLine = table.Column<string>(nullable: true),
                    sxdmPKValueDoB = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRegistryExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempClientRegistryExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientPK = table.Column<int>(nullable: true),
                    PatientID = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: true),
                    SiteCode = table.Column<int>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: false),
                    Emr = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    CheckError = table.Column<bool>(nullable: false),
                    ErrorType = table.Column<int>(nullable: false),
                    CCCNumber = table.Column<string>(nullable: true),
                    NationalId = table.Column<string>(nullable: true),
                    Passport = table.Column<string>(nullable: true),
                    HudumaNumber = table.Column<string>(nullable: true),
                    BirthCertificateNumber = table.Column<string>(nullable: true),
                    AlienIdNo = table.Column<string>(nullable: true),
                    DrivingLicenseNumber = table.Column<string>(nullable: true),
                    PatientClinicNumber = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    HighestLevelOfEducation = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    AlternativePhoneNumber = table.Column<string>(nullable: true),
                    SpousePhoneNumber = table.Column<string>(nullable: true),
                    NameOfNextOfKin = table.Column<string>(nullable: true),
                    NextOfKinRelationship = table.Column<string>(nullable: true),
                    NextOfKinTelNo = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    SubCounty = table.Column<string>(nullable: true),
                    Ward = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Village = table.Column<string>(nullable: true),
                    Landmark = table.Column<string>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    MFLCode = table.Column<string>(nullable: true),
                    DateOfInitiation = table.Column<DateTime>(nullable: true),
                    TreatmentOutcome = table.Column<string>(nullable: true),
                    DateOfLastEncounter = table.Column<DateTime>(nullable: true),
                    DateOfLastViralLoad = table.Column<DateTime>(nullable: true),
                    NextAppointmentDate = table.Column<DateTime>(nullable: true),
                    LastRegimen = table.Column<string>(nullable: true),
                    LastRegimenLine = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempClientRegistryExtracts", x => x.Id);
                });
            if (migrationBuilder.ActiveProvider.ToLower().Contains("MySql".ToLower()))
            {
                migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 0;");
                migrationBuilder.Sql($@"alter table {nameof(ExtractsContext.TempClientRegistryExtracts)} convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql($@"alter table {nameof(ExtractsContext.ClientRegistryExtracts)} convert to character set utf8 collate utf8_unicode_ci;");
                migrationBuilder.Sql(@"SET FOREIGN_KEY_CHECKS = 1;");
            }
        }
        

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientRegistryExtracts");

            migrationBuilder.DropTable(
                name: "TempClientRegistryExtracts");
        }
    }
}
