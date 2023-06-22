using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Context;

public partial class ES2DbContext : DbContext
{
    public ES2DbContext()
    {
    }

    public ES2DbContext(DbContextOptions<ES2DbContext> options)
        : base(options)
    {
    }

    public DbSet<Activity> Activities { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventTicket> TicketTypes { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql(
            "Host=localhost;Database=es2;Username=es2;Password=es2;SearchPath=public;Port=5480");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Event>()
            .Property(e => e.Category)
            .HasConversion<string>();

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = new Guid("f8551c2d-3172-4d56-af8e-e022d067e5e3"),
                Name = "Organizer",
                Email = "organizer@gmail.com",
                Password = "organizer",
                Role = "Regular",
                Username = "organizer",
                PhoneNumber = 937654321,
                Activities = new List<Activity>(),
                EventsCreated = new List<Event>()
            },
            new User
            {
                Id = new Guid("c9c70b4e-6d0c-4a6f-85f9-7a6b3c8d2f9e"),
                Name = "Admin",
                Email = "admin@gmail.com",
                Password = "admin",
                Role = "Admin",
                Username = "admin",
                PhoneNumber = 000000000,
                Activities = new List<Activity>(),
                EventsCreated = new List<Event>()
            }
        );

        modelBuilder.Entity<Event>().HasData(
            new Event
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb9"),
                Name = "Love Tiles Douro Granfondo",
                Date = DateOnly.FromDateTime(DateTime.Now),
                Location = "Porto",
                Description = "A maior corrida do Douro.",
                Category = EventCategory.Festival,
                OrganizerId = new Guid("f8551c2d-3172-4d56-af8e-e022d067e5e3")
            }
        );

        modelBuilder.Entity<Activity>().HasData(
            new Activity
            {
                Id = new Guid("8890c29b-f170-4ddd-b42d-8908acd076f4"),
                Name = "Granfondo",
                Date = DateTime.Now,
                Description = "Corria de 154Km",
                EventId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb9")
            },
            new Activity
            {
                Id = new Guid("1400030d-31e3-4969-889a-517206850d42"),
                Name = "Minifondo",
                Date = DateTime.Now,
                Description = "Corrida de 57Km",
                EventId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb9")
            },
            new Activity
            {
                Id = new Guid("3b86ca3e-26f5-437a-a4b1-0745eb127b59"),
                Name = "Mediofondo",
                Date = DateTime.Now,
                Description = "Corrida de 102Km",
                EventId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb9")
            }
        );

        modelBuilder.Entity<EventTicket>().HasData(
            new EventTicket
            {
                Id = new Guid("a6c7fa20-b0d1-49a4-9352-eb363db7ca57"),
                Name = "Bilhete Diário",
                QtyAvailable = 100,
                Price = 30,
                EventId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb9")
            }
        );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}