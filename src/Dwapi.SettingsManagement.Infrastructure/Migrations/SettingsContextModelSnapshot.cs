﻿// <auto-generated />
using System;
using Dwapi.SettingsManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dwapi.SettingsManagement.Infrastructure.Migrations
{
    [DbContext(typeof(SettingsContext))]
    partial class SettingsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Dwapi.SettingsManagement.Core.Model.AppMetric", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LogDate");

                    b.Property<string>("LogValue");

                    b.Property<string>("Name");

                    b.Property<int>("Status");

                    b.Property<string>("Version");

                    b.HasKey("Id");

                    b.ToTable("AppMetrics");
                });

            modelBuilder.Entity("Dwapi.SettingsManagement.Core.Model.CentralRegistry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthToken")
                        .HasMaxLength(100);

                    b.Property<string>("DocketId");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<string>("SubscriberId")
                        .HasMaxLength(50);

                    b.Property<string>("Url")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("DocketId");

                    b.ToTable("CentralRegistries");
                });

            modelBuilder.Entity("Dwapi.SettingsManagement.Core.Model.DatabaseProtocol", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdvancedProperties")
                        .HasMaxLength(100);

                    b.Property<string>("DatabaseName")
                        .HasMaxLength(100);

                    b.Property<int>("DatabaseType");

                    b.Property<Guid>("EmrSystemId");

                    b.Property<string>("Host")
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .HasMaxLength(100);

                    b.Property<int?>("Port");

                    b.Property<string>("Username")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("EmrSystemId");

                    b.ToTable("DatabaseProtocols");
                });

            modelBuilder.Entity("Dwapi.SettingsManagement.Core.Model.Docket", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Dockets");
                });

            modelBuilder.Entity("Dwapi.SettingsManagement.Core.Model.EmrSystem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EmrSetup");

                    b.Property<bool>("IsDefault");

                    b.Property<bool>("IsMiddleware");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<string>("Version")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("EmrSystems");
                });

            modelBuilder.Entity("Dwapi.SettingsManagement.Core.Model.Extract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("DatabaseProtocolId");

                    b.Property<string>("Destination");

                    b.Property<string>("Display")
                        .HasMaxLength(100);

                    b.Property<string>("DocketId");

                    b.Property<Guid>("EmrSystemId");

                    b.Property<string>("ExtractSql")
                        .HasMaxLength(8000);

                    b.Property<bool>("IsPriority");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<decimal>("Rank");

                    b.HasKey("Id");

                    b.HasIndex("DatabaseProtocolId");

                    b.HasIndex("DocketId");

                    b.HasIndex("EmrSystemId");

                    b.ToTable("Extracts");
                });

            modelBuilder.Entity("Dwapi.SettingsManagement.Core.Model.Resource", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EndPoint");

                    b.Property<string>("Name");

                    b.Property<Guid>("RestProtocolId");

                    b.HasKey("Id");

                    b.HasIndex("RestProtocolId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("Dwapi.SettingsManagement.Core.Model.RestProtocol", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthToken")
                        .HasMaxLength(100);

                    b.Property<Guid>("EmrSystemId");

                    b.Property<string>("Password")
                        .HasMaxLength(100);

                    b.Property<string>("Url")
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("EmrSystemId");

                    b.ToTable("RestProtocols");
                });

            modelBuilder.Entity("Dwapi.SettingsManagement.Core.Model.CentralRegistry", b =>
                {
                    b.HasOne("Dwapi.SettingsManagement.Core.Model.Docket")
                        .WithMany("Registries")
                        .HasForeignKey("DocketId");
                });

            modelBuilder.Entity("Dwapi.SettingsManagement.Core.Model.DatabaseProtocol", b =>
                {
                    b.HasOne("Dwapi.SettingsManagement.Core.Model.EmrSystem")
                        .WithMany("DatabaseProtocols")
                        .HasForeignKey("EmrSystemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dwapi.SettingsManagement.Core.Model.Extract", b =>
                {
                    b.HasOne("Dwapi.SettingsManagement.Core.Model.DatabaseProtocol")
                        .WithMany("Extracts")
                        .HasForeignKey("DatabaseProtocolId");

                    b.HasOne("Dwapi.SettingsManagement.Core.Model.Docket")
                        .WithMany("Extracts")
                        .HasForeignKey("DocketId");

                    b.HasOne("Dwapi.SettingsManagement.Core.Model.EmrSystem")
                        .WithMany("Extracts")
                        .HasForeignKey("EmrSystemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dwapi.SettingsManagement.Core.Model.Resource", b =>
                {
                    b.HasOne("Dwapi.SettingsManagement.Core.Model.RestProtocol")
                        .WithMany("Resources")
                        .HasForeignKey("RestProtocolId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dwapi.SettingsManagement.Core.Model.RestProtocol", b =>
                {
                    b.HasOne("Dwapi.SettingsManagement.Core.Model.EmrSystem")
                        .WithMany("RestProtocols")
                        .HasForeignKey("EmrSystemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
