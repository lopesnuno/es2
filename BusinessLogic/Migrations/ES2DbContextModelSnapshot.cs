﻿// <auto-generated />
using System;
using BusinessLogic.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BusinessLogic.Migrations
{
    [DbContext(typeof(ES2DbContext))]
    partial class ES2DbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ActivityUser", b =>
                {
                    b.Property<Guid>("ActivitiesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ParticipantsId")
                        .HasColumnType("uuid");

                    b.HasKey("ActivitiesId", "ParticipantsId");

                    b.HasIndex("ParticipantsId");

                    b.ToTable("ActivityUser", (string)null);
                });

            modelBuilder.Entity("BusinessLogic.Entities.Activity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Activities", (string)null);
                });

            modelBuilder.Entity("BusinessLogic.Entities.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OrganizerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OrganizerId");

                    b.ToTable("Events", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb9"),
                            Category = "Festival",
                            Date = new DateOnly(1, 1, 1),
                            Description = "A maior corrida do Douro.",
                            Location = "Porto",
                            Name = "Love Tiles Douro Granfondo",
                            OrganizerId = new Guid("f8551c2d-3172-4d56-af8e-e022d067e5e3")
                        });
                });

            modelBuilder.Entity("BusinessLogic.Entities.EventTicket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("QtyAvailable")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("TicketTypes", (string)null);
                });

            modelBuilder.Entity("BusinessLogic.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("f8551c2d-3172-4d56-af8e-e022d067e5e3"),
                            Email = "organizer@gmail.com",
                            Name = "Organizer",
                            Password = "organizer",
                            PhoneNumber = 937654321,
                            Username = "organizer"
                        },
                        new
                        {
                            Id = new Guid("c9c70b4e-6d0c-4a6f-85f9-7a6b3c8d2f9e"),
                            Email = "admin@gmail.com",
                            Name = "Admin",
                            Password = "admin",
                            PhoneNumber = 0,
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("ActivityUser", b =>
                {
                    b.HasOne("BusinessLogic.Entities.Activity", null)
                        .WithMany()
                        .HasForeignKey("ActivitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessLogic.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("ParticipantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessLogic.Entities.Activity", b =>
                {
                    b.HasOne("BusinessLogic.Entities.Event", "Event")
                        .WithMany("Activities")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("BusinessLogic.Entities.Event", b =>
                {
                    b.HasOne("BusinessLogic.Entities.User", "Organizer")
                        .WithMany("EventsCreated")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("BusinessLogic.Entities.EventTicket", b =>
                {
                    b.HasOne("BusinessLogic.Entities.Event", "Event")
                        .WithMany("Tickets")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("BusinessLogic.Entities.Event", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("BusinessLogic.Entities.User", b =>
                {
                    b.Navigation("EventsCreated");
                });
#pragma warning restore 612, 618
        }
    }
}
