using Backend.DB;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

builder.Services.AddControllers();
//swagger
builder.Services.AddOpenApi();

//data from .env
var host = Environment.GetEnvironmentVariable("POSTGRES_HOST");
var db = Environment.GetEnvironmentVariable("POSTGRES_DB");
var user = Environment.GetEnvironmentVariable("POSTGRES_USER");
var pass = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
var port = Environment.GetEnvironmentVariable("POSTGRES_PORT");

//connecting db
var connectionString =
    $"Host={host};Port={port};Database={db};Username={user};Password={pass}";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy
            .WithOrigins("http://localhost:4200")
            //do usuniecia
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});

//rate limit withou login
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 20,
                QueueLimit = 0,
                Window = TimeSpan.FromMinutes(1)
            }));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseRouting();

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthorization();

//usage cors
app.UseCors("AllowAngular");

app.MapControllers();

app.Run();
