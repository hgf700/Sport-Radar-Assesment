//using Microsoft.EntityFrameworkCore;
//using System.Text.Json;
//using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

//namespace Backend.DB;

//public class ApplicationDbContext
//{
//    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//        : base(options)
//    {
//    }
//    public DbSet<Movie> Movies { get; set; }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        base.OnModelCreating(modelBuilder);

//        modelBuilder.Entity<MoviePeopleRole>()
//            .HasKey(mg => new { mg.MovieId, mg.PeopleRolesId });

//        modelBuilder.Entity<MoviePeopleRole>()
//            .HasOne(mg => mg.Movie)
//            .WithMany(g => g.MoviePeopleRole)
//            .HasForeignKey(mg => mg.MovieId);

//        modelBuilder.Entity<MoviePeopleRole>()
//            .HasOne(mg => mg.PeopleRole)
//            .WithMany(g => g.MoviePeopleRole)
//            .HasForeignKey(mg => mg.PeopleRolesId);

//    }
//}
