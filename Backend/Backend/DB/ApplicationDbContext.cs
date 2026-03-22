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
            .HasForeignKey(e => e._SportId);

        modelBuilder.Entity<Event>()
            .HasOne(e => e.Venue)
            .WithMany()
            .HasForeignKey(e => e._VenueId);

        base.OnModelCreating(modelBuilder);
    }
}
