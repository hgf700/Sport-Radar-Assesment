using Backend.DB;
using Backend.Models.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject1;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("UnitTest");

        builder.ConfigureServices(services =>
        {
            // 🔥 1. znajdź i usuń WSZYSTKIE DbContext
            var descriptors = services
                .Where(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>))
                .ToList();

            foreach (var descriptor in descriptors)
                services.Remove(descriptor);

            // 🔥 2. dodaj InMemory DB
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase($"TestDb");
            });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            Seed(db);
        });
    }

    private void Seed(ApplicationDbContext db)
    {
        db.Database.EnsureCreated();

        if (!db.Sports.Any())
        {
            db.Sports.Add(new Sport
            {
                SportName = SportName.Ice_Hockey
            });
        }

        if (!db.Teams.Any())
        {
            db.Teams.AddRange(
                new Team { NameOfTeam = "A" },
                new Team { NameOfTeam = "B" }
            );
        }

        if (!db.Venues.Any())
        {
            db.Venues.Add(new Venue
            {
                Name = "Stadium",
                City = "City"
            });
        }

        db.SaveChanges();

    }
}