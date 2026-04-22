using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Backend.Models;

namespace Backend.DB;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Event> Events { get; set; }
    public DbSet<Sport> Sports{ get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Venue> Venues{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>()
            .HasCheckConstraint("CK_Event_Teams", "[_HomeTeamId] <> [_AwayTeamId]");

        modelBuilder.Entity<Event>()
            .HasOne(e => e.HomeTeam)
            .WithMany()
            .HasForeignKey(e => e._HomeTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Event>()
            .HasOne(e => e.AwayTeam)
            .WithMany()
            .HasForeignKey(e => e._AwayTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Event>()
            .HasOne(e => e.Sport)
            .WithMany()
            .HasForeignKey(e => e._SportId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Event>()
            .HasOne(e => e.Venue)
            .WithMany()
            .HasForeignKey(e => e._VenueId)
            .OnDelete(DeleteBehavior.Restrict);

        //hardcoded data
        modelBuilder.Entity<Sport>().HasData(
            new Sport { Id = 1, SportName = SportName.Football },
            new Sport { Id = 2, SportName = SportName.Ice_Hockey }
        );

        modelBuilder.Entity<Team>().HasData(
            new Team { Id = 1, NameOfTeam = "salzburg", TeamInformation="aaa" },
            new Team { Id = 2, NameOfTeam = "sturm" , TeamInformation = "aaa" },
            new Team { Id = 3, NameOfTeam = "kac", TeamInformation = "aaa" },
            new Team { Id = 4, NameOfTeam = "capitals", TeamInformation = "aaa" }
        );

        modelBuilder.Entity<Venue>().HasData(
           new Venue { Id = 1, Name = "stadium a", City = "salzburg" },
           new Venue { Id = 2, Name = "arena b", City = "vienna" }
       );

        modelBuilder.Entity<Event>().HasData(
            new Event
            {
                Id = 1,
                DateTime = new DateTime(2019, 7, 18, 18, 30, 0, DateTimeKind.Utc),
                Description = "salzburg vs sturm",
                _SportId = 1,
                _HomeTeamId = 1,
                _AwayTeamId = 2,
                _VenueId = 1
            },
            new Event
            {
                Id = 2,
                DateTime = new DateTime(2019, 10, 23, 9, 45, 0, DateTimeKind.Utc),
                Description = "kac vs capitals",
                _SportId = 2,
                _HomeTeamId = 3,
                _AwayTeamId = 4,
                _VenueId = 2
            }
        );

        base.OnModelCreating(modelBuilder);
    }
}
