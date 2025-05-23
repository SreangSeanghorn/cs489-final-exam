﻿// <auto-generated />
using System;
using AstronautSatelliteAPI.DataPersistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AstronautSatelliteAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250421163817_AddAstronautFields")]
    partial class AddAstronautFields
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AstronautSatellite", b =>
                {
                    b.Property<long>("AstronautsId")
                        .HasColumnType("bigint");

                    b.Property<long>("SatellitesId")
                        .HasColumnType("bigint");

                    b.HasKey("AstronautsId", "SatellitesId");

                    b.HasIndex("SatellitesId");

                    b.ToTable("AstronautSatellites", (string)null);
                });

            modelBuilder.Entity("AstronautSatelliteAPI.Models.Astronaut", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("ExperienceYears")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Astronauts");
                });

            modelBuilder.Entity("AstronautSatelliteAPI.Models.Satellite", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool>("Decommissioned")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LaunchDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("OrbitType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Satellites");
                });

            modelBuilder.Entity("AstronautSatellite", b =>
                {
                    b.HasOne("AstronautSatelliteAPI.Models.Astronaut", null)
                        .WithMany()
                        .HasForeignKey("AstronautsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AstronautSatelliteAPI.Models.Satellite", null)
                        .WithMany()
                        .HasForeignKey("SatellitesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
