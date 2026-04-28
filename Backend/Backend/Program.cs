using Backend.DB;
using Backend.Patterns;
using DotNetEnv;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// opcjonalnie .env (DEV)
DotNetEnv.Env.Load();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var env = builder.Environment;

// 🧠 DB CONFIGURATION (UNIT / INTEGRATION / PROD)
if (env.IsEnvironment("UnitTest"))
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase("UnitTestDb"));
}
else if (env.IsEnvironment("IntegrationTest"))
{
    // connection string będzie wstrzykiwany z WebApplicationFactory / Testcontainers
    var connectionString =
        builder.Configuration.GetConnectionString("Test");

    if (string.IsNullOrWhiteSpace(connectionString))
        throw new Exception("Missing 'Test' connection string for Integration tests");

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else
{
    // PROD normalny PostgreSQL
    var connectionString =
        builder.Configuration.GetConnectionString("Default");

    if (string.IsNullOrWhiteSpace(connectionString))
        throw new Exception("Missing connection string 'Default'");

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(connectionString, o =>
            o.EnableRetryOnFailure()));
}

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Prod",
        policy => policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// RATE LIMITING
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("RateLimitGet", opt =>
    {
        opt.PermitLimit = 30;
        opt.Window = TimeSpan.FromSeconds(2);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 10;
    });

    options.AddFixedWindowLimiter("RateLimitPost", opt =>
    {
        opt.PermitLimit = 3;
        opt.Window = TimeSpan.FromSeconds(10);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2;
    });
});

builder.Services.AddScoped<RetryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseRateLimiter();
app.UseAuthorization();
app.UseCors("Prod");
app.MapControllers();

// MIGRATIONS (tylko DEV/PROD, NIE TESTY)
if (!app.Environment.IsEnvironment("UnitTest") &&
    !app.Environment.IsEnvironment("IntegrationTest"))
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    try
    {
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Migration failed: {ex.Message}");
        throw;
    }
}

app.Run();

// potrzebne dla WebApplicationFactory
public partial class Program { }