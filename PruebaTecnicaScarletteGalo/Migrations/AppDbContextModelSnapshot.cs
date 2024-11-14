﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PruebaTecnicaScarletteGalo.Data;

#nullable disable

namespace PruebaTecnicaScarletteGalo.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PruebaTecnicaScarletteGalo.Models.Reservation", b =>
                {
                    b.Property<Guid>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<Guid>("SpaceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<Guid>("StateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ReservationId");

                    b.HasIndex("SpaceId");

                    b.HasIndex("StateId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("PruebaTecnicaScarletteGalo.Models.Space", b =>
                {
                    b.Property<Guid>("SpaceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("SpaceName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("StateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SpaceId");

                    b.HasIndex("StateId");

                    b.ToTable("Spaces");
                });

            modelBuilder.Entity("PruebaTecnicaScarletteGalo.Models.State", b =>
                {
                    b.Property<Guid>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StateName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("StateId");

                    b.ToTable("States");
                });

            modelBuilder.Entity("PruebaTecnicaScarletteGalo.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UserId");

                    b.HasIndex("StateId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PruebaTecnicaScarletteGalo.Models.Reservation", b =>
                {
                    b.HasOne("PruebaTecnicaScarletteGalo.Models.Space", "Space")
                        .WithMany("Reservations")
                        .HasForeignKey("SpaceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PruebaTecnicaScarletteGalo.Models.State", "State")
                        .WithMany("Reservations")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PruebaTecnicaScarletteGalo.Models.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Space");

                    b.Navigation("State");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PruebaTecnicaScarletteGalo.Models.Space", b =>
                {
                    b.HasOne("PruebaTecnicaScarletteGalo.Models.State", "State")
                        .WithMany("Spaces")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("State");
                });

            modelBuilder.Entity("PruebaTecnicaScarletteGalo.Models.User", b =>
                {
                    b.HasOne("PruebaTecnicaScarletteGalo.Models.State", "State")
                        .WithMany("Users")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("State");
                });

            modelBuilder.Entity("PruebaTecnicaScarletteGalo.Models.Space", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("PruebaTecnicaScarletteGalo.Models.State", b =>
                {
                    b.Navigation("Reservations");

                    b.Navigation("Spaces");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("PruebaTecnicaScarletteGalo.Models.User", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
